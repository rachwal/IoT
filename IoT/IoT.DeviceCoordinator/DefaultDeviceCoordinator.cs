// Copyright (c) 2017. Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using IoT.Core.Encryption;

namespace IoT.DeviceCoordinator
{
    public class DefaultDeviceCoordinator : IDeviceCoordinator
    {
        private readonly Dictionary<string, string> deviceRegister = new Dictionary<string, string>
        {
            {
                "d3eea2beeb292f7e57fbddb8cac330c8", "acd00f2964667ef33f23d3ce0d80117a"
            }
        };

        private readonly IHashAlgorithm hashAlgorithm;

        public DefaultDeviceCoordinator(IHashAlgorithm algorithm)
        {
            hashAlgorithm = algorithm;
        }

        public void Handle(byte[] buffer, int bytesRead)
        {
            if (buffer.Length < bytesRead || bytesRead < 64)
            {
                return;
            }

            var firmwareInfoBytes = new byte[4];
            var serialInfoBytes = new byte[32];
            var signagureBytes = new byte[32];

            Array.Copy(buffer, 0, firmwareInfoBytes, 0, 4);

            var firmwareVersion = 0.0f;
            var firmwareString = Encoding.UTF8.GetString(firmwareInfoBytes);

            var match = Regex.Match(firmwareString, @"@\d\.\d");
            if (match.Success)
            {
                firmwareVersion = float.Parse(firmwareString.Substring(1), NumberStyles.Float,
                    CultureInfo.InvariantCulture);

                Array.Copy(buffer, 4, serialInfoBytes, 0, 32);
                Array.Copy(buffer, 36, signagureBytes, 0, 32);
            }
            else
            {
                Array.Copy(buffer, 0, serialInfoBytes, 0, 32);
                Array.Copy(buffer, 32, signagureBytes, 0, 32);
            }

            var serial = Encoding.UTF8.GetString(serialInfoBytes);

            var key = deviceRegister[serial];
            var expectedSignature = hashAlgorithm.ComputeHash(serial, key);

            var isMatch = expectedSignature.SequenceEqual(signagureBytes);
            if (isMatch)
            {
                Console.WriteLine(
                    $"serial number: {serial} @Firmware Version: {firmwareVersion.ToString("F1", CultureInfo.InvariantCulture)}");
            }
        }
    }
}