﻿using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// TODO Implement Menu
// TODO Start game button
// TODO Add Surrenders
// TODO Add New Game Button

namespace Onitama
{
    internal class OniBoard : GraphicsControl
    {
        public GameState GameState { get; set; } = new GameState();
        public Color MatColor { get; set; } = Color.FromArgb(245, 191, 90);
        public override Color BackColor { get; set; } = Color.DarkGray;
        public Color GridColor { get; set; } = Color.Green;
        public PointF GridOrigin { get; set; } = new PointF();
        public override Font Font { get; set; } = new Font("Arial", 0.02f, GraphicsUnit.Pixel);
        public StringFormat Centered{ get; } = new() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
        public GameVisuals Visuals { get; set; }
        public OniBoard()
        {
            ViewSize = new SizeF(10, 7);
            Visuals = new(GridOrigin)
            {
                CurrentTeam = GameState.CurrentTeam,
                BlueStudents = GameState.BlueStudents,
                RedStudents = GameState.RedStudents,
                RedMaster = GameState.RedMaster,
                BlueMaster = GameState.BlueMaster,
            };
        }

        public void Reset()
        {
            GameState = new GameState();
            Visuals = new(GridOrigin)
            {
                CurrentTeam = GameState.CurrentTeam,
                BlueStudents = GameState.BlueStudents,
                RedStudents = GameState.RedStudents,
                RedMaster = GameState.RedMaster,
                BlueMaster = GameState.BlueMaster,
            };

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
            if (((point.X - 7.7f) * (9.5f - point.X)) > 0 && (((point.Y - 4.36f) * (6.61f - point.Y)) > 0)) return (BoardItem.RedCard2, new Point(0, 0));
            if (((point.X - 4.41f) * (5.91f - point.X)) > 0 && (((point.Y - 3.88f) * (4.38f - point.Y)) > 0) && GameState.IsGameOver) return (BoardItem.TryAgain, new Point(0, 0));

            for (int i = 0; i < 5; i++)
            {
                if ((point.X > GridOrigin.X + i + 0.1f) && (point.X < GridOrigin.X + i + 0.9f))
                {
                    squareX = i;
                    break;
                }
            }
            for (int i = 0; i < 5; i++)
            {
                if ((point.Y > GridOrigin.Y + i + 0.1f) && (point.Y < GridOrigin.Y + i + 0.9f))
                {
                    squareY = i;
                    break;
                }
            }
            if (squareX != 7 && squareY != 7)
            {
                return (BoardItem.Square, new Point(squareX, squareY));
            }
            return null;
        }

