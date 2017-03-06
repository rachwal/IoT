// Copyright (c) 2017. Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

using System.Text;

namespace IoT.Common.Tools
{
    public class ByteArrayConvert
    {
        public static string ToHexString(byte[] array)
        {
            if (array == null)
            {
                return string.Empty;
            }

            var builder = new StringBuilder();
            foreach (var element in array)
            {
                builder.Append($"{element:X}");
            }
            return builder.ToString();
        }
    }
}