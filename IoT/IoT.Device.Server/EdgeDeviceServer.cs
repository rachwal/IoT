// Copyright (c) 2017. Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

using System.Net;
using System.Net.Sockets;
using IoT.Device.Coordinator;
using IoT.Device.Server.Configuration;

namespace IoT.Device.Server
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

        public async void StartAsync()
        {
            var endPoint = new IPEndPoint(configuration.Address, configuration.Port);

            listener = new TcpListener(endPoint);
            listener.Start();

            while (true)
            {
                var socket = await listener.AcceptSocketAsync();

                deviceCoordinator.Handle(socket);
            }
        }
    }
}