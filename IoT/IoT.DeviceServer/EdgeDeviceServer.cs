﻿// Copyright (c) 2017. Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

using System;
using System.Net;
using System.Net.Sockets;
using IoT.DeviceCoordinator;
using IoT.DeviceServer.Configuration;

namespace IoT.DeviceServer
{
    public class EdgeDeviceServer
    {
        private readonly IDeviceServerConfiguration configuration;
        private readonly IDeviceCoordinator deviceCoordinator;

        private TcpListener listener;

        public EdgeDeviceServer(IDeviceServerConfiguration serverConfiguration, IDeviceCoordinator coordinator)
        {
            deviceCoordinator = coordinator;
            configuration = serverConfiguration;
        }

        public void Start()
        {
            var endPoint = new IPEndPoint(configuration.Address, configuration.Port);
            listener = new TcpListener(endPoint);

            listener.Start();

            while (true)
            {
                var clientTask = listener.AcceptTcpClientAsync();
                
                deviceCoordinator.Handle(clientTask.Result);
            }
        }
    }
}