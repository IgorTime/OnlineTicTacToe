using TTT.Shared.Models;

namespace TTT.Server.Utilities;

//TODO: Add unit tests

public static class GridExtensions
{
    public static (int, int) GetCoordinates(int index, int gridSize) => (index / gridSize, index % gridSize);

    public static (bool isWin, WinLineType winLineType) CheckWin(this MarkType[,] grid, int gridSize)
    {
        for (var i = 0; i < gridSize; i++)
        {
            if (grid.CheckRow(i, gridSize))
            {
                return (true, ResoleLineTypeRow(i));
            }

            if (grid.CheckColum(i, gridSize))
            {
                return (true, ResoleLineTypeColumn(i));
            }
        }

        if (grid.CheckDiagonal(gridSize))
        {
            return (true, WinLineType.Diagonal);
        }

        if (grid.CheckAntiDiagonal(gridSize))
        {
            return (true, WinLineType.AntiDiagonal);
        }

        return (false, WinLineType.None);
    }

    public static MarkType GetCell(this MarkType[,] grid, int index, int gridSize)
    {
        var (row, column) = GetCoordinates(index, gridSize);
        return grid[row, column];
    }

    public static bool CheckDraw(this MarkType[,] grid, int gridSize)
    {
        for (var row = 0; row < gridSize; row++)
        {
            for (var column = 0; column < gridSize; column++)
            {
                if (grid[row, column] == MarkType.None)
                {
                    return false;
                }
            }
        }

        return true;
    }

    private static WinLineType ResoleLineTypeRow(int row) => (WinLineType) row + 6;

    private static WinLineType ResoleLineTypeColumn(int column) => (WinLineType) column + 3;

    private static bool CheckRow(this MarkType[,] grid, int row, int gridSize)
    {
        var type = grid[0, row];
        if (type == MarkType.None)
        {
            return false;
        }

        for (var i = 1; i < gridSize; i++)
        {
            if (grid[i, row] != type)
            {
                return false;
            }
        }

        return true;
    }

    private static bool CheckColum(this MarkType[,] grid, int column, int gridSize)
    {
        var type = grid[column, 0];
        if (type == MarkType.None)
        {
            return false;
        }

        for (var i = 1; i < gridSize; i++)
        {
            if (grid[column, i] != type)
            {
                return false;
            }
        }

        return true;
    }

    private static bool CheckDiagonal(this MarkType[,] grid, int gridSize)
    {
        var type = grid[0, 0];
        if (type == MarkType.None)
        {
            return false;
        }

        for (var i = 0; i < gridSize; i++)
        {
            if (grid[i, i] != type)
            {
                return false;
            }
        }

        return true;
    }

    private static bool CheckAntiDiagonal(this MarkType[,] grid, int gridSize)
    {
        var type = grid[0, gridSize - 1];
        if (type == MarkType.None)
        {
            return false;
        }

        for (var i = 0; i < gridSize; i++)
        {
            if (grid[i, gridSize - 1 - i] != type)
            {
                return false;
            }
        }

        return true;
    }
}