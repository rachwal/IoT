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
                Console.WriteLine($"{DateTime.Now} | DEBUG | {message}");
            }
        }

        public void Info(object message)
        {
            if (logLevel <= LogLevel.Info)
            {
                Console.WriteLine($"{DateTime.Now} | INFO | {message}");
            }
        }

        public void Warn(object message)
        {
            if (logLevel <= LogLevel.Warn)
            {
                Console.WriteLine($"{DateTime.Now} | WARN | {message}");
            }
        }

        public void Error(object message)
        {
            if (logLevel <= LogLevel.Error)
            {
                Console.WriteLine($"{DateTime.Now} | ERROR | {message}");
            }
        }

        public void Fatal(object message)
        {
            if (logLevel <= LogLevel.Fatal)
            {
                Console.WriteLine($"{DateTime.Now} | FATAL | {message}");
            }
        }

        public void SetLevel(LogLevel level)
        {
            logLevel = level;
        }
    }
}