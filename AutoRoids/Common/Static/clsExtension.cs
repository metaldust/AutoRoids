using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Security;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using AutoRoids.CustomClass.Game;
using Application = Autodesk.AutoCAD.ApplicationServices.Core.Application;

namespace AutoRoids.Common.Static
{
    public static class clsExtension
    {
        internal static double GetDouble(this Random random, double lowerBound, double upperBound)
        {
            var rDouble = random.NextDouble();
            var rRangeDouble = rDouble * (upperBound - lowerBound) + lowerBound;
            return rRangeDouble;
        }

        internal static void DeleteBlockReference(this List<BlockReference> lstBlkRef, Transaction acTrans)
        {
            for (int i = 0; i < lstBlkRef.Count; i++)
            {
                BlockReference acBlkRef = lstBlkRef[i];

                if (acBlkRef.IsValid(acTrans))
                {
                    acBlkRef = acTrans.GetObject(acBlkRef.ObjectId, OpenMode.ForWrite) as BlockReference;

                    if (acBlkRef != null) acBlkRef.Erase();
                }
            }
        }

        internal static Boolean GetPosition(this string strBlockName, out int intPos)
        {
            intPos = 0;
            List<String> lstLine = strBlockName.Split('-').ToList();

            if (lstLine.Count == 2)
            {
                string strNum = lstLine[1];
                if (int.TryParse(strNum, out int x))
                {
                    intPos = x;
                    return true;
                }
            }
            return false;
        }

        internal static List<Point2d> CreateBoundingBoxFromPoint(this Point2d pt, double dblScale)
        {
            double d = dblScale / 6;

            Point2d pt1 = new Point2d(pt.X - d, pt.Y - d);
            Point2d pt2 = new Point2d(pt.X + d, pt.Y - d);
            Point2d pt3 = new Point2d(pt.X + d, pt.Y + d);
            Point2d pt4 = new Point2d(pt.X - d, pt.Y + d);

            return new List<Point2d> { pt1, pt2, pt3, pt4, pt1 };
        }

        public static Point2d GetMidPoint(this Point2d pt1, Point2d pt3)
        {
            return new Point2d((pt1.X + pt3.X) / 2, (pt1.Y + pt3.Y) / 2);
        }

        internal static double GetDistance(this Point2d pt1, Point2d pt2)
        {
            double x1 = pt1.X;
            double y1 = pt1.Y;
            double x2 = pt2.X;
            double y2 = pt2.Y;

            double dblValue = Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));

            if (double.NaN.Equals(dblValue))
                dblValue = 0;

