namespace GameOfLife;

public class Cell
{
    public bool isAlive { get; set; }

    public Cell(bool isAlive)
    {
        this.isAlive = isAlive;
    }

    public bool NextState(int numOfNeighbors)
    {
        if (isAlive)
        {
            if (numOfNeighbors == 2 || numOfNeighbors == 3)
                return this.isAlive;
        }
        if (numOfNeighbors == 3) return true;

        return false;
    }
}