using AdventOfCode24.Days.Day06;

namespace AdventOfCode24.Tests;

public class Day06Tests
{
    private readonly Day06 _solution = new();

    [Fact]
    public void Part1_Sample_ReturnsExpectedResult()
    {
        var input = InputHelper.GetDay06Sample();
        var result = _solution.Part1(input);
        // Based on AoC Day 6 sample, guard visits 41 unique positions
        Assert.Equal("41", result);
    }

    [Fact]
    public void Part1_RealInput_ReturnsResult()
    {
        var input = InputHelper.GetDay06Input();
        var result = _solution.Part1(input);
        // Add expected result once you know it
        Assert.Equal("5564", result);
    }

    [Fact]
    public void Part2_Sample_ReturnsExpectedResult()
    {
        var input = InputHelper.GetDay06Sample();
        var result = _solution.Part2(input);
        // Update with expected result when you know it
        Assert.Equal("0", result);
    }

    [Fact]
    public void Part2_RealInput_ReturnsResult()
    {
        var input = InputHelper.GetDay06Input();
        var result = _solution.Part2(input);
        // Add expected result once you know it
        Assert.Equal("1976", result);
    }

    [Fact]
    public void ParseInput_SampleData_ParsesCorrectly()
    {
        var input = @"....#.....
.........#
..........
..#.......
.......#..
..........
.#..^.....
........#.
#.........
......#...".Split('\n');

        var (grid, guardPos, guardDir) = _solution.ParseInput(input);

        // Verify grid dimensions
        Assert.Equal(10, grid.Length);
        Assert.Equal(10, grid[0].Length);

        // Verify guard starting position (should be at row 6, col 4)
        Assert.Equal(6, guardPos.row);
        Assert.Equal(4, guardPos.col);

        // Verify guard direction (^ means up = 0)
        Assert.Equal(0, guardDir);

        // Verify guard position was replaced with '.'
        Assert.Equal('.', grid[6][4]);

        // Verify some obstacles are in correct positions
        Assert.Equal('#', grid[0][4]); // First obstacle
        Assert.Equal('#', grid[6][1]); // Obstacle in guard's row
        Assert.Equal('#', grid[8][0]); // Bottom left obstacle
    }
}