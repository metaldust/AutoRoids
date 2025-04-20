using System;
using System.Collections.Generic;
using Autodesk.AutoCAD.DatabaseServices;

namespace AutoRoids.GameBuild.Geometry
{
    internal class clsDeleteEntity
    {
        internal void HideEntity(Transaction acTrans, ref List<Entity> lstEntity)
        {
            if (lstEntity != null)
            {
                for (int i = 0; i < lstEntity.Count; i++)
                {
                    Entity acLine = lstEntity[i];
                    if (acLine.ObjectId.IsValid && !acLine.ObjectId.IsErased)
                    {
                        try
                        {
                            acLine = acTrans.GetObject(acLine.ObjectId, OpenMode.ForWrite) as Entity;
                            acLine.Visible = false;
                        }
                        catch (System.Exception)
                        {
                        }
                    }
                }

                lstEntity.Clear();
            }
            else
                lstEntity = new List<Entity>();
        }

        internal void DeleteEntity(Transaction acTrans, ref List<BlockReference> lstEntity)
        {
            if (lstEntity != null)
            {
                for (int i = 0; i < lstEntity.Count; i++)
                {
                    Entity acLine = lstEntity[i];

                    try
                    {
                        if (acLine.ObjectId.IsValid && !acLine.ObjectId.IsErased)
                        {
                            acLine = acTrans.GetObject(acLine.ObjectId, OpenMode.ForWrite) as Entity;
                            acLine.Erase();
                        }
                    }
                    catch (Exception)
                    {
                    }
                }

                lstEntity.Clear();
            }
            else
                lstEntity = new List<BlockReference>();
        }

        internal void DeleteEntity(Transaction acTrans, ref List<DBPoint> lstEntity)
        {
            if (lstEntity != null)
            {
                for (int i = 0; i < lstEntity.Count; i++)
                {
                    Entity acLine = lstEntity[i];

                    try
                    {
                        if (acLine.ObjectId.IsValid && !acLine.ObjectId.IsErased)
                        {
                            acLine = acTrans.GetObject(acLine.ObjectId, OpenMode.ForWrite) as Entity;
                            acLine.Erase();
                        }
                    }
                    catch (Exception)
                    {
                    }
                }

                lstEntity.Clear();
            }
            else
                lstEntity = new List<DBPoint>();
        }

        internal void DeleteEntity(Transaction acTrans, ref List<Entity> lstEntity)
        {
            if (lstEntity != null)
            {
                for (int i = 0; i < lstEntity.Count; i++)
                {
                    Entity acLine = lstEntity[i];

                    try
                    {
                        if (acLine.ObjectId.IsValid && !acLine.ObjectId.IsErased)
                        {
                            acLine = acTrans.GetObject(acLine.ObjectId, OpenMode.ForWrite) as Entity;
                            acLine.Erase();
                        }
                    }
                    catch (Exception)
                    {
                    }
                }

                lstEntity.Clear();
            }
            else
                lstEntity = new List<Entity>();
        }
    }
}