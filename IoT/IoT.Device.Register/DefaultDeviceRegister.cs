// Copyright (c) 2017. Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Text;
using IoT.Common.Encryption.Hash;

namespace IoT.Device.Register
{
    public class DefaultDeviceRegister : IDeviceRegister
    {
        private readonly Dictionary<string, byte[]> deviceRegister = new Dictionary<string, byte[]>();
        private readonly IHashAlgorithm hashAlgorithm;

        public DefaultDeviceRegister(IHashAlgorithm algorithm)
        {
            hashAlgorithm = algorithm;
        }

        public void Register(string serial, string key)
        {
            var keyBytes = Encoding.UTF8.GetBytes(key);
            if (!deviceRegister.ContainsKey(serial))
            {
                deviceRegister.Add(serial, keyBytes);
            }
            else
            {
                deviceRegister[serial] = keyBytes;
            }
        }

        public void Unregister(string serial)
        {
            if (deviceRegister.ContainsKey(serial))
            {
                deviceRegister.Remove(serial);
            }
        }

        public byte[] GetKey(string serial, byte[] signature)
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

            var key = deviceRegister[serial];
            var expectedSignature = hashAlgorithm.ComputeHash(serial, key);
            var isMatch = expectedSignature.SequenceEqual(signature);

            return isMatch;
        }
    }
}