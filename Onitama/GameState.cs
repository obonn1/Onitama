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
        public Team CurrentTeam { get; set; }
        public bool IsGameOver { get; set; }
        public List<Card> cards { get; set; }
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
            while (cards.Count < 5)
            {
                var randomCard = Card.Deck[random.Next(Card.Deck.Length)];
                if (!cards.Contains(randomCard))
                    cards.Add(randomCard);
            }
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

    }
    public enum Team { Red, Blue }
}
