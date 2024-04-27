using System;
using System.Net;

namespace Juntz.BytesEx
{
    public static class BytesEx
    {
        private const int BitsPerByte = 8;

        /// <summary>
        /// Inverts every bit in the array.
        /// </summary>
        public static void Not(this byte[] self)
        {
            for (int i = 0; i < self.Length; i++)
            {
                self[i] = (byte)~self[i];
            }
        }

        /// <summary>
        /// Performs a bitwise AND operation with another array.
        /// </summary>
        /// <param name="value">The array to perform the operation with.</param>
        public static void And(this byte[] self, byte[] value)
        {
            ForEach(self, value, (a, b) => (byte)(a & b));
        }
        
        /// <summary>
        /// Performs a bitwise OR operation with another array.
        /// </summary>
        /// <param name="value">The array to perform the operation with.</param>
        public static void Or(this byte[] self, byte[] value)
        {
            ForEach(self, value, (a, b) => (byte)(a | b));
        }
        
        /// <summary>
        /// Performs a bitwise XOR operation with another array.
        /// </summary>
        /// <param name="value">The array to perform the operation with.</param>
        public static void Xor(this byte[] self, byte[] value)
        {
            ForEach(self, value, (a, b) => (byte)(a ^ b));
        }

        /// <summary>
        /// Shifts all bits in the array to the right by a specified number of positions.
        /// </summary>
        /// <param name="count">The number of positions to shift the bits to the right.</param>
        public static void RightShift(this byte[] self, int count)
        {
            for (int i = 0; i < self.Length; i++)
            {
                self[i] = ExtractByte(self, i * BitsPerByte + count);
            }
        }

        /// <summary>
        /// Shifts all bits in the array to the left by a specified number of positions.
        /// </summary>
        /// <param name="count">The number of positions to shift the bits to the left.</param>
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

        /// <summary>
        /// Extracts a single byte from the byte array at the specified bit index.
        /// </summary>
        /// <param name="bitIndex">The bit index indicating the position of the byte to extract.</param>
        /// <returns>The byte extracted from the byte array at the specified bit index.</returns>
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
