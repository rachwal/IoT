// Copyright (c) 2017. Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

using System;

namespace IoT.Common.Tools
{
    public class StringConvert
    {
        //http://stackoverflow.com/questions/321370/how-can-i-convert-a-hex-string-to-a-byte-array
        public static byte[] ToByteArray(string hex)
        {
            if (string.IsNullOrEmpty(hex))
            {
                return null;
            }

            if (hex.Length % 2 == 1)
            {
                throw new Exception("The binary key cannot have an odd number of digits");
            }

            var arr = new byte[hex.Length >> 1];

            for (var i = 0; i < hex.Length >> 1; ++i)
            {
                arr[i] = (byte) ((GetHexVal(hex[i << 1]) << 4) + GetHexVal(hex[(i << 1) + 1]));
            }

            return arr;
        }

        //http://stackoverflow.com/questions/321370/how-can-i-convert-a-hex-string-to-a-byte-array
        private static int GetHexVal(char hex)
        {
            int val = hex;
            return val - (val < 58 ? 48 : 55);
        }
    }
}