using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onitama
{
    internal class Square
    {
        public HasPiece? Team { get; set; }
        public bool IsMaster { get; set; }
        public Square(HasPiece? team = null)
        {
            Team = team;
        }
    }

    public enum HasPiece { Red, Blue }
}
