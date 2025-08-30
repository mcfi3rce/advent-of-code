using AdventOfCode24.Days.Day08;

namespace AdventOfCode24.Tests;

public class Day08Tests
{
    private readonly Day08 _solution = new();

    [Fact]
    public void Part1_Sample_ReturnsExpectedResult()
    {
        var input = InputHelper.GetDay08Sample();
        var result = _solution.Part1(input);
        // Update with expected result when you know it
        Assert.Equal("0", result);
    }

    [Fact]
    public void Part1_RealInput_ReturnsResult()
    {
        var input = InputHelper.GetDay08Input();
        var result = _solution.Part1(input);
        // Add expected result once you know it
        Assert.Equal("0", result);
    }

    [Fact]
    public void Part2_Sample_ReturnsExpectedResult()
    {
        var input = InputHelper.GetDay08Sample();
        var result = _solution.Part2(input);
        // Update with expected result when you know it
        Assert.Equal("0", result);
    }

    [Fact]
    public void Part2_RealInput_ReturnsResult()
    {
        var input = InputHelper.GetDay08Input();
        var result = _solution.Part2(input);
        // Add expected result once you know it
        Assert.Equal("0", result);
    }
}