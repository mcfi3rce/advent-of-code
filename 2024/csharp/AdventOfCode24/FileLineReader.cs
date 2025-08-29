using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode24
{
    /// <summary>
    /// Shared utilities for reading text files in a streaming, line-by-line manner.
    /// </summary>
    public static class FileLineReader
    {
        /// <summary>
        /// Streams lines from a file and yields each line with a 1-based index.
        /// </summary>
        /// <param name="path">Full or relative path to the input file.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="path"/> is null.</exception>
        /// <exception cref="FileNotFoundException">Thrown when the file does not exist.</exception>
        /// <exception cref="IOException">I/O related exceptions during reading.</exception>
        public static IEnumerable<(string line, int index)> ReadLinesWithIndex(string path)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            if (!File.Exists(path)) throw new FileNotFoundException("File not found.", path);

            int i = 0;
            foreach (var line in File.ReadLines(path))
            {
                i++;
                yield return (line, i);
            }
        }

        /// <summary>
        /// Streams raw lines from a file.
        /// </summary>
        /// <param name="path">Full or relative path to the input file.</param>
        public static IEnumerable<string> ReadLines(string path)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            if (!File.Exists(path)) throw new FileNotFoundException("File not found.", path);

            return File.ReadLines(path);
        }

        /// <summary>
        /// Streams trimmed, non-empty, non-comment (starting with '#') lines with index.
        /// </summary>
        /// <param name="path">Full or relative path to the input file.</param>
        /// <param name="commentPrefix">Lines starting with this string will be skipped. Default '#'.</param>
        public static IEnumerable<(string line, int index)> ReadContentLinesWithIndex(string path, string commentPrefix = "#")
        {
            foreach (var (line, idx) in ReadLinesWithIndex(path))
            {
                var trimmed = line?.Trim() ?? string.Empty;
                if (trimmed.Length == 0) continue;
                if (!string.IsNullOrEmpty(commentPrefix) && trimmed.StartsWith(commentPrefix)) continue;
                yield return (trimmed, idx);
            }
        }
    }
}
