using System;

namespace Juntz.BytesEx
{
    public static class BytesEx
    {
        public static void Not(this byte[] self)
        {
            for (int i = 0; i < self.Length; i++)
            {
                self[i] = (byte)~self[i];
            }
        }

        public static void And(this byte[] self, byte[] value)
        {
            ForEach(self, value, (a, b) => (byte)(a & b));
        }
        
        public static void Or(this byte[] self, byte[] value)
        {
            ForEach(self, value, (a, b) => (byte)(a | b));
        }
        
        public static void Xor(this byte[] self, byte[] value)
        {
            ForEach(self, value, (a, b) => (byte)(a ^ b));
        }

        private static void ForEach(byte[] self, byte[] value, Func<byte, byte, byte> operation)
        {
            var length = Math.Min(self.Length, value.Length);
            for (int i = 0; i < length; i++)
            {
                self[i] = operation(self[i], value[i]);
            }
        }
    }
}
