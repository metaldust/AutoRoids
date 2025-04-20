using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using AutoRoids.Common.Formulas;
using AutoRoids.CustomClass.Game;

namespace AutoRoids.GameLoop.Collision
{
    internal class clsTrimCommon
    {
        internal void GetRockIntersection(List<GameLine> lstBorder,
                                          List<GameLine> lstRock,
                                          ref List<Point2d> lstIntersection,
                                          ref List<double> lstDistance,
                                          ref List<int> lstRockRow)
        {
            for (int i = 0; i < lstRock.Count; i++)
            {
                for (int b = 0; b < lstBorder.Count; b++)
                {
                    GameLine LineRock = lstRock[i];
                    GameLine LineBorder = lstBorder[b];

                    {
                        clsGetIntersection clsGetIntersection = new clsGetIntersection();
                        Boolean bolIntersect = clsGetIntersection.LineSegmentsIntersect(
                             new Vector(LineRock.ptStart.X, LineRock.ptStart.Y),
                             new Vector(LineRock.ptEnd.X, LineRock.ptEnd.Y),
                             new Vector(LineBorder.ptStart.X, LineBorder.ptStart.Y),
                             new Vector(LineBorder.ptEnd.X, LineBorder.ptEnd.Y),
                             out Vector intersection);

                        if (bolIntersect && (!(intersection.X.Equals(double.NaN) ||
                                               intersection.Y.Equals(double.NaN))))

                        {
                            Point2d pt = new Point2d(intersection.X, intersection.Y);
                            lstIntersection.Add(pt);
                            lstDistance.Add(LineRock.ptStart.GetDistanceTo(pt));
                            lstRockRow.Add(i);
                        }
                    }
                }
            }
        }

        internal void OrderByRow(List<Point2d> _lstIntersection,
                               List<double> _lstDistance,
                               List<int> _lstRow,
                               out List<int> lstRow,
                               out List<List<Point2d>> lstLstIntersection,
                               out List<List<double>> lstLstDistance)
        {
            lstRow = _lstRow.Distinct().ToList();
            lstRow.Sort();

            lstLstIntersection = new List<List<Point2d>>();
            lstLstDistance = new List<List<double>>();

            for (int i = 0; i < lstRow.Count; i++)
            {
                List<Point2d> lstIntersection = new List<Point2d>();
                List<double> lstDistance = new List<double>();

                for (int r = 0; r < _lstRow.Count; r++)
                {
                    if (lstRow[i] == _lstRow[r])
                    {
                        lstIntersection.Add(_lstIntersection[r]);
                        lstDistance.Add(_lstDistance[r]);
                    }
                }

                lstLstIntersection.Add(new List<Point2d>(lstIntersection));
                lstLstDistance.Add(new List<double>(lstDistance));
            }
        }

        internal void OrderByDistance(ref List<List<Point2d>> lstLstIntersection, ref List<List<double>> lstLstDistance)
        {
            for (int i = 0; i < lstLstIntersection.Count; i++)
            {
                List<Point2d> lstIntersection = lstLstIntersection[i];
                List<Double> lstDistance = lstLstDistance[i];

                List<Tuple<double, Point2d>> lstTuple = new List<Tuple<double, Point2d>>();

                for (int k = 0; k < lstIntersection.Count; k++)
                    lstTuple.Add(new Tuple<double, Point2d>(lstDistance[k], lstIntersection[k]));

                lstTuple = lstTuple.Distinct().ToList();

                List<double> lstValue = new List<double>();
                for (int k = lstTuple.Count - 1; k >= 0; k--)
                {
                    if (lstValue.Contains(lstTuple[k].Item1))
                        lstTuple.RemoveAt(k);
                    else
                        lstValue.Add((lstTuple[k].Item1));
                }

                if (lstTuple.Count > 1)
                    lstTuple.Sort();

                for (int k = 0; k < lstTuple.Count; k++)
                {
                    lstDistance[k] = lstTuple[k].Item1;
                    lstIntersection[k] = lstTuple[k].Item2;
                }

                lstLstIntersection[i] = new List<Point2d>(lstIntersection);
                lstLstDistance[i] = new List<double>(lstDistance);
            }
        }

        internal List<GameLine> BuildLineGroup(Transaction acTrans, Database acDb,
                                             List<GameLine> lstRockLine, List<int> lstRow, List<List<Point2d>> lstLstIntersection)
        {
            clsTrimCommon clsTrimCommon = new clsTrimCommon();
            List<GameLine> rtnValue = new List<GameLine>();

            lstLstIntersection = AddStartAndEndPoints(lstRow, lstRockLine, lstLstIntersection);

            for (int i = 0; i < lstLstIntersection.Count; i++)
            {
                List<GameLine> lstLine = PointsToMyLine(lstLstIntersection[i]);

                for (int k = 0; k < lstLine.Count; k++)
                {
                    List<Point2d> lstNewPoint = LineToMyPoint(lstLine[k]);

                    rtnValue.Add(new GameLine(lstNewPoint[0], lstNewPoint[1]));
                }
            }

            return rtnValue;
        }

        internal List<List<Point2d>> AddStartAndEndPoints(List<int> lstRow, List<GameLine> lstLine, List<List<Point2d>> lstLstPoint)
        {
            List<List<Point2d>> rtnValue = new List<List<Point2d>>();

            for (int i = 0; i < lstLine.Count; i++)
            {
                List<Point2d> lstPoint = new List<Point2d> { lstLine[i].ptStart };

                for (int k = 0; k < lstRow.Count; k++)
                {
                    if (lstRow[k] == i)
                    {
                        for (int j = 0; j < lstLstPoint[k].Count; j++)
                            lstPoint.Add(lstLstPoint[k][j]);
                    }
                }

                lstPoint.Add(lstLine[i].ptEnd);

                rtnValue.Add(lstPoint);
            }

            return rtnValue;
        }

        internal List<Point2d> LineToMyPoint(GameLine MyLine)
        {
            return new List<Point2d>() { MyLine.ptStart, MyLine.ptEnd };
        }

        internal List<GameLine> PointsToMyLine(List<Point2d> lstPoint)
        {
            List<GameLine> lstLine = new List<GameLine>();
            for (int i = 1; i < lstPoint.Count; i++)
            {
                Point2d ptStart = lstPoint[i - 1];
                Point2d ptEnd = lstPoint[i];
                lstLine.Add(new GameLine(ptStart, ptEnd));
            }
            return lstLine;
        }
    }
}