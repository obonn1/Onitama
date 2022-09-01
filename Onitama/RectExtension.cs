using System.Drawing.Drawing2D;

namespace Onitama
{
    internal static class RectExtension
    {
        public static GraphicsPath RoundedRectF(RectangleF bounds, float radius)
        {
            float diameter = radius * 2;
            SizeF size = new(diameter, diameter);
            RectangleF arc = new(bounds.Location, size);
            GraphicsPath path = new();

            if (radius == 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            // top left arc
            path.AddArc(arc, 180, 90);

            // top right arc
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);

            // bottom right arc
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // bottom left ar
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }

        public static void DrawRoundedRectangleF(this Graphics graphics, Pen pen, RectangleF bounds, float cornerRadius)
        {
            if (graphics == null)
            {
                throw new ArgumentNullException(nameof(graphics));
            }

            if (pen == null)
            {
                throw new ArgumentNullException(nameof(pen));
            }

            using GraphicsPath path = RoundedRectF(bounds, cornerRadius);
            graphics.DrawPath(pen, path);
        }

        public static void FillRoundedRectangleF(this Graphics graphics, Brush brush, RectangleF bounds, float cornerRadius)
        {
            if (graphics == null)
            {
                throw new ArgumentNullException(nameof(graphics));
            }

            if (brush == null)
            {
                throw new ArgumentNullException(nameof(brush));
            }

            using (GraphicsPath path = RoundedRectF(bounds, cornerRadius))
            {
                graphics.FillPath(brush, path);
            }
        }
    }
}
