using AdventOfCode24.Days.Day05;

namespace AdventOfCode24.Tests;

public class Day05Tests
{
    private readonly Day05 _solution = new();

    [Fact]
    public void Part1_Sample_ReturnsExpectedResult()
    {
        var input = InputHelper.GetDay05Sample();
        var result = _solution.Part1(input);
        // Update with expected result when you know it
        Assert.Equal("143", result);
    }

    [Fact]
    public void Part1_RealInput_ReturnsResult()
    {
        var input = InputHelper.GetDay05Input();
        var result = _solution.Part1(input);
        // Add expected result once you know it
        Assert.Equal("6034", result);
    }

    [Fact]
    public void Part2_Sample_ReturnsExpectedResult()
    {
        var input = InputHelper.GetDay05Sample();
        var result = _solution.Part2(input);
        // Update with expected result when you know it
        Assert.Equal("123", result);
    }

    [Fact]
    public void Part2_RealInput_ReturnsResult()
    {
        var input = InputHelper.GetDay05Input();
        var result = _solution.Part2(input);
        // Add expected result once you know it
        Assert.Equal("6305", result);
    }
}