// Copyright (c) 2017. Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

using System;
using System.Globalization;
using System.IO;
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
            var stream = client.GetStream();

            var data = Read(stream);
            var deviceInfo = EdgeDeviceInfo.Create(data);
            if (deviceInfo == null)
            {
                stream.Dispose();
                log.Error("Incorrect device format.");
                return;
            }

            var serial = Encoding.ASCII.GetString(deviceInfo.Serial);
            var hello = FormatMessage("hello        ");
            var key = encryptionService.GenerateKey(serial, deviceInfo.Signagure, hello);

            if (key == null)
            {
                stream.Dispose();
                log.Warn($"Unknown Device. Serial number: {serial}");
                return;
            }

            log.Info(
                $"Accepted device. Serial number: {serial} @Firmware Version: {deviceInfo.FirmwareVersion.ToString("F1", CultureInfo.InvariantCulture)}");

            stream.Write(key, 0, key.Length);
        }

        private byte[] Read(NetworkStream stream)
        {
            if (!stream.CanRead)
            {
                return new byte[] { };
            }

            var buffer = new byte[1024];
            stream.Read(buffer, 0, buffer.Length);
            return buffer;
        }

        private byte[] FormatMessage(string message)
        {
            var mod = (message.Length + 3) % 16;
            var size = mod == 0 ? message.Length + 3 : message.Length + 3 + 16 - mod;
            var formatted = new byte[size];

            formatted[message.Length] = 23;
            formatted[message.Length + 1] = 56;
            formatted[message.Length + 2] = 10;

            Encoding.ASCII.GetBytes(message, 0, message.Length, formatted, 0);
            return formatted;
        }
    }
}