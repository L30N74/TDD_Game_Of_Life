namespace GameOfLife;

public class GameOfLifeGame : IGameOfLifeGame
{
    public Cell[,] _state { get; set; }

    public GameOfLifeGame(int[,] initState)
    {
        this._state = new Cell[initState.GetLength(0), initState.GetLength(1)];
        for (var i = 0; i < initState.GetLength(0); i++)
        {
            for (var j = 0; j < initState.GetLength(1); j++)
            {
                this._state[i, j] = new Cell(initState[i, j] == 1);
            }
        }
    }

    public GameOfLifeGame(int dimensions)
    {
        this._state = new Cell[dimensions, dimensions];
        for (var i = 0; i < dimensions; i++)
        {
            for (var j = 0; j < dimensions; j++)
            {
                this._state[i, j] = new Cell(false);
            }
        }
    }

    public GameOfLifeGame()
    {
        var dimensions = 20;
        this._state = new Cell[dimensions, dimensions];
        for (var i = 0; i < dimensions; i++)
        {
            for (var j = 0; j < dimensions; j++)
            {
                this._state[i, j] = new Cell(false);
            }
        }
    }

    public Cell[,] NextIteration()
    {
        var nextIteration = new Cell[this._state.GetLength(0), this._state.GetLength(1)];
        for (var i = 0; i < this._state.GetLength(0); i++)
        {
            for (var j = 0; j < this._state.GetLength(1); j++)
            {
                var numOfNeighbors = NumOfNeighbors(i, j);
                var cellStateInNextIteration = this._state[i, j].NextState(numOfNeighbors);
                nextIteration[i, j] = new Cell(cellStateInNextIteration);
            }
        }

        return nextIteration;
    }

    public int NumOfNeighbors(int cellX, int cellY)
    {
        var neighbors = 0;
        for (int xIndex = cellX - 1; xIndex <= cellX + 1; xIndex++)
        {
            for (int yIndex = cellY - 1; yIndex <= cellY + 1; yIndex++)
            {
                // Don't include yourself
                if (xIndex == cellX && yIndex == cellY)
                    continue;

                // Ignore top and left bounds
                if (xIndex == -1 || yIndex == -1)
                    continue;

                // Ignore bottom and right bounds
                if (xIndex == this._state.GetLength(0) || yIndex == this._state.GetLength(1))
                    continue;

                // Calculate this row's neighbors
                var neighborCell = this._state[xIndex, yIndex];
                if (neighborCell.isAlive)
                    neighbors++;
            }
        }

        return neighbors;
    }

    public Cell SetCellState(int xIndex, int yIndex, bool state)
    {
        this._state[xIndex, yIndex].isAlive = state;
        return this._state[xIndex, yIndex];
    }

    public Cell[,] SetState(Cell[,] newState)
    {
        this._state = newState;
        return this._state;
    }
}
