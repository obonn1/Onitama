using Shouldly;

namespace Onitama.Tests
{
    public class GameRuleTests
    {
        [Fact]
        public void Current_team_changes_after_move_is_completed()
        {
            var game = new Game();
            game.CurrentTeam = Team.Red;
            game.RedCards = new[] { Card.Monkey, Card.Monkey };

            // Perform a valid move given the cards in the test setup
            game.Move();

            game.CurrentTeam.ShouldBe(Team.Blue);
        }
    }
}
