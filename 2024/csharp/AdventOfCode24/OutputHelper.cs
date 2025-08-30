using System;

namespace AdventOfCode24
{
    public static class OutputHelper
    {
        private static Action<string>? _outputWriter;

        public static void SetOutputWriter(Action<string>? outputWriter)
        {
            _outputWriter = outputWriter;
        }

        public static void WriteLine(string message)
        {
            _outputWriter?.Invoke(message);
            Console.WriteLine(message);
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