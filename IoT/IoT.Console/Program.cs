// Copyright (c) 2017. Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

using System.Net;
using IoT.Common.Encryption;
using IoT.Common.Log;
using IoT.DeviceServer;
using IoT.DeviceServer.Configuration;
using IoT.DeviceCoordinator;
using IoT.DeviceRegister;

namespace IoT.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var deviceListenerIpAddress = IPAddress.Parse("192.168.12.3");
            var deviceServerConfiguration = new DefaultDeviceServerConfiguration(deviceListenerIpAddress, 8000);

            var hashAlgorithm = new HMACSHA256Algorithm();
            var deviceRegister = new DefaultDeviceRegister(hashAlgorithm);
            deviceRegister.Register("d3eea2beeb292f7e57fbddb8cac330c8", "acd00f2964667ef33f23d3ce0d80117a");

            var logger = new ConsoleLogger();

            var deviceCoordinator = new DefaultDeviceCoordinator(deviceRegister, logger);
            var deviceServer = new EdgeDeviceServer(deviceServerConfiguration, deviceCoordinator);

            deviceServer.Start();
        }
    }
}