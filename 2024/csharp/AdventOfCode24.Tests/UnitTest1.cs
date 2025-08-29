using AdventOfCode24.Days.Day01;

namespace AdventOfCode24.Tests;

public class Day01Tests
{
    private readonly Day01 _solution = new();

    [Fact]
    public void Part1_Sample_ReturnsExpectedResult()
    {
        var input = InputHelper.GetDay01Sample();
        var result = _solution.Part1(input);
        Assert.Equal("11", result);
    }

    [Fact]
    public void Part1_RealInput_ReturnsResult()
    {
        var input = InputHelper.GetDay01Input();
        var result = _solution.Part1(input);
        
        // Add expected result once you know it
        Assert.Equal("2430334", result);
    }

    [Fact]
    public void Part2_Sample_ReturnsExpectedResult()
    {
        var input = InputHelper.GetDay01Sample();
        var result = _solution.Part2(input);
        Assert.Equal("31", result);
    }

    [Fact]
    public void Part2_RealInput_ReturnsResult()
    {
        var input = InputHelper.GetDay01Input();
        var result = _solution.Part2(input);
        // Add expected result once you know it
        Assert.Equal("28786472", result);
    }
}