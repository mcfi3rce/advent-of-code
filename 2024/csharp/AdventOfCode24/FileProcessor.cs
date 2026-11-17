using System;

namespace AdventOfCode24
{
    public static class FileProcessor
    {
        public static void ProcessFile(string path)
        {
            int totalCount = 0;
            int nonEmptyCount = 0;

            foreach (var (line, index) in FileLineReader.ReadLinesWithIndex(path))
            {
                totalCount++;
                var trimmed = line.Trim();

                if (string.IsNullOrEmpty(trimmed) || trimmed.StartsWith('#'))
                {
                    continue;
                }

                nonEmptyCount++;
                var tokens = ParseLine(trimmed);
                Console.WriteLine($"Line {index}: tokens=[{string.Join(", ", tokens)}]");
            }

            Console.WriteLine($"Done. Lines processed: {totalCount}, non-empty/non-comment: {nonEmptyCount}.");
        }

        private static string[] ParseLine(string line)
        {
            return line
                .Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}