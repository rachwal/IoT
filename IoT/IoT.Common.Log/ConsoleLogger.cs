// Copyright (c) 2017. Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

using System;

namespace IoT.Common.Log
{
    public class ConsoleLogger : ILog
    {
        private LogLevel logLevel = LogLevel.All;

        public void Debug(object message)
        {
            if (logLevel <= LogLevel.Debug)
            {
                var color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"{DateTime.Now} | DEBUG | {message}");
                Console.ForegroundColor = color;
            }
        }

        public void Info(object message)
        {
            if (logLevel <= LogLevel.Info)
            {
                var color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"{DateTime.Now} | INFO | {message}");
                Console.ForegroundColor = color;
            }
        }

        public void Warn(object message)
        {
            if (logLevel <= LogLevel.Warn)
            {
                var color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"{DateTime.Now} | WARNING | {message}");
                Console.ForegroundColor = color;
            }
        }

        public void Error(object message)
        {
            if (logLevel <= LogLevel.Error)
            {
                var color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{DateTime.Now} | ERROR | {message}");
                Console.ForegroundColor = color;
            }
        }

        public void Fatal(object message)
        {
            if (logLevel <= LogLevel.Fatal)
            {
                var color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"{DateTime.Now} | FATAL | {message}");
                Console.ForegroundColor = color;
            }
        }

        public void SetLevel(LogLevel level)
        {
            logLevel = level;
        }
    }
}