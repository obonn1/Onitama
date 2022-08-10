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
        public List<Point> PossibleMoves { get; private set; } = new();

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

            //deactivate Cards
            if (item == activeCardLocation)
            {
                ResetActive();
            }
            //activate Cards
            else if ((item != BoardItem.Square) && (activeCardLocation == item))
            {
                if((item == BoardItem.BlueCard1 || item == BoardItem.BlueCard2) && CurrentTeam == Team.Blue)
                {
                    ResetActive();
                    activeCardLocation = item;
                    ActiveCard = item switch
                    {
                        BoardItem.BlueCard1 => BlueCards[0],
                        BoardItem.BlueCard2 => BlueCards[1],
                        _ => null
                    };
                }
                if ((item == BoardItem.RedCard1 || item == BoardItem.RedCard2) && CurrentTeam == Team.Red)
                {
                    ResetActive();
                    activeCardLocation = item;
                    ActiveCard = item switch
                    {
                        BoardItem.RedCard1 => RedCards[0],
                        BoardItem.RedCard2 => RedCards[1],
                        _ => null
                    };
                }
                
            }
            //activate square
            if (item == BoardItem.Square
                && (ActiveSquare == null)
                && (Grid[point.X, point.Y].Team == CurrentTeam))
            {
                ActiveSquare = point;
            }
            //deactivate square
            else if (ActiveSquare == point)
            {
                ActiveSquare = null;
                PossibleMoves = new();
            }
            //move
            if (ActiveSquare != null && CanMoveSquares(ActiveSquare.Value).Contains(point)) Move((Point)ActiveSquare, point);
            if (ActiveSquare != null)
            {
                ActiveSquare = point;
                for (var i = 0; i < 5; i++)
                    for (var j = 0; j < 5; j++)
                    {
                        if (ActiveCard is not null && BlueCards![0].Moves.Contains(new Size(i - point.X, j - point.Y)))
                        {
                            PossibleMoves.Add(new Point(i, j));
                        }
                    }

            }
        }

        public void Move(Point active, Point target)
        {

            if ((Grid[target.X, target.Y].IsMaster == true) 
                || (Grid[active.X, active.Y].Team == Team.Red && target == new Point(0,2)) 
                || (Grid[active.X, active.Y].Team == Team.Blue && target == new Point(4, 2)))
            {
                IsGameOver = true;
            }
            Grid[target.X, target.Y] = Grid[active.X, active.Y];
            Grid[active.X, active.Y] = new Square(null);
            CurrentTeam = CurrentTeam == Team.Red ? Team.Blue : Team.Red;
            ResetActive();
        }

        public void ResetActive()
        {
            ActiveCard = null;
            ActiveSquare = null;
            activeCardLocation = null;
            PossibleMoves = new();
        }
        public List<Point> CanMoveSquares(Point point)
        {
            List<Point> result = new();
            for (var j = 0; j < 5; j++)
                for (var i = 0; i < 5; i++)
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
