using AdventOfCode24.Days.Day07;

namespace AdventOfCode24.Tests;

public class Day07Tests
{
    private readonly Day07 _solution = new();

    [Fact]
    public void Part1_Sample_ReturnsExpectedResult()
    {
        var input = InputHelper.GetDay07Sample();
        var result = _solution.Part1(input);
        // Based on sample: 190, 3267, 292 are valid = 3749
        Assert.Equal("3749", result);
    }

    [Fact]
    public void Part1_RealInput_ReturnsResult()
    {
        var input = InputHelper.GetDay07Input();
        var result = _solution.Part1(input);

        OutputHelper.SetLogLevel(LogLevel.Result);
        // Add expected result once you know it
        Assert.Equal("4555081946288", result);
    }

    [Fact]
    public void Part2_Sample_ReturnsExpectedResult()
    {
        var input = InputHelper.GetDay07Sample();
        var result = _solution.Part2(input);

        OutputHelper.SetLogLevel(LogLevel.Info);
        // Update with expected result when you know it
        Assert.Equal("11387", result);
    }

    [Fact]
    public void Part2_RealInput_ReturnsResult()
    {
        var input = InputHelper.GetDay07Input();
        var result = _solution.Part2(input);

        OutputHelper.SetLogLevel(LogLevel.Progress);
        // Add expected result once you know it
        Assert.Equal("0", result);
    }

    [Fact]
    public void CanBeCalculated_Length5_TestsCombinations()
    {
        // 5 numbers = 4 operators = 2^4 = 16 combinations
        // 100: 1 2 3 4 5 -> try to make 100
        var result = _solution.CanBeCalculated(100, new long[] { 1, 2, 3, 4, 5 });
        // Will show all 16 binary combinations from 0000 to 1111
    }

