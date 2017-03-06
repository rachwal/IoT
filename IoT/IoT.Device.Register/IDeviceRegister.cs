// Copyright (c) 2017. Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

namespace IoT.Device.Register
{
    public interface IDeviceRegister
    {
        void Register(string serial, string key);
        void Unregister(string serial);
        byte[] GetKey(string serial, byte[] signature);
        bool IsRegistered(string serial, byte[] signature);
    }
}