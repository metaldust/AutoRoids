using System;
using System.Collections.Generic;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using AutoRoids.Common.Static;
using AutoRoids.CustomClass.Engine;
using AutoRoids.GameBuild.Geometry;

namespace AutoRoids.GameBuild.Cache
{
    internal class clsCacheGetBoundingBox
    {
        internal static List<Polyline> lstPolyline;
        internal static List<Boolean> lstVisible;
        internal static int intPolyline;

        internal Polyline GetBoundingBox(Transaction acTrans, Database acDb, List<Point2d> lstPoint,
                                       int intColor, double dblWidth)
        {
            if (lstPolyline == null)
                lstPolyline = new List<Polyline>();

            if (lstVisible == null)
                lstVisible = new List<Boolean>();

            Polyline acPline = null;

            if (intPolyline < lstPolyline.Count)
            {
                acPline = lstPolyline[intPolyline];
                ModifyBoundingBox(acTrans, acPline, lstPoint, intColor, dblWidth);
                lstVisible[intPolyline] = true;
            }
            else
            {
                clsAddGeometry clsPolylineAdd = new clsAddGeometry();
                acPline = clsPolylineAdd.AddPolyLine(acTrans, acDb, lstPoint, intColor, dblWidth);

                lstPolyline.Add(acPline);
                lstVisible.Add(true);
            }

            intPolyline++;

            return acPline;
        }

        internal void ModifyBoundingBox(Transaction acTrans, Polyline acPoly, List<Point2d> lstPoint,
                                     int intColor, double dblWidth)
        {
            if (acPoly.ObjectId.IsValid && !acPoly.ObjectId.IsErased)
            {
                acPoly = acTrans.GetObject(acPoly.ObjectId, OpenMode.ForWrite) as Polyline;

                acPoly.Color = Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, (Int16)intColor);

                if (acPoly.NumberOfVertices == lstPoint.Count)
                {
                    for (int i = 0; i < lstPoint.Count; i++)
                        acPoly.SetPointAt(i, lstPoint[i]);
                }

                acPoly.ConstantWidth = dblWidth;

                acPoly.Visible = true;
            }
        }

        internal void HideBoundingBox(Transaction acTrans)
        {
            if (lstPolyline != null)
            {
                if (lstVisible.Contains(true))
                {
                    for (int i = 0; i < lstPolyline.Count; i++)
                    {
                        Polyline acPoly = lstPolyline[i];

                        if (lstVisible[i] == true)
                        {
                            if (acPoly.ObjectId.IsValid && !acPoly.ObjectId.IsErased)
                            {
                                acPoly = acTrans.GetObject(acPoly.ObjectId, OpenMode.ForWrite) as Polyline;
                                acPoly.Visible = false;
                                lstVisible[i] = false;
                            }
                        }
                    }
                }
            }
        }

        internal void HideRemaining(Transaction acTrans)
        {
            if (lstPolyline != null)
            {
                int intTotal = GetCount();
                int intPline = lstPolyline.Count;

                {
                    for (int i = intTotal; i < intPline; i++)
                    {
                        if (lstVisible[i])
                        {
                            Polyline acPline = lstPolyline[i];
                            acPline = acTrans.GetObject(acPline.ObjectId, OpenMode.ForWrite) as Polyline;
                            acPline.Visible = false;
                            lstVisible[i] = false;
                        }
                    }
                }
            }
        }

        private int GetCount()
        {
            int intLineCount = 0;
            if (StaticRock.lstBoundingBox.Count > 0)
            {
                for (int i = 0; i < StaticRock.lstBoundingBox.Count; i++)
                {
                    EngineBoundingBox EngineBoundingBox = StaticRock.lstBoundingBox[i];
                    intLineCount += EngineBoundingBox.lstLine.Count;
                }
            }

            return intLineCount;
        }
    }
}