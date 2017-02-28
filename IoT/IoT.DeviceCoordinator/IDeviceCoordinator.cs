// Copyright (c) 2017. Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

namespace IoT.DeviceCoordinator
{
    public interface IDeviceCoordinator
    {
        void Handle(byte[] buffer, int bytesRead);
    }
}