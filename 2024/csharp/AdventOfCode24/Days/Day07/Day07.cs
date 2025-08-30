using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode24.Days.Day07
{
    public class Day07 : ISolution
    {
        // Thread-safe cache for memoization
        private static readonly ConcurrentDictionary<string, bool> _memoCache = new();
        private static long _cacheHits = 0;

        public static void ClearCache()
        {
            _memoCache.Clear();
            Interlocked.Exchange(ref _cacheHits, 0);
        }
        public string Part1(string input)
        {
            var stopwatch = Stopwatch.StartNew();
            
            OutputHelper.WriteLineWithLevel($"CPU cores available: {Environment.ProcessorCount}", LogLevel.Info);
            var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var equations = ParseEquations(lines);
            
            OutputHelper.WriteLineWithLevel($"Processing {equations.Count} equations...", LogLevel.Info);
            
            long totalCalibrationResult = 0;
            long processed = 0;
            int validCount = 0;
            var totalEquations = equations.Count;
            
            var lockObj = new object();
            
            // Progress reporting task
            var cts = new CancellationTokenSource();
            var progressTask = Task.Run(async () =>
            {
                while (!cts.Token.IsCancellationRequested)
                {
                    var currentProcessed = Interlocked.Read(ref processed);
                    var percentage = (double)currentProcessed / totalEquations * 100;
                    var currentValid = 0;
                    lock (lockObj) { currentValid = validCount; }
                    var elapsed = stopwatch.Elapsed;
                    OutputHelper.WriteLineWithLevel($"Progress: {currentProcessed}/{totalEquations} ({percentage:F1}%) - Found {currentValid} valid equations - Elapsed: {elapsed.TotalSeconds:F1}s", LogLevel.Progress);
                    
                    try { await Task.Delay(1000, cts.Token); } catch { break; }
                }
            }, cts.Token);
            
            Parallel.ForEach(equations, equation =>
            {
                if (CanBeCalculated(equation.target, equation.numbers))
                {
                    lock (lockObj)
                    {
                        totalCalibrationResult += equation.target;
                        validCount++;
                        var currentProcessed = Interlocked.Read(ref processed);
                        OutputHelper.WriteLineWithLevel($"Processed {currentProcessed} equations - Valid equation #{validCount}: {equation.target} can be calculated from [{string.Join(", ", equation.numbers)}]", LogLevel.Result);
                    }
                }
                
                Interlocked.Increment(ref processed);
            });
            
            // Stop progress reporting
            cts.Cancel();
            progressTask.Wait(500);
            
            stopwatch.Stop();
            OutputHelper.WriteLineWithLevel($"Completed in {stopwatch.Elapsed.TotalSeconds:F2} seconds!", LogLevel.Result);
            OutputHelper.WriteLineWithLevel($"Found {validCount} valid equations with total calibration result: {totalCalibrationResult}", LogLevel.Result);
            OutputHelper.WriteLineWithLevel($"Cache hits: {Interlocked.Read(ref _cacheHits)}, Cache size: {_memoCache.Count}", LogLevel.Result);
            return totalCalibrationResult.ToString();
        }

        public string Part2(string input)
        {
            var stopwatch = Stopwatch.StartNew();
            
            OutputHelper.WriteLineWithLevel($"CPU cores available: {Environment.ProcessorCount}", LogLevel.Info);
            var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var equations = ParseEquations(lines);
            
            OutputHelper.WriteLineWithLevel($"Processing {equations.Count} equations with 3 operators (+, *, ||)...", LogLevel.Info);
            
            long totalCalibrationResult = 0;
            long processed = 0;
            int validCount = 0;
            var totalEquations = equations.Count;
            
            var lockObj = new object();
            
            // Progress reporting task
            var cts = new CancellationTokenSource();
            var progressTask = Task.Run(async () =>
            {
                while (!cts.Token.IsCancellationRequested)
                {
                    var currentProcessed = Interlocked.Read(ref processed);
                    var percentage = (double)currentProcessed / totalEquations * 100;
                    var currentValid = 0;
                    lock (lockObj) { currentValid = validCount; }
                    var elapsed = stopwatch.Elapsed;
                    OutputHelper.WriteLineWithLevel($"Progress: {currentProcessed}/{totalEquations} ({percentage:F1}%) - Found {currentValid} valid equations - Elapsed: {elapsed.TotalSeconds:F1}s", LogLevel.Progress);
                    
                    try { await Task.Delay(1000, cts.Token); } catch { break; }
                }
            }, cts.Token);
            
            Parallel.ForEach(equations, equation =>
            {
                if (CanBeCalculatedWithThreeOperators(equation.target, equation.numbers))
                {
                    lock (lockObj)
                    {
                        totalCalibrationResult += equation.target;
                        validCount++;
                        var currentProcessed = Interlocked.Read(ref processed);
                        OutputHelper.WriteLineWithLevel($"Processed {currentProcessed} equations - Valid equation #{validCount}: {equation.target} can be calculated from [{string.Join(", ", equation.numbers)}]", LogLevel.Result);
                    }
                }
                
                Interlocked.Increment(ref processed);
            });
            
            // Stop progress reporting
            cts.Cancel();
            progressTask.Wait(500);
            
            stopwatch.Stop();
            OutputHelper.WriteLineWithLevel($"Completed in {stopwatch.Elapsed.TotalSeconds:F2} seconds!", LogLevel.Result);
            OutputHelper.WriteLineWithLevel($"Found {validCount} valid equations with total calibration result: {totalCalibrationResult}", LogLevel.Result);
            OutputHelper.WriteLineWithLevel($"Cache hits: {Interlocked.Read(ref _cacheHits)}, Cache size: {_memoCache.Count}", LogLevel.Result);
            return totalCalibrationResult.ToString();
        }

        public bool CanBeCalculatedWithThreeOperators(long target, long[] numbers, bool useMathematicalConcat = false, bool useMemoization = false)
        {
            if (numbers.Length == 1)
                return numbers[0] == target;
            
            // Check memoization cache first
            if (useMemoization)
            {
                string cacheKey = $"{target}:{string.Join(",", numbers)}";
                if (_memoCache.TryGetValue(cacheKey, out bool cachedResult))
                {
                    Interlocked.Increment(ref _cacheHits);
                    OutputHelper.WriteLineWithLevel($"Cache HIT for {target}: [{string.Join(", ", numbers)}]", LogLevel.Debug);
                    return cachedResult;
                }
            }
            
            int operatorCount = numbers.Length - 1;
            int totalCombinations = (int)Math.Pow(3, operatorCount);
            
            OutputHelper.WriteLineWithLevel($"\nTesting equation {target}: [{string.Join(", ", numbers)}] with 3 operators", LogLevel.Debug);
            
            for (int combination = 0; combination < totalCombinations; combination++)
            {
                // Convert combination number to ternary (base-3) instead of binary
                string ternaryPattern = NumberHelper.ToTernary(combination, operatorCount);
                string expression = BuildTernaryExpressionString(numbers, combination);
                
                OutputHelper.WriteLineWithLevel($"combination = {combination} (ternary: {ternaryPattern}) → {expression}", LogLevel.Debug);
                
                // Thread.Sleep(100);
                
                long result = numbers[0];
                
                for (int opIndex = 0; opIndex < operatorCount; opIndex++)
                {
                    // We want to extract the digit at position opIndex from the ternary representation
                    // of 'combination' number

                    // Example: combination = 5, operatorCount = 3
                    // Step 1: Convert 5 to ternary with 3 digits → "012"
                    // Step 2: Get digit at position opIndex
                    //   - opIndex = 0 → digit "0" → operatorType = 0 (addition)
                    //   - opIndex = 1 → digit "1" → operatorType = 1 (multiplication)  
                    //   - opIndex = 2 → digit "2" → operatorType = 2 (concatenation)

                    int operatorType = NumberHelper.GetTernaryDigit(combination, opIndex, operatorCount);
                                        
                    if (operatorType == 0)
                        result += numbers[opIndex + 1];
                    else if (operatorType == 1)
                        result *= numbers[opIndex + 1];
                    else if (operatorType == 2)
                    {
                        if (useMathematicalConcat)
                        {
                            // Mathematical concatenation (faster)
                            int digits = numbers[opIndex + 1].ToString().Length;
                            result = result * (long)Math.Pow(10, digits) + numbers[opIndex + 1];
                        }
                        else
                        {
                            // String concatenation (original method)
                            var concat = result + numbers[opIndex + 1].ToString();
                            result = Convert.ToInt64(concat);
                        }
                    }
                    
                    // Early termination if result exceeds target
                    if (result > target)
                        break;
                }
                
                if (result == target)
                {
                    // Cache the positive result if using memoization
                    if (useMemoization)
                    {
                        string cacheKey = $"{target}:{string.Join(",", numbers)}";
                        _memoCache.TryAdd(cacheKey, true);
                        OutputHelper.WriteLineWithLevel($"Cached result TRUE for {target}", LogLevel.Debug);
                    }
                    
                    OutputHelper.WriteLineWithLevel($"✅ Found match!", LogLevel.Result);
                    return true;
                }
            }
            
            // Cache the negative result if using memoization
            if (useMemoization)
            {
                string cacheKey = $"{target}:{string.Join(",", numbers)}";
                _memoCache.TryAdd(cacheKey, false);
                OutputHelper.WriteLineWithLevel($"Cached result FALSE for {target}", LogLevel.Debug);
            }
            
            OutputHelper.WriteLineWithLevel($"❌ No valid combination found", LogLevel.Debug);
            return false;
        }

        private string BuildTernaryExpressionString(long[] numbers, int combination)
        {
            var parts = new List<string>();
            parts.Add(numbers[0].ToString());
            
            int operatorCount = numbers.Length - 1;
            for (int opIndex = 0; opIndex < operatorCount; opIndex++)
            {
                // TODO: Extract ternary digit instead of bit
                // Need to convert combination to base-3 and get digit at position
                                
                int operatorType = 0; // EXTRACT TERNARY DIGIT HERE (0=+, 1=*, 2=||)

                operatorType = opIndex % 3;
                
                string opSymbol = operatorType switch
                {
                    0 => "+",
                    1 => "*", 
                    2 => "||",
                    _ => "+"
                };
                
                parts.Add(opSymbol);
                parts.Add(numbers[opIndex + 1].ToString());
            }
            
            return string.Join(" ", parts);
        }

        public List<(long target, long[] numbers)> ParseEquations(string[] lines)
        {
            var equations = new List<(long target, long[] numbers)>();
            
            foreach (var line in lines)
            {
                var parts = line.Split(':', StringSplitOptions.RemoveEmptyEntries);
                var target = long.Parse(parts[0]);
                var numbers = parts[1].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                    .Select(long.Parse).ToArray();
                equations.Add((target, numbers));
            }
            
            return equations;
        }

        public bool CanBeCalculated(long target, long[] numbers)
        {
            if (numbers.Length == 1)
                return numbers[0] == target;
            
            int operatorCount = numbers.Length - 1;
            int totalCombinations = (int)Math.Pow(2, operatorCount);
            
            OutputHelper.WriteLineWithLevel($"\nTesting equation {target}: [{string.Join(", ", numbers)}]", LogLevel.Debug);
            
            for (int combination = 0; combination < totalCombinations; combination++)
            {
                string binaryPattern = Convert.ToString(combination, 2).PadLeft(operatorCount, '0');
                string expression = BuildExpressionString(numbers, combination);
                
                OutputHelper.WriteLineWithLevel($"combination = {combination} (binary: {binaryPattern}) → {expression}", LogLevel.Debug);
                
                long result = numbers[0];
                
                for (int opIndex = 0; opIndex < operatorCount; opIndex++)
                {
                    // Flip the bit reading to match left-to-right binary pattern
                    int bitPosition = operatorCount - 1 - opIndex;
                    bool isMultiply = (combination & (1 << bitPosition)) != 0;
                    
                    if (isMultiply)
                        result *= numbers[opIndex + 1];
                    else
                        result += numbers[opIndex + 1];
                    
                    // Early termination if result exceeds target
                    if (result > target)
                        break;
                }
                
                if (result == target)
                {
                    OutputHelper.WriteLineWithLevel($"✅ Found match!", LogLevel.Debug);
                    return true;
                }
            }
            
            OutputHelper.WriteLineWithLevel($"❌ No valid combination found", LogLevel.Debug);
            return false;
        }

        private string BuildExpressionString(long[] numbers, int combination)
        {
            var parts = new List<string>();
            parts.Add(numbers[0].ToString());
            
            int operatorCount = numbers.Length - 1;
            for (int opIndex = 0; opIndex < operatorCount; opIndex++)
            {
                // Flip the bit reading to match left-to-right binary pattern
                int bitPosition = operatorCount - 1 - opIndex;
                bool isMultiply = (combination & (1 << bitPosition)) != 0;
                parts.Add(isMultiply ? "*" : "+");
                parts.Add(numbers[opIndex + 1].ToString());
            }
            
            return string.Join(" ", parts);
        }
    }
}