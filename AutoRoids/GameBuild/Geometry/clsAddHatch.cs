using System.Collections.Generic;
using Autodesk.AutoCAD.DatabaseServices;
using AutoRoids.Common.Static;

namespace AutoRoids.GameBuild.Geometry
{
    internal class clsAddHatch
    {
        internal List<Hatch> FillInPolyline(Transaction acTrans, Database acDb, BlockTableRecord acBlkTblRec,
                                            List<ObjectId> lstPolyline,
                                            string strPattern, double dblScale)
        {
            List<Hatch> rtnValue = new List<Hatch>();

            for (int i = 0; i < lstPolyline.Count; i++)
            {
                ObjectId objid = lstPolyline[i];

                if (objid.IsObjectIdValid(acDb))
                {
                    Entity acEntity = acTrans.GetObject(lstPolyline[i], OpenMode.ForWrite) as Entity;

                    if (acEntity is Polyline acPline)
                    {
                        if (acPline.Closed)
                            rtnValue.Add(ProcessHatch(acTrans, acDb, acBlkTblRec, strPattern, dblScale, acEntity));
                    }
                    else
                        rtnValue.Add(ProcessHatch(acTrans, acDb, acBlkTblRec, strPattern, dblScale, acEntity));
                }
            }

            return rtnValue;
        }

        internal Hatch ProcessHatch(Transaction acTrans, Database acDb, BlockTableRecord acBlkTblRec,
                                    string strPattern, double dblScale, Entity acEntity)
        {
            ObjectIdCollection objIdColl = new ObjectIdCollection();

            ObjectId objId = acEntity.ObjectId;

            Hatch acHatch = new Hatch();

            if (acEntity.IsObjectIdValid(acDb))
            {
                objIdColl.Add(objId);

                try
                {
                    acHatch.PatternScale = dblScale;
                    acHatch.ColorIndex = acEntity.ColorIndex;
                    acBlkTblRec.AppendEntity(acHatch);
                    acTrans.AddNewlyCreatedDBObject(acHatch, true);

                    acHatch.SetHatchPattern(HatchPatternType.PreDefined, strPattern);
                    acHatch.Associative = true;
                    acHatch.AppendLoop(HatchLoopTypes.External, objIdColl);
                    acHatch.EvaluateHatch(true);
                }
                catch (System.Exception)
                {
                    return null;
                }
            }

            return acHatch;
        }
    }
}