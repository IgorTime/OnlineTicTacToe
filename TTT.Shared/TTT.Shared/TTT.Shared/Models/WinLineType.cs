namespace TTT.Shared.Models
{
    public enum WinLineType : byte
    {
        None = 0,
        Diagonal = 1,
        AntiDiagonal = 2,
        ColLeft = 3,
        ColMiddle = 4,
        ColRight = 5,
        RowTop = 6,
        RowMiddle = 7,
        RowBottom = 8,
    }
}