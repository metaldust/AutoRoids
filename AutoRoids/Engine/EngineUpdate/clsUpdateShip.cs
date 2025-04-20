using System;
using System.Collections.Generic;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using AutoRoids.Common.Enum;
using AutoRoids.Common.Static;
using AutoRoids.CustomClass.Engine;
using AutoRoids.GameLoop.Input;
using AutoRoids.GameLoop.Movement;

namespace AutoRoids.Engine.EngineUpdate
{
    internal class clsUpdateShip
    {
        internal List<enumDirection> MoveShip()
        {
            EngineShip EngineShip = StaticRock.engineShip;

            clsKeyboard clsKeyboard = new clsKeyboard();
            List<enumDirection> lstDirection = clsKeyboard.GetDirection();

            clsMoveThrust clsMoveThrust = new clsMoveThrust();
            Boolean bolHideThrust = clsMoveThrust.Thrust(lstDirection, out Boolean bolToggle);

            clsMoveShip clsMovement = new clsMoveShip();
            clsMovement.ProcessShipMovement(ref EngineShip, lstDirection);

            clsMoveHyperSpace clsMoveHyperSpace = new clsMoveHyperSpace();
            clsMoveHyperSpace.HyperSpace(lstDirection, ref EngineShip.ptBlockOrigin);

            SetCurrentPosition(EngineShip.ptBlockOrigin, EngineShip.dblBlockRotation,
                               bolHideThrust, bolToggle, lstDirection.Contains(enumDirection.Shield));

            return lstDirection;
        }

        internal void SetCurrentPosition(Point2d pt, Double dblAngle, Boolean bolHideThrust,
                                      Boolean bolToggleThrust, Boolean bolShowShield)
        {
            EngineShip EngineShip = StaticRock.engineShip;
            List<BlockReference> lstShip = EngineShip.lstBlkRefShip;

            for (int i = 0; i < lstShip.Count; i++)
            {
                EngineShip.ptBlockOrigin = pt;
                EngineShip.dblBlockRotation = dblAngle;

                if (i == 1)
                {
                    if (bolHideThrust)
                        EngineShip.bolVisibleThrust = false;
                    else
                    {
                        if (bolToggleThrust)
                            StaticRock.engineShip.bolVisibleThrust = !StaticRock.engineShip.bolVisibleThrust;
                    }
                }
            }

            EngineShip.bolVisibleShield = bolShowShield;

            if (bolShowShield)
            {
                for (int i = 0; i < EngineShip.lstBlkRefShield.Count; i++)
                {
                    if (i.IsEven())
                        EngineShip.lstRotationShield[i] += 0.4;
                    else
                        EngineShip.lstRotationShield[i] -= 0.4;
                }
            }

            StaticRock.engineShip = EngineShip;
        }
    }
}