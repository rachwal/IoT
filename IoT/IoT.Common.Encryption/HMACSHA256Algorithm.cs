// Copyright (c) 2017. Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

using System.Security.Cryptography;
using System.Text;

namespace IoT.Common.Encryption
{
    public class HMACSHA256Algorithm : IHashAlgorithm
    {
        public byte[] ComputeHash(string input, string key)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(key))
            {
                return new byte[] {};
            }

            var inputBytes = Encoding.UTF8.GetBytes(input);
            var keyBytes = Encoding.UTF8.GetBytes(key);
            var hmac = new HMACSHA256(keyBytes);
            var result = hmac.ComputeHash(inputBytes);
            return result;
        }
    }
}