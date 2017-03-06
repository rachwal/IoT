// Copyright (c) 2017. Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

namespace IoT.Common.Encryption.Hash
{
    public interface IHashAlgorithm
    {
        byte[] ComputeHash(string input, byte[] key);
    }
}