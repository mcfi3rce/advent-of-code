using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode24.Days.Day03
{
    public class Day03 : ISolution
    {
        public string Part1(string input)
        {
            var pattern = @"mul\((\d+),(\d+)\)";
            var matches = Regex.Matches(input, pattern);

            int total = 0;
            foreach (Match match in matches)
            {
                int x = int.Parse(match.Groups[1].Value);
                int y = int.Parse(match.Groups[2].Value);
                total += x * y;
            }

            return total.ToString();
        }

        public string Part2(string input)
        {
            var pattern = @"(mul\((\d+),(\d+)\)|do\(\)|don't\(\))";
            var matches = Regex.Matches(input, pattern);

            int total = 0;
            bool enabled = true; // Multiplication starts enabled

            foreach (Match match in matches)
            {
                var instruction = match.Groups[1].Value;

                
                if (instruction == "do()")
                {
                    enabled = true;
                }
                else if (instruction == "don't()")
                {
                    enabled = false;
                }
                else if (instruction.StartsWith("mul(") && enabled)
                {
                    int x = int.Parse(match.Groups[2].Value);
                    int y = int.Parse(match.Groups[3].Value);
                    total += x * y;
                }
                Console.WriteLine($"Instruction{instruction}: {total}");
            }

            return total.ToString();
        }
    }
}