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

        public byte[] Encrypt(string serial, byte[] signature, string message)
        {
            if (!deviceRegister.IsRegistered(serial, signature))
            {
                return null;
            }

            var end = "\u00178\n";
            var input = message + end;
            var result = new byte[32];

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
                    return null;
                }

                crypt.RandomizeIV();

                Array.Copy(crypt.IV, 0, result, 0, 16);

                var keyBytes = deviceRegister.GetKey(serial, signature);

                var keyHex = ByteArrayConvert.ToHexString(keyBytes);
                crypt.SetEncodedKey(keyHex, "hex");

                var encryptedString = crypt.EncryptStringENC(input);
                var encyptedBytes = StringConvert.ToByteArray(encryptedString);

                Array.Copy(encyptedBytes, 0, result, 16, 16);
            }
            return result;
        }
    }
}