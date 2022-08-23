﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;

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

        public GameVisuals(PointF gridOrigin)
        {
            GridOrigin = gridOrigin;
        }


        public void DrawState(Graphics g)
        {
            Image blueStudentImage = Image.FromFile("bluestudent.png");
            Image redStudentImage = Image.FromFile("redstudent.png");
            Image blueMasterImage = Image.FromFile("bluemaster.png");
            Image redMasterImage = Image.FromFile("redmaster.png");
            foreach (var piece in BlueStudents)
            {
                RectangleF pawn = new(piece.X + GridOrigin.X + 0.1f, piece.Y + GridOrigin.Y + 0.1f, 0.8f, 0.8f);
                g.DrawImage(blueStudentImage, pawn);
            }
            foreach (var piece in RedStudents)
            {
                RectangleF pawn = new(piece.X + GridOrigin.X + 0.1f, piece.Y + GridOrigin.Y + 0.1f, 0.8f, 0.8f);
                g.DrawImage(redStudentImage, pawn);
            }

            RectangleF blueMasterPiece = new(BlueMaster.X + GridOrigin.X + 0.1f - 0.25f, BlueMaster.Y + GridOrigin.Y, 1.3f, 1.3f);

            RectangleF redMasterPiece = new(RedMaster.X + GridOrigin.X + 0.1f - 0.25f, RedMaster.Y + GridOrigin.Y, 1.3f, 1.3f);

            g.DrawImage(blueMasterImage, blueMasterPiece);
            g.DrawImage(redMasterImage, redMasterPiece);
            if (ActiveStudent != null)
            {
                RectangleF pieceActiveHighlight = new(ActiveStudent.Value.X + GridOrigin.X + 0.075f, ActiveStudent.Value.Y + GridOrigin.Y + 0.075f, 0.85f, 0.85f);
                g.DrawRoundedRectangleF(new Pen(Color.DarkOrange, 0.1f), pieceActiveHighlight, 0.4f);

                if (ActiveCard != null)
                {

                    foreach (var square in PossibleMoves)
                    {
                        RectangleF possibleMoveHighlight = new(square.X + GridOrigin.X + 0.1f, square.Y + GridOrigin.Y + 0.1f, 0.8f, 0.8f);
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
            RectangleF? highlightRect = ActiveCard switch
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
            BlueCards![0].CardGrid(g, new PointF(0.475f, 1.93f), 1.6f);
            g.DrawString(BlueCards[0].Name, Font, BlackBrush, blueCard1BG, CenteredFar);
            BlueCards[1].CardGrid(g, new PointF(0.475f, 4.41f), 1.6f);
            g.DrawString(BlueCards[1].Name, Font, BlackBrush, blueCard2BG, CenteredFar);
            RedCards![0].CardGrid(g, new PointF(7.75f, 1.93f), 1.6f);
            g.DrawString(RedCards[0].Name, Font, BlackBrush, redCard1BG, CenteredFar);
            RedCards[1].CardGrid(g, new PointF(7.75f, 4.41f), 1.6f);
            g.DrawString(RedCards[1].Name, Font, BlackBrush, redCard2BG, CenteredFar);
            NeutralCard!.CardGrid(g, new PointF(3.025f, 0.175f), 1.25f);
            Card.Invert(NeutralCard).CardGrid(g, new PointF(5.625f, 0.175f), 1.25f);
            g.DrawString(NeutralCard.Name, Font, BlackBrush, neutralCardBG, Centered);
            if (ActiveCard.ToString()!.Contains("Blue"))
                g.DrawRoundedRectangleF(new Pen(Color.Blue, 0.1f), (RectangleF)highlightRect!, 0.1f);
            if (ActiveCard.ToString()!.Contains("Red"))
                g.DrawRoundedRectangleF(new Pen(Color.Red, 0.1f), (RectangleF)highlightRect!, 0.1f);

            if (CurrentTeam == Team.Blue)
            {
                g.FillRoundedRectangleF(DarkBlueBrush, blueTurnBG, 0.02f);
                g.DrawRoundedRectangleF(BlackPen, blueTurnBG, 0.02f);
                g.DrawString("YOUR TURN", Font, MoccasinBrush, blueTurnBG, Centered);
            }
            else
            {
                g.FillRoundedRectangleF(DarkRedBrush, redTurnBG, 0.02f);
                g.DrawRoundedRectangleF(BlackPen, redTurnBG, 0.02f);
                g.DrawString("YOUR TURN", Font, MoccasinBrush, redTurnBG, Centered);
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
                g.DrawString($"{CurrentTeam.ToString().ToUpper()} WINS!!!", new Font("Arial", 0.5f, FontStyle.Bold, GraphicsUnit.Pixel), BlackBrush, gameOverBanner, Centered);
                g.DrawString("Play Again", new Font("Arial", 0.225f, FontStyle.Bold, GraphicsUnit.Pixel), BlackBrush, playAgain, Centered);

            }

        }

        public void DrawTutorial(Graphics g)
        {
            RectangleF cornerBox = new(0.15f, 0.15f, 2.7f, 1.4f);
            g.FillRoundedRectangleF(MoccasinBrush, cornerBox, 0.1f);
            g.DrawRoundedRectangleF(BlackPen, cornerBox, 0.1f);

            if (TutorialStep == 1)
            {
                g.DrawString("HOW TO PLAY 1/3", TitleFont, BlackBrush, 0.2f, 0.2f);
                g.DrawString("Each team has five pieces, four Students \nand one Master \n \nAll pieces can defeat an \nopposing piece by moving into their spot", TutorialFont, BlackBrush, 0.25f, 0.6f);
            }
            else if (TutorialStep == 2)
            {
                g.DrawString("HOW TO PLAY 2/3", TitleFont, BlackBrush, 0.2f, 0.2f);
                g.DrawString("The cards on your side of the board show \npossible piece movement. \n\nChoose a card, then a piece to move. \n\nAfter moving, the card used will switch with\nthe neutral card on top.", TutorialFont, BlackBrush, 0.25f, 0.5f);

            }
            else if (TutorialStep == 3)
            {
                g.DrawString("HOW TO PLAY 3/3", TitleFont, BlackBrush, 0.2f, 0.2f);
                g.DrawString("To win: \nDefeat the opposing Master \n\nor \n\nMove your Master into the opposing Temple", TutorialFont, BlackBrush, 0.25f, 0.5f);
                g.DrawRectangle(HighlightPen, GridOrigin.X + 0.07f, GridOrigin.Y + 2.07f, .85f, .85f);
                g.DrawRectangle(HighlightPen, GridOrigin.X + 4.07f, GridOrigin.Y + 2.07f, .85f, .85f);


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
            g.DrawString("New Game", Font, BlackBrush, newGame, Centered);
            g.DrawRoundedRectangleF(BlackPen, surrenderBlue, 0.1f);
            g.FillRoundedRectangleF(GreenBrush, surrenderBlue, 0.1f);
            g.DrawString("Surrender Blue", Font, BlackBrush, surrenderBlue, Centered);
            g.DrawRoundedRectangleF(BlackPen, surrenderRed, 0.1f);
            g.FillRoundedRectangleF(GreenBrush, surrenderRed, 0.1f);
            g.DrawString("Surrender Red", Font, BlackBrush, surrenderRed, Centered);
            g.DrawRoundedRectangleF(BlackPen, tutorial, 0.1f);
            g.FillRoundedRectangleF(GreenBrush, tutorial, 0.1f);
            g.DrawString("Tutorial", Font, BlackBrush, tutorial, Centered);
            g.DrawRoundedRectangleF(BlackPen, close, 0.1f);
            g.FillRoundedRectangleF(GreenBrush, close, 0.1f);
            g.DrawString("Close Game", Font, BlackBrush, close, Centered);
            g.DrawString("MENU", TitleFont, BlackBrush, menu, new(Centered) { LineAlignment = StringAlignment.Near });
            g.DrawRoundedRectangleF(new Pen(BlackBrush, 0.03f), closeMenu, 0.05f);
            g.FillRoundedRectangleF(GreenBrush, closeMenu, 0.05f);
            g.DrawString("X", TutorialFont, BlackBrush, closeMenu, Centered);
            
        }
    }
}

    