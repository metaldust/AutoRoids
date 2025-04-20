namespace AutoRoids.GameLoop.Collision
{
    internal class clsTrim
    {
        //internal void ProcessRockTrim(Transaction acTrans, Database acDb, int intPos, List<GameLine> lstRockLine,
        //                              List<GameLine> lstBorderLine, List<List<GameLine>> lstLstShieldLine)
        //{
        //    clsTrimCommon clsTrimCommon = new clsTrimCommon();
        //    List<Point2d> lstIntersection = new List<Point2d>();
        //    List<double> lstDistance = new List<double>();
        //    List<int> lstRockRow = new List<int>();

        //    List<List<GameLine>> lstLstBorderLine = new List<List<GameLine>> { lstBorderLine };

        //    for (int i = 0; i < lstLstShieldLine.Count; i++)
        //        lstLstBorderLine.Add(lstLstShieldLine[i]);

        //    for (int i = 0; i < lstLstBorderLine.Count; i++)
        //    {
        //        clsTrimCommon.GetRockIntersection(lstLstBorderLine[i], lstRockLine,
        //                            ref lstIntersection,
        //                            ref lstDistance,
        //                            ref lstRockRow);
        //    }
        //    if (lstIntersection.Count > 0)
        //    {
        //        // AddPoints(acTrans, acDb, lstIntersection);

        //        clsTrimCommon.OrderByRow(lstIntersection,
        //                   lstDistance,
        //                   lstRockRow,
        //                   out List<int> lstRow,
        //                   out List<List<Point2d>> lstLstIntersection,
        //                   out List<List<double>> lstLstDistance);

        //        clsTrimCommon.OrderByDistance(ref lstLstIntersection, ref lstLstDistance);

        //        lstRockLine = clsTrimCommon.BuildLineGroup(acTrans, acDb, lstRockLine, lstRow, lstLstIntersection);

        //        ProcessAddLines(acTrans, acDb, intPos,
        //                        lstRockLine,
        //                        lstBorderLine, lstLstShieldLine);
        //    }
        //    else
        //    {
        //        ProcessAddLines(acTrans, acDb, intPos,
        //                        lstRockLine,
        //                        lstBorderLine, lstLstShieldLine);
        //    }
        //}

        //internal void ProcessAddLines(Transaction acTrans, Database acDb, int intPos,
        //                              List<GameLine> lstNewLine,
        //                              List<GameLine> lstBorderLine, List<List<GameLine>> lstLstShieldLine)
        //{
        //    List<Boolean> lstVisible = ProcessVisible(lstNewLine,
        //                                              lstLstShieldLine,
        //                                              lstBorderLine);

        //    AddLines(acTrans, acDb, lstNewLine, lstVisible, intPos);
        //}

        //internal List<Boolean> ProcessVisible(List<GameLine> lstRockLine,
        //                                      List<List<GameLine>> lstLstShieldLine,
        //                                      List<GameLine> lstBorderLine)
        //{
        //    List<Boolean> rtnValue = new List<bool>();

        //    for (int i = 0; i < lstRockLine.Count; i++)
        //        rtnValue.Add(true);

        //    for (int i = 0; i < lstLstShieldLine.Count; i++)
        //    {
        //        List<Point2d> lstShieldPoint = lstLstShieldLine[i].LineToPoint();
        //        List<Point2d> lstBorderPoint = lstBorderLine.LineToPoint();

        //        List<Boolean> lstVisible = IsLineVisible(lstRockLine, lstBorderPoint, lstShieldPoint);

        //        for (int j = 0; j < lstVisible.Count; j++)
        //        {
        //            if (!lstVisible[j])
        //                rtnValue[j] = false;
        //        }
        //    }

        //    return rtnValue;
        //}

        //internal List<Polyline> AddLines(Transaction acTrans, Database acDb, List<GameLine> lstLine, List<Boolean> lstVisible, int intPos)
        //{
        //    List<Polyline> rtnValue = new List<Polyline>();
        //    for (int i = 0; i < lstLine.Count; i++)
        //    {
        //        if (lstVisible[i])
        //        {
        //            List<Point2d> lstPoints = new List<Point2d> { lstLine[i].ptStart, lstLine[i].ptEnd };

        //            // clsAddGeometry clsAddGeometry = new clsAddGeometry();
        //            clsAppend clsAppend = new clsAppend();
        //            clsCacheGetPolyline clsCacheGetPolyline = new clsCacheGetPolyline();
        //            Polyline acPline = clsCacheGetPolyline.GetPolyline(acTrans, acDb, lstPoints, intPos, 1);
        //            clsAppend.AppendToEntity(acPline);

        //            rtnValue.Add(acPline);
        //        }
        //    }
        //    return rtnValue;
        //}
        //internal List<MyLine> ProcessTrim(Transaction acTrans, Database acDb,
        //                          List<Point2d> lstRockPoints, List<List<Point2d>> lstRockGroup,
        //                          List<Point2d> lstBorderPoint, List<List<Point2d>> lstBorderGroup,
        //                          List<Point2d> lstShieldPoint, List<List<Point2d>> lstShieldGroup)

        //{
        //    List<MyLine> rtnValue = new List<MyLine>();

        //    if (PointToLine(lstRockGroup, out List<MyLine> lstRockLine) &&
        //        PointToLine(lstBorderGroup, out List<MyLine> lstBorderLine) &&
        //        PointToLine(lstShieldGroup, out List<MyLine> lstShieldLine))
        //    {
        //        List<Point2d> lstIntersection = new List<Point2d>();
        //    List<Point2d> lstDistance = new List<double>();
        //        List<int> lstRockRow = new List<int>();

        //        List<List<MyLine>> lstLstLine = new List<List<MyLine>> { lstBorderLine, lstShieldLine };

        //        for (int i = 0; i < lstLstLine.Count; i++)
        //        {
        //            GetRockIntersection(acTrans, acDb, lstLstLine[i], lstRockLine,
        //                                ref lstIntersection,
        //                                ref lstDistance,
        //                                ref lstRockRow);
        //        }

        //        if (lstIntersection.Count > 0)
        //        {
        //            OrderByRow(lstIntersection,
        //                       lstDistance,
        //                       lstRockRow,
        //                       out List<int> lstRow,
        //                       out List<List<Point2d>> lstLstIntersection,
        //                       out List<List<double>> lstLstDistance);

        //            OrderByDistance(ref lstLstIntersection, ref lstLstDistance);

        //            List<MyLine> lstNewLine = BuildLineGroup(acTrans, acDb, lstRockLine, lstRow, lstLstIntersection);

        //            List<Boolean> lstVisible = IsLineVisible(lstNewLine, lstBorderPoint, lstShieldPoint);

        //            for (int i = 0; i < lstVisible.Count; i++)
        //            {
        //                if (lstVisible[i])
        //                    rtnValue.Add(lstNewLine[i]);
        //            }
        //        }
        //        else
        //        {
        //            if (IsLineVisible(lstRockPoints, lstBorderPoint, lstShieldPoint))
        //                rtnValue = lstRockLine;
        //        }
        //    }

        //    return rtnValue;
        //}

        //internal void AddPoints(Transaction acTrans, Database acDb, List<Point2d> lstIntersection)
        //{
        //    for (int i = 0; i < lstIntersection.Count; i++)
        //    {
        //        clsAddGeometry clsAddGeometry = new clsAddGeometry();
        //        clsAppend clsAppend = new clsAppend();
        //        clsAppend.AppendToEntity(clsAddGeometry.AddPoint(acTrans, acDb, lstIntersection[i], 3));
        //    }
        //}
        //internal Boolean IsLineVisible(List<Point2d> lstRockPoints, List<Point2d> lstBorderPoint, List<Point2d> lstShieldPoint)
        //{
        //    List<Point2d> lstIntersection = new List<Point2d>();

        //    if (IsInside(lstShieldPoint, lstIntersection, lstRockPoints) ||
        //        !IsInside(lstBorderPoint, lstIntersection, lstRockPoints))
        //    {
        //        return false;
        //    }
        //    return true;
        //}

        //internal List<Boolean> IsLineVisible(List<GameLine> lstNewLine, List<Point2d> lstBorderPoint, List<Point2d> lstShieldPoint)
        //{
        //    List<Boolean> rtnValue = new List<bool>();

        //    List<Point2d> lstIntersection = new List<Point2d>();

        //    for (int i = 0; i < lstNewLine.Count; i++)
        //    {
        //        Point2d pt1 = lstNewLine[i].ptStart;
        //        Point2d pt2 = lstNewLine[i].ptEnd;

        //        lstIntersection.Add(pt1);
        //        lstIntersection.Add(pt2);
        //    }

        //    lstIntersection = lstIntersection.Distinct().ToList();

        //    for (int i = 0; i < lstNewLine.Count; i++)
        //    {
        //        Point2d pt1 = lstNewLine[i].ptStart;
        //        Point2d pt2 = lstNewLine[i].ptEnd;
        //        Point2d ptMid = pt1.GetMidPoint(pt2);
        //        List<Point2d> lstPoints = new List<Point2d> { pt1, pt2, ptMid };

        //        if (IsInside(lstShieldPoint, lstIntersection, lstPoints) ||
        //            !IsInside(lstBorderPoint, lstIntersection, lstPoints))
        //            rtnValue.Add(false);
        //        else
        //            rtnValue.Add(true);
        //    }

        //    return rtnValue;
        //}

        //internal void AddLine(Transaction acTrans, Database acDb, int intPos, GameLine myLine)
        //{
        //    clsTrimCommon clsTrimCommon = new clsTrimCommon();
        //    //clsPolylineAdd clsPolylineAdd = new clsPolylineAdd();

        //    List<Point2d> lstNewPoint = clsTrimCommon.LineToMyPoint(myLine);
        //    clsAppend clsAppend = new clsAppend();

        //    clsCacheGetPolyline clsCacheGetPolyline = new clsCacheGetPolyline();
        //    clsAppend.AppendToEntity(clsCacheGetPolyline.GetPolyline(acTrans, acDb, lstNewPoint, intPos, 1));

        //}

        //internal void AddLine(Transaction acTrans, Database acDb, int intPos, List<GameLine> lstLine)
        //{
        //    for (int i = 0; i < lstLine.Count; i++)
        //        AddLine(acTrans, acDb, intPos, lstLine[i]);
        //}

        //internal Boolean PointToLine(List<List<Point2d>> lstLstPoints, out List<GameLine> lstLine)
        //{
        //    lstLine = new List<GameLine>();
        //    for (int i = 0; i < lstLstPoints.Count; i++)
        //    {
        //        if (lstLstPoints[i].Count == 2)
        //        {
        //            lstLine.Add(new GameLine(lstLstPoints[i][0], lstLstPoints[i][1]));
        //        }
        //        else
        //            return false;
        //    }
        //    return true;
        //}

        //internal List<GameLine> PointToLine(List<List<Point2d>> lstLstPoints)
        //{
        //    List<GameLine> rtnValue = new List<GameLine>();
        //    for (int i = 0; i < lstLstPoints.Count; i++)
        //    {
        //        if (lstLstPoints[i].Count == 2)
        //        {
        //            rtnValue.Add(new GameLine(lstLstPoints[i][0], lstLstPoints[i][1]));
        //        }

        //    }
        //    return rtnValue;
        //}

        //internal void GroupIntoLines(GameIntersection Shield, ref List<List<Point2d>> lstRockGroup)
        //{
        //    for (int i = 0; i < Shield.lstIntersection.Count; i++)
        //        lstRockGroup[Shield.lstRockRow[i]] = InjectPoint(lstRockGroup[Shield.lstRockRow[i]], Shield.lstIntersection[i]);

        //    GroupPointsIntoLines(ref lstRockGroup, Shield.lstRockRow);
        //}

        //internal void DrawCornerToFirstPoint(List<int> lstInside, List<Point2d> _lstBorderPoint,
        //                                     ref List<List<Point2d>> lstLstBorderPoint, List<int> lstBorderSection,
        //                                     ref List<List<Point2d>> lstLstNewRock, ref List<Boolean> lstIsHidden)
        //{
        //    // Draw Corner to First Point
        //    for (int i = 0; i < lstInside.Count; i++)
        //    {
        //        Point2d ptBase = _lstBorderPoint[lstInside[i]];

        //        for (int k = 0; k < lstLstBorderPoint.Count; k++)
        //        {
        //            // Put in Order From the corner
        //            lstLstBorderPoint[k] = PutInOrder(lstLstBorderPoint[k], lstBorderSection[i], ptBase);

        //            for (int j = 0; j < lstLstBorderPoint[k].Count; j++)
        //            {
        //                lstLstNewRock.Add(new List<Point2d> { ptBase, lstLstBorderPoint[k][j] });
        //                lstIsHidden.Add(true);
        //                break;
        //            }
        //        }
        //    }
        //}

        //internal void FillinRemainingClosedLine(ref List<List<Point2d>> lstLstBorderPoint, List<int> lstBorderSection,
        //                                        List<Point2d> _lstBorderPoint, List<Point2d> lstRockPoints,
        //                                        ref List<Boolean> lstIsHidden, ref List<List<Point2d>> lstLstNewRock)
        //{
        //    // Fill in Remaining Points

        //    for (int k = 0; k < lstLstBorderPoint.Count; k++)
        //    {
        //        for (int j = 1; j < lstLstBorderPoint[k].Count; j++)
        //        {
        //            // Put in Order from start of intersecting Line
        //            lstLstBorderPoint[k] = PutInOrder(lstLstBorderPoint[k], lstBorderSection[k], _lstBorderPoint[lstBorderSection[k]]);

        //            Point2d pt0 = lstLstBorderPoint[k][j - 1];
        //            Point2d pt1 = lstLstBorderPoint[k][j];
        //            Point2d ptMid = pt0.GetMidPoint(pt1);

        //            if (IsInside(lstRockPoints, new List<Point2d>(), new List<Point2d> { ptMid }))
        //            {
        //                List<Point2d> lstNewPoint = new List<Point2d> { pt0, pt1 };
        //                lstIsHidden.Add(true);
        //                lstLstNewRock.Add(lstNewPoint);
        //            }
        //        }
        //    }
        //}

        //internal void ProcessAddingLines(Transaction acTrans, Database acDb, List<List<Point2d>> lstLstNewRock, int intColor, List<Boolean> lstIsHidden)
        //{
        //    clsPolylineAdd clsPolylineAdd = new clsPolylineAdd();
        //    List<Entity> lstEntity = new List<Entity>();

        //    for (int i = 0; i < lstLstNewRock.Count; i++)
        //    {
        //        lstEntity.Add(clsPolylineAdd.AddPolyLineEntity(acTrans, acDb,
        //                 new List<Point2d> { lstLstNewRock[i][0], lstLstNewRock[i][1] }, intColor, 3));

        //        clsCache clsCache = new clsCache();
        //        lstEntity.Add(clsCache.GetPolyline(acTrans, acDb, new List<Point2d> { lstLstNewRock[i][0], lstLstNewRock[i][1] }, intColor, 3));

        //    }

        //    List<Polyline> lstPolyline = new List<Polyline>();
        //    for (int i = 0; i < lstEntity.Count; i++)
        //    {
        //        Polyline acPline = acTrans.GetObject(lstEntity[i].ObjectId, OpenMode.ForWrite) as Polyline;

        //        lstPolyline.Add(acPline);
        //        clsAppend clsAppend = new clsAppend();
        //        clsAppend.AppendToEntity(acPline);
        //    }
        //}

        //internal List<List<Point2d>> BuildLoop(Transaction acTrans, Database acDb, List<List<Point2d>> lstLstRock)
        //{
        //    clsPolylineAdd clsPolylineAdd = new clsPolylineAdd();

        //    clsReorder clsReorder = new clsReorder();
        //    (List<List<Point2d>> orderedLoops, List<List<Point2d>> unmatchedSegments) = clsReorder.OrderLines(lstLstRock);

        //    if (unmatchedSegments.Count > 0)
        //    {
        //        Debug.Print("");
        //    }
        //    List<Boolean> lstIsHidden = new List<bool>();
        //    for (int i = 0; i < orderedLoops.Count; i++)
        //        lstIsHidden.Add(false);

        //    // Output the ordered loops
        //    for (int i = 0; i < orderedLoops.Count; i++)
        //    {
        //        clsAppend clsAppend = new clsAppend();
        //        clsAppend.AppendToEntity(clsPolylineAdd.AddPolyLineEntity(acTrans, acDb,
        //                     orderedLoops[i], i + 1, 3));
        //    }

        //    return orderedLoops;
        //}

        //internal List<Point2d> PutInOrder(List<Point2d> lstPoint, int intBoderLeg, Point2d ptBase)
        //{
        //    lstPoint = lstPoint.Distinct().ToList();
        //    List<Point2d> rtnValue = new List<Point2d>();

        //    List<Tuple<double, Point2d>> lstTuple = new List<Tuple<double, Point2d>>();

        //    for (int i = 0; i < lstPoint.Count; i++)
        //    {
        //        double dblDistance = ptBase.GetDistanceTo(lstPoint[i]);

        //        lstTuple.Add(new Tuple<double, Point2d>(dblDistance, lstPoint[i]));
        //    }

        //    if (lstTuple.Count > 0)
        //        lstTuple.Sort();

        //    for (int i = 0; i < lstTuple.Count; i++)
        //        rtnValue.Add(lstTuple[i].Item2);

        //    return rtnValue;
        //}

        //internal void GroupPointsIntoLines(ref List<List<Point2d>> lstRockGroup, List<int> lstRow)
        //{
        //    lstRow.Sort();

        //    for (int i = lstRow.Count - 1; i >= 0; i--)
        //    {
        //        if (lstRockGroup[lstRow[i]].Count > 2)
        //        {
        //            clsBuildRock clsBuildRock = new clsBuildRock();
        //            List<List<Point2d>> lstLstPoint = clsBuildRock.GroupIntoLines(lstRockGroup[lstRow[i]]);

        //            lstRockGroup.RemoveAt(lstRow[i]);

        //            for (int k = 0; k < lstLstPoint.Count; k++)
        //                lstRockGroup.Insert(lstRow[i] + k, lstLstPoint[k]);
        //        }
        //    }
        //}

        //internal void ProcessCollisionLine(Transaction acTrans, Database acDb, GameIntersection Border, List<List<Point2d>> lstRockGroup,
        //                                   List<Point2d> lstBorder, int intColor, Boolean bolInvert)
        //{
        //    for (int i = lstRockGroup.Count - 1; i >= 0; i--)
        //    {
        //        Point2d pt1 = lstRockGroup[i][0];
        //        Point2d pt2 = lstRockGroup[i][1];
        //        Point2d ptMid = pt1.GetMidPoint(pt2);

        //        List<Point2d> lstPoints = new List<Point2d> { pt1, pt2, ptMid };

        //        if (IsInside(lstBorder, Border.lstIntersection, lstPoints))
        //        {
        //            Border.lstLstNewRock.Add(lstRockGroup[i]);
        //            Border.lstIsHidden.Add(false);
        //        }
        //        else
        //        {
        //            Border.lstLstNewRock.Add(lstRockGroup[i]);
        //            Border.lstIsHidden.Add(false);
        //        }

        //        //if (!bolInvert)
        //        //{
        //        //    if (IsInside(lstBorder, Border.lstIntersection, lstPoints))
        //        //    {
        //        //        Border.lstLstNewRock.Add(lstRockGroup[i]);
        //        //        Border.lstIsHidden.Add(false);
        //        //    }
        //        //}

        //        //else
        //        //{
        //        //    if (!IsInside(lstBorder, Border.lstIntersection, lstPoints))
        //        //    {
        //        //        Border.lstLstNewRock.Add(lstRockGroup[i]);
        //        //        Border.lstIsHidden.Add(false);
        //        //    }
        //        //}
        //    }
        //}

        //internal Boolean IsInside(List<Point2d> lstBorderPoints, List<Point2d> lstIntersection, List<Point2d> lstPt)
        //{
        //    List<Boolean> rtnValue = new List<bool>();
        //    for (int i = 0; i < lstPt.Count; i++)
        //    {
        //        if (lstIntersection.Contains(lstPt[i]) || IsInsideBoundary(lstBorderPoints, lstPt[i]))
        //            rtnValue.Add(true);
        //        else
        //            rtnValue.Add(false);
        //    }

        //    if (rtnValue.Count > 0)
        //    {
        //        if (rtnValue.Contains(false))
        //            return false;
        //        else
        //            return true;
        //    }
        //    return false;
        //}

        //internal List<Point2d> InjectPoint(List<Point2d> lstPoint, Point2d pt)
        //{
        //    if (lstPoint.Count > 2)
        //    {
        //        Debug.Print("");
        //    }
        //    Boolean IsValid = false;
        //    if (!lstPoint.Contains(pt))
        //    {
        //        List<Double> lstDistance = new List<double> { 0 };
        //        double dblDistance = lstPoint[0].GetDistanceTo(pt);
        //        for (int i = 1; i < lstPoint.Count; i++)
        //            lstDistance.Add(lstPoint[0].GetDistanceTo(lstPoint[i]));

        //        for (int i = 1; i < lstDistance.Count; i++)
        //        {
        //            if (dblDistance > lstDistance[i - 1] &&
        //                dblDistance < lstDistance[i])
        //            {
        //                lstPoint.Insert(i, pt);
        //                IsValid = true;
        //            }
        //        }
        //    }

        //    if (!IsValid)
        //        Debug.Print("");
        //    return lstPoint;
        //}
    }
}

