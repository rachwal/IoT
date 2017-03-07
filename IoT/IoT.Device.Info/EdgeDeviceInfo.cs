// Copyright (c) 2017. Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace IoT.Device.Info
{
    public class EdgeDeviceInfo
    {
        private EdgeDeviceInfo(byte[] serial, byte[] signagure, float firmwareVersion)
        {
            Serial = serial;
            Signagure = signagure;
            FirmwareVersion = firmwareVersion;
        }

        public byte[] Serial { get; private set; }
        public byte[] Signagure { get; private set; }
        public float FirmwareVersion { get; private set; }

        public static EdgeDeviceInfo Create(byte[] buffer)
        {
            if (buffer == null || buffer.Length < 64)
            {
                return null;
            }

            var info = new EdgeDeviceInfo(new byte[32], new byte[32], 0);

            var firmwareInfoBytes = new byte[4];
            Array.Copy(buffer, 0, firmwareInfoBytes, 0, 4);
            var firmwareInfo = Encoding.ASCII.GetString(firmwareInfoBytes);

            var match = Regex.Match(firmwareInfo, @"@\d\.\d");
            if (match.Success)
            {
                if (buffer.Length < 68)
                {
                    return null;
                }

                info.FirmwareVersion = float.Parse(firmwareInfo.Substring(1), NumberStyles.Float,
                    CultureInfo.InvariantCulture);

                Array.Copy(buffer, 4, info.Serial, 0, 32);
                Array.Copy(buffer, 36, info.Signagure, 0, 32);
            }
            else
            {
                Array.Copy(buffer, 0, info.Serial, 0, 32);
                Array.Copy(buffer, 32, info.Signagure, 0, 32);
            }

            return info;
        }
    }
}