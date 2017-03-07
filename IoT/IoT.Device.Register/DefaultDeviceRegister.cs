// Copyright (c) 2017. Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using IoT.Common.Encryption.Hash;

namespace IoT.Device.Register
{
    public class DefaultDeviceRegister : IDeviceRegister
    {
        private readonly Dictionary<string, DeviceRegisterEntry> deviceRegister = new Dictionary<string, DeviceRegisterEntry>();
        private readonly IHashAlgorithm hashAlgorithm;

        public DefaultDeviceRegister(IHashAlgorithm algorithm)
        {
            hashAlgorithm = algorithm;
        }

        public void Register(string serial, string key)
        {
            if (deviceRegister.ContainsKey(serial))
            {
                Unregister(serial);
                Register(serial, key);
            }
            else
            {
                var keyBytes = Encoding.ASCII.GetBytes(key);
                using (var aes = Aes.Create())
                {
                    var entry = new DeviceRegisterEntry(keyBytes, aes.IV);
                    deviceRegister.Add(serial, entry);
                }
            }
        }

        public void Unregister(string serial)
        {
            if (deviceRegister.ContainsKey(serial))
            {
                deviceRegister.Remove(serial);
            }
        }

        public DeviceRegisterEntry GetEntry(string serial, byte[] signature)
        {
            if (!IsRegistered(serial, signature))
            {
                return null;
            }

            return deviceRegister[serial];
        }

        public bool IsRegistered(string serial, byte[] signature)
        {
            if (!deviceRegister.ContainsKey(serial))
            {
                return false;
            }

            var entry = deviceRegister[serial];
            var expectedSignature = hashAlgorithm.ComputeHash(serial, entry.Key);
            var isMatch = expectedSignature.SequenceEqual(signature);

            return isMatch;
        }
    }
}