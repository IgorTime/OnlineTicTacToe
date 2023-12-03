using System.Text;

namespace TTT.Server.Utilities;

public class StringBuilderInstance
{
    private static readonly StringBuilder instance = new();
    
    public static StringBuilder Get()
    {
        instance.Clear();
        return instance;
    }
}