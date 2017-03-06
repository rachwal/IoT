// Copyright (c) 2017. Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

using System.Globalization;
using System.Net.Sockets;
using System.Text;
using IoT.Common.Encryption.Service;
using IoT.Common.Log;
using IoT.Device.Info;

namespace IoT.Device.Coordinator
{
    public class DefaultDeviceCoordinator : IDeviceCoordinator
    {
        private readonly IEncryptionService encryptionService;
        private readonly ILog log;

        public DefaultDeviceCoordinator(IEncryptionService service, ILog logger)
        {
            encryptionService = service;
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

            var hello = encryptionService.Encrypt(serial, deviceInfo.Signagure, "hello        ");

            if (hello == null)
            {
                client.GetStream().Dispose();
                log.Warn($"Unknown Device. Serial number:{serial}");
                return;
            }

            log.Info(
                $"Accepted device. Serial number: {serial} @Firmware Version: {deviceInfo.FirmwareVersion.ToString("F1", CultureInfo.InvariantCulture)}");

            client.GetStream().Write(hello, 0, hello.Length);
        }
    }
}