// Copyright (c) 2017. Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

using System.Net.Sockets;

namespace IoT.Device.Server
{
    public class StateObject
    {
        public const int BufferSize = 1024;
        public readonly byte[] Buffer = new byte[BufferSize];
        public Socket WorkSocket;
    }
}