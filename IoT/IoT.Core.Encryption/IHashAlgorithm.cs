// Copyright (c) 2017. Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

namespace IoT.Core.Encryption
{
    public interface IHashAlgorithm
    {
        byte[] ComputeHash(string input, string key);
    }
}