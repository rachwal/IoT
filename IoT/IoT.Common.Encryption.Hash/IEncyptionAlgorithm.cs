// Copyright (c) 2017. Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

namespace IoT.Common.Encryption.Hash
{
    public interface IEncyptionAlgorithm
    {
        byte[] Encrypt(string plainText, byte[] key);
        string Decrypt(byte[] cipherText, byte[] key);
    }
}