    [Fact]
    public void CanBeCalculated_Length10_TestsCombinations()
    {
        // 10 numbers = 9 operators = 2^9 = 512 combinations
        // 1000: 1 2 3 4 5 6 7 8 9 10
        var result = _solution.CanBeCalculated(1000, new long[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
        // Will show all 512 binary combinations from 000000000 to 111111111
    }

    [Fact]
    public void CanBeCalculated_Length15_TestsCombinations()
    {
        // 15 numbers = 14 operators = 2^14 = 16,384 combinations
        // 5000: 1 2 3 4 5 6 7 8 9 10 11 12 13 14 15
        var result = _solution.CanBeCalculated(5000, new long[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 });
        // Will show all 16,384 binary combinations
    }

    [Fact]
    public void CanBeCalculated_Length20_TestsCombinations()
    {
        // 20 numbers = 19 operators = 2^19 = 524,288 combinations
        // 10000: 1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16 17 18 19 20
        var result = _solution.CanBeCalculated(10000,
            new long[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 });
        // Will show all 524,288 binary combinations - this will be a lot of output!
    }

    [Fact]
    public void CanBeCalculatedWithThreeOperators_SimpleConcatenation_ReturnsTrue()
    {
        // 156: 15 6 -> 15 || 6 = 156
        var result = _solution.CanBeCalculatedWithThreeOperators(156, new long[] { 15, 6 });
        Assert.True(result);
    }

    [Fact]
    public void CanBeCalculatedWithThreeOperators_ComplexConcatenation_ReturnsTrue()
    {
        // 7290: 6 8 6 15 -> 6 * 8 || 6 * 15 = 48 || 6 * 15 = 486 * 15 = 7290
        // Actually: 6 * 8 || 6 * 15 should be evaluated left to right as ((6 * 8) || 6) * 15 = (48 || 6) * 15 = 486 * 15 = 7290
        var result = _solution.CanBeCalculatedWithThreeOperators(7290, new long[] { 6, 8, 6, 15 });
        Assert.True(result);
    }

    [Fact]
    public void CanBeCalculatedWithThreeOperators_TernaryPatterns_ShowsAllCombinations()
    {
        // 3 numbers = 2 operators = 3^2 = 9 combinations
        // Should show ternary patterns: 00, 01, 02, 10, 11, 12, 20, 21, 22
        var result = _solution.CanBeCalculatedWithThreeOperators(999, new long[] { 1, 2, 3 });
        // This will show all 9 ternary combinations
    }

    [Fact]
    public void CanBeCalculatedWithThreeOperators_ImpossibleWithThreeOps_ReturnsFalse()
    {
        // Some equation that can't be solved even with concatenation
        var result = _solution.CanBeCalculatedWithThreeOperators(1, new long[] { 2, 3, 4 });
        Assert.False(result);
    }

    [Fact]
    public void Part2_PerformanceComparison_StringVsMathematicalConcat()
    {
        OutputHelper.SetLogLevel(LogLevel.Result);
        var input = InputHelper.GetDay07Input();
        
        // Test string concatenation method
        OutputHelper.WriteLineWithLevel("Testing with STRING concatenation method...", LogLevel.Result);
        var stopwatch1 = System.Diagnostics.Stopwatch.StartNew();
        var result1 = TestPart2WithMethod(input, false);
        stopwatch1.Stop();
        
        // Test mathematical concatenation method  
        OutputHelper.WriteLineWithLevel("Testing with MATHEMATICAL concatenation method...", LogLevel.Result);
        var stopwatch2 = System.Diagnostics.Stopwatch.StartNew();
        var result2 = TestPart2WithMethod(input, true);
        stopwatch2.Stop();
        
        OutputHelper.WriteLineWithLevel($"String method: {stopwatch1.Elapsed.TotalSeconds:F2} seconds", LogLevel.Result);
        OutputHelper.WriteLineWithLevel($"Math method: {stopwatch2.Elapsed.TotalSeconds:F2} seconds", LogLevel.Result);
        OutputHelper.WriteLineWithLevel($"Speedup: {stopwatch1.Elapsed.TotalSeconds / stopwatch2.Elapsed.TotalSeconds:F2}x", LogLevel.Result);
        
        // Both methods should return the same result
        Assert.Equal(result1, result2);
    }

    [Fact]
    public void Part2_PerformanceComparison_OriginalVsMemoization()
    {
        OutputHelper.SetLogLevel(LogLevel.Result);
        var input = InputHelper.GetDay07Input();
        
        // Test without memoization
        OutputHelper.WriteLineWithLevel("Testing WITHOUT memoization...", LogLevel.Result);
        var stopwatch1 = System.Diagnostics.Stopwatch.StartNew();
        var result1 = TestPart2WithMethod(input, true, useMemoization: false);
        stopwatch1.Stop();
        
        // Test with memoization
        OutputHelper.WriteLineWithLevel("Testing WITH memoization...", LogLevel.Result);
        var stopwatch2 = System.Diagnostics.Stopwatch.StartNew();
        var result2 = TestPart2WithMethod(input, true, useMemoization: true);
        stopwatch2.Stop();
        
        OutputHelper.WriteLineWithLevel($"No memoization: {stopwatch1.Elapsed.TotalSeconds:F2} seconds", LogLevel.Result);
        OutputHelper.WriteLineWithLevel($"With memoization: {stopwatch2.Elapsed.TotalSeconds:F2} seconds", LogLevel.Result);
        OutputHelper.WriteLineWithLevel($"Speedup: {stopwatch1.Elapsed.TotalSeconds / stopwatch2.Elapsed.TotalSeconds:F2}x", LogLevel.Result);
        
        // Both methods should return the same result
        Assert.Equal(result1, result2);
    }

    private string TestPart2WithMethod(string input, bool useMathematicalConcat, bool useMemoization = false)
    {
        // Clear cache before each test for fair comparison
        Day07.ClearCache();
        
        var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        var equations = _solution.ParseEquations(lines);
        
        long totalCalibrationResult = 0;
        
        foreach (var equation in equations)
        {
            if (_solution.CanBeCalculatedWithThreeOperators(equation.target, equation.numbers, useMathematicalConcat, useMemoization))
            {
                totalCalibrationResult += equation.target;
            }
        }
        
        return totalCalibrationResult.ToString();
    }
}