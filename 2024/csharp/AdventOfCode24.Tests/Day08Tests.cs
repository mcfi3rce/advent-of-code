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
        // Add expected result once you know it
        Assert.Equal("14", result);
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
        Assert.Equal("32", result);
    }

    [Fact]
    public void Part2_RealInput_ReturnsResult()
    {
        var input = InputHelper.GetDay08Input();
        var result = _solution.Part2(input);
        // Add expected result once you know it
        Assert.Equal("1333", result);
    }

    [Fact]
    public void IsAntinodePosition_SimpleVerticalPair_ReturnsCorrectAntinodes()
    {
        var antennaGroups = new Dictionary<char, List<(int row, int col)>>
        {
            ['A'] = new() { (1, 1), (3, 1) }
        };

        // Antenna positions: (1,1) and (3,1) - vertical line
        // Vector: (2, 0) - going down
        // Antinode1: (1,1) - (2,0) = (-1,1) - above first antenna
        // Antinode2: (3,1) + (2,0) = (5,1) - below second antenna

        var result1 = _solution.IsAntinodePosition(-1, 1, antennaGroups);
        var result2 = _solution.IsAntinodePosition(5, 1, antennaGroups);
        var result3 = _solution.IsAntinodePosition(2, 1, antennaGroups); // between antennas

        Assert.True(result1);
        Assert.True(result2);
        Assert.False(result3);
    }

    [Fact]
    public void IsAntinodePosition_SimpleHorizontalPair_ReturnsCorrectAntinodes()
    {
        var antennaGroups = new Dictionary<char, List<(int row, int col)>>
        {
            ['B'] = new() { (2, 1), (2, 4) }
        };

        // Antenna positions: (2,1) and (2,4) - horizontal line
        // Vector: (0, 3) - going right
        // Antinode1: (2,1) - (0,3) = (2,-2) - left of first antenna
        // Antinode2: (2,4) + (0,3) = (2,7) - right of second antenna

        var result1 = _solution.IsAntinodePosition(2, -2, antennaGroups);
        var result2 = _solution.IsAntinodePosition(2, 7, antennaGroups);
        var result3 = _solution.IsAntinodePosition(2, 2, antennaGroups); // between antennas

        Assert.True(result1);
        Assert.True(result2);
        Assert.False(result3);
    }

    [Fact]
    public void IsAntinodePosition_DiagonalPair_ReturnsCorrectAntinodes()
    {
        var antennaGroups = new Dictionary<char, List<(int row, int col)>>
        {
            ['C'] = new() { (1, 1), (3, 3) }
        };

        // Antenna positions: (1,1) and (3,3) - diagonal line
        // Vector: (2, 2) - going down-right
        // Antinode1: (1,1) - (2,2) = (-1,-1) - up-left of first antenna
        // Antinode2: (3,3) + (2,2) = (5,5) - down-right of second antenna

        var result1 = _solution.IsAntinodePosition(-1, -1, antennaGroups);
        var result2 = _solution.IsAntinodePosition(5, 5, antennaGroups);
        var result3 = _solution.IsAntinodePosition(2, 2, antennaGroups); // between antennas

        Assert.True(result1);
        Assert.True(result2);
        Assert.False(result3);
    }

    [Fact]
    public void IsAntinodePosition_MultipleAntennaTypes_OnlyChecksRelevantType()
    {
        var antennaGroups = new Dictionary<char, List<(int row, int col)>>
        {
            ['A'] = new() { (1, 1), (3, 1) },
            ['B'] = new() { (2, 2), (2, 4) }
        };

        // This position is an antinode for type A antennas
        var result = _solution.IsAntinodePosition(-1, 1, antennaGroups);
        Assert.True(result);
    }

    [Fact]
    public void IsAntinodePosition_ThreeAntennasSameType_ChecksAllPairs()
    {
        var antennaGroups = new Dictionary<char, List<(int row, int col)>>
        {
            ['A'] = new() { (0, 0), (1, 1), (2, 2) }
        };

        // Three antennas create multiple pairs:
        // Pair 1: (0,0) and (1,1) -> antinodes at (-1,-1) and (2,2)
        // Pair 2: (0,0) and (2,2) -> antinodes at (-2,-2) and (4,4)  
        // Pair 3: (1,1) and (2,2) -> antinodes at (0,0) and (3,3)

        var result1 = _solution.IsAntinodePosition(-1, -1, antennaGroups); // from pair 1
        var result2 = _solution.IsAntinodePosition(4, 4, antennaGroups);   // from pair 2
        var result3 = _solution.IsAntinodePosition(0, 0, antennaGroups);   // from pair 3 (antenna becomes antinode)
        var result4 = _solution.IsAntinodePosition(1, 0, antennaGroups);   // not an antinode

        Assert.True(result1);
        Assert.True(result2);
        Assert.True(result3);
        Assert.False(result4);
    }

    [Fact]
    public void IsAntinodePosition_NoAntennas_ReturnsFalse()
    {
        var antennaGroups = new Dictionary<char, List<(int row, int col)>>();

        var result = _solution.IsAntinodePosition(0, 0, antennaGroups);
        Assert.False(result);
    }

    [Fact]
    public void IsAntinodePosition_SingleAntenna_ReturnsFalse()
    {
        var antennaGroups = new Dictionary<char, List<(int row, int col)>>
        {
            ['A'] = new() { (1, 1) }
        };

        // Single antenna can't create antinodes (need pairs)
        var result = _solution.IsAntinodePosition(0, 0, antennaGroups);
        Assert.False(result);
    }
}