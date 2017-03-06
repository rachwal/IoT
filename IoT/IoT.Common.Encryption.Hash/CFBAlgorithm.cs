// Copyright (c) 2017. Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

using System;
using System.IO;
using System.Security.Cryptography;

namespace IoT.Common.Encryption.Hash
{
    public class CFBAlgorithm : IEncyptionAlgorithm
    {
        public byte[] Encrypt(string plainText, byte[] key)
        {
            if (string.IsNullOrEmpty(plainText))
            {
                return new byte[] {};
            }
            if (key == null || key.Length == 0)
            {
                return new byte[] {};
            }

            byte[] encrypted;

            using (var aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.Mode = CipherMode.ECB;

                var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (var writer = new StreamWriter(cryptoStream))
                        {
                            writer.Write(plainText);
                        }
                        encrypted = memoryStream.ToArray();
                    }
                }
            }

            return encrypted;
        }

        public string Decrypt(byte[] cipherText, byte[] key)
        {
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException("key");

            string plaintext = null;

            using (var aesAlg = Aes.Create())
            {
                aesAlg.Key = key;

                var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (var msDecrypt = new MemoryStream(cipherText))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }
    }
}