//internal void GetIntersectionRock(Transaction acTrans, Database acDb, List<List<Point2d>> lstBorderGroup,
//                                  ref List<List<Point2d>> lstRockGroup,
//                                  ref MyIntersection Border)

//{
//    int intEdge = 0;
//    for (int b = 0; b < lstBorderGroup.Count; b++)
//    {
//        List<Point2d> lstBorderPointTemp = new List<Point2d>();
//        for (int i = 0; i < lstRockGroup.Count; i++)
//        {
//            MyLine ptRock = new MyLine(lstRockGroup[i][0], lstRockGroup[i][1]);
//            MyLine ptBorder = new MyLine(lstBorderGroup[b][0], lstBorderGroup[b][1]);
//            {
//                clsIntersection clsIntersection = new clsIntersection();
//                Boolean bolIntersect = clsIntersection.LineSegmentsIntersect(
//                     new Vector(ptRock.ptStart.X, ptRock.ptStart.Y),
//                     new Vector(ptRock.ptEnd.X, ptRock.ptEnd.Y),
//                     new Vector(ptBorder.ptStart.X, ptBorder.ptStart.Y),
//                     new Vector(ptBorder.ptEnd.X, ptBorder.ptEnd.Y),
//                     out Vector intersection);

//                if (bolIntersect && (!(intersection.X.Equals(double.NaN) ||
//                                       intersection.Y.Equals(double.NaN))))

