// Copyright (c) 2017. Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using IoT.Common.Encryption.Hash;
using IoT.Common.Encryption.Service;
using IoT.Common.Log;
using IoT.Common.Tools;
using IoT.Device.Coordinator;
using IoT.Device.Register;
using IoT.Device.Server;
using IoT.Device.Server.Configuration;

namespace IoT.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = "192.168.12.3";
            ushort port = 8000;

            var deviceListenerIpAddress = IPAddress.Parse(host);
            var deviceServerConfiguration = new DefaultDeviceServerConfiguration(deviceListenerIpAddress, port);

            var hashAlgorithm = new HMACSHA256Algorithm();
            var deviceRegister = new DefaultDeviceRegister(hashAlgorithm);
            deviceRegister.Register("d3eea2beeb292f7e57fbddb8cac330c8", "acd00f2964667ef33f23d3ce0d80117a");
            deviceRegister.Register("7bbabd1e128a7642d27757bf47012950", "68b3ca8f0a5eb99b1650f70069071abf");

            var logger = new ConsoleLogger();
            var encryptionService = new DefaultEncyptionService(deviceRegister);
            var deviceCoordinator = new DefaultDeviceCoordinator(encryptionService, logger);
            var deviceServer = new EdgeDeviceServer(deviceServerConfiguration, deviceCoordinator);

            deviceServer.StartAsync();

            WaitForExit();
        }

        private static byte[] FormatMessage(string message)
        {
            var mod = (message.Length + 3) % 16;
            var size = mod == 0 ? message.Length + 3 : message.Length + 3 + 16 - mod;
            var formatted = new byte[size];

            formatted[message.Length] = 23;
            formatted[message.Length + 1] = 56;
            formatted[message.Length + 2] = 10;

            Encoding.UTF8.GetBytes(message, 0, message.Length, formatted, 0);
            return formatted;
        }

        private static void WaitForExit()
        {
            var run = true;
            while (run)
            {
                var text = System.Console.ReadLine();
                if (string.IsNullOrEmpty(text))
                    continue;
                if (text.Trim().ToLower().Equals("exit"))
                {
                    run = false;
                }
            }
        }
    }
}