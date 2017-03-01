// Copyright (c) 2017. Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using IoT.Common.Encryption;

namespace IoT.DeviceService
{
    public class DefaultDeviceService : IDeviceService
    {
        private readonly Dictionary<string, string> deviceRegister = new Dictionary<string, string>();
        private readonly IHashAlgorithm hashAlgorithm;

        public DefaultDeviceService(IHashAlgorithm algorithm)
        {
            hashAlgorithm = algorithm;
        }

        public void Register(string serial, string key)
        {
            if (!deviceRegister.ContainsKey(serial))
            {
                deviceRegister.Add(serial, key);
            }
            else
            {
                deviceRegister[serial] = key;
            }
        }

        public void Unregister(string serial)
        {
            if (deviceRegister.ContainsKey(serial))
            {
                deviceRegister.Remove(serial);
            }
        }
        
        public string Encrypt(string serial, byte[] signature, string message)
        {
            if (!IsRegistered(serial, signature))
            {
                return null;
            }
            return string.Empty;
        }

        public string Decypt(string serial, byte[] signature, string message)
        {
            if (!IsRegistered(serial, signature))
            {
                return null;
            }
            return string.Empty;
        }

        private bool IsRegistered(string serial, byte[] signature)
        {
            if (!deviceRegister.ContainsKey(serial))
            {
                return false;
            }

            var key = deviceRegister[serial];
            var expectedSignature = hashAlgorithm.ComputeHash(serial, key);
            var isMatch = expectedSignature.SequenceEqual(signature);

            return isMatch;
        }
    }
}