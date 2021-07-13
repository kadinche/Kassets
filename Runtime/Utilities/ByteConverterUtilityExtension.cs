using System;
using System.Linq;
using UnityEngine;

namespace Kadinche.Kassets.Utilities
{
    public static class ByteConverterUtilityExtension
    {
        public static byte[] ToBytes(this Vector3 position)
        {
            var bytes = new byte[sizeof(float) * 3];

            Buffer.BlockCopy(BitConverter.GetBytes(position.x), 0, bytes, 0 * sizeof(float), sizeof(float));
            Buffer.BlockCopy(BitConverter.GetBytes(position.y), 0, bytes, 1 * sizeof(float), sizeof(float));
            Buffer.BlockCopy(BitConverter.GetBytes(position.z), 0, bytes, 2 * sizeof(float), sizeof(float));

            return bytes;
        }

        public static Vector3 ToVector3(this byte[] buff)
        {
            Vector3 vect = Vector3.zero;
            vect.x = BitConverter.ToSingle(buff, 0 * sizeof(float));
            vect.y = BitConverter.ToSingle(buff, 1 * sizeof(float));
            vect.z = BitConverter.ToSingle(buff, 2 * sizeof(float));

            return vect;
        }

        public static Vector3 Vector3WithIndex(this byte[] buff, int index)
        {
            Vector3 vect = Vector3.zero;
            vect.x = BitConverter.ToSingle(buff, index + 0 * sizeof(float));
            vect.y = BitConverter.ToSingle(buff, index + 1 * sizeof(float));
            vect.z = BitConverter.ToSingle(buff, index + 2 * sizeof(float));

            return vect;
        }

        public static byte[] ToBytes(this Vector2 vec)
        {
            var bytes = new byte[sizeof(float) * 2];

            Buffer.BlockCopy(BitConverter.GetBytes(vec.x), 0, bytes, 0 * sizeof(float), sizeof(float));
            Buffer.BlockCopy(BitConverter.GetBytes(vec.y), 0, bytes, 1 * sizeof(float), sizeof(float));
            return bytes;
        }

        public static Vector2 ToVector2(this byte[] buff)
        {
            Vector2 vect = Vector3.zero;
            vect.x = BitConverter.ToSingle(buff,  + 0 * sizeof(float));
            vect.y = BitConverter.ToSingle(buff,  + 1 * sizeof(float));

            return vect;
        }

        public static byte[] ToBytes(this Quaternion quaternion)
        {
            var bytes = new byte[sizeof(float) * 4];

            Buffer.BlockCopy(BitConverter.GetBytes(quaternion.x), 0, bytes, 0 * sizeof(float), sizeof(float));
            Buffer.BlockCopy(BitConverter.GetBytes(quaternion.y), 0, bytes, 1 * sizeof(float), sizeof(float));
            Buffer.BlockCopy(BitConverter.GetBytes(quaternion.z), 0, bytes, 2 * sizeof(float), sizeof(float));
            Buffer.BlockCopy(BitConverter.GetBytes(quaternion.w), 0, bytes, 3 * sizeof(float), sizeof(float));

            return bytes;
        }

        public static Quaternion ToQuaternion(this byte[] buff)
        {
            Quaternion quaternion = Quaternion.identity;
            quaternion.x = BitConverter.ToSingle(buff, 0 * sizeof(float));
            quaternion.y = BitConverter.ToSingle(buff, 1 * sizeof(float));
            quaternion.z = BitConverter.ToSingle(buff, 2 * sizeof(float));
            quaternion.w = BitConverter.ToSingle(buff, 3 * sizeof(float));

            return quaternion;
        }

        public static Quaternion ToQuaternionWithIndex(this byte[] buff, int index)
        {
            Quaternion quaternion = Quaternion.identity;
            quaternion.x = BitConverter.ToSingle(buff, index + 0 * sizeof(float));
            quaternion.y = BitConverter.ToSingle(buff, index + 1 * sizeof(float));
            quaternion.z = BitConverter.ToSingle(buff, index + 2 * sizeof(float));
            quaternion.w = BitConverter.ToSingle(buff, index + 3 * sizeof(float));

            return quaternion;
        }

        public static byte[] ToBytes(this float value)
        {
            var bytes = new byte[sizeof(float)];
            Buffer.BlockCopy(BitConverter.GetBytes(value), 0, bytes, 0, sizeof(float));
            return bytes;
        }

        public static float ToFloatWithIndex(this byte[] buff, int index)
        {
            return BitConverter.ToSingle(buff, index + 0 * sizeof(float));
        }

        public static byte[] Combine(this byte[][] arrays)
        {
            byte[] ret = new byte[arrays.Sum(x => x.Length)];
            int offset = 0;
            foreach (byte[] data in arrays)
            {
                Buffer.BlockCopy(data, 0, ret, offset, data.Length);
                offset += data.Length;
            }
            return ret;
        }
    }
}