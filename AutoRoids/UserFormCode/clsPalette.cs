using System;
using System.Drawing;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Windows;
using AutoRoids.UserForm;
using AutoRoids.UserFormCode;

[assembly: CommandClass(typeof(clsPalette))]

namespace AutoRoids.UserFormCode
{
    public class clsPalette
    {
        private static PaletteSet _ps = null;
        private string strCurrentPalette = string.Empty;

        private const int intWidth = 220;
        private const int intHeight = 200;

        [CommandMethod("AutoRoids")]
        public void ShowWPFPalette()
        {
            if (_ps == null)
            {
                // Create the palette set
                clsReg clsReg = new clsReg();
                _ps = new PaletteSet("AutoRoids", new Guid(clsReg.GetAutoRoids()));

                _ps.DockEnabled = DockSides.Right | DockSides.Left;

                _ps.Style = Autodesk.AutoCAD.Windows.PaletteSetStyles.ShowPropertiesMenu
                            | Autodesk.AutoCAD.Windows.PaletteSetStyles.ShowAutoHideButton
                            | Autodesk.AutoCAD.Windows.PaletteSetStyles.ShowCloseButton
                            | Autodesk.AutoCAD.Windows.PaletteSetStyles.Snappable;

                xmlAutoRoids xmlAutoRoids = new xmlAutoRoids();
                xmlProjectManager xmlProjectManager = new xmlProjectManager();

                _ps.MinimumSize = new Size(intWidth, intHeight);

                _ps.AddVisual("AutoRoids", xmlAutoRoids);
                _ps.AddVisual("Project", xmlProjectManager);

                _ps.RecalculateDockSiteLayout();

                _ps.StateChanged += new PaletteSetStateEventHandler(PaletteSet_StateChanged);

                _ps.PaletteActivated += _ps_PaletteActivated;

                // Display our palette set
                _ps.KeepFocus = true;
                _ps.Visible = true;
            }

            _ps.KeepFocus = true;
            _ps.Visible = true;
        }

        private void _ps_PaletteActivated(object sender, PaletteActivatedEventArgs e)
        {
            strCurrentPalette = e.Activated.Name;
        }

        private void PaletteSet_StateChanged(object sender, PaletteSetStateEventArgs e)
        {
            if (e.NewState.ToString().ToUpper() == "SHOW")
            {
            }
        }
    }
}