using System;
using System.Collections.Generic;
using AutoRoids.Common.Static;
using AutoRoids.CustomClass.Engine;
using AutoRoids.GameLoop.Input;
using AutoRoids.GameLoop.Timers;
using AutoRoids.UserForm;

namespace AutoRoids.UserFormCode
{
    internal class clsInit
    {
        internal void Init(xmlAutoRoids xmlAutoRoids)
        {
            clsReg clsReg = new clsReg();

            String strRockCount = clsReg.GetRockCount(out int intRockCount);

            string strRockMinDistance = clsReg.GetRockMinDistance(out double dblRockMinDistance);
            string strRockMaxDistance = clsReg.GetRockMaxDistance(out double dblRockMaxDistance);

            string strRockMinRotation = clsReg.GetRockMinRotation(out double dblRockMinRotation);
            string strRockMaxRotation = clsReg.GetRockMaxRotation(out double dblRockMaxRotation);

            string strRockAngle = clsReg.GetRockAngle(out double dblRockAngle);

            string strShipRotation = clsReg.GetShipRotation(out double dblShipRotation);
            string strShipThrust = clsReg.GetShipThrust(out double dblThrust);

            string strGameScale = clsReg.GetGameScale(out double dblGameScale);
            string strIdleDelay = clsReg.GetIdleDelay(out int intIdleDelay);

            string strBulletDelay = clsReg.GetBulletDelay(out int dblBulletDelay);

            string strDeathBlossomDelay = clsReg.GetDeathBlossomDelay(out int dblDeathBlossomDelay);

            xmlAutoRoids.txtRockCount.txtNum.Text = strRockCount;
            xmlAutoRoids.txtRockMinDistance.txtNum.Text = strRockMinDistance;
            xmlAutoRoids.txtRockMaxDistance.txtNum.Text = strRockMaxDistance;

            xmlAutoRoids.txtRockMinRotation.txtNum.Text = strRockMinRotation;
            xmlAutoRoids.txtRockMaxRotation.txtNum.Text = strRockMaxRotation;

            xmlAutoRoids.txtRockAngle.txtNum.Text = strRockAngle;

            xmlAutoRoids.txtShipRotation.txtNum.Text = strShipRotation;
            xmlAutoRoids.txtShipThrust.txtNum.Text = strShipThrust;

            xmlAutoRoids.txtGameScale.txtNum.Text = strGameScale;
            xmlAutoRoids.txtIdleDelay.txtNum.Text = strIdleDelay;

            xmlAutoRoids.chkBlocks.IsChecked = clsReg.GetShowBlocks();
            xmlAutoRoids.chkBoundingBox.IsChecked = clsReg.GetShowBoundingBox();
            xmlAutoRoids.chkOverlay.IsChecked = clsReg.GetShowOverlay();
            xmlAutoRoids.chkZoomToShip.IsChecked = clsReg.GetZoomToShip();

            xmlAutoRoids.txtBulletDelay.txtNum.Text = strBulletDelay;
            xmlAutoRoids.txtDeathBlossomDelay.txtNum.Text = strDeathBlossomDelay;
            clsTimers.GameStopWatch = new GameStopWatch();

            InitBoundingBox();

            // Init Keyboard
            clsSharpDX.Main();
        }

        private void InitBoundingBox()
        {
            if (StaticRock.lstBoundingBox == null)
                StaticRock.lstBoundingBox = new List<EngineBoundingBox>();

            StaticRock.lstBoundingBox.Clear();
        }
    }
}