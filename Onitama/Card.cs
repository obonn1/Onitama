using System.Collections.Immutable;

namespace Onitama;
// TODO finish test for team change on move
// TODO add more tests
// TODO Change Card names to be properties
// TODO add pictures and build instructions to README
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
        for (var y = 0; y < 5; y++)
        {
            for (var x = 0; x < 5; x++)
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

    public static Card Monkey { get; } = new("Monkey", ImmutableArray.Create(new Size(-1, -1), new Size(-1, 1), new Size(+1, 1), new Size(+1, -1)));
    public static Card Rooster { get; } = new Card("Rooster", ImmutableArray.Create(new Size(-1, -1), new Size(0, -1), new Size(0, 1), new Size(1, 1)));
    public static Card Horse { get; } = new("Horse", ImmutableArray.Create(new Size(-1, 0), new Size(0, -1), new Size(1, 0))),
    public static Card Goose { get; } = new Card("Goose", ImmutableArray.Create(new Size(-1, 1), new Size(0, -1), new Size(0, 1), new Size(1, -1)));
    public static Card Dragon { get; } = new("Dragon", ImmutableArray.Create(new Size(-1, -1), new Size(-1, 1), new Size(1, -2), new Size(1, 2)));
    public static Card Rabbit { get; } = new Card("Rabbit", ImmutableArray.Create(new Size(-1, -1), new Size(0, 2), new Size(1, 1)));
    public static Card Crab { get; } = new("Crab", ImmutableArray.Create(new Size(0, 2), new Size(0, -2), new Size(1, 0)));
    public static Card Boar { get; } = new Card("Boar", ImmutableArray.Create(new Size(0, 1), new Size(0, -1), new Size(1, 0)));
    public static Card Tiger { get; } = new("Tiger", ImmutableArray.Create(new Size(-1, 0), new Size(2, 0)));
    public static Card Eel { get; } = new Card("Eel", ImmutableArray.Create(new Size(-1, -1), new Size(0, 1), new Size(1, -1)));
    public static Card Mantis { get; } = new Card("Mantis", ImmutableArray.Create(new Size(-1, 0), new Size(1, -1), new Size(1, 1)));
    public static Card Ox { get; } = new Card("Ox", ImmutableArray.Create(new Size(-1, 0), new Size(0, 1), new Size(1, 0)));
    public static Card Frog { get; } = new Card("Frog", ImmutableArray.Create(new Size(-1, 1), new Size(0, -2), new Size(1, -1)));
    public static Card Elephant { get; } = new Card("Elephant", ImmutableArray.Create(new Size(0, -1), new Size(0, 1), new Size(1, -1), new Size(1, 1)));
    public static Card Cobra { get; } = new Card("Cobra", ImmutableArray.Create(new Size(-1, 1), new Size(0, -1), new Size(1, 1)));
    public static Card Crane { get; } = new Card("Crane", ImmutableArray.Create(new Size(-1, -1), new Size(-1, 1), new Size(1, 0))));

    public static ImmutableArray<Card> Deck { get; } = ImmutableArray.Create(
    Monkey,
    Rooster,
    Horse,
    Goose,
    Dragon,
    Rabbit,
    Crab,
    Boar,
    Tiger,
    Eel,
    Mantis,
    Ox,
    Frog,
    Elephant,
    Cobra,
    Crane
    );
}
