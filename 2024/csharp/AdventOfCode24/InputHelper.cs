using System;
using System.IO;
using System.Linq;

namespace AdventOfCode24
{
    public static class InputHelper
    {
        // Day 01
        public static string GetDay01Sample() => GetDayInput(1, true);
        public static string GetDay01Input() => GetDayInput(1, false);
        public static string[] GetDay01SampleLines() => GetDayInputLines(1, true);
        public static string[] GetDay01InputLines() => GetDayInputLines(1, false);

        // Day 02
        public static string GetDay02Sample() => GetDayInput(2, true);
        public static string GetDay02Input() => GetDayInput(2, false);
        public static string[] GetDay02SampleLines() => GetDayInputLines(2, true);
        public static string[] GetDay02InputLines() => GetDayInputLines(2, false);

        // Day 03
        public static string GetDay03Sample() => GetDayInput(3, true);
        public static string GetDay03Input() => GetDayInput(3, false);
        public static string[] GetDay03SampleLines() => GetDayInputLines(3, true);
        public static string[] GetDay03InputLines() => GetDayInputLines(3, false);

        // Day 04
        public static string GetDay04Sample() => GetDayInput(4, true);
        public static string GetDay04Input() => GetDayInput(4, false);
        public static string[] GetDay04SampleLines() => GetDayInputLines(4, true);
        public static string[] GetDay04InputLines() => GetDayInputLines(4, false);

        // Day 05
        public static string GetDay05Sample() => GetDayInput(5, true);
        public static string GetDay05Input() => GetDayInput(5, false);
        public static string[] GetDay05SampleLines() => GetDayInputLines(5, true);
        public static string[] GetDay05InputLines() => GetDayInputLines(5, false);

        // Day 06
        public static string GetDay06Sample() => GetDayInput(6, true);
        public static string GetDay06Input() => GetDayInput(6, false);
        public static string[] GetDay06SampleLines() => GetDayInputLines(6, true);
        public static string[] GetDay06InputLines() => GetDayInputLines(6, false);

        // Day 07
        public static string GetDay07Sample() => GetDayInput(7, true);
        public static string GetDay07Input() => GetDayInput(7, false);
        public static string[] GetDay07SampleLines() => GetDayInputLines(7, true);
        public static string[] GetDay07InputLines() => GetDayInputLines(7, false);

        // Day 08
        public static string GetDay08Sample() => GetDayInput(8, true);
        public static string GetDay08Input() => GetDayInput(8, false);
        public static string[] GetDay08SampleLines() => GetDayInputLines(8, true);
        public static string[] GetDay08InputLines() => GetDayInputLines(8, false);

        // Day 09
        public static string GetDay09Sample() => GetDayInput(9, true);
        public static string GetDay09Input() => GetDayInput(9, false);
        public static string[] GetDay09SampleLines() => GetDayInputLines(9, true);
        public static string[] GetDay09InputLines() => GetDayInputLines(9, false);

        // Day 10
        public static string GetDay10Sample() => GetDayInput(10, true);
        public static string GetDay10Sample(int sampleNumber) => GetDayInput(10, true, sampleNumber);
        public static string GetDay10Input() => GetDayInput(10, false);
        public static string[] GetDay10SampleLines() => GetDayInputLines(10, true);
        public static string[] GetDay10SampleLines(int sampleNumber) => GetDayInputLines(10, true, sampleNumber);
        public static string[] GetDay10InputLines() => GetDayInputLines(10, false);

        // Day 11
        public static string GetDay11Sample() => GetDayInput(11, true);
        public static string GetDay11Input() => GetDayInput(11, false);
        public static string[] GetDay11SampleLines() => GetDayInputLines(11, true);
        public static string[] GetDay11InputLines() => GetDayInputLines(11, false);

        // Day 12
        public static string GetDay12Sample() => GetDayInput(12, true);
        public static string GetDay12Input() => GetDayInput(12, false);
        public static string[] GetDay12SampleLines() => GetDayInputLines(12, true);
        public static string[] GetDay12InputLines() => GetDayInputLines(12, false);

        // Day 13
        public static string GetDay13Sample() => GetDayInput(13, true);
        public static string GetDay13Input() => GetDayInput(13, false);
        public static string[] GetDay13SampleLines() => GetDayInputLines(13, true);
        public static string[] GetDay13InputLines() => GetDayInputLines(13, false);

        // Day 14
        public static string GetDay14Sample() => GetDayInput(14, true);
        public static string GetDay14Input() => GetDayInput(14, false);
        public static string[] GetDay14SampleLines() => GetDayInputLines(14, true);
        public static string[] GetDay14InputLines() => GetDayInputLines(14, false);

        // Day 15
        public static string GetDay15Sample() => GetDayInput(15, true);
        public static string GetDay15Input() => GetDayInput(15, false);
        public static string[] GetDay15SampleLines() => GetDayInputLines(15, true);
        public static string[] GetDay15InputLines() => GetDayInputLines(15, false);

