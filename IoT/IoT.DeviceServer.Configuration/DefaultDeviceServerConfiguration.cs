// Copyright (c) 2017. Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

using System.Net;

namespace IoT.DeviceServer.Configuration
{
    public class DefaultDeviceServerConfiguration : IDeviceServerConfiguration
    {
        public DefaultDeviceServerConfiguration(IPAddress address, ushort port)
        {
            Address = address;
            Port = port;
        }

        public IPAddress Address { get; }

        public ushort Port { get; }
    }
}