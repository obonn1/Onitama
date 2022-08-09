using System;
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
        public Color MatColor { get; set; } = Color.Goldenrod;
        public override Color BackColor { get; set; } = Color.DarkGray;
        public Color GridColor { get; set; } = Color.Green;
        public PointF GridOrigin { get; set; } = new PointF();

        public override Font Font { get; set; } = new Font("Arial", 0.02f);

        public GameVisuals Visuals { get; set; }

        public OniBoard()
        {
            ViewSize = new SizeF(10, 7);
            Visuals = new GameVisuals(GridOrigin);
        }

        public PointF ViewToGrid(float x, float y)
        {
            return new PointF(x - GridOrigin.X, y - GridOrigin.Y);
        }
        public PointF GridToView(float x, float y)
        {
            return new PointF(x + GridOrigin.X, y + GridOrigin.Y);
        }

        protected override void ViewDraw(Graphics g)
        {
            Visuals.GridOrigin = GridOrigin;
            using var pen = new Pen(Color.Black, 0.05f);
            RectangleF board = new(0.05f, 0.05f, 9.9f, 6.9f);
            RectangleF mat = new(0.15f, 1.65f, 9.7f, 5.2f);
            RectangleF grid = new(2.5f, 1.75f, 5f, 5f);
            GridOrigin = new PointF(grid.X, grid.Y);

            g.DrawRectangle(pen, board.X, board.Y, board.Width, board.Height);
            g.FillRectangle(new SolidBrush(Color.DarkGreen), board);
            g.FillRoundedRectangleF(new SolidBrush(MatColor), mat, .1f);
            g.FillRectangle(new SolidBrush(Color.Olive), grid);
            g.DrawRectangle(pen, grid.X, grid.Y, grid.Width, grid.Height);
            for (int y = 0; y < 5; y++)
                for (int x = 0; x < 5; x++)
                {
                    g.DrawRectangle(new Pen(Color.DarkOliveGreen, 0.03f), GridToView(x, y).X + 0.1f, GridToView(x, y).Y + 0.1f, 0.8f, 0.8f);
                }
        }

        protected override void VisualsDraw(Graphics g)
        {
            for (int y = 0; y < 5; y++)
                for (int x = 0; x < 5; x++)
                {
                    if (GameState.Grid[x, y].Team == Team.Blue && !GameState.Grid[x, y].IsMaster) Visuals.blueStudents.Add(((float)x, (float)y));
                    if (GameState.Grid[x, y].Team == Team.Red && !GameState.Grid[x, y].IsMaster) Visuals.redStudents.Add(((float)x, (float)y));
                    if (GameState.Grid[x, y].Team == Team.Blue && GameState.Grid[x, y].IsMaster) Visuals.blueMaster = ((float)x, (float)y);
                    if (GameState.Grid[x, y].Team == Team.Red && GameState.Grid[x, y].IsMaster) Visuals.redMaster = ((float)x, (float)y);
                }
            Visuals.BlueCards = GameState.BlueCards;
            Visuals.RedCards = GameState.RedCards;
            Visuals.NeutralCard = GameState.NeutralCard;
            Visuals.DrawState(g);
        }

        
    }
}
