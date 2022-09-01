namespace Onitama
{
    internal class OniBoard : GraphicsControl
    {
        public GameState GameState { get; set; } = new GameState();

        public Color GridColor { get; set; } = Color.Green;

        public override Font Font { get; set; } = new Font("Arial", 0.02f, GraphicsUnit.Pixel);

        public GameVisuals Visuals { get; set; }

        public OniBoard()
        {
            ViewSize = new SizeF(10, 7);
            Visuals = new()
            {
                CurrentTeam = GameState.CurrentTeam,
                BlueStudents = GameState.BlueStudents,
                RedStudents = GameState.RedStudents,
                RedMaster = GameState.RedMaster,
                BlueMaster = GameState.BlueMaster,
            };
            BackColor = Color.DarkGray;
        }

        public void Reset()
        {
            GameState = new GameState();
            Visuals = new()
            {
                CurrentTeam = GameState.CurrentTeam,
                BlueStudents = GameState.BlueStudents,
                RedStudents = GameState.RedStudents,
                RedMaster = GameState.RedMaster,
                BlueMaster = GameState.BlueMaster,
            };
        }

        public (BoardItem, Point)? FindItem(PointF point)
        {
            int squareX = 7;
            int squareY = 7;
            if (GameState.IsMenuOpen)
            {
                if (((point.X - 4f) * (6f - point.X)) > 0 && (((point.Y - 1.8f) * (2.3f - point.Y)) > 0))
                {
                    return (BoardItem.NewGame, new Point(0, 0));
                }

                if (((point.X - 4f) * (6f - point.X)) > 0 && (((point.Y - 2.6f) * (3.1f - point.Y)) > 0))
                {
                    return (BoardItem.BlueSurrender, new Point(0, 0));
                }

                if (((point.X - 4f) * (6f - point.X)) > 0 && (((point.Y - 3.4f) * (3.9f - point.Y)) > 0))
                {
                    return (BoardItem.RedSurrender, new Point(0, 0));
                }

                if (((point.X - 4f) * (6f - point.X)) > 0 && (((point.Y - 4.2f) * (4.7f - point.Y)) > 0))
                {
                    return (BoardItem.Tutorial, new Point(0, 0));
                }

                if (((point.X - 4f) * (6f - point.X)) > 0 && (((point.Y - 5f) * (5.5f - point.Y)) > 0))
                {
                    return (BoardItem.CloseGame, new Point(0, 0));
                }

                if (((point.X - 6.2f) * (6.4f - point.X)) > 0 && (((point.Y - 1.1f) * (1.3f - point.Y)) > 0))
                {
                    return (BoardItem.CloseMenu, new Point(0, 0));
                }

                if (((point.X - 3.5f) * (6.5f - point.X)) < 0 && (((point.Y - 1f) * (6f - point.Y)) < 0))
                {
                    return (BoardItem.OffMenu, new Point(0, 0));
                }
            }

            if (((point.X - 0.1f) * (0.9f - point.X)) > 0 && (((point.Y - 0.1f) * (0.375f - point.Y)) > 0))
            {
                return (BoardItem.Menu, new Point(0, 0));
            }

            if (((point.X - 0.425f) * (2.225f - point.X)) > 0 && (((point.Y - 1.88f) * (4.13f - point.Y)) > 0))
            {
                return (BoardItem.BlueCard1, new Point(0, 0));
            }

            if (((point.X - 0.425f) * (2.225f - point.X)) > 0 && (((point.Y - 4.36f) * (6.61f - point.Y)) > 0))
            {
                return (BoardItem.BlueCard2, new Point(0, 0));
            }

            if (((point.X - 7.7f) * (9.5f - point.X)) > 0 && (((point.Y - 1.88f) * (4.13f - point.Y)) > 0))
            {
                return (BoardItem.RedCard1, new Point(0, 0));
            }

            if (((point.X - 7.7f) * (9.5f - point.X)) > 0 && (((point.Y - 4.36f) * (6.61f - point.Y)) > 0))
            {
                return (BoardItem.RedCard2, new Point(0, 0));
            }

            if (((point.X - 4.41f) * (5.91f - point.X)) > 0 && (((point.Y - 3.88f) * (4.38f - point.Y)) > 0) && GameState.IsGameOver)
            {
                return (BoardItem.TryAgain, new Point(0, 0));
            }

            for (int i = 0; i < 5; i++)
            {
                if ((point.X > Visuals.GridOrigin.X + i + 0.1f) && (point.X < Visuals.GridOrigin.X + i + 0.9f))
                {
                    squareX = i;
                    break;
                }
            }

            for (int i = 0; i < 5; i++)
            {
                if ((point.Y > Visuals.GridOrigin.Y + i + 0.1f) && (point.Y < Visuals.GridOrigin.Y + i + 0.9f))
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

        protected override void VisualsDraw(Graphics g)
        {
            Visuals.BlueCards = GameState.BlueCards;
            Visuals.RedCards = GameState.RedCards;
            Visuals.NeutralCard = GameState.NeutralCard;
            Visuals.IsMenuOpen = GameState.IsMenuOpen;
            Visuals.CurrentTeam = GameState.CurrentTeam;
            Visuals.DrawGame(g);
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

            if (GameState.TutorialStep > 0 && GameState.TutorialStep < 4)
            {
                GameState.TutorialStep++;
                Visuals.TutorialStep = GameState.TutorialStep;
            }
            else if (location != null && buttons == MouseButtons.Left && location == GameState.MouseDownLocation)
            {
                if ((GameState.IsGameOver && location.Value.Item1 == BoardItem.TryAgain)
                    || (GameState.IsMenuOpen && location.Value.Item1 == BoardItem.NewGame))
                {
                    Reset();
                    GameState.TutorialStep = 0;
                    Visuals.TutorialStep = 0;
                    Invalidate();
                    return;
                }

                Visuals.IsGameOver = GameState.IsGameOver;
                if (!GameState.IsGameOver)
                {
                    GameState.MouseUp(location!.Value.Item1, location.Value.Item2);
                }

                Visuals.CurrentTeam = GameState.CurrentTeam;
                Visuals.ActiveCard = GameState.ActiveCard;
                Visuals.ActiveStudent = GameState.ActiveSquare;
                Visuals.BlueStudents = GameState.BlueStudents;
                Visuals.RedStudents = GameState.RedStudents;
                Visuals.RedMaster = GameState.RedMaster;
                Visuals.BlueMaster = GameState.BlueMaster;
                Visuals.PossibleMoves = GameState.PossibleMoves;
                Visuals.IsGameOver = GameState.IsGameOver;
                Visuals.TutorialStep = GameState.TutorialStep;
            }

            GameState.MouseDownLocation = null;

            Invalidate();
        }
    }

    public enum Team
    {
        Red,
        Blue,
    }

    public enum BoardItem
    {
        BlueCard1,
        BlueCard2,
        RedCard1,
        RedCard2,
        Square,
        TryAgain,
        Menu,
        NewGame,
        BlueSurrender,
        RedSurrender,
        CloseMenu,
        Tutorial,
        CloseGame,
        OffMenu,
    }
}
