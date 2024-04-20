using System;
using System.Net;

namespace Juntz.BytesEx
{
    public static class BytesEx
    {
        private const int BitsPerByte = 8;

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

        public static void RightShift(this byte[] self, int count)
        {
            for (int i = 0; i < self.Length; i++)
            {
                self[i] = ExtractByte(self, i * BitsPerByte + count);
            }
        }

        public static void LeftShift(this byte[] self, int count)
        {
            for (int i = self.Length - 1; i >= 0; i--)
            {
                self[i] = ExtractByte(self, i * BitsPerByte - count);
            }
        }

        private static void ForEach(byte[] bytes, byte[] value, Func<byte, byte, byte> operation)
        {
            var length = Math.Min(bytes.Length, value.Length);
            for (int i = 0; i < length; i++)
            {
                bytes[i] = operation(bytes[i], value[i]);
            }
        }

        public static byte ExtractByte(byte[] bytes, int bitIndex)
        {
            var byteIndex =  Math.DivRem(bitIndex, BitsPerByte, out var bitOffset);
            if (byteIndex < 0
                || byteIndex >= bytes.Length)
            {
                return 0;
            }

            if (bitOffset == 0)
            {
                return bytes[byteIndex];
            }

            var result = bitOffset > 0
                ? bytes[byteIndex] >> bitOffset
                : bytes[byteIndex] << -bitOffset;
            if (byteIndex < bytes.Length - 1)
            {
                result |= bytes[byteIndex + 1] << (BitsPerByte - bitOffset);
            }

            return (byte) result;
        }
    }
}
