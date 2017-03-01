// Copyright (c) 2017. Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

using IoT.Common.Encryption;
using IoT.DeviceService;
using Moq;
using Xunit;

namespace IoT.DeviceService.Tests
{
    public class DefaultDeviceService_Should
    {
        [Fact]
        public void NotTryToCalculateHashWhenSerialWasNotRegistered()
        {
            //GIVEN
            var hashingAlgorithm = new Mock<IHashAlgorithm>();
            var register = new DefaultDeviceService(hashingAlgorithm.Object);

            //WHEN
            var message = register.Encrypt("not registered serial", new byte[] {}, "test");

            //THEN
            hashingAlgorithm.Verify(m => m.ComputeHash(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            Assert.Equal(null, message);
        }

        [Fact]
        public void TryToCalculateHashWhenSerialWasRegistered()
        {
            //GIVEN
            const string testSerial = "dummySerial";
            const string testKey = "dummyKey";

            var hashingAlgorithm = new Mock<IHashAlgorithm>();

            var register = new DefaultDeviceService(hashingAlgorithm.Object);
            register.Register(testSerial, testKey);

            //WHEN
            var message = register.Encrypt(testSerial, new byte[] { }, "test");

            //THEN
            hashingAlgorithm.Verify(m => m.ComputeHash(testSerial, It.IsAny<string>()), Times.Once);
            Assert.Equal(string.Empty, message);
        }

        [Fact]
        public void UnregisterPreviouslyRegisteredSerial()
        {
            //GIVEN
            const string testSerial = "dummySerial";
            const string testKey = "dummyKey";

            var hashingAlgorithm = new Mock<IHashAlgorithm>();

            var register = new DefaultDeviceService(hashingAlgorithm.Object);
            register.Register(testSerial, testKey);
            register.Unregister(testSerial);

            //WHEN
            var message = register.Encrypt(testSerial, new byte[] { }, "test");

            //THEN
            hashingAlgorithm.Verify(m => m.ComputeHash(testSerial, It.IsAny<string>()), Times.Never);
            Assert.Equal(null, message);
        }
    }
}