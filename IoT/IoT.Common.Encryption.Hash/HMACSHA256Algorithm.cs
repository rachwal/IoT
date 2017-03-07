// Copyright (c) 2017. Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

using System.Security.Cryptography;
using System.Text;

namespace IoT.Common.Encryption.Hash
{
    public class HMACSHA256Algorithm : IHashAlgorithm
    {
        public byte[] ComputeHash(string input, byte[] key)
        {
            if (string.IsNullOrEmpty(input) || key == null)
            {
                return new byte[] {};
            }

            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hmac = new HMACSHA256(key);
            var result = hmac.ComputeHash(inputBytes);
            return result;
        }
    }
}