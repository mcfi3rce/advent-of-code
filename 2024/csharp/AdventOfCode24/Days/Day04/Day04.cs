using System;
using System.Linq;

namespace AdventOfCode24.Days.Day04
{
    public class Day04 : ISolution
    {
        public string Part1(string input)
        {
            return Part1WithOutput(input, null);
        }

        public string Part1WithOutput(string input, Action<string> outputWriter)
        {
            OutputHelper.SetOutputWriter(outputWriter);
            var grid = input.Split('\n', StringSplitOptions.RemoveEmptyEntries)
                           .Select(line => line.ToCharArray())
                           .ToArray();
            
            // 8 directions: right, left, down, up, down-right, down-left, up-right, up-left
            var directions = new (int dx, int dy, string name)[]
            {
                (0, 1, "right"),
                (0, -1, "left"),  
                (1, 0, "down"),
                (-1, 0, "up"),
                (1, 1, "down-right"),
                (1, -1, "down-left"),
                (-1, 1, "up-right"),
                (-1, -1, "up-left")
            };
            
            string target = "XMAS";
            int count = 0;
            
            for (int row = 0; row < grid.Length; row++)
            {
                for (int col = 0; col < grid[row].Length; col++)
                {
                    foreach (var (dx, dy, name) in directions)
                    {
                        if (CheckWord(grid, row, col, dx, dy, target))
                        {
                            count++;
                            var message = $"\n=== XMAS #{count} found ===\nLocation: ({row}, {col}) going {name}";
                            OutputHelper.WriteLine(message);
                            
                            var gridDisplay = GetWordInGrid(grid, row, col, dx, dy, target);
                            OutputHelper.WriteLine(gridDisplay);
                        }
                    }
                }
            }
            
            var totalMessage = $"\nTotal XMAS found: {count}";
            OutputHelper.WriteLine(totalMessage);
            OutputHelper.Clear();
            return count.ToString();
        }

        private string GetWordInGrid(char[][] grid, int startRow, int startCol, int dx, int dy, string word)
        {
            // Create a copy of the grid filled with dots
            var visualGrid = new char[grid.Length][];
            for (int i = 0; i < grid.Length; i++)
            {
                visualGrid[i] = new char[grid[i].Length];
                for (int j = 0; j < grid[i].Length; j++)
                {
                    visualGrid[i][j] = '.';
                }
            }
            
            // Mark the found word
            for (int i = 0; i < word.Length; i++)
            {
                int row = startRow + i * dx;
                int col = startCol + i * dy;
                visualGrid[row][col] = word[i];
            }
            
            // Return the grid as a string
            return string.Join("\n", visualGrid.Select(row => new string(row)));
        }

        private bool CheckWord(char[][] grid, int startRow, int startCol, int dx, int dy, string word)
        {
            for (int i = 0; i < word.Length; i++)
            {
                int row = startRow + i * dx;
                int col = startCol + i * dy;
                
                // Check bounds
                if (row < 0 || row >= grid.Length || col < 0 || col >= grid[row].Length)
                    return false;
                    
                // Check character
                if (grid[row][col] != word[i])
                    return false;
            }
            
            return true;
        }

        public string Part2(string input)
        {
            var grid = input.Split('\n', StringSplitOptions.RemoveEmptyEntries)
                           .Select(line => line.ToCharArray())
                           .ToArray();
            
            int count = 0;
            
            // Check each position as potential center of X-MAS
            for (int row = 1; row < grid.Length - 1; row++)
            {
                for (int col = 1; col < grid[row].Length - 1; col++)
                {
                    // Center must be 'A'
                    if (grid[row][col] == 'A')
                    {
                        if (IsXmas(grid, row, col))
                        {
                            count++;
                        }
                    }
                }
            }
            
            return count.ToString();
        }

       

        private bool IsXmas(char[][] grid, int centerRow, int centerCol)
        {
            // Check both diagonals for MAS or SAM
            // Top-left to bottom-right diagonal
            char topLeft = grid[centerRow - 1][centerCol - 1];
            char bottomRight = grid[centerRow + 1][centerCol + 1];
            bool diagonal1 = (topLeft == 'M' && bottomRight == 'S') || (topLeft == 'S' && bottomRight == 'M');
            
            // Top-right to bottom-left diagonal  
            char topRight = grid[centerRow - 1][centerCol + 1];
            char bottomLeft = grid[centerRow + 1][centerCol - 1];
            bool diagonal2 = (topRight == 'M' && bottomLeft == 'S') || (topRight == 'S' && bottomLeft == 'M');
            
            return diagonal1 && diagonal2;
        }

       
    }
}