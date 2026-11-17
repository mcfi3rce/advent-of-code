using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode24.Days.Day08
{
    public class Day08 : ISolution
    {
        public string Part1(string input)
        {
            var stopwatch = Stopwatch.StartNew();
            OutputHelper.WriteLineWithLevel($"CPU cores available: {Environment.ProcessorCount}", LogLevel.Info);
            
            var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var grid = ParseGrid(lines);
            var antennaGroups = GroupAntennasByCharacter(grid);
            
            OutputHelper.WriteLineWithLevel($"Grid size: {grid.Length}x{grid[0].Length}", LogLevel.Info);
            OutputHelper.WriteLineWithLevel($"Antenna groups: {antennaGroups.Count}", LogLevel.Info);
            
            var totalPositions = grid.Length * grid[0].Length;
            var antinodesFound = new ConcurrentBag<(int row, int col)>();
            long processed = 0;
            
            // Progress reporting background task
            var cts = new CancellationTokenSource();
            var progressTask = Task.Run(async () =>
            {
                while (!cts.Token.IsCancellationRequested)
                {
                    var currentProcessed = Interlocked.Read(ref processed);
                    var percentage = (double)currentProcessed / totalPositions * 100;
                    var elapsed = stopwatch.Elapsed;
                    OutputHelper.WriteLineWithLevel($"Progress: {currentProcessed}/{totalPositions} ({percentage:F1}%) - Found {antinodesFound.Count} antinodes - Elapsed: {elapsed.TotalSeconds:F1}s", LogLevel.Progress);
                    
                    try { await Task.Delay(1000, cts.Token); } catch { break; }
                }
            }, cts.Token);
            
            // PARALLELISM WILL GO HERE:
            // Replace this sequential nested loop with Parallel.For
            // Each thread will process different rows simultaneously
            // Use ConcurrentBag.Add() to safely collect results from all threads
            // Use Interlocked.Increment() to safely update progress counter
            
            for (int row = 0; row < grid.Length; row++)
            {
                for (int col = 0; col < grid[0].Length; col++)
                {
                    if (IsAntinodePosition(row, col, antennaGroups))
                    {
                        antinodesFound.Add((row, col));
                    }
                    Interlocked.Increment(ref processed);
                }
            }
            
            cts.Cancel();
            progressTask.Wait(500);
            
            var uniqueAntinodes = antinodesFound.Distinct().Count();
            
            stopwatch.Stop();
            OutputHelper.WriteLineWithLevel($"Completed in {stopwatch.Elapsed.TotalSeconds:F2} seconds!", LogLevel.Result);
            OutputHelper.WriteLineWithLevel($"Found {uniqueAntinodes} unique antinode positions", LogLevel.Result);
            
            return uniqueAntinodes.ToString();
        }

        public string Part2(string input)
        {
            var stopwatch = Stopwatch.StartNew();
            OutputHelper.WriteLineWithLevel($"CPU cores available: {Environment.ProcessorCount}", LogLevel.Info);
            
            var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var grid = ParseGrid(lines);
            var antennaGroups = GroupAntennasByCharacter(grid);
            
            OutputHelper.WriteLineWithLevel($"Grid size: {grid.Length}x{grid[0].Length}", LogLevel.Info);
            OutputHelper.WriteLineWithLevel($"Antenna groups: {antennaGroups.Count}", LogLevel.Info);
            
            var totalPositions = grid.Length * grid[0].Length;
            var antinodesFound = new ConcurrentBag<(int row, int col)>();
            long processed = 0;
            
            var cts = new CancellationTokenSource();
            var progressTask = Task.Run(async () =>
            {
                while (!cts.Token.IsCancellationRequested)
                {
                    var currentProcessed = Interlocked.Read(ref processed);
                    var percentage = (double)currentProcessed / totalPositions * 100;
                    var elapsed = stopwatch.Elapsed;
                    OutputHelper.WriteLineWithLevel($"Progress: {currentProcessed}/{totalPositions} ({percentage:F1}%) - Found {antinodesFound.Count} harmonic antinodes - Elapsed: {elapsed.TotalSeconds:F1}s", LogLevel.Progress);
                    
                    try { await Task.Delay(1000, cts.Token); } catch { break; }
                }
            }, cts.Token);
            
            // PARALLELISM WILL GO HERE FOR PART 2:
            // Replace this sequential nested loop with Parallel.For
            // Each thread will process different rows simultaneously
            // Use ConcurrentBag.Add() to safely collect results from all threads
            // Use Interlocked.Increment() to safely update progress counter
            
            for (int row = 0; row < grid.Length; row++)
            {
                for (int col = 0; col < grid[0].Length; col++)
                {
                    if (IsHarmonicAntinodePosition(row, col, antennaGroups, grid.Length, grid[0].Length))
                    {
                        antinodesFound.Add((row, col));
                    }
                    Interlocked.Increment(ref processed);
                }
            }
            
            cts.Cancel();
            progressTask.Wait(500);
            
            var uniqueAntinodes = antinodesFound.Distinct().Count();
            
            stopwatch.Stop();
            OutputHelper.WriteLineWithLevel($"Completed in {stopwatch.Elapsed.TotalSeconds:F2} seconds!", LogLevel.Result);
            OutputHelper.WriteLineWithLevel($"Found {uniqueAntinodes} unique harmonic antinode positions", LogLevel.Result);
            
            // Debug: Print final board with antinodes
            PrintFinalBoard(grid, antinodesFound.Distinct().ToHashSet(), antennaGroups);
            
            return uniqueAntinodes.ToString();
        }

        private char[][] ParseGrid(string[] lines)
        {
            return lines.Select(line => line.ToCharArray()).ToArray();
        }

        private Dictionary<char, List<(int row, int col)>> GroupAntennasByCharacter(char[][] grid)
        {
            var groups = new Dictionary<char, List<(int row, int col)>>();
            
            for (int row = 0; row < grid.Length; row++)
            {
                for (int col = 0; col < grid[row].Length; col++)
                {
                    char cell = grid[row][col];
                    if (cell != '.' && char.IsLetterOrDigit(cell))
                    {
                        if (!groups.ContainsKey(cell))
                            groups[cell] = new List<(int row, int col)>();
                        
                        groups[cell].Add((row, col));
                    }
                }
            }
            
            return groups;
        }

        public bool IsAntinodePosition(int testRow, int testCol, Dictionary<char, List<(int row, int col)>> antennaGroups)
        {
            foreach (var group in antennaGroups.Values)
            {
                for (int i = 0; i < group.Count; i++)
                {
                    for (int j = i + 1; j < group.Count; j++)
                    {
                        var antenna1 = group[i];
                        var antenna2 = group[j];
                        
                        // Calculate vector from antenna1 to antenna2
                        var vector = (row: antenna2.row - antenna1.row, col: antenna2.col - antenna1.col);
                        
                        // Check if test position is an antinode
                        // Antinode1: antenna1 - vector (on opposite side of antenna1)
                        var antinode1 = (row: antenna1.row - vector.row, col: antenna1.col - vector.col);
                        
                        // Antinode2: antenna2 + vector (on opposite side of antenna2)  
                        var antinode2 = (row: antenna2.row + vector.row, col: antenna2.col + vector.col);
                        
                        if ((testRow == antinode1.row && testCol == antinode1.col) ||
                            (testRow == antinode2.row && testCol == antinode2.col))
                        {
                            return true;
                        }
                    }
                }
            }
            
            return false;
        }

        public bool IsHarmonicAntinodePosition(int testRow, int testCol, Dictionary<char, List<(int row, int col)>> antennaGroups, int gridHeight, int gridWidth)
        {
            foreach (var group in antennaGroups.Values)
            {
                if (group.Count < 2) continue;
                
                for (int i = 0; i < group.Count; i++)
                {
                    for (int j = i + 1; j < group.Count; j++)
                    {
                        var antenna1 = group[i];
                        var antenna2 = group[j];
                        
                        // For Part 2: any point collinear with two antennas is an antinode
                        if (IsCollinear(testRow, testCol, antenna1.row, antenna1.col, antenna2.row, antenna2.col))
                        {
                            return true;
                        }
                    }
                }
            }
            
            return false;
        }

        private bool IsCollinear(int x1, int y1, int x2, int y2, int x3, int y3)
        {
            // Three points are collinear if the cross product of vectors is zero
            // Vector from point1 to point2: (x2-x1, y2-y1)
            // Vector from point1 to point3: (x3-x1, y3-y1)
            // Cross product: (x2-x1)*(y3-y1) - (y2-y1)*(x3-x1) = 0
            return (x2 - x1) * (y3 - y1) - (y2 - y1) * (x3 - x1) == 0;
        }

        private void PrintFinalBoard(char[][] grid, HashSet<(int row, int col)> antinodes, Dictionary<char, List<(int row, int col)>> antennaGroups)
        {
            OutputHelper.WriteLineWithLevel("\n=== FINAL BOARD WITH ANTINODES ===", LogLevel.Result);
            
            for (int row = 0; row < grid.Length; row++)
            {
                var line = "";
                for (int col = 0; col < grid[row].Length; col++)
                {
                    if (antinodes.Contains((row, col)))
                    {
                        // Check if this position is also an antenna
                        bool isAntenna = false;
                        foreach (var group in antennaGroups.Values)
                        {
                            if (group.Contains((row, col)))
                            {
                                isAntenna = true;
                                break;
                            }
                        }
                        line += isAntenna ? grid[row][col] : '#'; // Show antenna if it's both antenna and antinode
                    }
                    else
                    {
                        line += grid[row][col];
                    }
                }
                OutputHelper.WriteLineWithLevel(line, LogLevel.Result);
            }
            
            OutputHelper.WriteLineWithLevel($"Total antinodes marked: {antinodes.Count}", LogLevel.Result);
        }
    }
}