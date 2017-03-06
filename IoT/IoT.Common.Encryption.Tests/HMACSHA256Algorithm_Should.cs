// Copyright (c) 2017. Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

using System.Linq;
using System.Text;
using IoT.Common.Encryption.Hash;
using Xunit;

namespace IoT.Common.Encryption.Tests
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
            var keyBytes = Encoding.UTF8.GetBytes(key);
            var expectedResult = new byte[] {};

            //WHEN
            var result = algorithm.ComputeHash(input, keyBytes);

            //THEN
            Assert.True(expectedResult.SequenceEqual(result));
        }
    }
}