using System;
using System.Collections.Generic;
using Autodesk.AutoCAD.Geometry;
using AutoRoids.Common.Enum;
using AutoRoids.Common.Static;
using AutoRoids.CustomClass.Engine;
using AutoRoids.GameBuild.CreateLevel;
using AutoRoids.GameBuild.Geometry;
using AutoRoids.GameBuild.GetPoints;
using AutoRoids.GameLoop.Movement;
using AutoRoids.GameLoop.Timers;

namespace AutoRoids.GameLoop.Fire
{
    internal class clsFireBullets
    {
        internal void FireBullet(List<enumDirection> lstDirection)
        {
            if (lstDirection.Contains(enumDirection.DeathBlossom) &&
                !lstDirection.Contains(enumDirection.Shield))
            {
                AddDeathBlossom();
                return;
            }

            if (lstDirection.Contains(enumDirection.Fire) &&
                !lstDirection.Contains(enumDirection.Shield))
            {
                AddBullet();
            }
        }

        internal void OffsetBullets()
        {
            double dblOffset = StaticRock.dblBulletOffset;

            double dblMaxTraveled = StaticRock.dblBulletMaxTravel;

            if (StaticRock.lstBullets == null)
                StaticRock.lstBullets = new List<EngineBullet>();

            List<EngineBullet> lstBullets = StaticRock.lstBullets;

            for (int i = lstBullets.Count - 1; i >= 0; i--)
            {
                EngineBullet GameBullets = lstBullets[i];

                if (GameBullets.dblTraveled < dblMaxTraveled)
                {
                    Point2d ptOrigin = GameBullets.ptOrigin;
                    double dblAngle = GameBullets.dblAngle;

                    ptOrigin = ptOrigin.CalculatePoint(dblAngle, dblOffset);
                    GameBullets.dblTraveled += dblOffset + clsMoveShip.dblVelocity;

                    GameBullets.ptOrigin = ptOrigin;
                    lstBullets[i] = GameBullets;
                }
                else
                {
                    clsDeleteEntity clsDeleteEntity = new clsDeleteEntity();
                    StaticRock.lstBullets.RemoveAt(i);

                    //List<EngineBoundingBox>  lstBoundingBox = StaticRock.lstBoundingBox;
                    //StaticRock.lstBoundingBox.RemoveAt(i);
                }
            }
        }

        internal void AddBullet()
        {
            if (CanFire(StaticRock.intBulletDelay))
            {
                // Update Values
                Point2d ptOrigin = GetStartPoint(out double dblAngle);

                EngineBullet EngineBullets = new EngineBullet(ptOrigin, dblAngle);

                if (StaticRock.lstBullets == null)
                    StaticRock.lstBullets = new List<EngineBullet>();

                // Save back
                StaticRock.lstBullets.Add(EngineBullets);
            };
        }

        internal void AddDeathBlossom()
        {
            if (CanFire(StaticRock.intDeathBlossomDelay))
            {
                Point2d ptCenter = StaticRock.engineShip.ptBlockOrigin;
                clsCreateExplode clsCreateExplode = new clsCreateExplode();
                clsCreateExplode.AddDeathBlossom(ptCenter);
            }
        }

        internal Point2d GetStartPoint(out double dblAngle)
        {
            dblAngle = StaticRock.engineShip.dblBlockRotation;

            // Distance from the insertion point to top of ship
            double dblOffset = 44.149 * StaticRock.dblGameScale;

            Point2d ptOrigin = StaticRock.engineShip.ptBlockOrigin;
            return ptOrigin.CalculatePoint(StaticRock.engineShip.dblBlockRotation, dblOffset);
        }

        internal Boolean CanFire(int intDelay)
        {
            Boolean rtnValue = false;
            clsCreatePoints clsGeneratePoints = new clsCreatePoints();

            if (clsTimers.GameStopWatch.StopWatchBullet.IsRunning)
            {
                int intElapsed = Convert.ToInt32(clsTimers.GameStopWatch.StopWatchBullet.ElapsedMilliseconds);

                if (intElapsed > intDelay)
                {
                    rtnValue = true;
                    clsTimers.GameStopWatch.StopWatchBullet.Restart();
                }
            }
            else
            {
                clsTimers.GameStopWatch.StopWatchBullet.Restart();
                rtnValue = true;
            }

            return rtnValue;
        }

        internal void WrapBullet()
        {
            clsGetPointsBorder clsGetPointsBorder = new clsGetPointsBorder();
            List<Point2d> lstBorder = clsGetPointsBorder.GetBorder(1, true);

            for (int i = 0; i < StaticRock.lstBullets.Count; i++)
            {
                EngineBullet EngineBullets = StaticRock.lstBullets[i];

                Point2d pt = EngineBullets.ptOrigin;

                WrapBullet(StaticRock.engineShip.dblScale, ref pt, EngineBullets.dblAngle, lstBorder);

                EngineBullets.ptOrigin = pt;

                StaticRock.lstBullets[i] = EngineBullets;
            }
        }

        internal void WrapBullet(double dblScale, ref Point2d pt, Double dblAngle, List<Point2d> lstBorder)
        {
            clsWrap clsWrap = new clsWrap();
            clsWrap.CalculateRiseOverRun(dblAngle, 10.0,
                                 out double dblRise, out double dblRun);

            List<Point2d> lstBox = pt.CreateBoundingBoxFromPoint(dblScale);

            clsWrap.WrapPosition(lstBox, lstBorder, dblRise, dblRun, 3, false, ref pt);;
        }

    }
}