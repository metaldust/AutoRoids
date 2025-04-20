using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using static Autodesk.AutoCAD.ApplicationServices.Application;
using Application = Autodesk.AutoCAD.ApplicationServices.Core.Application;

namespace AutoRoids 
{
    public class clsViewUtil
    {
        private Document _dwg;

        public clsViewUtil(Document dwg)
        {
            _dwg = dwg;
        }

        internal Point2d GetCurrentViewSize()
        {
            //Get current view height
            double h = (double)Application.GetSystemVariable("VIEWSIZE");

            //Get current view width,
            //by calculate current view's width-height ratio
            Point2d screen = (Point2d)Application.GetSystemVariable("SCREENSIZE");
            double w = h * (screen.X / screen.Y);

            return new Point2d(w, h);
        }

        internal Extents2d GetCurrentViewBound(double shrinkScale = 1.0, bool drawBoundBox = false)
        {
            //Get current view size
            Point2d vSize = GetCurrentViewSize();

            double w = vSize.X * shrinkScale;
            double h = vSize.Y * shrinkScale;


            //Get current view's centre.
            //Note, the centre point from VIEWCTR is in UCS and
            //need to be transformed back to World CS
            Point3d cent = ((Point3d)Application.GetSystemVariable("VIEWCTR")).
                TransformBy(_dwg.Editor.CurrentUserCoordinateSystem);

            Point2d minPoint = new Point2d(cent.X - w / 2.0, cent.Y - h / 2.0);
            Point2d maxPoint = new Point2d(cent.X + w / 2.0, cent.Y + h / 2.0);

            if (drawBoundBox)
            {
                DrawBoundBox(minPoint, maxPoint);
            }

            return new Extents2d(minPoint, maxPoint);
        }

        private void DrawBoundBox(Point2d minPoint, Point2d maxPoint)
        {
            using (Transaction tran = _dwg.TransactionManager.StartTransaction())
            {
                //Get current space
                BlockTableRecord space = (BlockTableRecord)tran.GetObject(
                    _dwg.Database.CurrentSpaceId, OpenMode.ForWrite);

                //Create a polyline as bounding box
                Polyline pl = new Polyline(4);

                pl.AddVertexAt(0, minPoint, 0.0, 0.0, 0.0);
                pl.AddVertexAt(1, new Point2d(minPoint.X, maxPoint.Y), 0.0, 0.0, 0.0);
                pl.AddVertexAt(2, maxPoint, 0.0, 0.0, 0.0);
                pl.AddVertexAt(3, new Point2d(maxPoint.X, minPoint.Y), 0.0, 0.0, 0.0);
                pl.Closed = true;

                pl.SetDatabaseDefaults(_dwg.Database);

                space.AppendEntity(pl);
                tran.AddNewlyCreatedDBObject(pl, true);

                tran.Commit();
            }
        }
    }
}
