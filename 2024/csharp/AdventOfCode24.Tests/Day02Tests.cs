using AdventOfCode24.Days.Day02;

namespace AdventOfCode24.Tests;

public class Day02Tests
{
    private readonly Day02 _solution = new();

    [Fact]
    public void Part1_Sample_ReturnsExpectedResult()
    {
        var input = InputHelper.GetDay02Sample();
        var result = _solution.Part1(input);
        // Update with expected result when you know it
        Assert.Equal("2", result);
    }

    [Fact]
    public void Part1_RealInput_ReturnsResult()
    {
        var input = InputHelper.GetDay02Input();
        var result = _solution.Part1(input);
        // Add expected result once you know it
        Assert.Equal("526", result);
    }

    [Fact]
    public void Part2_Sample_ReturnsExpectedResult()
    {
        var input = InputHelper.GetDay02Sample();
        var result = _solution.Part2(input);
        // Update when Part2 is implemented
        Assert.Equal("4", result);
    }

    [Fact]
    public void Part2_RealInput_ReturnsResult()
    {
        var input = InputHelper.GetDay02Input();
        var result = _solution.Part2(input);
        // Update when Part2 is implemented
        Assert.Equal("566", result);
    }
}