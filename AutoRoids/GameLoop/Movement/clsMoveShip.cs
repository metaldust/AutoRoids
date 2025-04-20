using System;
using System.Collections.Generic;
using System.Diagnostics;
using Autodesk.AutoCAD.Geometry;
using AutoRoids.Common.Enum;
using AutoRoids.Common.Formulas;
using AutoRoids.Common.Static;
using AutoRoids.CustomClass.Engine;
using AutoRoids.UserFormCode;

namespace AutoRoids.GameLoop.Movement
{
    internal class clsMoveShip
    {
        internal void ProcessShipMovement(ref EngineShip EngineShip, List<enumDirection> lstDirection)
        {
            Point2d pt = EngineShip.ptBlockOrigin;
            double dblAngleLocal = EngineShip.dblBlockRotation;

            Point2d ptStart = new Point2d(pt.X, pt.Y);

            GetRotate(lstDirection, ref dblAngleLocal);

            EngineShip.dblBlockRotation = dblAngleLocal;

            ShipPhysics(lstDirection.Contains(enumDirection.Up), dblAngleLocal, ref pt);
            EngineShip.ptBlockOrigin = pt;

            double dblTravel = ptStart.GetAngle(pt);
            clsWrap clsWrap = new clsWrap();
            clsWrap.WrapShip(ref EngineShip, dblTravel);

            // clsWrap.ShowBoundingBox(EngineShip);
            if (StaticRock.bolZoomToShip)
            {
                clsZoom clsZoom = new clsZoom();
                clsZoom.ZoomToPoint(pt.ToPoint3d());
            }
        }

        internal void GetRotate(List<enumDirection> lstDirection, ref double dblAngle)
        {
            for (int i = 0; i < lstDirection.Count; i++)
            {
                enumDirection dir = lstDirection[i];

                if (dir == enumDirection.Right)
                    dblAngle -= StaticRock.dblShipAngle;

                if (dir == enumDirection.Left)
                    dblAngle += StaticRock.dblShipAngle;

                //if (dir == enumDirection.DeathBlossom)
                //    dblAngle -= 7.5;
            }
        }

        public static double dblLocalAngle = 0.0;
        public static double dblAccelerationAngle = 0.0;
        public static double dblVelocity = 0.0;
 

        private void ShipPhysics(Boolean m_bAccelerating, double dblAngleLocal, ref Point2d position)
        {
            dblAccelerationAngle = (dblAngleLocal + 90.0).ToRadians();

            double ACCELERATIONSPEED = 0.015;
            double DECELERATIONSPEED = 0.0002;

            clsReg clsReg = new clsReg();
            clsReg.GetShipThrust(out double dblThrust);

            // Step 1: Calculate the new acceleration, no change in trajectory cases
            if (!m_bAccelerating)
            {
                if (dblVelocity > 0.0)
                    dblVelocity -= DECELERATIONSPEED;
                else
                    dblVelocity = 0.0;
            }
            // Step 2: Calculate acceleration changes, positive acceleration...
            // Increase velocity from full stop...
            else if (dblVelocity == 0.0)
            {
                dblVelocity += ACCELERATIONSPEED;
                dblLocalAngle = dblAccelerationAngle;
            }
            // Constant Increase in Velocity, Same Direction.
            else if (((dblVelocity + ACCELERATIONSPEED) < dblThrust)
                      && (dblLocalAngle == dblAccelerationAngle))
            {
                dblVelocity += ACCELERATIONSPEED;
            }
            else /// Increase velocity, mid flight, could include trajectory change....
            {
                // Calculate existing expected offsets from current position
                double vfCX = dblVelocity * Math.Cos(dblLocalAngle);
                double vfCY = dblVelocity * Math.Sin(dblLocalAngle);
                // Calculate the change in position based on the thrust direction
                double vfNX = ACCELERATIONSPEED * Math.Cos(dblAccelerationAngle);
                double vfNY = ACCELERATIONSPEED * Math.Sin(dblAccelerationAngle);
                // Make sure the accelertaion adjustment does not exceed maximum acceleration, and then set it if it won't.
                if ((Math.Sqrt(Math.Pow(vfCX + vfNX, 2) + Math.Pow(vfCY + vfNY, 2)) < dblThrust))
                    dblVelocity = Math.Sqrt(Math.Pow(vfCX + vfNX, 2) + Math.Pow(vfCY + vfNY, 2));

                // calculate the new angle...
                // I was using ATAN at first, which was causing glitchiness, changing it to ATAN2 resolves that glitch.
                // This StackOverflow question details the different in quadrant calculations that each returns
                // https://stackoverflow.com/questions/283406/what-is-the-difference-between-atan-and-atan2-in-c
                dblLocalAngle = (float)Math.Atan2((vfCY + vfNY), (vfCX + vfNX));
                // Reference this wonderful fuckin video for how this was obtained!
                // From: Michel van Biezen  https://www.youtube.com/watch?v=l53G_Y1kLc4
            }

            double dblOffset = dblVelocity * dblThrust;

            if (dblOffset > dblThrust)
                dblOffset = dblThrust;

            if (dblVelocity > 1)
                dblVelocity = 1;

            double sa = Math.Sin(dblLocalAngle);
            double ca = Math.Cos(dblLocalAngle);

            Point2d ptNew = new Point2d(position.X + (sa * dblOffset),
                                        position.Y - (ca * dblOffset));

            // ShowDebug(position, ptNew);

            position = ptNew;
        }

        private void ShowDebug(Point2d position, Point2d ptNew)
        {
            double dblLength = ptNew.GetDistance(position);

            Debug.Print("V:" + dblVelocity + " L:" + dblLength);
        }
    }
}