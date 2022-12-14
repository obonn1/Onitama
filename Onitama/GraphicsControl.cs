namespace Onitama;

public abstract class GraphicsControl : Control
{
    protected SizeF ViewSize { get; set; } = new(10, 5);

    protected float ViewScale { get; private set; }

    protected bool IsLeftMouseDown { get; private set; }

    public GraphicsControl()
    {
        ResizeRedraw = true;
        DoubleBuffered = true;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
        e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
        ViewScale = ScaleGraphics(e.Graphics, ViewSize.Width, ViewSize.Height);
        VisualsDraw(e.Graphics);
    }

    protected virtual void VisualsDraw(Graphics g)
    {
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            IsLeftMouseDown = true;
        }

        var viewLocation = ClientToView(e.Location);
        ViewMouseDown(viewLocation.X, viewLocation.Y, e.Button);
        base.OnMouseDown(e);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            IsLeftMouseDown = false;
        }

        var viewLocation = ClientToView(e.Location);
        ViewMouseUp(viewLocation.X, viewLocation.Y, e.Button);
        base.OnMouseUp(e);
    }

    protected virtual void ViewMouseDown(float x, float y, MouseButtons buttons)
    {
    }

    protected virtual void ViewMouseMove(float x, float y, MouseButtons buttons)
    {
    }

    protected virtual void ViewMouseUp(float x, float y, MouseButtons buttons)
    {
    }

    protected PointF ClientToView(Point client)
    {
        var physicalWidth = ClientSize.Width;
        var physicalHeight = ClientSize.Height;

        if (ViewSize.Width * physicalHeight > physicalWidth * ViewSize.Height)
        {
            ViewScale = physicalWidth / ViewSize.Width;
            return new PointF(client.X / ViewScale, ((client.Y - (physicalHeight / 2f)) / ViewScale) + (ViewSize.Height / 2f));
        }
        else
        {
            ViewScale = physicalHeight / ViewSize.Height;
            return new PointF(((client.X - (physicalWidth / 2f)) / ViewScale) + (ViewSize.Width / 2f), client.Y / ViewScale);
        }
    }

    protected PointF ViewToClient(PointF view)
    {
        var physicalWidth = ClientSize.Width;
        var physicalHeight = ClientSize.Height;

        if (ViewSize.Width * physicalHeight > physicalWidth * ViewSize.Height)
        {
            ViewScale = physicalWidth / ViewSize.Width;
            return new PointF(view.X * ViewScale, ((view.Y - (ViewSize.Height / 2f)) * ViewScale) + (physicalHeight / 2f));
        }
        else
        {
            ViewScale = physicalHeight / ViewSize.Height;
            return new PointF(((view.Y - (ViewSize.Width / 2f)) * ViewScale) + (physicalWidth / 2f), view.Y * ViewScale);
        }
    }

    private float ScaleGraphics(Graphics graphics, float width, float height)
    {
        float scale;

        var physicalWidth = ClientSize.Width;
        var physicalHeight = ClientSize.Height;

        if (width * physicalHeight > physicalWidth * height)
        {
            graphics.TranslateTransform(0, (physicalHeight - (height * physicalWidth / width)) / 2);
            scale = physicalWidth / width;
        }
        else
        {
            graphics.TranslateTransform((physicalWidth - (width * physicalHeight / height)) / 2, 0);
            scale = physicalHeight / height;
        }

        graphics.ScaleTransform(scale, scale);
        return scale;
    }
}