//                {
//                    Point2d pt = new Point2d(intersection.X, intersection.Y);
//                    intEdge++;
//                    lstBorderPointTemp.Add(pt);

//                    clsPolylineAdd clsPolylineAdd = new clsPolylineAdd();
//                    AppendEntity((Entity)clsPolylineAdd.AddPoint(acTrans, acDb, pt, 3));

//                    Border.lstIntersection.Add(pt);
//                    Border.lstRockRow.Add(i);
//                }
//            }
//        }

//        if (lstBorderPointTemp.Count > 0)
//        {
//            Border.lstBorderRow.Add(b);
//            Border.lstLstBorderIntersection.Add(lstBorderPointTemp);
//        }
//    }

//    GroupIntoLines(Border, ref lstRockGroup);
//}

// --------------------------

//MyIntersection Shield = new MyIntersection(false);
//GetIntersection(acTrans, acDb, lstShieldGroup, ref lstRockGroup, ref Shield);

//MyIntersection Border = new MyIntersection(true);
//GetIntersection(acTrans, acDb, lstBorderGroup, ref lstRockGroup, ref Border);

//if (Shield.lstIntersection.Count > 0 )
//{
//    ProcessCollisionLine(acTrans, acDb, lstRockGroup,
//                         Shield.lstIntersection, lstShieldPoint, intColor, ref lstLstNewRock, ref lstIsHidden, true);
//}

