using System;
using System.Collections.Generic;
using System.Diagnostics;
using AutoRoids.Common.Enum;
using SharpDX.DirectInput;

namespace AutoRoids.GameLoop.Input
{
    internal class clsKeyboard
    {
        internal List<enumDirection> GetDirection()
        {
            List<enumDirection> lstDirection = new List<enumDirection>();

            if (!GetMovementKeyboard(out lstDirection))
            {
                if (clsSharpDX.bolJoystickValid)
                    GetMovementJoystick(out lstDirection);
            }

            return lstDirection;
        }

        internal Boolean GetMovementJoystick(out List<enumDirection> lstDirection)
        {
            Boolean rtnValue = false;

            lstDirection = new List<enumDirection>();

            var joystickState = new JoystickState();

            try
            {
                clsSharpDX.joystick.GetCurrentState(ref joystickState);

            }
            catch (Exception)
            {
                return rtnValue;
            }

            if (joystickState != null)
            {
                for (int i = 0; i < joystickState.Buttons.Length; i++)
                {
                    if (joystickState.Buttons[i] == true)
                    {
                        // 0 = B, 1 = A  2 = Y, 3 = X ;
                        Debug.Print("");
                    }
                }

                if (joystickState.X > 32767) lstDirection.Add(enumDirection.Right);
                if (joystickState.X < 32767) lstDirection.Add(enumDirection.Left);
                if (joystickState.Y < 32766) lstDirection.Add(enumDirection.Up);

                if (joystickState.Buttons[3] == true) lstDirection.Add(enumDirection.Fire);
                if (joystickState.Buttons[2] == true) lstDirection.Add(enumDirection.DeathBlossom);

                if (joystickState.Buttons[1] == true) lstDirection.Add(enumDirection.Shield);
                if (joystickState.Buttons[0] == true) lstDirection.Add(enumDirection.Hyperspace);

            }
            return rtnValue;

        }

        public bool GetMovementKeyboard(out List<enumDirection> lstDirection)
        {
            Boolean rtnValue = false;
            lstDirection = new List<enumDirection>();

            var keyboardState = new KeyboardState();
            try
            {
                clsSharpDX.keyboard.GetCurrentState(ref keyboardState);
            }
            catch (Exception)
            {
                return rtnValue;
            }

            if (keyboardState != null)
            {
                if (keyboardState.PressedKeys.Count > 0)
                {
                    List<Key> lstKeys = keyboardState.PressedKeys;

                    if (lstKeys.Count > 0)
                    {
                        rtnValue = true;

                        for (int i = 0; i < lstKeys.Count; i++)
                        {
                            if (lstKeys[i] == Key.Up) lstDirection.Add(enumDirection.Up);
                            if (lstKeys[i] == Key.Right) lstDirection.Add(enumDirection.Right);
                            if (lstKeys[i] == Key.Left) lstDirection.Add(enumDirection.Left);

                            if (lstKeys[i] == Key.LeftControl) lstDirection.Add(enumDirection.Fire);
                            if (lstKeys[i] == Key.RightControl) lstDirection.Add(enumDirection.Fire);

                            if (lstKeys[i] == Key.LeftShift) lstDirection.Add(enumDirection.Shield);
                            if (lstKeys[i] == Key.RightShift) lstDirection.Add(enumDirection.Shield);

                            if (lstKeys[i] == Key.Delete) lstDirection.Add(enumDirection.Hyperspace);
                            if (lstKeys[i] == Key.End) lstDirection.Add(enumDirection.DeathBlossom);

                        }
                    }
                }
            }
            return rtnValue;
        }
    }
}