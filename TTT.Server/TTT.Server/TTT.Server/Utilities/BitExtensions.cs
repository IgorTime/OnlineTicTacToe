using System.Runtime.CompilerServices;

namespace TTT.Server.Utilities;

public static class BitExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short SetBit(this in short input, in int bitPosition)
    {
        return (short) ((ushort) input | (1 << bitPosition));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CompareBitMask(this in short input, in short bitMask)
    {
        return (input & bitMask) == bitMask;
    }
}