using System;
using System.Collections.Generic;
using Autodesk.AutoCAD.DatabaseServices;

namespace AutoRoids.GameBuild.CreateLevel
{
    internal class clsCreateCommon
    {
        internal void SetDefaultProperties()
        {
            try
            {
                Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("LTSCALE", 100);
                Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("UCSICON", 0);
                Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("NAVVCUBEDISPLAY", 0);
                Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("NAVBARDISPLAY", 0);
            }
            catch (Exception)
            {
            }
        }

        internal void HideBlockReference(Transaction acTrans, List<BlockReference> lstBlkRef)
        {
            for (int i = 0; i < lstBlkRef.Count; i++)
                HideBlockReference(acTrans, lstBlkRef[i]);
        }

        internal void HideBlockReference(Transaction acTrans, BlockReference acBlkRef)
        {
            if (acBlkRef.Visible)
            {
                acBlkRef = acTrans.GetObject(acBlkRef.ObjectId, OpenMode.ForWrite) as BlockReference;
                acBlkRef.Visible = false;
            }
        }
    }
}