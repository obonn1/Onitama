// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Onitama;

public abstract class GraphicsControl : Control
{
    protected SizeF ViewSize { get; set; } = new(10, 5);

    protected float ViewScale { get; private set; }

    protected bool IsLeftMouseDown { get; private set; }

    protected PointF MouseView { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GraphicsControl"/> class.
    /// </summary>
    public GraphicsControl()
    {
        this.ResizeRedraw = true;
        this.DoubleBuffered = true;
    }

    /// <inheritdoc/>
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        this.ViewScale = this.ScaleGraphics(e.Graphics, this.ViewSize.Width, this.ViewSize.Height);
        this.ViewDraw(e.Graphics);
        this.VisualsDraw(e.Graphics);
    }

    protected virtual void ViewDraw(Graphics g)
    {
    }

    protected virtual void VisualsDraw(Graphics g)
    {
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            this.IsLeftMouseDown = true;
        }

        var viewLocation = this.ClientToView(e.Location);
        this.ViewMouseDown(viewLocation.X, viewLocation.Y, e.Button);
        base.OnMouseDown(e);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        // MouseView = ClientToView(e.Location);
        // ViewMouseMove(MouseView.X, MouseView.Y, e.Button);
        // base.OnMouseMove(e);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            this.IsLeftMouseDown = false;
        }

        var viewLocation = this.ClientToView(e.Location);
        this.ViewMouseUp(viewLocation.X, viewLocation.Y, e.Button);
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
        var physicalWidth = this.ClientSize.Width;
        var physicalHeight = this.ClientSize.Height;

        if (this.ViewSize.Width * physicalHeight > physicalWidth * this.ViewSize.Height)
        {
            this.ViewScale = physicalWidth / this.ViewSize.Width;
            return new PointF(client.X / this.ViewScale, ((client.Y - (physicalHeight / 2f)) / this.ViewScale) + (this.ViewSize.Height / 2f));
        }
        else
        {
            this.ViewScale = physicalHeight / this.ViewSize.Height;
            return new PointF(((client.X - (physicalWidth / 2f)) / this.ViewScale) + (this.ViewSize.Width / 2f), client.Y / this.ViewScale);
        }
    }

    protected PointF ViewToClient(PointF view)
    {
        var physicalWidth = this.ClientSize.Width;
        var physicalHeight = this.ClientSize.Height;

        if (this.ViewSize.Width * physicalHeight > physicalWidth * this.ViewSize.Height)
        {
            this.ViewScale = physicalWidth / this.ViewSize.Width;
            return new PointF(view.X * this.ViewScale, ((view.Y - (this.ViewSize.Height / 2f)) * this.ViewScale) + (physicalHeight / 2f));
        }
        else
        {
            this.ViewScale = physicalHeight / this.ViewSize.Height;
            return new PointF(((view.Y - (this.ViewSize.Width / 2f)) * this.ViewScale) + (physicalWidth / 2f), view.Y * this.ViewScale);
        }
    }

    private float ScaleGraphics(Graphics graphics, float width, float height)
    {
        float scale;

        var physicalWidth = this.ClientSize.Width;
        var physicalHeight = this.ClientSize.Height;

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
