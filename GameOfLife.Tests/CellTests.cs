namespace GameOfLife.Tests;

public class CellTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    [InlineData(8)]
    public void NextState_DeadCell_ShouldBeDead_WhenMoreOrLessThanThreeNeighbors(int numOfNeighbors)
    {
        // Arrange
        var cell = new Cell(false);

        // Act
        var isAliveInNextState = cell.NextState(numOfNeighbors);

        // Assert
        isAliveInNextState.Should().Be(false);
    }

    [Theory]
    [InlineData(0, false)]
    [InlineData(1, false)]
    public void NextState_ShouldBeDead_WhenLessThan2Neighbors(int numberOfNeighbors, bool expectedState)
    {
        // Arrange
        var cell = new Cell(isAlive: true);

        // Act
        var isAliveInNextState = cell.NextState(numberOfNeighbors);

        // Assert
        isAliveInNextState.Should().Be(expectedState);
    }

    [Theory]
    [InlineData(4, false)]
    [InlineData(5, false)]
    [InlineData(6, false)]
    [InlineData(7, false)]
    [InlineData(8, false)]
    public void NextState_ShouldBeDead_WhenMoreThan3Neighbors(int numberOfNeighbors, bool expectedState)
    {
        // Arrange
        var cell = new Cell(isAlive: true);

        // Act
        var isAliveInNextState = cell.NextState(numberOfNeighbors);

        // Assert
        isAliveInNextState.Should().Be(expectedState);
    }

    [Theory]
    [InlineData(2, true)]
    [InlineData(3, true)]
    public void NextState_ShouldBeAlive_WhenTwoOrThreeNeighbors(int numberOfNeighbors, bool expectedState)
    {
        // Arrange
        var cell = new Cell(isAlive: true);

        // Act
        var isAliveInNextState = cell.NextState(numberOfNeighbors);

        // Assert
        isAliveInNextState.Should().Be(expectedState);
    }

    [Fact]
    public void NextState_DeadCellShouldBeAlive_WhenExactlyThreeNeighbors()
    {
        // Arrange
        var cell = new Cell(isAlive: false);

        // Act
        var isAliveInNextState = cell.NextState(3);

        // Assert
        isAliveInNextState.Should().Be(true);
    }
}
