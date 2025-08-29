using System;
using System.Linq;

namespace AdventOfCode24.Days.Day02
{
    public class Day02 : ISolution
    {
        public string Part1(string input)
        {
            var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            int safeCount = 0;

            foreach (var line in lines)
            {
                var levels = line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                if (IsSafe(levels))
                {
                    safeCount++;
                }
            }

            return safeCount.ToString();
        }

        public string Part2(string input)
        {
            var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            int safeCount = 0;

            foreach (var line in lines)
            {
                var levels = line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                if (IsSafeTolerant(levels))
                {
                    safeCount++;
                }
            }

            return safeCount.ToString();
        }

        private bool IsSafe(int[] levels)
        {
            if (levels.Length < 2) return true;

            bool isIncreasing = levels[1] > levels[0];
            
            for (int i = 1; i < levels.Length; i++)
            {
                int diff = levels[i] - levels[i - 1];
                int absDiff = Math.Abs(diff);

                // Check if difference is between 1 and 3
                if (absDiff < 1 || absDiff > 3)
                    return false;

                // Check if direction is consistent
                if (isIncreasing && diff <= 0)
                    return false;
                if (!isIncreasing && diff >= 0)
                    return false;
            }

            return true;
        }

                

        private bool IsSafeTolerant(int[] levels)
        {
            // First check if it's already safe without removing any level
            if (IsSafe(levels))
                return true;

            // Try removing each level one at a time
            for (int i = 0; i < levels.Length; i++)
            {
                var dampened = levels.Where((_, index) => index != i).ToArray();
                if (IsSafe(dampened))
                {
                    return true;
                }
            }

            return false;
        }
        
    }
}