        // Day 16
        public static string GetDay16Sample() => GetDayInput(16, true);
        public static string GetDay16Input() => GetDayInput(16, false);
        public static string[] GetDay16SampleLines() => GetDayInputLines(16, true);
        public static string[] GetDay16InputLines() => GetDayInputLines(16, false);

        // Day 17
        public static string GetDay17Sample() => GetDayInput(17, true);
        public static string GetDay17Input() => GetDayInput(17, false);
        public static string[] GetDay17SampleLines() => GetDayInputLines(17, true);
        public static string[] GetDay17InputLines() => GetDayInputLines(17, false);

        // Day 18
        public static string GetDay18Sample() => GetDayInput(18, true);
        public static string GetDay18Input() => GetDayInput(18, false);
        public static string[] GetDay18SampleLines() => GetDayInputLines(18, true);
        public static string[] GetDay18InputLines() => GetDayInputLines(18, false);

        // Day 19
        public static string GetDay19Sample() => GetDayInput(19, true);
        public static string GetDay19Input() => GetDayInput(19, false);
        public static string[] GetDay19SampleLines() => GetDayInputLines(19, true);
        public static string[] GetDay19InputLines() => GetDayInputLines(19, false);

        // Day 20
        public static string GetDay20Sample() => GetDayInput(20, true);
        public static string GetDay20Input() => GetDayInput(20, false);
        public static string[] GetDay20SampleLines() => GetDayInputLines(20, true);
        public static string[] GetDay20InputLines() => GetDayInputLines(20, false);

        // Day 21
        public static string GetDay21Sample() => GetDayInput(21, true);
        public static string GetDay21Input() => GetDayInput(21, false);
        public static string[] GetDay21SampleLines() => GetDayInputLines(21, true);
        public static string[] GetDay21InputLines() => GetDayInputLines(21, false);

        // Day 22
        public static string GetDay22Sample() => GetDayInput(22, true);
        public static string GetDay22Input() => GetDayInput(22, false);
        public static string[] GetDay22SampleLines() => GetDayInputLines(22, true);
        public static string[] GetDay22InputLines() => GetDayInputLines(22, false);

        // Day 23
        public static string GetDay23Sample() => GetDayInput(23, true);
        public static string GetDay23Input() => GetDayInput(23, false);
        public static string[] GetDay23SampleLines() => GetDayInputLines(23, true);
        public static string[] GetDay23InputLines() => GetDayInputLines(23, false);

        // Day 24
        public static string GetDay24Sample() => GetDayInput(24, true);
        public static string GetDay24Input() => GetDayInput(24, false);
        public static string[] GetDay24SampleLines() => GetDayInputLines(24, true);
        public static string[] GetDay24InputLines() => GetDayInputLines(24, false);

        // Day 25
        public static string GetDay25Sample() => GetDayInput(25, true);
        public static string GetDay25Input() => GetDayInput(25, false);
        public static string[] GetDay25SampleLines() => GetDayInputLines(25, true);
        public static string[] GetDay25InputLines() => GetDayInputLines(25, false);

        private static string GetDayInput(int day, bool useSample, int? sampleNumber = null)
        {
            string fileName;
            if (useSample)
            {
                fileName = sampleNumber.HasValue ? $"sample{sampleNumber.Value}.txt" : "sample.txt";
            }
            else
            {
                fileName = "input.txt";
            }
            
            var dayFolder = $"Day{day:D2}";
            var basePath = GetBasePath();
            var filePath = Path.Combine(basePath, "Days", dayFolder, fileName);
            
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Input file not found: {filePath}");
            }
           
            
            return File.ReadAllText(filePath).Trim();
        }

        private static string[] GetDayInputLines(int day, bool useSample, int? sampleNumber = null)
        {
            return GetDayInput(day, useSample, sampleNumber)
                .Split('\n', StringSplitOptions.RemoveEmptyEntries)
                .Select(line => line.TrimEnd('\r'))
                .ToArray();
        }

        private static string GetBasePath()
        {
            var currentDir = Directory.GetCurrentDirectory();
            
            // Look for the solution folder first, then find the AdventOfCode24 project folder
            var dir = new DirectoryInfo(currentDir);
            while (dir != null)
            {
                // Look for solution file to identify solution root
                if (Directory.GetFiles(dir.FullName, "*.sln").Length > 0)
                {
                    var projectPath = Path.Combine(dir.FullName, "AdventOfCode24");
                    if (Directory.Exists(projectPath) && Directory.Exists(Path.Combine(projectPath, "Days")))
                    {
                        return projectPath;
                    }
                }
                
                // If we're already in the AdventOfCode24 project folder
                if (dir.Name == "AdventOfCode24" && Directory.Exists(Path.Combine(dir.FullName, "Days")))
                {
                    return dir.FullName;
                }
                
                dir = dir.Parent;
            }
            
            
            // Fallback to current directory
            return currentDir;
        }
    }
}