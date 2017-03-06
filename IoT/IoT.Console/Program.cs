// Copyright (c) 2017. Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

using System.Net;
using IoT.Common.Encryption.Hash;
using IoT.Common.Encryption.Service;
using IoT.Common.Log;
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
            var deviceListenerIpAddress = IPAddress.Parse("192.168.12.3");
            var deviceServerConfiguration = new DefaultDeviceServerConfiguration(deviceListenerIpAddress, 8000);

            var hashAlgorithm = new HMACSHA256Algorithm();
            var deviceRegister = new DefaultDeviceRegister(hashAlgorithm);
            deviceRegister.Register("d3eea2beeb292f7e57fbddb8cac330c8", "acd00f2964667ef33f23d3ce0d80117a");
            deviceRegister.Register("7bbabd1e128a7642d27757bf47012950", "68b3ca8f0a5eb99b1650f70069071abf");

            var logger = new ConsoleLogger();
            var encryptionService = new DefaultEncyptionService(deviceRegister);
            var deviceCoordinator = new DefaultDeviceCoordinator(encryptionService, logger);
            var deviceServer = new EdgeDeviceServer(deviceServerConfiguration, deviceCoordinator);

            deviceServer.Start();
        }
    }
}