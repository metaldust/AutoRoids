using System;
using System.Collections.Generic;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using AutoRoids.Common.Static;
using AutoRoids.GameBuild.Geometry;

namespace AutoRoids.GameBuild.Cache
{
    internal class clsCacheGetPoint
    {
        internal static List<DBPoint> _lstPoint;
        internal static int intPoint;

        internal DBPoint GetPoint(Transaction acTrans, Database acDb, Point2d pt, int intColor)
        {
            if (_lstPoint == null)
                _lstPoint = new List<DBPoint>();

            DBPoint acPoint = null;

            if (intPoint < _lstPoint.Count)
            {
                acPoint = _lstPoint[intPoint];
                ModifyPoint(acTrans, acPoint, pt, intColor);
            }
            else
            {
                clsAddGeometry clsPolylineAdd = new clsAddGeometry();
                acPoint = clsPolylineAdd.AddPoint(acTrans, acDb, pt, intColor);

                _lstPoint.Add(acPoint);
            }

            intPoint++;

            return acPoint;
        }

        internal void ModifyPoint(Transaction acTrans, DBPoint acPoint, Point2d pt, int intColor)
        {
            acPoint = acTrans.GetObject(acPoint.ObjectId, OpenMode.ForWrite) as DBPoint;

            acPoint.Color = Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, (Int16)intColor);

            acPoint.Position = pt.ToPoint3d();

            acPoint.Visible = true;
        }

        internal void HidePoint(Transaction acTrans)
        {
            if (_lstPoint != null)
            {
                for (int i = 0; i < _lstPoint.Count; i++)
                {
                    DBPoint acPoint = _lstPoint[i];
                    if (acPoint.ObjectId.IsValid && !acPoint.ObjectId.IsErased)
                    {
                        if (acPoint.Visible == true)
                        {
                            acPoint = acTrans.GetObject(acPoint.ObjectId, OpenMode.ForWrite) as DBPoint;
                            acPoint.Visible = false;
                        }
                    }
                }
            }
        }
    }
}