        protected override void ViewDraw(Graphics g)
        {
            Visuals.GridOrigin = GridOrigin;
            Visuals.CurrentTeam = GameState.CurrentTeam;
            GameState.GridOrigin = GridOrigin;
            using var pen = new Pen(Color.Black, 0.05f);
            RectangleF board = new(0.05f, 0.05f, 9.9f, 6.9f);
            RectangleF mat = new(0.15f, 1.65f, 9.7f, 5.2f);
            RectangleF grid = new(2.5f, 1.75f, 5f, 5f);
            RectangleF menuButton = new(0.1f, 0.1f, 0.8f, 0.275f);
            GridOrigin = new PointF(grid.X, grid.Y);


            g.DrawRectangle(pen, board.X, board.Y, board.Width, board.Height);
            g.FillRectangle(new SolidBrush(Color.DarkGreen), board);
            g.FillRoundedRectangleF(new SolidBrush(MatColor), mat, .1f);
            g.FillRectangle(new SolidBrush(Color.Olive), grid);
            g.DrawRectangle(pen, grid.X, grid.Y, grid.Width, grid.Height);
            g.DrawRoundedRectangleF(pen, menuButton, 0.05f);
            g.FillRoundedRectangleF(Brushes.Moccasin, menuButton, 0.05f);
            g.DrawString("MENU", new Font("Arial", 0.2f, GraphicsUnit.Pixel), Brushes.Black, menuButton, Centered);
            for (int y = 0; y < 5; y++)
                for (int x = 0; x < 5; x++)
                {
                    g.DrawRectangle(new Pen(Color.DarkOliveGreen, 0.03f), GridToView(x, y).X + 0.1f, GridToView(x, y).Y + 0.1f, 0.8f, 0.8f);
                }

            RectangleF templeBlue = new(GridToView(0, 2).X + 0.075f, GridToView(0, 2).Y + 0.075f, .85f, .85f);
            RectangleF templeRed = new(GridToView(4, 2).X + 0.075f, GridToView(0, 2).Y + 0.075f, .85f, .85f);
            RectangleF templeBlue2 = new(GridToView(0, 2).X + 0.07f, GridToView(0, 2).Y + 0.07f, .95f, .95f);
            RectangleF templeRed2 = new(GridToView(4, 2).X + 0.07f, GridToView(0, 2).Y + 0.07f, .95f, .95f);
            g.DrawRoundedRectangleF(new Pen(Color.DarkBlue, 0.04f), templeBlue, 0.1f);
            g.DrawRoundedRectangleF(new Pen(Color.DarkBlue, 0.04f), templeBlue, 0.1f);
            g.DrawRoundedRectangleF(new Pen(Color.DarkRed, 0.04f), templeRed, 0.1f);
            g.DrawRoundedRectangleF(new Pen(Color.DarkRed, 0.04f), templeRed, 0.1f);
        }

        protected override void VisualsDraw(Graphics g)
        {
            Visuals.BlueCards = GameState.BlueCards;
            Visuals.RedCards = GameState.RedCards;
            Visuals.NeutralCard = GameState.NeutralCard;
            Visuals.DrawState(g);
            if (Visuals.TutorialStep > 0 && Visuals.TutorialStep < 4) Visuals.DrawTutorial(g);
        }

        protected override void ViewMouseMove(float x, float y, MouseButtons buttons)
        {
            (BoardItem, Point)? location = FindItem(new PointF(x, y));
            if (buttons == MouseButtons.Left && location is not null)
            {
                Visuals.MouseOverItem = location.Value.Item1;
                Visuals.MouseOverXY = location.Value.Item2;
            }
        }

        protected override void ViewMouseDown(float x, float y, MouseButtons buttons)
        {
            (BoardItem, Point)? location = FindItem(new PointF(x, y));
            if (buttons == MouseButtons.Left && location != null)
            {
                GameState.MouseDownLocation = location;
                Invalidate();
            }
        }

        protected override void ViewMouseUp(float x, float y, MouseButtons buttons)
        {
            (BoardItem, Point)? location = FindItem(new PointF(x, y));

            if (Visuals.TutorialStep > 0 && Visuals.TutorialStep < 4)
            {
                Visuals.TutorialStep++;
            }
            else if (location != null && buttons == MouseButtons.Left && location == GameState.MouseDownLocation)
            {
                if (GameState.IsGameOver && location.Value.Item1 == BoardItem.TryAgain)
                {
                    Reset();
                    Visuals.TutorialStep = 0;
                    return;
                }
                Visuals.IsGameOver = GameState.IsGameOver;
                if (GameState.IsGameOver && location.Value.Item1 != BoardItem.TryAgain) return;
                GameState.MouseUp(location!.Value.Item1, location.Value.Item2);
                Visuals.CurrentTeam = GameState.CurrentTeam;
                Visuals.ActiveCard = GameState.activeCardLocation;
                Visuals.ActiveStudent = GameState.ActiveSquare;
                Visuals.BlueStudents = GameState.BlueStudents;
                Visuals.RedStudents = GameState.RedStudents;
                Visuals.RedMaster = GameState.RedMaster;
                Visuals.BlueMaster = GameState.BlueMaster;
                Visuals.PossibleMoves = GameState.PossibleMoves;
                Visuals.IsGameOver = GameState.IsGameOver;
            }
            GameState.MouseDownLocation = null;
            Invalidate();
        }
    }
}
