using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onitama
{
    public class Square
    {
        public Team? Team { get; set; }
        public bool IsMaster { get; set; }
        public Square(Team? team = null)
        {
            Team = team;
        }
    }
}
