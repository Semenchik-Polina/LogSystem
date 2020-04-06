using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace LogSystem.Common.Helpers
{
    public static class ByteArrayHelper
    {
        public static byte[] ObjectToByte(Object obj)
        {
            if (obj == null)
            {
                return null;
            }
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);

                return ms.ToArray();
            }
        }

        public static byte[] ConcatByteArrays(byte[] arr1, byte[] arr2)
        {
            byte[] result = new byte[arr1.Length + arr2.Length];
            Buffer.BlockCopy(arr1, 0, result, 0, arr1.Length);
            Buffer.BlockCopy(arr2, 0, result, arr1.Length, arr2.Length);

            return result;
        }
    }
}
