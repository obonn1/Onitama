using System.Collections.Immutable;

namespace Onitama
{
    public record Card
    {
        public ImmutableArray<Size> Moves { get; set; }

        public string Name { get; set; }

        public Card(string name, ImmutableArray<Size> moves)
        {
            Name = name;
            Moves = moves;
        }

        public void DrawCardGrid(Graphics g, PointF location, float length)
        {
            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    if ((x, y) == (2, 2))
                    {
                        g.FillRectangle(DrawTools.BlackBrush, (float)(location.X + (.21 * x * length) + 0.05f), (float)(location.Y + (.21 * y * length) + 0.05f), length * 0.16f, length * 0.16f);
                    }
                    else if (Moves.Any(s => new Point(x, y) == new Point(2 + s.Width, 2 + s.Height)))
                    {
                        g.FillRectangle(new SolidBrush(Color.Gray), (float)(location.X + (.21 * x * length) + 0.05f), (float)(location.Y + (.21 * y * length) + 0.05f), length * 0.16f, length * 0.16f);
                    }
                    else
                    {
                        g.FillRectangle(new SolidBrush(Color.White), (float)(location.X + (.21 * x * length) + 0.05f), (float)(location.Y + (.21 * y * length) + 0.05f), length * 0.16f, length * 0.16f);
                    }
                }
            }
        }

        public static Card Invert(Card card)
        {
            var invertedMoves = card.Moves.Select(move => new Size(-move.Width, -move.Height)).ToImmutableArray();
            return card with { Moves = invertedMoves };
        }

        public static ImmutableArray<Card> Deck { get; } = ImmutableArray.Create(
        new Card("Monkey", ImmutableArray.Create(new Size(-1, -1), new Size(-1, 1), new Size(+1, 1), new Size(+1, -1))),
        new Card("Rooster", ImmutableArray.Create(new Size(-1, -1), new Size(0, -1), new Size(0, 1), new Size(1, 1))),
        new Card("Horse", ImmutableArray.Create(new Size(-1, 0), new Size(0, -1), new Size(1, 0))),
        new Card("Goose", ImmutableArray.Create(new Size(-1, 1), new Size(0, -1), new Size(0, 1), new Size(1, -1))),
        new Card("Dragon", ImmutableArray.Create(new Size(-1, -1), new Size(-1, 1), new Size(1, -2), new Size(1, 2))),
        new Card("Rabbit", ImmutableArray.Create(new Size(-1, -1), new Size(0, 2), new Size(1, 1))),
        new Card("Crab", ImmutableArray.Create(new Size(0, 2), new Size(0, -2), new Size(1, 0))),
        new Card("Boar", ImmutableArray.Create(new Size(0, 1), new Size(0, -1), new Size(1, 0))),
        new Card("Tiger", ImmutableArray.Create(new Size(-1, 0), new Size(2, 0))),
        new Card("Eel", ImmutableArray.Create(new Size(-1, -1), new Size(0, 1), new Size(1, -1))),
        new Card("Mantis", ImmutableArray.Create(new Size(-1, 0), new Size(1, -1), new Size(1, 1))),
        new Card("Ox", ImmutableArray.Create(new Size(-1, 0), new Size(0, 1), new Size(1, 0))),
        new Card("Frog", ImmutableArray.Create(new Size(-1, 1), new Size(0, -2), new Size(1, -1))),
        new Card("Elephant", ImmutableArray.Create(new Size(0, -1), new Size(0, 1), new Size(1, -1), new Size(1, 1))),
        new Card("Cobra", ImmutableArray.Create(new Size(-1, 1), new Size(0, -1), new Size(1, 1))),
        new Card("Crane", ImmutableArray.Create(new Size(-1, -1), new Size(-1, 1), new Size(1, 0))));
    }
}
