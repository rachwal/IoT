// Copyright (c) 2017. Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

namespace IoT.DeviceService
{
    public interface IDeviceService
    {
        void Register(string serial, string key);
        void Unregister(string serial);
        string Encrypt(string serial, byte[] signature, string message);
        string Decypt(string serial, byte[] signature, string message);
    }
}