using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreasuryChallenge;
public static class UniqueCodeGenerator
{
    static readonly StringBuilder Lines = new();
    static readonly Random random = new();
    static HashSet<string> HASH_UNIQUE_CODES = new();
    static readonly object varlock = false;
    const int ARRAY_CHAR_LENGTH = 26;
    const int UNIQUE_CODE_SIZE = 7;
    const int BATCH_SIZE = 100000;
    static readonly char[] CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray(0, ARRAY_CHAR_LENGTH);
    public static async Task<StringBuilder> Generate(int lines)
    {
        List<Task> tasks = new();

        var linesSubtract = lines;
        do
        {
            if (linesSubtract >= BATCH_SIZE)
                tasks.Add(Task.Run(() => AppendCodesByBatch(BATCH_SIZE)));
            else
                AppendCodesByBatch(linesSubtract);

            linesSubtract -= BATCH_SIZE;

        } while (linesSubtract > 0);

        if (tasks.Any())
            await Task.WhenAll(tasks);

        return Lines;
    }
    private static void AppendCodesByBatch(int batchSize)
    {
        for (var i = 0; i < batchSize; i++)
        {
            string line = GenerateUniqueCode();

            lock (varlock)
            {
                while (HASH_UNIQUE_CODES.Contains(line))
                {
                    line = GenerateUniqueCode();
                }
                HASH_UNIQUE_CODES.Add(line);
                Lines.Append(line);
                Lines.Append('\n');
            }
        }
    }
    static string GenerateUniqueCode()
    {
        string line = string.Empty;
        do
        {
            char c = CHARS[random.Next(0, ARRAY_CHAR_LENGTH)];
            if (!line.Contains(c))
                line += c;

        } while (line.Length <= UNIQUE_CODE_SIZE);

        return line;
    }

}

