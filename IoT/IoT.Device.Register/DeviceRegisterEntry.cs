namespace IoT.Device.Register
{
    public class DeviceRegisterEntry
    {
        public byte[] Key { get; }
        public byte [] IV { get; }

        public DeviceRegisterEntry(byte[] key, byte[] iv)
        {
            Key = key;
            IV = iv;
        }
    }
}