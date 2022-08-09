using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Onitama
{
    public class GameVisuals
    {
        public List<(float, float)> blueStudents = new();
        public List<(float, float)> redStudents = new();
        public (float, float) blueMaster;
        public (float, float) redMaster;
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
           foreach (var piece in blueStudents)
            {
                g.FillRectangle(Brushes.Blue, piece.Item1 + GridOrigin.X + 0.1f, piece.Item2 + GridOrigin.Y + 0.1f, 0.8f, 0.8f);
            }
            foreach (var piece in redStudents)
            {
                g.FillRectangle(Brushes.Red, piece.Item1 + GridOrigin.X + 0.1f, piece.Item2 + GridOrigin.Y + 0.1f, 0.8f, 0.8f);
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
            NeutralCard!.CardGrid(g, new PointF(3.05f, 0.2f), 1.35f);
            Card.Invert(NeutralCard).CardGrid(g, new PointF(5.6f, 0.2f), 1.35f);
        }
    }
}

