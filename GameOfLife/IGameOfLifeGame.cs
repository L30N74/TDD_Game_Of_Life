namespace GameOfLife;

public interface IGameOfLifeGame
{
    Cell[,] NextIteration();
    int NumOfNeighbors(int xIndex, int yIndex);
    Cell SetCellState(int xIndex, int yIndex, bool state);
}