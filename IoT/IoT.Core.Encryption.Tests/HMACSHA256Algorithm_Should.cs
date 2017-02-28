// Copyright (c) 2017. Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

using System.Linq;
using Xunit;

namespace IoT.Core.Encryption.Tests
{
    public class HMACSHA256Algorithm_Should
    {
        public HMACSHA256Algorithm_Should()
        {
            algorithm = new HMACSHA256Algorithm();
        }

        private readonly IHashAlgorithm algorithm;

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ReturnAnEmptyArrayWhenGetNullOrEmptyInput(string input)
        {
            //GIVEN
            const string key = "acd00f2964667ef33f23d3ce0d80117a";
            var expectedResult = new byte[] {};

            //WHEN
            var result = algorithm.ComputeHash(input, key);

            //THEN
            Assert.True(expectedResult.SequenceEqual(result));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ReturnAnEmptyArrayWhenGetNullKey(string key)
        {
            //GIVEN
            const string input = "d3eea2beeb292f7e57fbddb8cac330c8";
            var expectedResult = new byte[] {};

            //WHEN
            var result = algorithm.ComputeHash(input, key);

            //THEN
            Assert.True(expectedResult.SequenceEqual(result));
        }

        [Fact]
        public void EncryptSerialWithGivenKey()
        {
            //GIVEN
            const string serial = "d3eea2beeb292f7e57fbddb8cac330c8";
            const string key = "acd00f2964667ef33f23d3ce0d80117a";

            var expectedResult = new byte[]
            {
                62, 29, 69, 71, 212, 127, 6, 205, 128, 153, 175, 90, 247, 227, 43, 165, 100, 134, 126, 16, 249, 68, 56,
                43,
                222, 181, 173, 68, 110, 242, 129, 29
            };

            //WHEN
            var result = algorithm.ComputeHash(serial, key);

            //THEN
            Assert.True(expectedResult.SequenceEqual(result));
        }
    }
}