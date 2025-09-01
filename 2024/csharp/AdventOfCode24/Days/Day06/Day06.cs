using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode24.Days.Day06
{
    public class Day06 : ISolution
    {
        public string Part1(string input)
        {
            return Part1WithOutput(input, null);
        }

        public string Part1WithOutput(string input, System.Action<string> outputWriter)
        {
            OutputHelper.SetOutputWriter(outputWriter);
            var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var (grid, guardPos, guardDir) = ParseInput(lines);
            
            var visitedPositions = SimulateGuardMovement(grid, guardPos, guardDir);
            OutputHelper.Clear();
            return visitedPositions.Count.ToString();
        }

        public string Part2(string input)
        {
            var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var (originalGrid, guardPos, guardDir) = ParseInput(lines);
            
            // Get all empty positions where we could place obstacles
            var emptyPositions = GetEmptyPositions(originalGrid);
            OutputHelper.WriteLine($"CPU cores available: {Environment.ProcessorCount}");
            OutputHelper.WriteLine($"Testing {emptyPositions.Count} possible obstacle positions...");
            
            long loopCount = 0;
            long processed = 0;
            var totalPositions = emptyPositions.Count;
            
            // Progress reporting task
            var cts = new CancellationTokenSource();
            var progressTask = Task.Run(async () =>
            {
                while (!cts.Token.IsCancellationRequested)
                {
                    var currentProcessed = Interlocked.Read(ref processed);
                    var percentage = (double)currentProcessed / totalPositions * 100;
                    var currentLoops = Interlocked.Read(ref loopCount);
                    OutputHelper.WriteLine($"Progress: {currentProcessed}/{totalPositions} ({percentage:F1}%) - Found {currentLoops} loops so far");
                    
                    try { await Task.Delay(1000, cts.Token); } catch { break; }
                }
            }, cts.Token);
            
            // Parallel brute force: test each position
            Parallel.ForEach(emptyPositions, position =>
            {
                var testGrid = CloneGrid(originalGrid);
                testGrid[position.row][position.col] = '#'; // Add obstacle
                
                if (CausesInfiniteLoop(testGrid, guardPos, guardDir))
                {
                    Interlocked.Increment(ref loopCount);
                }
                
                Interlocked.Increment(ref processed);
            });
            
            // Stop progress reporting
            cts.Cancel();
            progressTask.Wait(500); // Wait briefly for cleanup
            
            OutputHelper.WriteLine($"Completed! Found {loopCount} positions that cause infinite loops.");
            return loopCount.ToString();
        }

        public (char[][] grid, (int row, int col) guardPos, int guardDir) ParseInput(string[] lines)
        {
            var grid = lines.Select(line => line.ToCharArray()).ToArray();
            (int row, int col) guardPos = (-1, -1);
            int guardDir = 0; // 0=up, 1=right, 2=down, 3=left
            
            // Find guard starting position and direction
            for (int row = 0; row < grid.Length; row++)
            {
                for (int col = 0; col < grid[row].Length; col++)
                {
                    char cell = grid[row][col];
                    if (cell == '^' || cell == '>' || cell == 'v' || cell == '<')
                    {
                        guardPos = (row, col);
                        guardDir = cell switch
                        {
                            '^' => 0, // up
                            '>' => 1, // right
                            'v' => 2, // down
                            '<' => 3, // left
                            _ => 0
                        };
                        grid[row][col] = '.'; // Replace guard with empty space
                        break;
                    }
                }
                if (guardPos.row != -1) break;
            }
            
            return (grid, guardPos, guardDir);
        }

        private HashSet<(int row, int col)> SimulateGuardMovement(char[][] grid, (int row, int col) guardPos, int guardDir)
        {
            var visited = new HashSet<(int row, int col)>();
            var directions = new (int dRow, int dCol)[] { (-1, 0), (0, 1), (1, 0), (0, -1) }; // up, right, down, left
            var dirChars = new char[] { '^', '>', 'v', '<' };
            
            int currentRow = guardPos.row;
            int currentCol = guardPos.col;
            int currentDir = guardDir;
            int stepCount = 0;

            while (true)
            {
                // Mark current position as visited
                visited.Add((currentRow, currentCol));
                
                // Print current state
                PrintBoard(grid, visited, currentRow, currentCol, currentDir, dirChars, stepCount++);
                
                // Calculate next position
                var (dRow, dCol) = directions[currentDir];
                int nextRow = currentRow + dRow;
                int nextCol = currentCol + dCol;
                
                // Check if guard would leave the grid
                if (nextRow < 0 || nextRow >= grid.Length || nextCol < 0 || nextCol >= grid[0].Length)
                {
                    OutputHelper.WriteLine($"\nGuard exits the grid at step {stepCount}!");
                    break; // Guard leaves the area
                }
                
                // Check if there's an obstacle ahead
                if (grid[nextRow][nextCol] == '#')
                {
                    // Turn right (rotate 90 degrees clockwise)
                    currentDir = (currentDir + 1) % 4;
                    OutputHelper.WriteLine($"Hit obstacle at ({nextRow},{nextCol}), turning right to face {dirChars[currentDir]}");
                }
                else
                {
                    // Move forward
                    currentRow = nextRow;
                    currentCol = nextCol;
                }
                
                // Safety check to prevent infinite loops
                if (stepCount > 10000)
                {
                    OutputHelper.WriteLine("Breaking due to step limit");
                    break;
                }
            }
            
            return visited;
        }

        private void PrintBoard(char[][] grid, HashSet<(int row, int col)> visited, int guardRow, int guardCol, int guardDir, char[] dirChars, int stepCount)
        {
            OutputHelper.WriteLine($"\n=== Step {stepCount} - Guard at ({guardRow},{guardCol}) facing {dirChars[guardDir]} ===");
            
            for (int row = 0; row < grid.Length; row++)
            {
                var line = "";
                for (int col = 0; col < grid[row].Length; col++)
                {
                    if (row == guardRow && col == guardCol)
                    {
                        line += dirChars[guardDir]; // Show guard with direction
                    }
                    else if (visited.Contains((row, col)))
                    {
                        line += 'X'; // Show visited positions
                    }
                    else
                    {
                        line += grid[row][col]; // Show original grid
                    }
                }
                OutputHelper.WriteLine(line);
            }
            
            OutputHelper.WriteLine($"Visited positions so far: {visited.Count}");
            // Thread.Sleep(500); // Pause for visibility
        }

        private char[][] CloneGrid(char[][] grid)
        {
            return grid.Select(row => row.ToArray()).ToArray();
        }

        private List<(int row, int col)> GetEmptyPositions(char[][] grid)
        {
            var positions = new List<(int row, int col)>();
            for (int row = 0; row < grid.Length; row++)
            {
                for (int col = 0; col < grid[row].Length; col++)
                {
                    if (grid[row][col] == '.')
                    {
                        positions.Add((row, col));
                    }
                }
            }
            return positions;
        }

        private bool CausesInfiniteLoop(char[][] grid, (int row, int col) guardPos, int guardDir)
        {
            var visitedStates = new HashSet<(int row, int col, int dir)>();
            var directions = new (int dRow, int dCol)[] { (-1, 0), (0, 1), (1, 0), (0, -1) }; // up, right, down, left

            int currentRow = guardPos.row;
            int currentCol = guardPos.col;
            int currentDir = guardDir;

            while (true)
            {
                // Check if we've seen this state before (position + direction)
                var state = (currentRow, currentCol, currentDir);
                if (visitedStates.Contains(state))
                {
                    return true; // Found a loop!
                }
                visitedStates.Add(state);

                // Calculate next position
                var (dRow, dCol) = directions[currentDir];
                int nextRow = currentRow + dRow;
                int nextCol = currentCol + dCol;

                // Check if guard would leave the grid
                if (nextRow < 0 || nextRow >= grid.Length || nextCol < 0 || nextCol >= grid[0].Length)
                {
                    return false; // Guard exits, no loop
                }

                // Check if there's an obstacle ahead
                if (grid[nextRow][nextCol] == '#')
                {
                    // Turn right (rotate 90 degrees clockwise)
                    currentDir = (currentDir + 1) % 4;
                }
                else
                {
                    // Move forward
                    currentRow = nextRow;
                    currentCol = nextCol;
                }

                // Safety check to prevent infinite loops (just in case)
                if (visitedStates.Count > grid.Length * grid[0].Length * 4)
                {
                    return true; // Probably a loop
                }
            }
        }
    }
}