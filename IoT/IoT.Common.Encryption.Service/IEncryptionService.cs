// Copyright (c) 2017. Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

namespace IoT.Common.Encryption.Service
{
    public interface IEncryptionService
    {
        byte[] Encrypt(string serial, byte[] signature, string message);
    }
}