// Copyright (c) 2017. Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

using System.Security.Cryptography.X509Certificates;

namespace IoT.DeviceRegister
{
    public interface IDeviceRegister
    {
        void Register(string serial, string key);
        void Unregister(string serial);
        bool IsRegistered(string serial, byte[] signature);
    }
}