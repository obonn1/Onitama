using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Onitama
{
    public class GameVisuals
    {
        public List<(float, float)> BlueStudents { get; set; } = new();
        public List<(float, float)> RedStudents { get; set; } = new();
        public (float, float) blueMaster;
        public (float, float) redMaster;
        public List<(float, float)> possibleMoves = new();
        public (float x, float y) activeStudent = (1,0);
        public Card[]? BlueCards { get; set; }
        public Font Font { get; set; } = new Font("Arial", 0.2f);
        public Card[]? RedCards { get; set; }
        public Card? NeutralCard { get; set; }
        public PointF GridOrigin { get; set; }

        public GameVisuals(PointF gridOrigin)
        {
            GridOrigin = gridOrigin;
        }

        public void DrawState(Graphics g)
        {
           foreach (var piece in BlueStudents)
            {
                g.FillRectangle(Brushes.Blue, piece.Item1 + GridOrigin.X + 0.1f, piece.Item2 + GridOrigin.Y + 0.1f, 0.8f, 0.8f);
            }
            foreach (var piece in RedStudents)
            {
                g.FillRectangle(Brushes.Red, piece.Item1 + GridOrigin.X + 0.1f, piece.Item2 + GridOrigin.Y + 0.1f, 0.8f, 0.8f);
            }

            g.FillRectangle(Brushes.DarkBlue, blueMaster.Item1 + GridOrigin.X + 0.1f, blueMaster.Item2 + GridOrigin.Y + 0.1f, 0.8f, 0.8f);
            g.FillRectangle(Brushes.DarkRed, redMaster.Item1 + GridOrigin.X + 0.1f, blueMaster.Item2 + GridOrigin.Y + 0.1f, 0.8f, 0.8f);
            g.DrawRectangle(new Pen(Color.DarkOrange, 0.05f), activeStudent.x + GridOrigin.X + 0.05f, activeStudent.y + GridOrigin.Y + 0.05f, 0.9f, 0.9f);

            foreach (var square in possibleMoves)
            {
                g.DrawRectangle(new Pen(Color.White, 0.05f), square.Item1 + activeStudent.x + GridOrigin.X + 0.05f, square.Item2 + activeStudent.y + GridOrigin.Y + 0.05f, 0.9f, 0.9f);
            }
            RectangleF blueCard1BG = new(.425f, 1.88f, 1.8f, 2.25f);
            RectangleF blueCard2BG = new(.425f, 4.36f, 1.8f, 2.25f);
            RectangleF redCard1BG = new(7.7f, 1.88f, 1.8f, 2.25f);
            RectangleF redCard2BG = new(7.7f, 4.36f, 1.8f, 2.25f);
            RectangleF neutralCardBG = new(3f, 0.15f, 4f, 1.4f);
            g.FillRectangle(Brushes.DarkBlue, blueMaster.Item1 + GridOrigin.X + 0.1f, blueMaster.Item2 + GridOrigin.Y + 0.1f, 0.8f, 0.8f);
            g.FillRectangle(Brushes.DarkRed, redMaster.Item1 + GridOrigin.X + 0.1f, blueMaster.Item2 + GridOrigin.Y + 0.1f, 0.8f, 0.8f);
            g.FillRoundedRectangleF(Brushes.Moccasin, blueCard1BG, .1f);
            g.FillRoundedRectangleF(Brushes.Moccasin, blueCard2BG, .1f);
            g.FillRoundedRectangleF(Brushes.Moccasin, redCard1BG, .1f);
            g.FillRoundedRectangleF(Brushes.Moccasin, redCard2BG, .1f);
            g.FillRoundedRectangleF(Brushes.Moccasin, neutralCardBG, .1f);
            BlueCards![0].CardGrid(g, new PointF(0.475f, 1.93f), 1.6f);
            g.DrawString(BlueCards[0].Name, Font, Brushes.Black, (float)(0.5f + ((1.6f - (BlueCards[0].Name.Length* (Font.Size*0.75))) /2)), 3.7f);
            BlueCards[1].CardGrid(g, new PointF(0.475f, 4.41f), 1.6f);
            g.DrawString(BlueCards[1].Name, Font, Brushes.Black, (float)(0.5f + ((1.6f - (BlueCards[1].Name.Length* (Font.Size * 0.75))) / 2)), 6.18f);
            RedCards![0].CardGrid(g, new PointF(7.75f, 1.93f), 1.6f);
            g.DrawString(RedCards[0].Name, Font, Brushes.Black, (float)(7.78f + ((1.6f - (RedCards[0].Name.Length* (Font.Size * 0.75))) / 2)), 3.7f);
            RedCards[1].CardGrid(g, new PointF(7.75f, 4.41f), 1.6f);
            g.DrawString(RedCards[1].Name, Font, Brushes.Black, (float)(7.78f + ((1.6f - (RedCards[1].Name.Length* (Font.Size * 0.75))) / 2)), 6.18f);
            NeutralCard!.CardGrid(g, new PointF(3.025f, 0.175f), 1.25f);
            (Card.Invert(new Card(NeutralCard))).CardGrid(g, new PointF(5.625f, 0.175f), 1.25f);
            g.DrawString(NeutralCard.Name, Font, Brushes.Black, (float)(4.2f + ((1.6f - (NeutralCard.Name.Length * (Font.Size * 0.75))) / 2)), 0.73f);
        }
    }
}

