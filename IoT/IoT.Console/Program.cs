// Copyright (c) 2017. Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

using System.Net;
using IoT.Core.Encryption;
using IoT.DeviceServer;
using IoT.DeviceServer.Configuration;
using IoT.DeviceCoordinator;

namespace IoT.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var ipAddress = IPAddress.Parse("192.168.12.3");
            var configuration = new DefaultDeviceServerConfiguration(ipAddress, 8000);

            var hashAlgorithm = new HMACSHA256Algorithm();
            var coordinator = new DefaultDeviceCoordinator(hashAlgorithm);

            var deviceServer = new EdgeDeviceServer(configuration, coordinator);

            deviceServer.Start();
        }
    }
}