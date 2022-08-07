using System;
using System.Collections.Generic;

public class Card : Control
{
	public List<int> Moves { get; set; }
    public Card(string name, int a, int b, int c, int d)
	{
		Moves.Add(a);
		Moves.Add(b);
		Moves.Add(c);
		Moves.Add(d);
		Name = name;

	}
	public List<Card> Deck { get; set; }
    Deck.Add(new Card())
}
