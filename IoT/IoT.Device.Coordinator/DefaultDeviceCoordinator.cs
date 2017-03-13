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

        public void Handle(Socket socket)
        {
            var data = new byte[128];

            socket.Receive(data);

            log.Debug(Encoding.UTF8.GetString(data));

            var deviceInfo = EdgeDeviceInfo.Create(data);
            if (deviceInfo == null)
            {
                log.Error("Incorrect device format.");
                return;
            }

            var serial = Encoding.UTF8.GetString(deviceInfo.Serial);

            var key = encryptionService.GenerateKey(serial, deviceInfo.Signagure, FormatMessage("hello        "));
            if (key == null)
            {
                log.Warn($"Unknown Device. Serial number: {serial}");
                return;
            }

            socket.Send(key);

            log.Info(
                $"Accepted device. Serial number: {serial} @Firmware Version: {deviceInfo.FirmwareVersion.ToString("F1", CultureInfo.InvariantCulture)}");
        }

        private byte[] FormatMessage(string message)
        {
            var mod = (message.Length + 3) % 16;
            var size = mod == 0 ? message.Length + 3 : message.Length + 3 + 16 - mod;
            var formatted = new byte[size];

            formatted[message.Length] = 23;
            formatted[message.Length + 1] = 56;
            formatted[message.Length + 2] = 10;

            Encoding.UTF8.GetBytes(message, 0, message.Length, formatted, 0);
            return formatted;
        }
    }
}