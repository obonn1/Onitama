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
        public (BoardItem, Point)? FindItem(PointF point)
        {
            int squareX = 7;
            int squareY = 7;
            if (((point.X - 0.425f) * (2.225f - point.X)) > 0 && (((point.Y - 1.88f) * (4.13f - point.Y)) > 0)) return (BoardItem.BlueCard1, new Point(0, 0));
            if (((point.X - 0.425f) * (2.225f - point.X)) > 0 && (((point.Y - 4.36f) * (6.61f - point.Y)) > 0)) return (BoardItem.BlueCard2, new Point(0, 0));
            if (((point.X - 7.7f) * (9.5f - point.X)) > 0 && (((point.Y - 1.88f) * (4.13f - point.Y)) > 0)) return (BoardItem.RedCard1, new Point(0, 0));
            if (((point.X - 7.7f) * (9.5f - point.X)) > 0 && (((point.Y - 4.36f) * (6.61f - point.Y)) > 0)) return (BoardItem.RedCard1, new Point(0, 0));

            for (int i = 0; i < 4; i++)
            {
                if ((point.X > GridOrigin.X + i + 0.1f) && (point.X < GridOrigin.X + i + 0.9f)) squareX = i;
            }
            for (int i = 0; i < 4; i++)
            {
                if ((point.Y > GridOrigin.Y + i + 0.1f) && (point.Y < GridOrigin.Y + i + 0.9f)) squareX = i;
            }
            if ((point.X > GridOrigin.X / 5 + 0.1f) && (point.X < GridOrigin.X / 5 + 0.9f)
                &&
               (point.Y > GridOrigin.Y / 5 + 0.1f) && (point.Y < GridOrigin.Y / 5 + 0.9f))
            {
                return (BoardItem.Square, new Point(squareX, squareY));
            }
            return null;
        }

        protected override void ViewDraw(Graphics g)
        {
            Visuals.GridOrigin = GridOrigin;
            GameState.GridOrigin = GridOrigin;
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
                    if (GameState.Grid[x, y].Team == Team.Blue && !GameState.Grid[x, y].IsMaster) Visuals.BlueStudents.Add((x, y));
                    if (GameState.Grid[x, y].Team == Team.Red && !GameState.Grid[x, y].IsMaster) Visuals.RedStudents.Add((x, y));
                    if (GameState.Grid[x, y].Team == Team.Blue && GameState.Grid[x, y].IsMaster) Visuals.blueMaster = (x, y);
                    if (GameState.Grid[x, y].Team == Team.Red && GameState.Grid[x, y].IsMaster) Visuals.redMaster = (x, y);
                }
            Visuals.BlueCards = GameState.BlueCards;
            Visuals.RedCards = GameState.RedCards;
            Visuals.NeutralCard = GameState.NeutralCard;
            Visuals.DrawState(g);
        }

        protected override void ViewMouseMove(float x, float y, MouseButtons buttons)
        {
            if (buttons == MouseButtons.Left)
            {
                GameState.mouseLocation = new PointF(x, y);
                Invalidate();
            }
        }

        protected override void ViewMouseDown(float x, float y, MouseButtons buttons)
        {
            (BoardItem, Point)? location = FindItem(new PointF(x, y));
            if (buttons == MouseButtons.Left && location != null)
            {
                if (buttons == MouseButtons.Left)
                {
                    GameState.mouseDownLocation = location;
                    Invalidate();
                }

            }
        }

        protected override void ViewMouseUp(float x, float y, MouseButtons buttons)
        {
            (BoardItem, Point)? location = FindItem(new PointF(x, y));
            if (location != null && buttons == MouseButtons.Left && location == GameState.mouseDownLocation)
            {
                GameState.MouseUp(location.Value.Item1, location.Value.Item2);
                Visuals.MouseUp(location.Value.Item1, location.Value.Item2);
                Invalidate();
            }
            GameState.mouseDownLocation = null;
        }
    }
}
