// Copyright (c) 2017. Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

using System;
using System.Text;
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

            return Encrypt(message, deviceInfo.IV, deviceInfo.Key);
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

            var encypted = Encrypt(message, deviceInfo.IV, deviceInfo.Key);

            Array.Copy(encypted, 0, result, 16, 16);

            return result;
        }

        public byte[] Encrypt(byte[] message, byte[] iv, byte[] key)
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

                var ivHex = ByteArrayConvert.ToHexString(iv);
                crypt.SetEncodedIV(ivHex, "hex");

                var keyHex = ByteArrayConvert.ToHexString(key);
                crypt.SetEncodedKey(keyHex, "hex");

                var enc = crypt.EncryptBytesENC(message);
                var encyptedBytes = StringConvert.ToByteArray(enc);

                return encyptedBytes;
            }
        }
    }
}