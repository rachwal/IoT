// Copyright (c) 2017. Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

namespace IoT.Common.Encryption.Service
{
    public interface IEncryptionService
    {
        byte[] GenerateKey(string serial, byte[] signature, byte[] message);
        byte[] Encrypt(string serial, byte[] signature, byte[] message);
        byte[] Encrypt(byte[] message, byte[] iv, byte[] key);
    }
}