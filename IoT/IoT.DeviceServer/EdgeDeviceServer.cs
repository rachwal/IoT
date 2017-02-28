// Copyright (c) 2017. Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

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

                if (clientTask.Result == null)
                {
                    continue;
                }

                var client = clientTask.Result;
                var buffer = new byte[1024];
                var bytesRead = client.GetStream().Read(buffer, 0, buffer.Length);

                deviceCoordinator.Handle(buffer, bytesRead);

                client.GetStream().Dispose();
            }
        }
    }
}