//ProcessCollisionLine(acTrans, acDb, Border, lstRockGroup,
//                     lstBorderPoint, intColor, false);

//if (IsInside(lstShieldPoint, Shield.lstIntersection, lstRockPoints))
//{
//    Shield.lstLstNewRock.Clear();
//    Shield.lstIsHidden.Clear();
//}

//if (Border.lstIntersection.Count > 0 && Shield.lstIntersection.Count == 0)
//    ProcessAddingLines(acTrans, acDb, Border.lstLstNewRock, intColor, Border.lstIsHidden);

//if (Shield.lstIntersection.Count > 0 && Border.lstIntersection.Count == 0)
//    ProcessAddingLines(acTrans, acDb, Shield.lstLstNewRock, intColor, Shield.lstIsHidden);

//List<int> lstInside = new List<int>();
//for (int i = 0; i < lstShieldPoint.Count - 1; i++)
//{
//    if (IsInside(lstRockPoints, new List<Point2d>(), new List<Point2d> { lstShieldPoint[i] }))
//        lstInside.Add(i);
//}

//ProcessCollisionLine(acTrans, acDb, lstRockGroup,
//                   lstAllIntersection, lstBorderPoint, intColor, ref lstLstNewRock, ref lstIsHidden, bolInvert);

//List<int> lstInside = new List<int>();
//for (int i = 0; i < lstBorderPoint.Count - 1; i++)
//{
//    if (IsInside(lstRockPoints, new List<Point2d>(), new List<Point2d> { lstBorderPoint[i] }))
//        lstInside.Add(i);
//}

//for (int k = 0; k < lstLstBorderIntersection.Count; k++)
//    lstLstBorderIntersection[k] = lstLstBorderIntersection[k].Distinct().ToList();

//DrawCornerToFirstPoint(lstInside, lstBorderPoint,
//                       ref lstLstBorderIntersection, lstBorderRow,
//                       ref lstLstNewRock, ref lstIsHidden);

//FillinRemainingClosedLine(ref lstLstBorderIntersection, lstBorderRow,
//                          lstBorderPoint, lstRockPoints,
//                          ref lstIsHidden, ref lstLstNewRock);

// BuildLoop(acTrans, acDb, lstLstNewRock);
// ==========================================