using System;
using System.Collections.Generic;

namespace Onitama
{
    public class GameVisuals
    {
        public List<(float, float)> blueStudents = new List<(float, float)>();
        public List<(float, float)> redStudents = new List<(float, float)>();
        public (float, float) blueMaster;
        public (float, float) redMaster;
        public Card[]? BlueCards { get; set; }
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
                g.FillRectangle(new SolidBrush(Color.Blue), piece.Item1 + GridOrigin.X, piece.Item2 + GridOrigin.Y, 0.8f, 0.8f);
            }
            foreach (var piece in redStudents)
            {
                g.FillRectangle(new SolidBrush(Color.Red), piece.Item1 + GridOrigin.X, piece.Item2+ GridOrigin.Y, 0.8f, 0.8f);
            }
            g.FillRectangle(new SolidBrush(Color.DarkBlue), blueMaster.Item1 + GridOrigin.X, blueMaster.Item2 + GridOrigin.Y, 0.8f, 0.8f);
            g.FillRectangle(new SolidBrush(Color.DarkRed), redMaster.Item1 + GridOrigin.X, blueMaster.Item2 + GridOrigin.Y, 0.8f, 0.8f);

        }
    }
}

