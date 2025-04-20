using System;
using System.Collections.Generic;
using Autodesk.AutoCAD.Geometry;
using AutoRoids.Common.Static;

namespace AutoRoids.GameBuild.CreateLevel
{
    internal class clsCreateStarfield
    {
        internal List<Point2d> GetStarField()
        {
            clsCreatePoints clsGeneratePoints = new clsCreatePoints();
            Random random = clsGeneratePoints.GetRandom();
            return GetStarField(random, StaticRock.dblOuterWidth + 425, StaticRock.dblOuterHeight + 425, 100, 100);
        }

        private List<Point2d> GetStarField(Random random, double outerWidth, double outerHeight,
                                         int numPoints, double minSpacing)
        {
            List<Point2d> lstPoints = new List<Point2d>();

            for (int i = 0; i < numPoints * 100; i++)
            {
                if (lstPoints.Count < numPoints)
                {
                    double x = (random.NextDouble() - 0.5) * outerWidth;
                    double y = (random.NextDouble() - 0.5) * outerHeight;

                    // Check if the point is too close to existing points
                    bool isValidPoint = true;
                    foreach (Point2d existingPoint in lstPoints)
                    {
                        double distance = existingPoint.GetDistanceTo(new Point2d(x, y));
                        if (distance < minSpacing)
                        {
                            isValidPoint = false;
                            break;
                        }
                    }

                    // Add the point if it's valid
                    if (isValidPoint)
                    {
                        lstPoints.Add(new Point2d(x, y));
                    }
                }
            }

            return lstPoints;
        }
    }
}