            return dblValue;
        }
        internal static Boolean IsDrawingValid()
        {
            if (Application.DocumentManager.Count > 0 &&
                Application.DocumentManager.MdiActiveDocument != null)
            {
                return true;
            }
            return false;
        }

        [HandleProcessCorruptedStateExceptions, SecurityCritical]
        public static Boolean IsValid(this BlockReference acBlkRef, Transaction acTrans)
        {
            if (acBlkRef == null)
                return false;

            try
            {
                if (acTrans.GetObject(acBlkRef.BlockTableRecord, OpenMode.ForRead) is object acObject)
                {
                    BlockTableRecord acBTR = acObject as BlockTableRecord;
                    if (acBTR != null)
                    {
                        Database blockDatabase = acBTR.Database;

                        Document acDoc = Application.DocumentManager.MdiActiveDocument;

                        if (acDoc.Database == blockDatabase)
                        {
                            if (acBlkRef.ObjectId.IsValid && !acBlkRef.ObjectId.IsErased)
                                return true;
                        }
                    }
                }

            }
            catch (Exception)
            {
                // ignored
            }

            return false;
        }

        public static Boolean IsValid(this ObjectId objId, Transaction acTrans)
        {

            try
            {
                if (objId.IsValid && !objId.IsErased)
                {
                    if (acTrans.GetObject(objId, OpenMode.ForRead) is Object acObject)
                    {
                        BlockTableRecord acBTR = acObject as BlockTableRecord;
                        if (acBTR != null)
                        {
                            Database blockDatabase = acBTR.Database;

                            Document acDoc = Application.DocumentManager.MdiActiveDocument;

                            if (acDoc.Database == blockDatabase)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return false;
        }


        public static bool IsEven(this int value)
        {
            return value % 2 == 0;
        }

        public static double GetAngle(this Point2d pt1, Point2d pt2)
        {
            double xDiff = pt2.X - pt1.X;
            double yDiff = pt2.Y - pt1.Y;
            return Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI;
        }

        public static double NormalizeAngle(this double angle)
        {
            // Use modulo to ensure the angle is in the range [0, 360)
            double normalizedAngle = angle % 360.0;

            // Handle negative angles
            if (normalizedAngle < 0)
                normalizedAngle += 360.0;

            return normalizedAngle;
        }

        public static double ToDouble(this String strValue)
        {
            double.TryParse(strValue, out double dblValue);

            return dblValue;
        }

        internal static List<Point2d> LineToPoint(this List<GameLine> lstLine)
        {
            List<Point2d> rtnValue = new List<Point2d>();

            if (lstLine.Count > 0)
            {
                for (int i = 0; i < lstLine.Count; i++)
                    rtnValue.Add(lstLine[i].ptStart);
                rtnValue.Add(lstLine[0].ptStart);
            }
            return rtnValue;
        }

        // Function to calculate the new point
        internal static Point2d CalculatePoint(this Point2d pt, double angleDegrees, double distance)
        {
            angleDegrees += 180;
            // Convert angle to radians
            double angleRadians = angleDegrees * Math.PI / 180.0;

            double newX = pt.X - distance * Math.Cos(angleRadians);
            double newY = pt.Y - distance * Math.Sin(angleRadians);

            // Create and return the new point
            return new Point2d(newX, newY);
        }

        internal static Point2d GetOffset(this double angleDegrees, double distance)
        {
            angleDegrees += 180;
            // Convert angle to radians
            double angleRadians = angleDegrees * Math.PI / 180.0;

            double newX = distance * Math.Cos(angleRadians);
            double newY = distance * Math.Sin(angleRadians);

            // Create and return the new point
            return new Point2d(newX, newY);
        }

        public static List<Point2d> ToPoint2d(this Point3dCollection ptCol)
        {
            List<Point2d> rtnValue = new List<Point2d>();

            for (int i = 0; i < ptCol.Count; i++)
            {
                rtnValue.Add(ptCol[i].ToPoint2d());
            }
            return rtnValue;
        }

        public static Point2d ToPoint2d(this Point3d pt3)
        {
            return new Point2d(pt3.X, pt3.Y);
        }

        public static Point3d ToPoint3d(this Point2d pt2)
        {
            return new Point3d(pt2.X, pt2.Y, 0);
        }

        private const double Epsilon = 1e-10;

        public static bool IsZero(this double d)
        {
            return Math.Abs(d) < Epsilon;
        }

        public static Boolean IsObjectIdValid(this ObjectId objId, Database acDb)
        {
            try
            {
                if (objId.Database == acDb)
                {
                    if (objId.IsValid)
                    {
                        if (!objId.IsErased)
                            return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }

        public static Boolean IsObjectIdValid(this Entity acEntity, Database acDb)
        {
            if (acEntity.Database == acDb)
            {
                if (acEntity != null)
                {
                    ObjectId objId = acEntity.ObjectId;

                    if (objId.IsValid)
                    {
                        if (!objId.IsErased)
                        {
                            if (objId.Database == acDb)
                                return true;
                        }
                    }
                }
            }

            return false;
        }

        internal static int GetCounter(this int intPos, int intMin, int intMax)
        {
            intPos++;
            if (intPos > intMax)
                intPos = intMin;

            return intPos;
        }

        internal static int GetCounter(this int intPos, int intMin, int intMax, List<int> lstExclude)
        {
            intPos++;

            while (!lstExclude.Contains(intPos))
                intPos++;

            if (intPos > intMax)
                intPos = intMin;

            return intPos;
        }



        public static double ToRadians(this double val)
        {
            return (Math.PI / 180) * val;
        }

        public static double ToDegrees(this double val)
        {
            return val * (180.0 / Math.PI);
        }

        internal static List<double> RoundTo(this List<double> lstValue, int intPercision)
        {
            for (int i = 0; i < lstValue.Count; i++)
                lstValue[i] = Math.Round(lstValue[i], intPercision);

            return lstValue;
        }
    }
}