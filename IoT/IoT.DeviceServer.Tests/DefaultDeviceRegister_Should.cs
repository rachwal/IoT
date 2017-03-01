// Copyright (c) 2017. Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

using System.Xml.Serialization;
using IoT.Common.Encryption;
using IoT.DeviceRegister;
using Moq;
using Xunit;

namespace IoT.DeviceServer.Tests
{
    public class DefaultDeviceRegister_Should
    {
        [Fact]
        public void NotTryToCalculateHashWhenSerialWasNotRegistered()
        {
            //GIVEN
            var hashingAlgorithm = new Mock<IHashAlgorithm>();
            var register = new DefaultDeviceRegister(hashingAlgorithm.Object);

            //WHEN
            var isRegistered = register.IsRegistered("not registered serial", new byte[] {});

            //THEN
            hashingAlgorithm.Verify(m => m.ComputeHash(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            Assert.Equal(false, isRegistered);
        }

        [Fact]
        public void TryToCalculateHashWhenSerialWasRegistered()
        {
            //GIVEN
            const string testSerial = "dummySerial";
            const string testKey = "dummyKey";

            var hashingAlgorithm = new Mock<IHashAlgorithm>();

            var register = new DefaultDeviceRegister(hashingAlgorithm.Object);
            register.Register(testSerial, testKey);

            //WHEN
            var isRegistered = register.IsRegistered(testSerial, new byte[] {});

            //THEN
            hashingAlgorithm.Verify(m => m.ComputeHash(testSerial, It.IsAny<string>()), Times.Once);
            Assert.Equal(true, isRegistered);
        }

        [Fact]
        public void UnregisterPreviouslyRegisteredSerial()
        {
            //GIVEN
            const string testSerial = "dummySerial";
            const string testKey = "dummyKey";

            var hashingAlgorithm = new Mock<IHashAlgorithm>();

            var register = new DefaultDeviceRegister(hashingAlgorithm.Object);
            register.Register(testSerial, testKey);
            register.Unregister(testSerial);

            //WHEN
            var isRegistered = register.IsRegistered(testSerial, new byte[] {});

            //THEN
            hashingAlgorithm.Verify(m => m.ComputeHash(testSerial, It.IsAny<string>()), Times.Never);
            Assert.Equal(false, isRegistered);
        }
    }
}