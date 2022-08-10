using System;
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
        public List<Point> possibleMoves = new();
        public Point? activeStudent;
        public BoardItem? ActiveCard { get; set; }
        public Card[]? BlueCards { get; set; }
        public Font Font { get; set; } = new("Arial", 0.2f);
        public Card[]? RedCards { get; set; }
        public Card? NeutralCard { get; set; }
        public PointF GridOrigin { get; set; }
        public Team CurrentTeam { get; internal set; }
        public bool IsGameOver { get; set; }

        public GameVisuals(PointF gridOrigin)
        {
            GridOrigin = gridOrigin;
        }

        public void MouseDown(PointF location)
        {
        }

        public void MouseUp(BoardItem item, Point point)
        {
        }

        public void DrawState(Graphics g)
        {
            foreach (var piece in BlueStudents)
            {
                g.FillRectangle(BlueBrush, piece.X + GridOrigin.X + 0.1f, piece.Y + GridOrigin.Y + 0.1f, 0.8f, 0.8f);
            }
            foreach (var piece in RedStudents)
            {
                g.FillRectangle(RedBrush, piece.X + GridOrigin.X + 0.1f, piece.Y + GridOrigin.Y + 0.1f, 0.8f, 0.8f);
            }

            g.FillRectangle(DarkBlueBrush, BlueMaster.X + GridOrigin.X + 0.1f, BlueMaster.Y + GridOrigin.Y + 0.1f, 0.8f, 0.8f);
            g.FillRectangle(DarkRedBrush, RedMaster.X + GridOrigin.X + 0.1f, RedMaster.Y + GridOrigin.Y + 0.1f, 0.8f, 0.8f);
            if (activeStudent != null)
            {
                g.DrawRectangle(new Pen(Color.DarkOrange, 0.1f), activeStudent.Value.X + GridOrigin.X + 0.05f, activeStudent.Value.Y + GridOrigin.Y + 0.05f, 0.9f, 0.9f);

                foreach (var square in possibleMoves)
                {
                    g.DrawRectangle(new Pen(Color.White, 0.05f), square.X + GridOrigin.X + 0.05f, square.Y + GridOrigin.Y + 0.05f, 0.9f, 0.9f);
                }
            }

            RectangleF blueCard1BG = new(.425f, 1.88f, 1.8f, 2.25f);
            RectangleF blueCard2BG = new(.425f, 4.36f, 1.8f, 2.25f);
            RectangleF redCard1BG = new(7.7f, 1.88f, 1.8f, 2.25f);
            RectangleF redCard2BG = new(7.7f, 4.36f, 1.8f, 2.25f);
            RectangleF neutralCardBG = new(3f, 0.15f, 4f, 1.4f);
            RectangleF turnBG = new(CurrentTeam==Team.Blue?0.2f:7.4f, 0.95f, 2.3f, 0.5f);
            RectangleF? highlightRect = ActiveCard switch
            {
                BoardItem.BlueCard1 => blueCard1BG,
                BoardItem.BlueCard2 => blueCard2BG,
                BoardItem.RedCard1 => redCard1BG,
                BoardItem.RedCard2 => redCard2BG,
                _ => null
            };
            g.FillRoundedRectangleF(MoccasinBrush, blueCard1BG, .1f);
            g.FillRoundedRectangleF(MoccasinBrush, blueCard2BG, .1f);
            g.FillRoundedRectangleF(MoccasinBrush, redCard1BG, .1f);
            g.FillRoundedRectangleF(MoccasinBrush, redCard2BG, .1f);
            g.FillRoundedRectangleF(MoccasinBrush, neutralCardBG, .1f);
            BlueCards![0].CardGrid(g, new PointF(0.475f, 1.93f), 1.6f);
            g.DrawString(BlueCards[0].Name, Font, BlackBrush, (float)(0.425f + ((1.8f - (BlueCards[0].Name.Length * (Font.Size * 0.95))) / 2)), 3.7f);
            BlueCards[1].CardGrid(g, new PointF(0.475f, 4.41f), 1.6f);
            g.DrawString(BlueCards[1].Name, Font, BlackBrush, (float)(0.425f + ((1.8f - (BlueCards[1].Name.Length * (Font.Size * 0.95))) / 2)), 6.18f);
            RedCards![0].CardGrid(g, new PointF(7.75f, 1.93f), 1.6f);
            g.DrawString(RedCards[0].Name, Font, BlackBrush, (float)(7.7f + ((1.8f - (RedCards[0].Name.Length * (Font.Size * 0.95))) / 2)), 3.7f);
            RedCards[1].CardGrid(g, new PointF(7.75f, 4.41f), 1.6f);
            g.DrawString(RedCards[1].Name, Font, BlackBrush, (float)(7.7f + ((1.8f - (RedCards[1].Name.Length * (Font.Size * 0.95))) / 2)), 6.18f);
            NeutralCard!.CardGrid(g, new PointF(5.625f, 0.175f), 1.25f);
            Card.Invert(new Card(NeutralCard)).CardGrid(g, new PointF(3.025f, 0.175f), 1.25f);
            g.DrawString(NeutralCard.Name, Font, BlackBrush, (float)(3f + ((4f - (NeutralCard.Name.Length * (Font.Size * 0.9))) / 2)), 0.73f);
            if (ActiveCard != null) g.DrawRoundedRectangleF(new Pen(Color.White, 0.1f), (RectangleF)highlightRect!, 0.1f);
            g.FillRoundedRectangleF(CurrentTeam == Team.Red ? DarkRedBrush : DarkBlueBrush, turnBG, 0.1f);
            g.DrawString("YOUR TURN", Font, BlackBrush, CurrentTeam == Team.Blue ? 0.3f : 7.5f, 1f);
            // Test gameover
            if (IsGameOver)
            {
                Rectangle gameOverBanner = new(3, 2, 4, 3);
                RectangleF playAgain = new(4.41f, 3.88f, 1.5f, 0.5f);
                NeutralCard = null;
                g.FillRoundedRectangleF(MoccasinBrush, gameOverBanner, 0.5f);
                g.FillRoundedRectangleF(WhiteBrush, playAgain, 0.1f);
                g.DrawRoundedRectangleF(new Pen(Color.Black, 0.05f), gameOverBanner, 0.5f);
                g.DrawRoundedRectangleF(new Pen(Color.Black, 0.02f), playAgain, 0.2f);
                g.DrawString($"{CurrentTeam.ToString().ToUpper()} WINS!!!", new Font("Arial", 0.325f, FontStyle.Bold), BlackBrush, 3.1f, 2.5f);
                g.DrawString("Play Again", new Font("Arial", 0.15f, FontStyle.Bold), BlackBrush, 4.5f, 4f);

            }
        }
    }
}

