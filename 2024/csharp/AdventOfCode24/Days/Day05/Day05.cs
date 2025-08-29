using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode24.Days.Day05
{
    public class Day05 : ISolution
    {
        public string Part1(string input)
        {
            var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            
            var (rules, sequences) = ParseInput(lines);
            
            int sum = 0;
            foreach (var sequence in sequences)
            {
                if (IsValidSequence(sequence, rules))
                {
                    int middleIndex = sequence.Count / 2;
                    sum += sequence[middleIndex];
                }
            }
            
            return sum.ToString();
        }

        public string Part2(string input)
        {
            var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var (rules, sequences) = ParseInput(lines);
            int sum = 0;
            
            foreach (var sequence in sequences)
            {
                if (!IsValidSequence(sequence, rules))
                {
                    var correctedSequence = SortSequenceByRules(sequence, rules);
                    int middleIndex = correctedSequence.Count / 2;
                    sum += correctedSequence[middleIndex];
                }
            }
            
            return sum.ToString();
        }

        private (HashSet<(int, int)> rules, List<List<int>> sequences) ParseInput(string[] lines)
        {
            var rules = new HashSet<(int, int)>();
            var sequences = new List<List<int>>();
            bool parsingRules = true;

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    parsingRules = false;
                    continue;
                }

                if (parsingRules)
                {
                    var parts = line.Split('|');
                    rules.Add((int.Parse(parts[0]), int.Parse(parts[1])));
                }
                else
                {
                    var sequence = line.Split(',').Select(int.Parse).ToList();
                    sequences.Add(sequence);
                }
            }

            return (rules, sequences);
        }

        private bool IsValidSequence(List<int> sequence, HashSet<(int, int)> rules)
        {
            for (int i = 0; i < sequence.Count; i++)
            {
                for (int j = i + 1; j < sequence.Count; j++)
                {
                    if (rules.Contains((sequence[j], sequence[i])))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private List<int> SortSequenceByRules(List<int> sequence, HashSet<(int, int)> rules)
        {
            var sortedSequence = new List<int>(sequence);
            
            sortedSequence.Sort((a, b) =>
            {
                if (rules.Contains((a, b))) return -1;
                if (rules.Contains((b, a))) return 1;
                return 0;
            });
            
            return sortedSequence;
        }
    }
}