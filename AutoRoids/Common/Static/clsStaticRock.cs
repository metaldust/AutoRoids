using System;
using System.Collections.Generic;
using Autodesk.AutoCAD.DatabaseServices;
using AutoRoids.CustomClass.Engine;
using AutoRoids.UserFormCode;

namespace AutoRoids.Common.Static
{
    internal static class StaticRock
    {
        internal static string strDrawing;

        // Number of Ships
        internal const int intPlayerNumber = 5;

        // Star field
        public const double dblOuterWidth = 3150;

        public const double dblOuterHeight = 1950;

        // Default Rock
        public const double dblInnerWidth = 1200;

        public const double dblInnerHeight = 2200;

        // Block Line Width
        internal const double dblLineWidth = 0.03;

        // Bullet Times
        internal static double dblBulletOffset = 30;

        internal const double dblBulletMaxTravel = 2000;
        internal static int intBulletDelay = 150;
        internal static int intDeathBlossomDelay = 50;

        // Explode Rock Speed
        internal static readonly double dblExplodeSpeed = 1.0;

        // Registry

        internal static double dblRockAngle;

        internal static double dblRockMinDistance;
        internal static double dblRockMaxDistance;

        internal static double dblRockMinRotation;
        internal static double dblRockMaxRotation;

        internal static double dblShipAngle;
        private static double dblShipMaxSpeed;

        internal static double dblGameScale;
        //internal static int intIdleDelay;

        //internal static Boolean bolShowBlocks;
        //internal static Boolean bolShowOverlay;
        internal static Boolean bolBoundingBox;
        internal static Boolean bolZoomToShip;

        // Engine Objects
        internal static List<EngineRock> lstEngineRock;
        internal static List<EngineBullet> lstBullets;
        internal static List<EngineExplode> lstExplode;

        // Temp Lines
        internal static List<EngineBoundingBox> lstBoundingBox;
        internal static List<EngineShipDebris> lstShipDebris;

        internal static EngineShip engineShip;
        internal static EngineBackGround engineBackGround;
        internal static EngineScore engineScore;

        internal static List<Polyline> lstLines = new List<Polyline>();
        internal static List<DBPoint> lstPoint = new List<DBPoint>();

        internal static void SetStaticRegistry(Boolean bolInit)
        {
            clsReg clsReg = new clsReg();

            clsReg.GetRockAngle(out dblRockAngle);

            clsReg.GetRockMaxDistance(out dblRockMaxDistance);
            clsReg.GetRockMinDistance(out dblRockMinDistance);

            clsReg.GetRockMaxRotation(out dblRockMaxRotation);
            clsReg.GetRockMinRotation(out dblRockMinRotation);

            clsReg.GetShipRotation(out dblShipAngle);
            clsReg.GetShipThrust(out dblShipMaxSpeed);

            clsReg.GetGameScale(out dblGameScale);

            //clsReg.GetIdleDelay(out StaticRock.intIdleDelay);
            clsReg.GetDeathBlossomDelay(out intDeathBlossomDelay);

            clsReg.GetBulletDelay(out intBulletDelay);
 ;
            StaticRock.bolBoundingBox = clsReg.GetShowBoundingBox(); 
            StaticRock.bolZoomToShip = clsReg.GetZoomToShip();
            StaticRock.dblBulletOffset = dblShipMaxSpeed * 2;
        }
    }
}