using System.Drawing;
using System.Drawing.Drawing2D;

namespace ExtraControls.Helpers
{
    public static class GraphicsHelper
    {
        public static Graphics SetOptions(this Graphics gfx)
        {
            gfx.SmoothingMode = SmoothingMode.HighQuality;
            gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;
            gfx.CompositingQuality = CompositingQuality.AssumeLinear;
            gfx.CompositingMode = CompositingMode.SourceOver;
            return gfx;
        }

        public static Graphics DrawRoundedRectangle(this Graphics gfx, Rectangle drawRect, int penThickness, int cornerRadius, Color? colorBorder = null, Color? colorFill = null, bool drawBorder = true, bool fillRectangle = true)
        {
            if (!(fillRectangle || drawBorder)) return gfx;

            var colBorder = colorBorder ?? Color.Empty;
            var colFill = colorFill ?? colBorder;

            var path = GetRoundedRectPath(drawRect, cornerRadius, penThickness);
            if (fillRectangle)
            {
                using (var brush = new SolidBrush(colFill))
                    gfx.FillPath(brush, path);
            }
            if (drawBorder)
            {
                using (var pen = new Pen(colBorder, penThickness))
                    gfx.DrawPath(pen, path);
            }

            return gfx;
        }

        private static GraphicsPath GetRoundedRectPath(Rectangle bounds, int cornerRadius, int penThickness)
        {


            //fix size by penThickness
            int deltaLock = penThickness / 2;
            int deltaSize = deltaLock * 2 + 1;

            var resultBounds = new Rectangle(
                bounds.X + deltaLock,
                bounds.Y + deltaLock,
                bounds.Width - deltaSize,
                bounds.Height - deltaSize);


            int diameter = cornerRadius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(resultBounds.Location, size);

            GraphicsPath path = new GraphicsPath();

            if (cornerRadius == 0)
            {
                path.AddRectangle(resultBounds);
                return path;
            }

            //top left

            // top left arc  
            path.AddArc(arc, 180, 90);

            // top right arc  
            arc.X = resultBounds.Right - diameter;
            path.AddArc(arc, 270, 90);

            // bottom right arc  
            arc.Y = resultBounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // bottom left arc 
            arc.X = resultBounds.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }
    }
}
