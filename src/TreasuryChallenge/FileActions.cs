using System.IO;
using System.Text;

namespace TreasuryChallenge;
internal static class FileActions
{
    public static void RecordStringBuilder(StringBuilder value)
    {
        using FileStream stream = new("aleatory-file.txt", FileMode.Create, FileAccess.Write, FileShare.None, 900000096, true);
        using StreamWriter sw = new(stream);
        sw.Write(value);
    }
}
