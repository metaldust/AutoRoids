using System;
using SharpDX.DirectInput;

namespace AutoRoids.GameLoop.Input
{
    internal static class clsSharpDX
    {
        internal static Boolean bolJoystickValid;
        internal static void Main()
        {
            MainForKeyboard();
            bolJoystickValid = MainForJoystick();
        }

        internal static Keyboard keyboard = null;

        internal static Joystick joystick = null;

        private static void MainForKeyboard()
        {
            // Initialize DirectInput
            var directInput = new DirectInput();

            // Instantiate the joystick
            keyboard = new Keyboard(directInput);

            // Acquire the joystick
            keyboard.Properties.BufferSize = 128;
            keyboard.Acquire();
        }

        static Boolean MainForJoystick()
        {
            try
            {
                // Initialize DirectInput
                var directInput = new DirectInput();

                // Find a Joystick Guid
                var joystickGuid = Guid.Empty;

                foreach (var deviceInstance in directInput.GetDevices(DeviceType.Gamepad, DeviceEnumerationFlags.AllDevices))
                    joystickGuid = deviceInstance.InstanceGuid;

                // If Gamepad not found, look for a Joystick
                if (joystickGuid == Guid.Empty)
                    foreach (var deviceInstance in directInput.GetDevices(DeviceType.Joystick, DeviceEnumerationFlags.AllDevices))
                        joystickGuid = deviceInstance.InstanceGuid;

                // If Joystick not found, throws an error
                if (joystickGuid == Guid.Empty)
                {
                    Console.WriteLine("No joystick/Gamepad found.");
                    Console.ReadKey();
                    Environment.Exit(1);
                }

                // Instantiate the joystick
                joystick = new Joystick(directInput, joystickGuid);

                Console.WriteLine("Found Joystick/Gamepad with GUID: {0}", joystickGuid);

                // Query all suported ForceFeedback effects
                var allEffects = joystick.GetEffects();
                foreach (var effectInfo in allEffects)
                    Console.WriteLine("Effect available {0}", effectInfo.Name);

                // Set BufferSize in order to use buffered data.
                joystick.Properties.BufferSize = 128;

                // Acquire the joystick
                joystick.Acquire();

                return true;
            }
            catch (Exception)
            {
                return false;

            }

        }
    }
}