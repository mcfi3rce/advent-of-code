using System;
using System.Linq;

namespace AdventOfCode24.Days.Day10
{
    public class Day10 : ISolution
    {
        public string Part1(string input)
        {
            var grid = ParseGrid(input);
            
            // TODO: Implement Part 1
            return "0";
        }

        public string Part2(string input)
        {
            var grid = ParseGrid(input);
            
            // TODO: Implement Part 2
            return "0";
        }

        public int[,] ParseGrid(string input)
        {
            var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries)
                            .Select(line => line.Trim())
                            .Where(line => !string.IsNullOrEmpty(line))
                            .ToArray();

            if (lines.Length == 0)
                throw new ArgumentException("Input contains no valid lines");

            var height = lines.Length;
            var width = lines[0].Length;

            var grid = new int[height, width];

            for (int row = 0; row < height; row++)
            {
                if (lines[row].Length != width)
                    throw new ArgumentException($"Inconsistent row length at row {row}");

                for (int col = 0; col < width; col++)
                {
                    if (!char.IsDigit(lines[row][col]))
                        throw new ArgumentException($"Invalid character '{lines[row][col]}' at position ({row}, {col})");

                    grid[row, col] = int.Parse(lines[row][col].ToString());
                }
            }

            return grid;
        }
    }
}