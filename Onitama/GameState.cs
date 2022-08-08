using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onitama
{
    internal class GameState
    {
        public Square[,] Grid { get; set; } = new Square[5, 5];
        public Card? ActiveCard { get; set; }
        public Team CurrentTeam { get; set; }
        public bool IsGameOver { get; set; }
        public List<Card> Cards { get; set; }
        public Card[] BlueCards { get; set; }
        public Card[] RedCards { get; set; }
        public Card? NeutralCard { get; set; }
        public GameState()
        {
            Random random = new Random();
            for (int i = 0; i < Grid.GetLength(0); i++)
            {
                for (int j = 0; j < Grid.GetLength(1); j++)
                {
                    if (i == 0) Grid[i, j] = new Square(Team.Blue);
                    else if (i == 4) Grid[i, j] = new Square(Team.Red);
                    else Grid[i, j] = new Square();
                };
            }

            Grid[0, 2].IsMaster = true;
            Grid[4, 2].IsMaster = true;
            CurrentTeam = random.Next(2) == 1 ? Team.Red : Team.Blue;
            while (Cards.Count < 5)
            {
                var randomCard = Card.Deck[random.Next(Card.Deck.Length)];
                if (!Cards.Contains(randomCard))
                    Cards.Add(randomCard);
            }

            BlueCards = new Card[2] { Cards[1], Cards[2] };
            RedCards = new Card[2] { Cards[3], Cards[4] };
            NeutralCard = Cards[5];
        }

        public void Move(Square origin, Square target)
        {

            if (target.IsMaster == true 
                || (origin.Team == Team.Red && target == Grid[0, 2]) 
                || (origin.Team == Team.Blue && target == Grid[4, 2]))
            {
                IsGameOver = true;
            }
            target = origin;
            origin = new Square();
        }

        public List<(int X, int Y, bool)> CanMoveSquares(int x, int y)
        {
            List<(int X, int Y, bool)> result = new List<(int X, int Y, bool)>();
            for (var i = 0; i < 5; i++)
                for (var j = 0; j < 5; j++)
                {
                    if (ActiveCard.Moves.Contains((i - x, j - y)))
                    {
                        if (Grid[i, j].Team != Grid[x, y].Team) result.Add((i, j, true));
                        else result.Add((i, j, false));
                    }
                }
            return result;
        }

    }
    public enum Team { Red, Blue }
}
