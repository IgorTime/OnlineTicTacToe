using TTT.Server.Utilities;
using TTT.Shared.Models;

namespace TTT.Server.GameLogic;

public struct Grid3X3
{
    private const short ROW_0_BIT_MASK = 7; //0000 0000 0000 0111
    private const short ROW_1_BIT_MASK = 56; //0000 0000 0011 1000
    private const short ROW_2_BIT_MASK = 448; //0000 0001 1100 0000
    private const short COL_0_BIT_MASK = 73; //0000 0000 0100 1001
    private const short COL_1_BIT_MASK = 146; //0000 0000 1010 0010
    private const short COL_2_BIT_MASK = 292; //0000 0001 0100 0100
    private const short DIAGONAL_BIT_MASK = 273; //0000 0001 0001 0001
    private const short ANTI_DIAGONAL_BIT_MASK = 84; //0000 0000 0101 0100
    private const short DRAW_BIT_MASK = 511; //0000 0011 1111 1111

    private readonly Dictionary<short, WinLine> winLineTypeMap = new()
    {
        {ROW_0_BIT_MASK, WinLine.RowTop},
        {ROW_1_BIT_MASK, WinLine.RowMiddle},
        {ROW_2_BIT_MASK, WinLine.RowBottom},
        {COL_0_BIT_MASK, WinLine.ColLeft},
        {COL_1_BIT_MASK, WinLine.ColMiddle},
        {COL_2_BIT_MASK, WinLine.ColRight},
        {DIAGONAL_BIT_MASK, WinLine.Diagonal},
        {ANTI_DIAGONAL_BIT_MASK, WinLine.AntiDiagonal},
    };

    private readonly int gridSize;
    private short xBitMap;
    private short oBitMap;

    public Grid3X3()
    {
        gridSize = 3;
        xBitMap = 0;
        oBitMap = 0;
    }

    public void MarkCell(in int x, in int y, in MarkType mark)
    {
        var index = GetIndex(x, y);
        MarkCell(index, mark);
    }

    public void MarkCell(in int index, in MarkType mark)
    {
        if (mark == MarkType.X)
        {
            xBitMap = xBitMap.SetBit(index);
        }
        else
        {
            oBitMap = oBitMap.SetBit(index);
        }
    }

    public readonly (bool, WinLine) CheckWin()
    {
        foreach (var (bitMask, winLineType) in winLineTypeMap)
        {
            if (CompareGridWithBitMask(bitMask))
            {
                return (true, winLineType);
            }
        }

        return (false, WinLine.None);
    }

    public readonly bool CheckDraw() => xBitMap + oBitMap == DRAW_BIT_MASK;

    public readonly MarkType GetCell(in int cellIndex)
    {
        var bitMask = (short) (1 << cellIndex);
        if ((xBitMap & bitMask) != 0)
        {
            return MarkType.X;
        }

        if ((oBitMap & bitMask) != 0)
        {
            return MarkType.O;
        }

        return MarkType.None;
    }

    public MarkType GetCell(in int x, in int y)
    {
        var index = GetIndex(x, y);
        return GetCell(index);
    }

    private readonly bool CompareGridWithBitMask(in short bitMask) =>
        xBitMap.CompareBitMask(bitMask) || 
        oBitMap.CompareBitMask(bitMask);

    private int GetIndex(in int x, in int y) => y * gridSize + x;
}