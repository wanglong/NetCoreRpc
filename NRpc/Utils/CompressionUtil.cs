using System.IO;
using System.IO.Compression;

namespace NRpc.Utils
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：CompressionUtil.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2018/1/29 10:45:43
    /// </summary>
    public static class CompressionUtil
    {
        /// <summary>
        /// Gzip压缩
        /// </summary>
        /// <param name="data">要压缩的字节数组</param>
        /// <returns>Gzip压缩后的数组</returns>
        public static byte[] Compress(byte[] data)
        {
            if (data == null || data.Length < 1)
                return data;
            try
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    using (GZipStream gZipStream = new GZipStream(stream, CompressionMode.Compress))
                    {
                        gZipStream.Write(data, 0, data.Length);
                        gZipStream.Close();
                    }
                    return stream.ToArray();
                }
            }
            catch
            {
                return data;
            }
        }

        public static byte[] Decompress(byte[] data)
        {
            if (data == null || data.Length < 1)
                return data;
            try
            {
                using (MemoryStream inputStream = new MemoryStream(data))
                {
                    using (MemoryStream outStream = new MemoryStream())
                    {
                        using (GZipStream zipStream = new GZipStream(inputStream, CompressionMode.Decompress))
                        {
                            zipStream.CopyTo(outStream);
                            zipStream.Close();
                            return outStream.ToArray();
                        }
                    }
                }
            }
            catch
            {
                return data;
            }
        }
    }
}