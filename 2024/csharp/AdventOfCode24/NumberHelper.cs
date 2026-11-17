using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode24
{
    public static class NumberHelper
    {
        public static string ToTernary(int number, int length)
        {
            if (number == 0) return new string('0', length);
            
            var digits = new List<char>();
            while (number > 0)
            {
                digits.Add((char)('0' + (number % 3)));
                number /= 3;
            }
            
            // Reverse to get correct order and pad with zeros
            digits.Reverse();
            var result = new string(digits.ToArray());
            return result.PadLeft(length, '0');
        }

        public static int GetTernaryDigit(int number, int position, int totalDigits)
        {
            // Convert to ternary and get digit at position (left-to-right)
            // Position 0 is leftmost digit
            var ternaryString = ToTernary(number, totalDigits);
            return int.Parse(ternaryString[position].ToString());
        }
    }
}