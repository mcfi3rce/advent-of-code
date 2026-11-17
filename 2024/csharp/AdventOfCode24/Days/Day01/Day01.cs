using System;
using System.Linq;

namespace AdventOfCode24.Days.Day01
{
    public class Day01 : ISolution
    {
        public string Part1(string input)
        {
            var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var leftList = new List<int>();
            var rightList = new List<int>();
            
            foreach (var line in lines)
            {
                var parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length >= 2)
                {
                    leftList.Add(int.Parse(parts[0]));
                    rightList.Add(int.Parse(parts[1]));
                }
            }
            
            leftList.Sort();
            rightList.Sort();
            
            var totalDistance = leftList.Zip(rightList, (left, right) => Math.Abs(left - right)).Sum();
            
            return totalDistance.ToString();
        }

        public string Part2(string input)
        {
            var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var leftList = new List<int>();
            var rightList = new List<int>();
            
            foreach (var line in lines)
            {
                var parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length >= 2)
                {
                    leftList.Add(int.Parse(parts[0]));
                    rightList.Add(int.Parse(parts[1]));
                }
            }
            
            var rightCounts = rightList.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
            
            var similarityScore = leftList.Sum(left => left * rightCounts.GetValueOrDefault(left, 0));
            
            return similarityScore.ToString();
        }
    }
}