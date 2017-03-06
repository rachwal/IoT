// Copyright (c) 2017. Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

using IoT.Common.Encryption.Hash;
using IoT.Device.Register;
using Moq;
using Xunit;

namespace IoT.Device.Tests
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
            hashingAlgorithm.Verify(m => m.ComputeHash(It.IsAny<string>(), It.IsAny<byte[]>()), Times.Never);
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
            register.IsRegistered(testSerial, new byte[] {});

            //THEN
            hashingAlgorithm.Verify(m => m.ComputeHash(testSerial, It.IsAny<byte[]>()), Times.Once);
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
            hashingAlgorithm.Verify(m => m.ComputeHash(testSerial, It.IsAny<byte[]>()), Times.Never);
            Assert.Equal(false, isRegistered);
        }
    }
}