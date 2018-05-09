using NRpc.Utils;
using System;
using System.IO;

namespace NRpc.Serializing.RpcSerializer
{
    /// <summary>
    /// 类名：SerializerInputStream.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/8 18:32:53
    /// </summary>
    public class SerializerInputStream : SelfDisposable
    {
        public SerializerInputStream()
        {
            CurrentStream = new MemoryStream();
        }

        public MemoryStream CurrentStream { get; }

        public int Length
        {
            get
            {
                return (int)CurrentStream.Length;
            }
        }

        public void Write(byte value)
        {
            CurrentStream.WriteByte(value);
        }

        public void Write(byte[] buffer, int offset, int count)
        {
            CurrentStream.Write(buffer, offset, count);
        }

        public void Write(byte[] buffer)
        {
            CurrentStream.Write(buffer, 0, buffer.Length);
        }

        public void Write(int value)
        {
            Write(BitConverter.GetBytes(value), 0, 4);
        }

        public void UpdateCurrentLength(int value, int offset)
        {
            CurrentStream.Position = offset;
            Write(BitConverter.GetBytes(value), 0, 4);
            CurrentStream.Position = CurrentStream.Length;
        }

        public void Write(long value)
        {
            Write(BitConverter.GetBytes(value), 0, 8);
        }

        public void Write(string value)
        {
            ByteUtil.EncodeString(value, out byte[] lengthBytes, out byte[] dataBytes);
            Write(lengthBytes);
            Write(dataBytes);
        }

        public byte[] ToBytes()
        {
            return CurrentStream.ToArray();
        }

        protected override void DisposeCode()
        {
            CurrentStream.Dispose();
        }
    }
}