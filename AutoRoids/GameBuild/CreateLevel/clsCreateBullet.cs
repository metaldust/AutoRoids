using System.Collections.Generic;
using Autodesk.AutoCAD.DatabaseServices;
using AutoRoids.Common.Static;
using AutoRoids.GameBuild.Geometry;

namespace AutoRoids.GameBuild.CreateLevel
{
    internal class clsCreateBullet
    {
        internal void EraseGameBullets()
        {
            if (StaticRock.lstBullets != null)
                StaticRock.lstBullets.Clear();
        }

        internal void DeleteBullet(Transaction acTrans, BlockReference acBlkRef)
        {
            clsDeleteEntity clsDeleteEntity = new clsDeleteEntity();
            List<BlockReference> lstBlkRef = new List<BlockReference> { acBlkRef };
            clsDeleteEntity.DeleteEntity(acTrans, ref lstBlkRef);
        }
    }
}