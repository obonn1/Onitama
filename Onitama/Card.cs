using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Onitama
{
	public class Card : Control
	{
		public Card()
        {
        }

		public ImmutableArray<(int X, int Y)> Moves { get; private set; }
		public static ImmutableArray<Card> Deck { get; } = ImmutableArray.Create<Card>(
		new Card("Monkey", ImmutableArray.Create((-1, -1), (-1, 1), (+1, 1), (+1, -1))),
		new Card("Rooster", ImmutableArray.Create((-1, -1), (0, -1), (0, 1), (1, 1))),
		new Card("Horse", ImmutableArray.Create((-1, 0), (0, -1), (1, 0))),
		new Card("Goose", ImmutableArray.Create((-1, 1), (0, -1), (0, 1), (1, -1))),
		new Card("Dragon", ImmutableArray.Create((-1, -1), (-1, 1), (1, -2), (1, 2))),
		new Card("Rabbit", ImmutableArray.Create((-1, -1), (0, 2), (1, 1))),
		new Card("Crab", ImmutableArray.Create((0, 2), (0, -2), (1, 0))),
		new Card("Boar", ImmutableArray.Create((0, 1), (0, -1), (1, 0))),
		new Card("Tiger", ImmutableArray.Create((-1, 0), (2, 0))),
		new Card("Eel", ImmutableArray.Create((-1, -1), (0, 1), (1, -1))),
		new Card("Mantis", ImmutableArray.Create((-1, 0), (1, -1), (1, 1))),
		new Card("Ox", ImmutableArray.Create((-1, 0), (0, 1), (1, 0))),
		new Card("Frog", ImmutableArray.Create((-1, 1), (0, -2), (1, -1))),
		new Card("Elephant", ImmutableArray.Create((0, -1), (0, 1), (1, -1), (1, 1))),
		new Card("Cobra", ImmutableArray.Create((-1, 1), (0, -1), (1, 1))),
		new Card("Crane", ImmutableArray.Create((-1, -1), (-1, 1), (1, 0))));
		public Card(string name, ImmutableArray<(int X, int Y)> moves)
		{
			Name = name;
			Moves = moves;
		}
	}
}