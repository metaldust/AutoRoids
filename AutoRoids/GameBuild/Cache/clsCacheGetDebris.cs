using System;
using System.Collections.Generic;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using AutoRoids.GameBuild.Geometry;

namespace AutoRoids.GameBuild.Cache
{
    internal class clsCacheGetDebris
    {
        internal static List<Polyline> lstPolyline;
        internal static List<Boolean> lstVisible;
        internal static int intPolyline;

        internal Polyline GetDebris(Transaction acTrans, Database acDb, List<Point2d> lstPoint,
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
                ModifyDebris(acTrans, acPline, lstPoint, intColor, dblWidth);
                lstVisible[intPolyline] = true;
            }
            else
            {
                clsAddGeometry clsPolylineAdd = new clsAddGeometry();
                acPline = clsPolylineAdd.AddPolyLine(acTrans, acDb, lstPoint, intColor, dblWidth);

                lstPolyline.Add(acPline);
                lstVisible.Add(true);
            }

            // acPline.Visible = true;


            intPolyline++;

            return acPline;
        }

        internal void ModifyDebris(Transaction acTrans, Polyline acPoly, List<Point2d> lstPoint,
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

        internal void HideDebris(Transaction acTrans)
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
    }
}