// Simple file reader and line-by-line parser for .NET 6 Console App
// Usage:
//   dotnet run -- <path-to-input-file>
// Behavior:
//   - Streams the file line by line (low memory footprint)
//   - Trims each line
//   - Skips empty lines and lines starting with '#'
//   - Demonstrates a simple parse: splits by whitespace into tokens
//   - Prints the line number and tokens; you can replace ParseLine with your own logic

using System;
using System.IO;
using System.Linq;
using AdventOfCode24;

static class Program
{
    static int Main(string[] args)
    {
        try
        {
            if (args.Length == 0)
            {
                Console.Error.WriteLine("Error: Please provide a file path.\nUsage: dotnet run -- <path-to-input-file>");
                return 1;
            }

            var path = args[0];
            FileProcessor.ProcessFile(path);
            return 0;
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.Error.WriteLine($"Access error: {ex.Message}");
            return 2;
        }
        catch (IOException ex)
        {
            Console.Error.WriteLine($"I/O error: {ex.Message}");
            return 3;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex}");
            return 99;
        }
    }
}