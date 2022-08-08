using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onitama
{
    internal class OniBoard : GraphicsControl
    {
        public GameState GameState { get; set; }
        public GameVisuals Visuals { get; set; }

        public Color MatColor { get; set; } = Color.Orange;

        public Color BoardColor { get; set; } = Color.Blue;
        public Color GridColor { get; set; } = Color.Green;

        public OniBoard()
        {
            Visuals = new GameVisuals();
            GameState = new GameState();
        }

        protected override void ViewDraw(Graphics g)
        {
            using (var pen = new Pen(Color.Black, 0.05f))
            {
                RectangleF rect = new RectangleF(0.5f, 5f, 10f, 5f);
                g.TranslateTransform(0.5f, 0.5f);

                g.DrawRectangleF(pen, rect);
                g.FillRectangle(new Brush(Color.Black, ))
            }
        }
    }
}
