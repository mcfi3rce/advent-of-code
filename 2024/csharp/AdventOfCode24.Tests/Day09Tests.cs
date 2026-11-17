using AdventOfCode24.Days.Day09;

namespace AdventOfCode24.Tests;

public class Day09Tests
{
    private readonly Day09 _solution = new();

    [Fact]
    public void Part1_Sample_ReturnsExpectedResult()
    {
        var input = InputHelper.GetDay09Sample();
        var result = _solution.Part1(input);
        // Update with expected result when you know it
        Assert.Equal("1928", result);
    }

    [Fact]
    public void Part1_RealInput_ReturnsResult()
    {
        var input = InputHelper.GetDay09Input();
        var result = _solution.Part1(input);
        // Add expected result once you know it
        Assert.Equal("6307275788409", result);
    }

    [Fact]
    public void Part2_Sample_ReturnsExpectedResult()
    {
        var input = InputHelper.GetDay09Sample();
        var result = _solution.Part2(input);
        // Update with expected result when you know it
        Assert.Equal("2858", result);
    }

    [Fact]
    public void Part2_RealInput_ReturnsResult()
    {
        var input = InputHelper.GetDay09Input();
        var result = _solution.Part2(input);
        // Add expected result once you know it
        Assert.Equal("6327174563252", result);
    }
}