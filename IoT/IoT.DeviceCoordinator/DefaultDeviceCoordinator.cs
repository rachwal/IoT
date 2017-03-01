// Copyright (c) 2017. Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

using System.Globalization;
using System.Net.Sockets;
using System.Text;
using IoT.Common.Log;
using IoT.DeviceRegister;
using IoT.EdgeDevice.Info;

namespace IoT.DeviceCoordinator
{
    public class DefaultDeviceCoordinator : IDeviceCoordinator
    {
        private readonly IDeviceRegister deviceRegister;
        private readonly ILog log;

        public DefaultDeviceCoordinator(IDeviceRegister register, ILog logger)
        {
            deviceRegister = register;
            log = logger;
        }

        public void Handle(TcpClient client)
        {
            var buffer = new byte[1024];

            client.GetStream().Read(buffer, 0, buffer.Length);

            var deviceInfo = EdgeDeviceInfo.Create(buffer);
            if (deviceInfo == null)
            {
                client.GetStream().Dispose();
                log.Error("Incorrect device format.");
                return;
            }

            var serial = Encoding.UTF8.GetString(deviceInfo.Serial);

            var isRegistered = deviceRegister.IsRegistered(serial, deviceInfo.Signagure);

            if (!isRegistered)
            {
                client.GetStream().Dispose();
                log.Error($"Unknown Device. Serial number:{serial}");
                return;
            }

            log.Info(
                $"Accepted device. Serial number: {serial} @Firmware Version: {deviceInfo.FirmwareVersion.ToString("F1", CultureInfo.InvariantCulture)}");

            client.GetStream().Dispose();
        }
    }
}