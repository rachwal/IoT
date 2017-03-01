// Copyright (c) 2017. Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

using System.Globalization;
using System.IO;
using System.Net.Sockets;
using System.Text;
using IoT.Common.Log;
using IoT.DeviceService;
using IoT.EdgeDevice.Info;

namespace IoT.DeviceCoordinator
{
    public class DefaultDeviceCoordinator : IDeviceCoordinator
    {
        private readonly IDeviceService deviceService;
        private readonly ILog log;

        public DefaultDeviceCoordinator(IDeviceService service, ILog logger)
        {
            deviceService = service;
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

            var hello = deviceService.Encrypt(serial, deviceInfo.Signagure, "hello        \u00178\n");
           
            if (hello == null)
            {
                client.GetStream().Dispose();
                log.Warn($"Unknown Device. Serial number:{serial}");
                return;
            }

            log.Info(
                $"Accepted device. Serial number: {serial} @Firmware Version: {deviceInfo.FirmwareVersion.ToString("F1", CultureInfo.InvariantCulture)}");
        }
    }
}