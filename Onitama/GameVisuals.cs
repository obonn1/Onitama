// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Onitama
{
    public class GameVisuals : DrawTools
    {
        public List<Point> BlueStudents { get; set; } = new();

        public List<Point> RedStudents { get; set; } = new();

        public Point BlueMaster { get; set; }

        public Point RedMaster { get; set; }

        public List<Point> PossibleMoves { get; set; } = new();

        public PointF MouseOver { get; set; }

        public Point? ActiveStudent { get; set; }

        public BoardItem? ActiveCard { get; set; }

        public Card[]? BlueCards { get; set; }

        public Font Font { get; set; } = new("Arial", 0.275f, GraphicsUnit.Pixel);

        public Card[]? RedCards { get; set; }

        public Card? NeutralCard { get; set; }

        public PointF GridOrigin { get; set; }

        public Team CurrentTeam { get; internal set; }

        public bool IsGameOver { get; set; }

        public BoardItem MouseOverItem { get; set; }

        public Point MouseOverXY { get; set; }

        public int TutorialStep { get; set; } = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameVisuals"/> class.
        /// </summary>
        /// <param name="gridOrigin"></param>
        public GameVisuals(PointF gridOrigin)
        {
            this.GridOrigin = gridOrigin;
        }

        public void DrawState(Graphics g)
        {
            Image blueStudentImage = Image.FromFile("bluestudent.png");
            Image redStudentImage = Image.FromFile("redstudent.png");
            Image blueMasterImage = Image.FromFile("bluemaster.png");
            Image redMasterImage = Image.FromFile("redmaster.png");
            foreach (var piece in this.BlueStudents)
            {
                RectangleF pawn = new(piece.X + this.GridOrigin.X + 0.1f, piece.Y + this.GridOrigin.Y + 0.1f, 0.8f, 0.8f);
                g.DrawImage(blueStudentImage, pawn);
            }

            foreach (var piece in this.RedStudents)
            {
                RectangleF pawn = new(piece.X + this.GridOrigin.X + 0.1f, piece.Y + this.GridOrigin.Y + 0.1f, 0.8f, 0.8f);
                g.DrawImage(redStudentImage, pawn);
            }

            RectangleF blueMasterPiece = new(this.BlueMaster.X + this.GridOrigin.X + 0.1f - 0.25f, this.BlueMaster.Y + this.GridOrigin.Y, 1.3f, 1.3f);

            RectangleF redMasterPiece = new(this.RedMaster.X + this.GridOrigin.X + 0.1f - 0.25f, this.RedMaster.Y + this.GridOrigin.Y, 1.3f, 1.3f);

            g.DrawImage(blueMasterImage, blueMasterPiece);
            g.DrawImage(redMasterImage, redMasterPiece);
            if (this.ActiveStudent != null)
            {
                RectangleF pieceActiveHighlight = new(this.ActiveStudent.Value.X + this.GridOrigin.X + 0.075f, this.ActiveStudent.Value.Y + this.GridOrigin.Y + 0.075f, 0.85f, 0.85f);
                g.DrawRoundedRectangleF(new Pen(Color.DarkOrange, 0.1f), pieceActiveHighlight, 0.4f);

                if (this.ActiveCard != null)
                {

                    foreach (var square in this.PossibleMoves)
                    {
                        RectangleF possibleMoveHighlight = new(square.X + this.GridOrigin.X + 0.1f, square.Y + this.GridOrigin.Y + 0.1f, 0.8f, 0.8f);
                        g.DrawRoundedRectangleF(new Pen(Color.White, 0.05f), possibleMoveHighlight, 0.4f);
                    }
                }
            }

            RectangleF blueCard1BG = new(.425f, 1.88f, 1.8f, 2.25f);
            RectangleF blueCard2BG = new(.425f, 4.36f, 1.8f, 2.25f);
            RectangleF redCard1BG = new(7.7f, 1.88f, 1.8f, 2.25f);
            RectangleF redCard2BG = new(7.7f, 4.36f, 1.8f, 2.25f);
            RectangleF neutralCardBG = new(3f, 0.15f, 4f, 1.4f);
            RectangleF redTurnBG = new(7.45f, 0.95f, 1.8f, .35f);
            RectangleF blueTurnBG = new(.25f, 0.95f, 1.8f, .35f);
            RectangleF? highlightRect = this.ActiveCard switch
            {
                BoardItem.BlueCard1 => blueCard1BG,
                BoardItem.BlueCard2 => blueCard2BG,
                BoardItem.RedCard1 => redCard1BG,
                BoardItem.RedCard2 => redCard2BG,
                _ => null,
            };
            g.FillRoundedRectangleF(MoccasinBrush, blueCard1BG, .1f);
            g.FillRoundedRectangleF(MoccasinBrush, blueCard2BG, .1f);
            g.FillRoundedRectangleF(MoccasinBrush, redCard1BG, .1f);
            g.FillRoundedRectangleF(MoccasinBrush, redCard2BG, .1f);
            g.FillRoundedRectangleF(MoccasinBrush, neutralCardBG, .1f);
            this.BlueCards![0].CardGrid(g, new PointF(0.475f, 1.93f), 1.6f);
            g.DrawString(this.BlueCards[0].Name, this.Font, BlackBrush, blueCard1BG, this.CenteredFar);
            this.BlueCards[1].CardGrid(g, new PointF(0.475f, 4.41f), 1.6f);
            g.DrawString(this.BlueCards[1].Name, this.Font, BlackBrush, blueCard2BG, this.CenteredFar);
            this.RedCards![0].CardGrid(g, new PointF(7.75f, 1.93f), 1.6f);
            g.DrawString(this.RedCards[0].Name, this.Font, BlackBrush, redCard1BG, this.CenteredFar);
            this.RedCards[1].CardGrid(g, new PointF(7.75f, 4.41f), 1.6f);
            g.DrawString(this.RedCards[1].Name, this.Font, BlackBrush, redCard2BG, this.CenteredFar);
            this.NeutralCard!.CardGrid(g, new PointF(3.025f, 0.175f), 1.25f);
            Card.Invert(this.NeutralCard).CardGrid(g, new PointF(5.625f, 0.175f), 1.25f);
            g.DrawString(this.NeutralCard.Name, this.Font, BlackBrush, neutralCardBG, this.Centered);
            if (this.ActiveCard.ToString()!.Contains("Blue"))
            {
                g.DrawRoundedRectangleF(new Pen(Color.Blue, 0.1f), (RectangleF)highlightRect!, 0.1f);
            }

            if (this.ActiveCard.ToString()!.Contains("Red"))
            {
                g.DrawRoundedRectangleF(new Pen(Color.Red, 0.1f), (RectangleF)highlightRect!, 0.1f);
            }

            if (this.CurrentTeam == Team.Blue)
            {
                g.FillRoundedRectangleF(DarkBlueBrush, blueTurnBG, 0.02f);
                g.DrawRoundedRectangleF(BlackPen, blueTurnBG, 0.02f);
                g.DrawString("YOUR TURN", this.Font, MoccasinBrush, blueTurnBG, this.Centered);
            }
            else
            {
                g.FillRoundedRectangleF(DarkRedBrush, redTurnBG, 0.02f);
                g.DrawRoundedRectangleF(BlackPen, redTurnBG, 0.02f);
                g.DrawString("YOUR TURN", this.Font, MoccasinBrush, redTurnBG, this.Centered);
            }

            if (this.IsGameOver)
            {
                RectangleF gameOverBanner = new(3, 2.5f, 4, 2);
                RectangleF playAgain = new(4.25f, 3.88f, 1.5f, 0.5f);
                this.NeutralCard = null;
                g.FillRoundedRectangleF(MoccasinBrush, gameOverBanner, 0.5f);
                g.FillRoundedRectangleF(WhiteBrush, playAgain, 0.1f);
                g.DrawRoundedRectangleF(new Pen(Color.Black, 0.05f), gameOverBanner, 0.5f);
                g.DrawRoundedRectangleF(new Pen(Color.Black, 0.02f), playAgain, 0.1f);
                g.DrawString($"{this.CurrentTeam.ToString().ToUpper()} WINS!!!", new Font("Arial", 0.5f, FontStyle.Bold, GraphicsUnit.Pixel), BlackBrush, gameOverBanner, this.Centered);
                g.DrawString("Play Again", new Font("Arial", 0.225f, FontStyle.Bold, GraphicsUnit.Pixel), BlackBrush, playAgain, this.Centered);
            }
        }

        public void DrawTutorial(Graphics g)
        {
            RectangleF cornerBox = new(0.15f, 0.15f, 2.7f, 1.4f);
            g.FillRoundedRectangleF(MoccasinBrush, cornerBox, 0.1f);
            g.DrawRoundedRectangleF(BlackPen, cornerBox, 0.1f);

            if (this.TutorialStep == 1)
            {
                g.DrawString("HOW TO PLAY 1/3", TitleFont, BlackBrush, 0.2f, 0.2f);
                g.DrawString("Each team has five pieces, four Students \nand one Master \n \nAll pieces can defeat an \nopposing piece by moving into their spot", TutorialFont, BlackBrush, 0.25f, 0.6f);
            }
            else if (this.TutorialStep == 2)
            {
                g.DrawString("HOW TO PLAY 2/3", TitleFont, BlackBrush, 0.2f, 0.2f);
                g.DrawString("The cards on your side of the board show \npossible piece movement. \n\nChoose a card, then a piece to move. \n\nAfter moving, the card used will switch with\nthe neutral card on top.", TutorialFont, BlackBrush, 0.25f, 0.5f);
            }
            else if (this.TutorialStep == 3)
            {
                g.DrawString("HOW TO PLAY 3/3", TitleFont, BlackBrush, 0.2f, 0.2f);
                g.DrawString("To win: \nDefeat the opposing Master \n\nor \n\nMove your Master into the opposing Temple", TutorialFont, BlackBrush, 0.25f, 0.5f);
                g.DrawRectangle(HighlightPen, this.GridOrigin.X + 0.07f, this.GridOrigin.Y + 2.07f, .85f, .85f);
                g.DrawRectangle(HighlightPen, this.GridOrigin.X + 4.07f, this.GridOrigin.Y + 2.07f, .85f, .85f);
            }
        }

        public void DrawMenu(Graphics g)
        {

            RectangleF menu = new(3.5f, 1f, 3f, 5f);
            RectangleF newGame = new(4f, 1.8f, 2f, 0.5f);
            RectangleF surrenderBlue = new(4f, 2.6f, 2f, 0.5f);
            RectangleF surrenderRed = new(4f, 3.4f, 2f, 0.5f);
            RectangleF tutorial = new(4f, 4.2f, 2f, 0.5f);
            RectangleF close = new(4f, 5f, 2f, 0.5f);
            RectangleF closeMenu = new(6.2f, 1.1f, 0.2f, 0.2f);
            g.DrawRoundedRectangleF(BlackPen, menu, 0.1f);
            g.FillRoundedRectangleF(MoccasinBrush, menu, 0.1f);
            g.DrawRoundedRectangleF(BlackPen, newGame, 0.1f);
            g.FillRoundedRectangleF(GreenBrush, newGame, 0.1f);
            g.DrawString("New Game", this.Font, BlackBrush, newGame, this.Centered);
            g.DrawRoundedRectangleF(BlackPen, surrenderBlue, 0.1f);
            g.FillRoundedRectangleF(GreenBrush, surrenderBlue, 0.1f);
            g.DrawString("Surrender Blue", this.Font, BlackBrush, surrenderBlue, this.Centered);
            g.DrawRoundedRectangleF(BlackPen, surrenderRed, 0.1f);
            g.FillRoundedRectangleF(GreenBrush, surrenderRed, 0.1f);
            g.DrawString("Surrender Red", this.Font, BlackBrush, surrenderRed, this.Centered);
            g.DrawRoundedRectangleF(BlackPen, tutorial, 0.1f);
            g.FillRoundedRectangleF(GreenBrush, tutorial, 0.1f);
            g.DrawString("Tutorial", this.Font, BlackBrush, tutorial, this.Centered);
            g.DrawRoundedRectangleF(BlackPen, close, 0.1f);
            g.FillRoundedRectangleF(GreenBrush, close, 0.1f);
            g.DrawString("Close Game", this.Font, BlackBrush, close, this.Centered);
            g.DrawString("MENU", TitleFont, BlackBrush, menu, new(this.Centered) { LineAlignment = StringAlignment.Near });
            g.DrawRoundedRectangleF(new Pen(BlackBrush, 0.03f), closeMenu, 0.05f);
            g.FillRoundedRectangleF(GreenBrush, closeMenu, 0.05f);
            g.DrawString("X", TutorialFont, BlackBrush, closeMenu, this.Centered);
        }
    }
}

