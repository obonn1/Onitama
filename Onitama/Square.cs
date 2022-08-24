// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Onitama
{
    public class Square
    {
        public Team? Team { get; set; }

        public bool IsMaster { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Square"/> class.
        /// </summary>
        /// <param name="team"></param>
        public Square(Team? team = null)
        {
            Team = team;
        }
    }
}
