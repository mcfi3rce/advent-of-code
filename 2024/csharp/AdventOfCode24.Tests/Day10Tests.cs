using AdventOfCode24.Days.Day10;

namespace AdventOfCode24.Tests;

public class Day10Tests
{
    private readonly Day10 _solution = new();

    #region ParseGrid Tests
    
    [Fact]
    public void ParseGrid_ValidInput_ReturnsCorrectGrid()
    {
        var input = "012\n345\n678";
        var grid = _solution.ParseGrid(input);
        
        Assert.Equal(3, grid.GetLength(0)); // height
        Assert.Equal(3, grid.GetLength(1)); // width
        
        Assert.Equal(0, grid[0, 0]);
        Assert.Equal(5, grid[1, 2]);
        Assert.Equal(8, grid[2, 2]);
    }

    [Fact]
    public void ParseGrid_EmptyInput_ThrowsException()
    {
        var input = "";
        Assert.Throws<ArgumentException>(() => _solution.ParseGrid(input));
    }

    [Fact]
    public void ParseGrid_InconsistentRowLength_ThrowsException()
    {
        var input = "012\n34\n567";
        Assert.Throws<ArgumentException>(() => _solution.ParseGrid(input));
    }

    #endregion

    [Fact]
    public void Part1_Sample_ReturnsExpectedResult()
    {
        var input = InputHelper.GetDay10Sample();
        var result = _solution.Part1(input);
        // Update with expected result when you know it
        Assert.Equal("0", result);
    }

    [Fact]
    public void Part1_Sample1_ReturnsExpectedResult()
    {
        var input = InputHelper.GetDay10Sample(1);
        var result = _solution.Part1(input);
        // Update with expected result when you know it
        Assert.Equal("0", result);
    }

    [Fact]
    public void Part1_Sample2_ReturnsExpectedResult()
    {
        var input = InputHelper.GetDay10Sample(2);
        var result = _solution.Part1(input);
        // Update with expected result when you know it
        Assert.Equal("0", result);
    }

    [Fact]
    public void Part1_Sample3_ReturnsExpectedResult()
    {
        var input = InputHelper.GetDay10Sample(3);
        var result = _solution.Part1(input);
        // Update with expected result when you know it
        Assert.Equal("0", result);
    }

    [Fact]
    public void Part1_Sample4_ReturnsExpectedResult()
    {
        var input = InputHelper.GetDay10Sample(4);
        var result = _solution.Part1(input);
        // Update with expected result when you know it
        Assert.Equal("0", result);
    }

    [Fact]
    public void Part1_RealInput_ReturnsResult()
    {
        var input = InputHelper.GetDay10Input();
        var result = _solution.Part1(input);
        // Add expected result once you know it
        Assert.Equal("0", result);
    }

    [Fact]
    public void Part2_Sample_ReturnsExpectedResult()
    {
        var input = InputHelper.GetDay10Sample();
        var result = _solution.Part2(input);
        // Update with expected result when you know it
        Assert.Equal("0", result);
    }

    [Fact]
    public void Part2_Sample1_ReturnsExpectedResult()
    {
        var input = InputHelper.GetDay10Sample(1);
        var result = _solution.Part2(input);
        // Update with expected result when you know it
        Assert.Equal("0", result);
    }

    [Fact]
    public void Part2_Sample2_ReturnsExpectedResult()
    {
        var input = InputHelper.GetDay10Sample(2);
        var result = _solution.Part2(input);
        // Update with expected result when you know it
        Assert.Equal("0", result);
    }

    [Fact]
    public void Part2_Sample3_ReturnsExpectedResult()
    {
        var input = InputHelper.GetDay10Sample(3);
        var result = _solution.Part2(input);
        // Update with expected result when you know it
        Assert.Equal("0", result);
    }

    [Fact]
    public void Part2_Sample4_ReturnsExpectedResult()
    {
        var input = InputHelper.GetDay10Sample(4);
        var result = _solution.Part2(input);
        // Update with expected result when you know it
        Assert.Equal("0", result);
    }

    [Fact]
    public void Part2_RealInput_ReturnsResult()
    {
        var input = InputHelper.GetDay10Input();
        var result = _solution.Part2(input);
        // Add expected result once you know it
        Assert.Equal("0", result);
    }
}