namespace GameOfLife.Tests;

public class GameTests
{
    [Fact]
    public void Game_ShouldCreateBoardOfCellsFromState_WhenCreated()
    {
        // Arrange
        var initState = new int[,] {
            {0, 0, 0},
            {0, 0, 0},
            {0, 0, 0},
        };
        var expectedState = new Cell[,] {
            {new Cell(false), new Cell(false), new Cell(false)},
            {new Cell(false), new Cell(false), new Cell(false)},
            {new Cell(false), new Cell(false), new Cell(false)},
        };

        // Act
        var game = new GameOfLifeGame(initState);

        // Assert
        game._state.Should().BeEquivalentTo(expectedState);
    }

    [Fact]
    public void NumOfNeighbors_ShouldReturn2_WhenLeftAndRightAlive()
    {
        // Arrange
        var initState = new int[,] {
            {0, 0, 0},
            {1, 1, 1},
            {0, 0, 0},
        };
        var game = new GameOfLifeGame(initState);

        // Act
        var numberOfNeighbors = game.NumOfNeighbors(1, 1);

        // Assert
        numberOfNeighbors.Should().Be(2);
    }

    [Fact]
    public void NumOfNeighbors_ShouldReturn3_WhenTopRowAlive()
    {
        // Arrange
        var initState = new int[,] {
            {1, 1, 1},
            {0, 1, 0},
            {0, 0, 0},
        };
        var game = new GameOfLifeGame(initState);

        // Act
        var numberOfNeighbors = game.NumOfNeighbors(1, 1);

        // Assert
        numberOfNeighbors.Should().Be(3);
    }

    [Fact]
    public void NumOfNeighbors_ShouldReturn3_WhenBottomRowAlive()
    {
        // Arrange
        var initState = new int[,] {
            {0, 0, 0},
            {0, 1, 0},
            {1, 1, 1},
        };
        var game = new GameOfLifeGame(initState);

        // Act
        var numberOfNeighbors = game.NumOfNeighbors(1, 1);

        // Assert
        numberOfNeighbors.Should().Be(3);
    }

    [Fact]
    public void NumOfNeighbors_ShouldReturn8_WhenAllNeighborsAlive()
    {
        // Arrange
        var initState = new int[,] {
            {1, 1, 1},
            {1, 1, 1},
            {1, 1, 1},
        };
        var game = new GameOfLifeGame(initState);

        // Act
        var numberOfNeighbors = game.NumOfNeighbors(1, 1);

        // Assert
        numberOfNeighbors.Should().Be(8);
    }

    [Fact]
    public void NumOfNeighbors_ShouldReturn2_WhenTopEdgeCellAndBottomAndRightAlive()
    {
        // Arrange
        var initState = new int[,] {
            {1, 1, 0},
            {1, 0, 0},
            {0, 0, 0}
        };
        var game = new GameOfLifeGame(initState);

        // Act
        var numberOfNeighbors = game.NumOfNeighbors(0, 0);

        // Assert
        numberOfNeighbors.Should().Be(2);
    }

    [Fact]
    public void NumOfNeighbors_ShouldReturn2_WhenTopEdgeCellAndLeftAndRightAlive()
    {
        // Arrange
        var initState = new int[,] {
            {1, 1, 1},
            {0, 0, 0},
            {0, 0, 0}
        };
        var game = new GameOfLifeGame(initState);

        // Act
        var numberOfNeighbors = game.NumOfNeighbors(0, 1);

        // Assert
        numberOfNeighbors.Should().Be(2);
    }

    [Fact]
    public void NumOfNeighbors_ShouldReturn2_WhenBottomEdgeCellAndLeftAndRightAlive()
    {
        // Arrange
        var initState = new int[,] {
            {0, 0, 0},
            {0, 0, 0},
            {1, 1, 1}
        };
        var game = new GameOfLifeGame(initState);

        // Act
        var numberOfNeighbors = game.NumOfNeighbors(2, 1);

        // Assert
        numberOfNeighbors.Should().Be(2);
    }

    [Fact]
    public void NumOfNeighbors_ShouldReturn3_WhenBottomEdgeCellAndTopAndRightAlive()
    {
        // Arrange
        var initState = new int[,] {
            {0, 0, 0},
            {0, 1, 0},
            {1, 1, 1},
        };
        var game = new GameOfLifeGame(initState);

        // Act
        var numberOfNeighbors = game.NumOfNeighbors(2, 1);

        // Assert
        numberOfNeighbors.Should().Be(3);
    }

    [Fact]
    public void NextIteration_StateShouldBeReversedBlinker_WhenCalled()
    {
        // Arrange
        var initState = new int[,] {
            {0, 1, 0},
            {0, 1, 0},
            {0, 1, 0},
        };
        var game = new GameOfLifeGame(initState);

        var expectedState = new Cell[,] {
            {new Cell(false), new Cell(false), new Cell(false)},
            {new Cell(true), new Cell(true), new Cell(true)},
            {new Cell(false), new Cell(false), new Cell(false)},
        };

        // Act
        var nextIteration = game.NextIteration();

        // Assert
        nextIteration.Should().BeEquivalentTo(expectedState);
    }

    [Fact]
    public void NextIteration_StateShouldBeConstant_WhenCalled()
    {
        // Arrange
        var initState = new int[,] {
            {1, 1, 0},
            {1, 1, 0},
            {0, 0, 0},
        };
        var game = new GameOfLifeGame(initState);

        var expectedState = new Cell[,] {
            {new Cell(true), new Cell(true), new Cell(false)},
            {new Cell(true), new Cell(true), new Cell(false)},
            {new Cell(false), new Cell(false), new Cell(false)},
        };

        // Act
        var nextIteration = game.NextIteration();

        // Assert
        nextIteration.Should().BeEquivalentTo(expectedState);
    }

    [Fact]
    public void SetState_ShouldChangeCellState_WhenCalled()
    {
        // Arrange
        int xIndex = 1, yIndex = 1;
        var game = new GameOfLifeGame(5);

        // Act
        var cellAfterChange = game.SetCellState(xIndex, yIndex, true);

        // Assert
        cellAfterChange.isAlive.Should().Be(true);
    }
}