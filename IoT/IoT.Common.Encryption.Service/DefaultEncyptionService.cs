// Copyright (c) 2017. Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

using System;
using IoT.Common.Tools;
using IoT.Device.Register;

namespace IoT.Common.Encryption.Service
{
    public class DefaultEncyptionService : IEncryptionService
    {
        private readonly IDeviceRegister deviceRegister;

        public DefaultEncyptionService(IDeviceRegister register)
        {
            deviceRegister = register;
        }

        public byte[] Encrypt(string serial, byte[] signature, byte[] message)
        {
            var deviceInfo = deviceRegister.GetEntry(serial, signature);
            if (deviceInfo == null)
            {
                return new byte[] { };
            }

            return Encrypt(message, deviceInfo);
        }
        
        public byte[] GenerateKey(string serial, byte[] signature, byte[] message)
        {
            var deviceInfo = deviceRegister.GetEntry(serial, signature);
            if (deviceInfo == null)
            {
                return new byte[] { };
            }

            var result = new byte[32];

            Array.Copy(deviceInfo.IV, 0, result, 0, 16);

            var encypted = Encrypt(message, deviceInfo);
          
            Array.Copy(encypted, 0, result, 16, 16);

            return result;
        }

        private byte[] Encrypt(byte[] message, DeviceRegisterEntry entry)
        {
            using (
               var crypt = new Chilkat.Crypt2
               {
                   CryptAlgorithm = "aes",
                   CipherMode = "cfb",
                   EncodingMode = "hex",
                   Charset = "utf-8"
               })
            {
                var success = crypt.UnlockComponent("test");
                if (success != true)
                {
                    return new byte[] { };
                }

                var ivHex = ByteArrayConvert.ToHexString(entry.IV);
                crypt.SetEncodedIV(ivHex, "hex");

                var keyHex = ByteArrayConvert.ToHexString(entry.Key);
                crypt.SetEncodedKey(keyHex, "hex");

                var enc = crypt.EncryptBytesENC(message);
                var encyptedBytes = StringConvert.ToByteArray(enc);

                return encyptedBytes;
            }
        }
    }
}