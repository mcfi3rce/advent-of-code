using System;

namespace AdventOfCode24
{
    public enum LogLevel { Debug, Info, Progress, Result }

    public static class OutputHelper
    {
        private static Action<string>? _outputWriter;
        private static LogLevel _currentLevel = LogLevel.Info;

        public static void SetOutputWriter(Action<string>? outputWriter)
        {
            _outputWriter = outputWriter;
        }

        public static void SetLogLevel(LogLevel level)
        {
            _currentLevel = level;
        }

        public static void WriteLine(string message)
        {
            _outputWriter?.Invoke(message);
            Console.WriteLine(message);
        }

        public static void WriteLineWithLevel(string message, LogLevel level = LogLevel.Info)
        {
            if (level >= _currentLevel)
            {
                _outputWriter?.Invoke(message);
                Console.WriteLine(message);
            }
        }

        public static void Write(string message)
        {
            _outputWriter?.Invoke(message);
            Console.Write(message);
        }

        public static void Clear()
        {
            _outputWriter = null;
        }
    }
}