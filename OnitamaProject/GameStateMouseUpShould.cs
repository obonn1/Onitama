

namespace OnitamaTests
{
    public class GameStateMouseUpShould
    {
        [Theory]
        [InlineData(BoardItem.BlueCard1)]
        [InlineData(BoardItem.BlueCard2)]
        [InlineData(BoardItem.RedCard1)]
        [InlineData(BoardItem.RedCard2)]
        [InlineData(BoardItem.OffMenu)]
        [InlineData(BoardItem.Menu)]
        [InlineData(BoardItem.CloseMenu)]
        [InlineData(BoardItem.BlueSurrender)]
        [InlineData(BoardItem.RedSurrender)]
        [InlineData(BoardItem.CloseGame)]
        [InlineData(BoardItem.Square)]
        [InlineData(BoardItem.TryAgain)]
        [InlineData(BoardItem.Tutorial)]
        [InlineData(BoardItem.NewGame)]

        public void MouseUpWhenIsGameOverShouldDoNothing(BoardItem value)
        {
            GameState? result = new GameState() { IsGameOver = true };
            GameState? candidate = result;
            candidate.MouseUp(value, new System.Drawing.Point(0, 0));
            Assert.Equal(result, candidate);
        }

        [Fact]
        public void MouseUpOnCurrentTeamCardShouldActivate()
        {
            GameState? candidate = new GameState() { TutorialStep = 0, CurrentTeam = Team.Blue };
            candidate.MouseUp(BoardItem.BlueCard2, new System.Drawing.Point(0, 0));
            Assert.True(candidate.ActiveCard == candidate.BlueCards[1]);
        }
    }
}