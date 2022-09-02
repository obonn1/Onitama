namespace Onitama;

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
