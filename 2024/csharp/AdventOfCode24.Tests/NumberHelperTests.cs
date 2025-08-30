using AdventOfCode24;

namespace AdventOfCode24.Tests;

public class NumberHelperTests
{
    [Fact]
    public void ToTernary_Zero_ReturnsZeroPadded()
    {
        var result = NumberHelper.ToTernary(0, 3);
        Assert.Equal("000", result);
    }

    [Fact]
    public void ToTernary_SimpleNumbers_ReturnsCorrectTernary()
    {
        // 0 = 0, 1 = 1, 2 = 2, 3 = 10, 4 = 11, 5 = 12, 6 = 20, 7 = 21, 8 = 22
        Assert.Equal("00", NumberHelper.ToTernary(0, 2));
        Assert.Equal("01", NumberHelper.ToTernary(1, 2));
        Assert.Equal("02", NumberHelper.ToTernary(2, 2));
        Assert.Equal("10", NumberHelper.ToTernary(3, 2));
        Assert.Equal("11", NumberHelper.ToTernary(4, 2));
        Assert.Equal("12", NumberHelper.ToTernary(5, 2));
        Assert.Equal("20", NumberHelper.ToTernary(6, 2));
        Assert.Equal("21", NumberHelper.ToTernary(7, 2));
        Assert.Equal("22", NumberHelper.ToTernary(8, 2));
    }

    [Fact]
    public void GetTernaryDigit_ExtractsCorrectDigits()
    {
        // Number 5 = "012" in ternary (length 3)
        Assert.Equal(0, NumberHelper.GetTernaryDigit(5, 0, 3)); // leftmost
        Assert.Equal(1, NumberHelper.GetTernaryDigit(5, 1, 3)); // middle
        Assert.Equal(2, NumberHelper.GetTernaryDigit(5, 2, 3)); // rightmost
    }

    [Fact]
    public void GetTernaryDigit_Number8_ExtractsCorrectDigits()
    {
        // Number 8 = "22" in ternary (length 2)
        Assert.Equal(2, NumberHelper.GetTernaryDigit(8, 0, 2)); // leftmost
        Assert.Equal(2, NumberHelper.GetTernaryDigit(8, 1, 2)); // rightmost
    }

    [Fact]
    public void TernarySequence_ShowsCountingPattern()
    {
        // Show how ternary counts for 2 operators (3^2 = 9 combinations)
        var patterns = new List<string>();
        for (int i = 0; i < 9; i++)
        {
            patterns.Add(NumberHelper.ToTernary(i, 2));
        }
        
        var expected = new[] { "00", "01", "02", "10", "11", "12", "20", "21", "22" };
        Assert.Equal(expected, patterns.ToArray());
    }

    [Theory]
    [InlineData(0, "00", 0, 0)] // 0 + 0
    [InlineData(1, "01", 0, 1)] // 0 + 1  
    [InlineData(2, "02", 0, 2)] // 0 + 2
    [InlineData(3, "10", 1, 0)] // 1 + 0
    [InlineData(4, "11", 1, 1)] // 1 + 1
    [InlineData(5, "12", 1, 2)] // 1 + 2
    [InlineData(6, "20", 2, 0)] // 2 + 0
    [InlineData(7, "21", 2, 1)] // 2 + 1
    [InlineData(8, "22", 2, 2)] // 2 + 2
    public void GetTernaryDigit_AllCombinationsFor2Digits_ExtractsCorrectly(int number, string expectedTernary, int expectedLeftDigit, int expectedRightDigit)
    {
        Assert.Equal(expectedTernary, NumberHelper.ToTernary(number, 2));
        Assert.Equal(expectedLeftDigit, NumberHelper.GetTernaryDigit(number, 0, 2));
        Assert.Equal(expectedRightDigit, NumberHelper.GetTernaryDigit(number, 1, 2));
    }
}