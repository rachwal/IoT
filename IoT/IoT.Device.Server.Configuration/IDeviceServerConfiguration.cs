// Copyright (c) 2017. Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

using System.Net;

namespace IoT.Device.Server.Configuration
{
    public interface IDeviceServerConfiguration
    {
        IPAddress Address { get; }
        ushort Port { get; }
    }
}