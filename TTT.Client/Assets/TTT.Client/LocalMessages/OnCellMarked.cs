using TTT.Shared.Models;

namespace TTT.Client.LocalMessages
{
    public struct OnCellMarked
    {
        public string Actor { get; }
        public byte Index { get; }
        public MarkOutcome Outcome { get; }
        public WinLine WinLine { get; }

        public OnCellMarked(string actor, byte index, MarkOutcome outcome, WinLine winLine)
        {
            Actor = actor;
            Index = index;
            Outcome = outcome;
            WinLine = winLine;
        }
    }
}