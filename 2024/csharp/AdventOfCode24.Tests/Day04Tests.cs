using AdventOfCode24.Days.Day04;
using Xunit.Abstractions;

namespace AdventOfCode24.Tests;

public class Day04Tests
{
    private readonly Day04 _solution = new();
    private readonly ITestOutputHelper _output;

    public Day04Tests(ITestOutputHelper output) => _output = output;

    [Fact]
    public void Part1_Sample_ReturnsExpectedResult()
    {
        var input = InputHelper.GetDay04Sample();
        var result = _solution.Part1WithOutput(input, _output.WriteLine);
        // Update with expected result when you know it
        Assert.Equal("18", result);
    }

    [Fact]
    public void Part1_RealInput_ReturnsResult()
    {
        var input = InputHelper.GetDay04Input();
        var result = _solution.Part1WithOutput(input,  _output.WriteLine);
        // Add expected result once you know it
        Assert.Equal("2517", result);
    }

    [Fact]
    public void Part2_Sample_ReturnsExpectedResult()
    {
        var input = InputHelper.GetDay04Sample();
        var result = _solution.Part2(input);
        // Update with expected result when you know it
        Assert.Equal("9", result);
    }

    [Fact]
    public void Part2_RealInput_ReturnsResult()
    {
        var input = InputHelper.GetDay04Input();
        var result = _solution.Part2(input);
        // Add expected result once you know it
        Assert.Equal("0", result);
    }
}