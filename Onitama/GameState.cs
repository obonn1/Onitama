using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onitama
{
    public class GameState
    {
        public Square[,] Grid { get; set; } = new Square[5, 5];
        public Card? ActiveCard { get; set; } = null;

        public BoardItem? activeCardLocation = null;
        public Point? ActiveSquare { get; set; }
        public Team CurrentTeam { get; set; }
        public bool IsGameOver { get; set; }
        public List<Card> Cards { get; set; } = new List<Card>(5);
        public Card[] BlueCards { get; set; }
        public Card[] RedCards { get; set; }
        public Card? NeutralCard { get; set; }
        public List<(float, float)> BlueStudents { get; set; } = new List<(float, float)>();
        public List<(float, float)> RedStudents { get; set; } = new List<(float, float)>();
        public (BoardItem, Point)? mouseDownLocation { get; set; }
        public PointF? mouseLocation { get; set; }
        public PointF GridOrigin { get; set; }

        public GameState()
        {
            Random random = new Random();
            for (int i = 0; i < Grid.GetLength(0); i++)
            {
                for (int j = 0; j < Grid.GetLength(1); j++)
                {
                    if (i == 0)
                    {
                        Grid[i, j] = new Square(Team.Blue);
                        BlueStudents.Add(((float)i, (float)j));
                    }
                    else if (i == 4)
                    {
                        Grid[i, j] = new Square(Team.Red);
                        RedStudents.Add(((float)i, (float)j));
                    }
                    else
                    {
                        Grid[i, j] = new Square();
                    }
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

            BlueCards = new Card[2] { Cards[0], Cards[1] };
            RedCards = new Card[2] { Card.Invert(Cards[2]), Card.Invert(Cards[3]) };
            NeutralCard = Cards[4];
        }

        public void MouseUp(BoardItem item, Point point)
        {
            //activate Cards
            if ((item != BoardItem.Square) && (activeCardLocation == null)) activeCardLocation = item;
            //deactivate Cards
            if (item == activeCardLocation) ActiveCard = null;
            //activate square
            if (item == BoardItem.Square
                && (ActiveSquare == null)
                && (Grid[point.X, point.Y].Team == CurrentTeam))
            {
                ActiveSquare = point;
            }
            //deactivate square
            if (ActiveSquare == point) ActiveSquare = null;
            //move
            if (ActiveSquare != null
                && CanMoveSquares(ActiveSquare.Value).Contains(point)) Move(Grid[ActiveSquare.Value.X, ActiveSquare.Value.Y], Grid[point.X, point.Y]);
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

        public List<Point> CanMoveSquares(Point point)
        {
            if (point.X < 0 || point.Y < 0) throw new ArgumentOutOfRangeException();
            List<Point> result = new();
            for (var i = 0; i < 5; i++)
                for (var j = 0; j < 5; j++)
                {
                    if (ActiveCard is not null && ActiveCard.Moves.Contains(new Size(i - point.X, j - point.Y)))
                    {
                        result.Add(new Point(i, j));
                    }
                }
            return result;
        }

    }
    public enum Team { Red, Blue }
    public enum BoardItem
    {
        BlueCard1,
        BlueCard2,
        RedCard1,
        RedCard2,
        Square,
    }
}
