using System;
using System.Collections.Generic;
using Autodesk.AutoCAD.Geometry;
using AutoRoids.Common.Static;

namespace AutoRoids.GameBuild.CreateLevel
{
    internal class clsCreatePoints
    {
        internal Random GetRandom()
        {
            int i = (int)(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond);

            return new Random(i);
        }

        internal Point2d GetPoint()
        {
            Random random = GetRandom();

            return GetRandomPoint(random, StaticRock.dblOuterWidth, StaticRock.dblOuterHeight);
        }

        internal List<Point2d> GetPoints(int numPoints, Boolean bolGameStart)
        {
            Random random = GetRandom();

            double minSpacing = 250 * StaticRock.dblGameScale;

            Point2d pt = StaticRock.engineShip.ptBlockOrigin;

            List<Point2d> lstTotal = new List<Point2d>();
            while (lstTotal.Count < numPoints)
            {
                if (bolGameStart)
                {
                    // Exclude middle section of the screen
                    List<Point2d> lstPoint = GenerateRandomPoints(random, StaticRock.dblOuterWidth, StaticRock.dblOuterHeight,
                                           StaticRock.dblInnerWidth, StaticRock.dblInnerHeight,
                                           numPoints, minSpacing);

                    Append(lstPoint, numPoints, ref lstTotal);
                }
                else
                {
                    // Exclude area around ship
                    List<Point2d> lstPoint = GenerateRandomPoints(random, pt, StaticRock.dblOuterWidth, StaticRock.dblOuterHeight,
                                             700, numPoints, minSpacing);

                    Append(lstPoint, numPoints, ref lstTotal);
                }
            }

            return lstTotal;
        }

        private void Append(List<Point2d> lstPoint, int numPoints,
                            ref List<Point2d> lstTotal)
        {
            for (int i = 0; i < lstPoint.Count; i++)
            {
                if (lstTotal.Count < numPoints)
                    lstTotal.Add(lstPoint[i]);
                else
                    break;
            }
        }

        private List<Point2d> GenerateRandomPoints(Random random, double outerWidth, double outerHeight,
                                                   double innerWidth, double innerHeight,
                                                   int numPoints, double minSpacing)
        {
            List<Point2d> lstPoints = new List<Point2d>();

            for (int i = 0; i < numPoints * 100; i++)
            {
                if (lstPoints.Count < numPoints)
                {
                    double x = (random.NextDouble() - 0.5) * outerWidth;
                    double y = (random.NextDouble() - 0.5) * outerHeight;

                    // Check if the point is inside the inner bounding box
                    if (Math.Abs(x) < innerWidth / 2.0 && Math.Abs(y) < innerHeight / 2.0)
                    {
                        continue; // Skip points inside the inner box
                    }

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

                        if (lstPoints.Count == numPoints)
                            break;
                    }
                }
            }

            return lstPoints;
        }


        private List<Point2d> GenerateRandomPoints(Random random, Point2d centerPoint, double outerWidth, double outerHeight,
                                                   double innerRadius, int numPoints, double minSpacing)
        {
            List<Point2d> lstPoints = new List<Point2d>();

            for (int i = 0; i < numPoints * 100; i++)
            {
                if (lstPoints.Count < numPoints)
                {
                    double x = (random.NextDouble() - 0.5) * outerWidth;
                    double y = (random.NextDouble() - 0.5) * outerHeight;

                    // Check if the point is inside the circular exclusion zone
                    double distanceToCenter = centerPoint.GetDistanceTo(new Point2d(x, y));
                    if (distanceToCenter < innerRadius)
                    {
                        continue; // Skip points inside the circular exclusion zone
                    }

                    // Check if the point is too close to existing points
                    bool isValidPoint = true;
                    foreach (Point2d existingPoint in lstPoints)
                    {
                        double pointDistance = existingPoint.GetDistanceTo(new Point2d(x, y));
                        if (pointDistance < minSpacing)
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

        private Point2d GetRandomPoint(Random random, double outerWidth, double outerHeight)
        {
            double x = (random.NextDouble() - 0.5) * outerWidth;
            double y = (random.NextDouble() - 0.5) * outerHeight;
            return new Point2d(x, y);
        }
    }
}