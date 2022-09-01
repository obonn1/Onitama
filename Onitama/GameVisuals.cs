namespace Onitama
{
    public class GameVisuals : DrawTools
    {
        public GameVisuals(PointF gridOrigin) => GridOrigin = gridOrigin;

        public List<Point> BlueStudents { get; set; } = new();

        public List<Point> RedStudents { get; set; } = new();

        public List<Point> PossibleMoves { get; set; } = new();

        public Point BlueMaster { get; set; }

        public Point RedMaster { get; set; }

        public PointF GridOrigin { get; set; }

        public Point MouseOverXY { get; set; }

        public Point? ActiveStudent { get; set; }

        public BoardItem? ActiveCard { get; set; }

        public BoardItem MouseOverItem { get; set; }

        public Card[]? BlueCards { get; set; }

        public Card[]? RedCards { get; set; }

        public Font Font { get; set; } = new("Arial", 0.275f, GraphicsUnit.Pixel);

        public Card? NeutralCard { get; set; }

        public Team CurrentTeam { get; set; }

        public bool IsGameOver { get; set; }

        public int TutorialStep { get; set; } = 1;

        public float CornerRadius { get; } = 0.1f;

        public Brush TeamBrush { get; private set; } = DarkBlueBrush;

        public void DrawState(Graphics g)
        {
            TeamBrush = CurrentTeam == Team.Blue ? DarkBlueBrush : DarkRedBrush;

            foreach (Point piece in BlueStudents)
            {
                g.DrawImage(Resources.blueStudentImg, new RectangleF(piece.X + GridOrigin.X + 0.1f, piece.Y + GridOrigin.Y + 0.1f, 0.8f, 0.8f));
            }

            foreach (Point piece in RedStudents)
            {
                g.DrawImage(Resources.redStudentImg, new RectangleF(piece.X + GridOrigin.X + 0.1f, piece.Y + GridOrigin.Y + 0.1f, 0.8f, 0.8f));
            }

            g.DrawImage(Resources.blueMasterImg, new RectangleF(BlueMaster.X + GridOrigin.X + 0.1f - 0.25f, BlueMaster.Y + GridOrigin.Y, 1.3f, 1.3f));
            g.DrawImage(Resources.redMasterImg, new RectangleF(RedMaster.X + GridOrigin.X + 0.1f - 0.25f, RedMaster.Y + GridOrigin.Y, 1.3f, 1.3f));
            if (ActiveStudent != null)
            {
                g.DrawRoundedRectangleF(new Pen(Color.DarkOrange, 0.1f), new(ActiveStudent.Value.X + GridOrigin.X + 0.075f, ActiveStudent.Value.Y + GridOrigin.Y + 0.075f, 0.85f, 0.85f), 0.4f);

                if (ActiveCard != null)
                {
                    foreach (Point square in PossibleMoves)
                    {
                        g.DrawRoundedRectangleF(new Pen(Color.White, 0.05f), new(square.X + GridOrigin.X + 0.1f, square.Y + GridOrigin.Y + 0.1f, 0.8f, 0.8f), 0.4f);
                    }
                }
            }

            Dictionary<string, RectangleF> cardRects = new()
            {
                { BlueCards![0].Name, new(.425f, 1.88f, 1.8f, 2.25f) },
                { BlueCards[1].Name, new(.425f, 4.36f, 1.8f, 2.25f) },
                { RedCards![0].Name, new(7.7f, 1.88f, 1.8f, 2.25f) },
                { RedCards![1].Name, new(7.7f, 4.36f, 1.8f, 2.25f) },
            };

            foreach (KeyValuePair<string, RectangleF> card in cardRects)
            {
                g.FillRoundedRectangleF(MoccasinBrush, card.Value, CornerRadius);
                g.DrawString(card.Key, Font, BlackBrush, card.Value, StringFormats.CenterBottom);
            }

            RectangleF neutralCardBG = new(3f, 0.15f, 4f, 1.4f);
            RectangleF redTurnBG = new(7.45f, 0.95f, 1.8f, .35f);
            RectangleF blueTurnBG = new(.25f, 0.95f, 1.8f, .35f);
            RectangleF? highlightRect = ActiveCard switch
            {
                BoardItem.BlueCard1 => cardRects[BlueCards[0].Name],
                BoardItem.BlueCard2 => cardRects[BlueCards[1].Name],
                BoardItem.RedCard1 => cardRects[RedCards[0].Name],
                BoardItem.RedCard2 => cardRects[RedCards[1].Name],
                _ => null,
            };

            g.FillRoundedRectangleF(MoccasinBrush, neutralCardBG, .1f);
            g.DrawString(NeutralCard!.Name, Font, BlackBrush, neutralCardBG, StringFormats.Center);

            BlueCards![0].CardGrid(g, new PointF(0.475f, 1.93f), 1.6f);
            BlueCards[1].CardGrid(g, new PointF(0.475f, 4.41f), 1.6f);
            RedCards![0].CardGrid(g, new PointF(7.75f, 1.93f), 1.6f);
            RedCards[1].CardGrid(g, new PointF(7.75f, 4.41f), 1.6f);
            NeutralCard!.CardGrid(g, new PointF(3.025f, 0.175f), 1.25f);
            Card.Invert(NeutralCard).CardGrid(g, new PointF(5.625f, 0.175f), 1.25f);

            g.FillRoundedRectangleF(TeamBrush, CurrentTeam == Team.Blue ? blueTurnBG : redTurnBG, 0.02f);
            g.DrawRoundedRectangleF(BlackPen, CurrentTeam == Team.Blue ? blueTurnBG : redTurnBG, 0.02f);
            g.DrawString("YOUR TURN", Font, MoccasinBrush, CurrentTeam == Team.Blue ? blueTurnBG : redTurnBG, StringFormats.Center);

            if (ActiveCard.ToString()!.Contains("Blue"))
            {
                g.DrawRoundedRectangleF(new Pen(Color.Blue, 0.1f), (RectangleF)highlightRect!, CornerRadius);
            }

            if (ActiveCard.ToString()!.Contains("Red"))
            {
                g.DrawRoundedRectangleF(new Pen(Color.Red, 0.1f), (RectangleF)highlightRect!, CornerRadius);
            }

            if (IsGameOver)
            {
                RectangleF gameOverBanner = new(3, 2.5f, 4, 2);
                RectangleF playAgain = new(4.25f, 3.88f, 1.5f, 0.5f);
                NeutralCard = null;
                g.FillRoundedRectangleF(MoccasinBrush, gameOverBanner, 0.5f);
                g.FillRoundedRectangleF(WhiteBrush, playAgain, 0.1f);
                g.DrawRoundedRectangleF(new Pen(Color.Black, 0.05f), gameOverBanner, 0.5f);
                g.DrawRoundedRectangleF(new Pen(Color.Black, 0.02f), playAgain, 0.1f);
                g.DrawString($"{CurrentTeam.ToString().ToUpper()} WINS!!!", new Font("Arial", 0.5f, FontStyle.Bold, GraphicsUnit.Pixel), BlackBrush, gameOverBanner, StringFormats.Center);
                g.DrawString("Play Again", new Font("Arial", 0.225f, FontStyle.Bold, GraphicsUnit.Pixel), BlackBrush, playAgain, StringFormats.Center);
            }
        }

        public void DrawTutorial(Graphics g)
        {
            RectangleF cornerBox = new(0.15f, 0.15f, 2.7f, 1.4f);
            g.FillRoundedRectangleF(MoccasinBrush, cornerBox, 0.1f);
            g.DrawRoundedRectangleF(BlackPen, cornerBox, 0.1f);
            g.DrawString($"HOW TO PLAY {TutorialStep}/3", TitleFont, BlackBrush, 0.2f, 0.2f);

            if (TutorialStep == 1)
            {
                g.DrawString("Each team has five pieces, four Students \nand one Master \n \nAll pieces can defeat an \nopposing piece by moving into their spot", TutorialFont, BlackBrush, 0.25f, 0.6f);
            }
            else if (TutorialStep == 2)
            {
                g.DrawString("The cards on your side of the board show \npossible piece movement. \n\nChoose a card, then a piece to move. \n\nAfter moving, the card used will switch with\nthe neutral card on top.", TutorialFont, BlackBrush, 0.25f, 0.5f);
            }
            else if (TutorialStep == 3)
            {
                g.DrawString("To win: \nDefeat the opposing Master \n\nor \n\nMove your Master into the opposing Temple", TutorialFont, BlackBrush, 0.25f, 0.5f);
                g.DrawRectangle(HighlightPen, GridOrigin.X + 0.07f, GridOrigin.Y + 2.07f, .85f, .85f);
                g.DrawRectangle(HighlightPen, GridOrigin.X + 4.07f, GridOrigin.Y + 2.07f, .85f, .85f);
            }
        }

        public void DrawButton(Graphics g, MenuButton button)
        {
            g.DrawRoundedRectangleF(BlackPen, button.Bounds, button.CornerRadius);
            g.FillRoundedRectangleF(GreenBrush , button.Bounds, button.CornerRadius);
            g.DrawString(button.Text, button.Font, BlackBrush, button.Bounds, StringFormats.Center);
        }

        public void DrawMenu(Graphics g)
        {
            g.FillRoundedRectangleF(MoccasinBrush, new(3.5f, 1f, 3f, 5f), CornerRadius);
            g.DrawRoundedRectangleF(BlackPen, new(3.5f, 1f, 3f, 5f), CornerRadius);
            g.DrawString("MENU", TitleFont, BlackBrush, new RectangleF(3.5f, 1f, 3f, 5f), StringFormats.CenterTop);

            MenuButton[] menuButtons = new MenuButton[] {
                new MenuButton("New Game", new(4f, 1.8f, 2f, 0.5f), Font),
                new MenuButton("Surrender Blue", new(4f, 2.6f, 2f, 0.5f), Font),
                new MenuButton("Surrender Red", new(4f, 3.4f, 2f, 0.5f), Font),
                new MenuButton("Tutorial", new(4f, 4.2f, 2f, 0.5f), Font),
                new MenuButton("Close Game", new(4f, 5f, 2f, 0.5f), Font),
                new MenuButton("X", new(6.2f, 1.1f, 0.2f, 0.2f), TutorialFont),
            };

            foreach (var button in menuButtons)
            {
                    DrawButton(g, button);
            }
        }
    }
}
