﻿using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onitama
{
    internal class OniBoard : GraphicsControl
    {
        public GameState GameState { get; set; } = new GameState();
        public GameVisuals Visuals { get; set; } = new GameVisuals();
        public Color MatColor { get; set; } = Color.Orange;
        public override Color BackColor { get; set; } = Color.Gray;
        public Color GridColor { get; set; } = Color.Green;
        public PointF GridOrigin { get; set; } = new PointF();

        public OniBoard()
        {
            ViewSize = new SizeF(10, 7);
        }

        public PointF ViewToGrid(PointF value)
        {
            return new PointF(value.X - GridOrigin.X, value.Y - GridOrigin.Y);
        }
        public PointF GridToView(PointF value)
        {
            return new PointF(value.X + GridOrigin.X, value.Y + GridOrigin.Y);
        }

        protected override void ViewDraw(Graphics g)
        {
            using (var pen = new Pen(Color.Black, 0.05f))
            {
                RectangleF mat = new RectangleF(0.15f, 1.65f, 9.7f, 5.2f);
                RectangleF board = new RectangleF(0.05f, 0.05f, 9.9f, 6.9f);
                RectangleF grid = new RectangleF(2.5f, 1.75f, 5f, 5f);
                GridOrigin = new PointF(grid.X, grid.Y);

                g.DrawRectangle(pen, board.X, board.Y, board.Width, board.Height);
                g.FillRectangle(new SolidBrush(Color.Blue), board);
                g.FillRoundedRectangleF(new SolidBrush(MatColor), mat, .1f);
                g.FillRectangle(new SolidBrush(Color.Black), grid);
                g.DrawRectangle(pen, grid.X, grid.Y, grid.Width, grid.Height);
            }
        }

        
    }
}
