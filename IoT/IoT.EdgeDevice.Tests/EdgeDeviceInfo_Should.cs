// Copyright (c) 2017. Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

using System.Text;
using IoT.EdgeDevice.Info;
using Xunit;

namespace IoT.EdgeDevice.Tests
{
    public class EdgeDeviceInfo_Should
    {
        [Theory]
        [InlineData(new byte[] {})]
        [InlineData(new byte[] {1, 2, 3})]
        [InlineData(null)]
        public void ReturnNullWhenTryToCreateAnObjectFromNullOrTooSmallBuffer(byte[] buffer)
        {
            //WHEN
            var deviceInfo = EdgeDeviceInfo.Create(buffer);

            //THEN
            Assert.Null(deviceInfo);
        }

        [Theory]
        [InlineData(new byte[]
        {
            64, 49, 46, 50, 100, 51, 101, 101, 97, 50, 98, 101, 101, 98, 50, 57, 50, 102, 55, 101,
            53, 55, 102, 98, 100, 100, 98, 56, 99, 97, 99, 51, 51, 48, 99, 56, 62, 29, 69, 71, 212,
            127, 6, 205, 128, 153, 175, 90, 247, 227, 43, 165, 100, 134, 126, 16, 249, 68, 56, 43,
            222, 181, 173, 68, 110, 242, 129, 29
        }, 1.2)]
        [InlineData(new byte[]
        {
            64, 50, 46, 49, 100, 51, 101, 101, 97, 50, 98, 101, 101, 98, 50, 57, 50, 102, 55, 101,
            53, 55, 102, 98, 100, 100, 98, 56, 99, 97, 99, 51, 51, 48, 99, 56, 62, 29, 69, 71, 212,
            127, 6, 205, 128, 153, 175, 90, 247, 227, 43, 165, 100, 134, 126, 16, 249, 68, 56, 43,
            222, 181, 173, 68, 110, 242, 129, 29
        }, 2.1)]
        public void ReturnValidObjectWithModernFirmwareVersionAndSerial(byte[] buffer, float expectedFirmwareVersion)
        {
            //GIVEN
            const string expectedSerial = "d3eea2beeb292f7e57fbddb8cac330c8";

            //WHEN
            var deviceInfo = EdgeDeviceInfo.Create(buffer);

            //THEN
            var serial = Encoding.UTF8.GetString(deviceInfo.Serial);
            Assert.Equal(expectedFirmwareVersion, deviceInfo.FirmwareVersion);
            Assert.Equal(expectedSerial, serial);
        }

        [Theory]
        [InlineData(new byte[]
        {
            100, 51, 101, 101, 97, 50, 98, 101, 101, 98, 50, 57, 50, 102, 55, 101,
            53, 55, 102, 98, 100, 100, 98, 56, 99, 97, 99, 51, 51, 48, 99, 56, 62, 29, 69, 71, 212,
            127, 6, 205, 128, 153, 175, 90, 247, 227, 43, 165, 100, 134, 126, 16, 249, 68, 56, 43,
            222, 181, 173, 68, 110, 242, 129, 29
        }, 0.0)]
        public void ReturnValidObjectWithUnknownFirmwareVersionAndSerial(byte[] buffer, float expectedFirmwareVersion)
        {
            //GIVEN
            const string expectedSerial = "d3eea2beeb292f7e57fbddb8cac330c8";

            //WHEN
            var deviceInfo = EdgeDeviceInfo.Create(buffer);

            //THEN
            var serial = Encoding.UTF8.GetString(deviceInfo.Serial);
            Assert.Equal(expectedFirmwareVersion, deviceInfo.FirmwareVersion);
            Assert.Equal(expectedSerial, serial);
        }
    }
}