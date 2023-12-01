using TTT.Server.Utilities;
using TTT.Shared.Models;

namespace TTT.Server.GameLogic;

public class Grid
{
    private readonly int gridSize;
    private readonly MarkType[,] internalGrid;
    
    public Grid(int gridSize)
    {
        this.gridSize = gridSize;
        internalGrid = new MarkType[gridSize, gridSize];
    }
    
    public void MarkCell(int row, int column, MarkType mark)
    {
        internalGrid[row, column] = mark;
    }
    
    public void MarkCell(int index, MarkType mark)
    {
        var (row, column) = GridExtensions.GetCoordinates(index, gridSize);
        internalGrid[row, column] = mark;
    }
    
    public (bool, WinLineType) CheckWin()
    {
        var (isWin, winLineType) = internalGrid.CheckWin(gridSize);
        return (isWin, winLineType);
    }
    
    public bool CheckDraw()
    {
        return internalGrid.CheckDraw(gridSize);
    }

    public MarkType GetCell(byte cellIndex)
    {
        var (row, column) = GridExtensions.GetCoordinates(cellIndex, gridSize);
        return internalGrid[row, column];
    }
}