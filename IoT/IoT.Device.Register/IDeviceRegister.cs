// Copyright (c) 2017. Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

namespace IoT.Device.Register
{
    public interface IDeviceRegister
    {
        void Register(string serial, string key);
        void Unregister(string serial);
        DeviceRegisterEntry GetEntry(string serial, byte[] signature);
        bool IsRegistered(string serial, byte[] signature);
    }
}