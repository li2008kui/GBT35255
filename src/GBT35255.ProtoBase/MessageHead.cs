using System.Collections.Generic;
using System.Text;

namespace GBT35255.ProtoBase
{
    /// <summary>
    /// 消息头结构体。
    /// </summary>
    public struct MessageHead
    {
        /// <summary>
        /// 消息类型。
        /// <para><see cref="MessageType"/>类型，长度为1个字节。</para>
        /// </summary>
        public MessageType Type { get; set; }

        /// <summary>
        /// 消息序号。
        /// <para>uint类型，长度为4个字节。</para>
        /// </summary>
        public uint SeqNumber { get; set; }

        /// <summary>
        /// 消息体长度。
        /// <para>ushort类型，长度为2个字节。</para>
        /// </summary>
        public ushort Length { get; set; }

        /// <summary>
        /// 预留字段。
        /// <para>ulong类型，长度为5字节。</para>
        /// </summary>
        public ulong Reserved { get; set; }

        /// <summary>
        /// 消息体CRC32校验。
        /// <para>uint类型，长度为4个字节。</para>
        /// </summary>
        public uint Crc32 { get; set; }

        /// <summary>
        /// 通过“消息类型”初始化消息头对象实例。
        /// </summary>
        /// <param name="type">
        /// 消息类型。
        /// <para><see cref="MessageType"/>类型，长度为1个字节。</para>
        /// </param>
        public MessageHead(MessageType type)
            : this(type, Sequencer.Instance.SeqNumber++)
        { }

        /// <summary>
        /// 通过“消息类型”初始化消息头对象实例。
        /// </summary>
        /// <param name="type">
        /// 消息类型。
        /// <para><see cref="MessageType"/>类型，长度为1个字节。</para>
        /// </param>
        /// <param name="seqNumber">
        /// 消息序号。
        /// <para>uint类型，长度为4个字节。</para>
        /// </param>
        public MessageHead(MessageType type, uint seqNumber)
            : this()
        {
            Type = type;
            SeqNumber = seqNumber;
        }

        /// <summary>
        /// 通过“消息类型”、“消息体长度”和“消息体CRC32校验”初始化消息头对象实例。
        /// </summary>
        /// <param name="type">
        /// 消息类型。
        /// <para><see cref="MessageType"/>类型，长度为1个字节。</para>
        /// </param>
        /// <param name="length">
        /// 消息体长度。
        /// <para>ushort类型，长度为2个字节。</para>
        /// </param>
        /// <param name="crc32">
        /// 消息体CRC32校验。
        /// <para>uint类型，长度为4个字节。</para>
        /// </param>
        public MessageHead(MessageType type, ushort length, uint crc32)
                : this(type)
        {
            Length = length;
            Crc32 = crc32;
        }

        /// <summary>
        /// 通过“消息类型”、“消息序号”、“消息体长度”和“消息体CRC32校验”初始化消息头对象实例。
        /// </summary>
        /// <param name="type">
        /// 消息类型。
        /// <para><see cref="MessageType"/>类型，长度为1个字节。</para>
        /// </param>
        /// <param name="seqNumber">
        /// 消息序号。
        /// <para>uint类型，长度为4个字节。</para>
        /// </param>
        /// <param name="length">
        /// 消息体长度。
        /// <para>ushort类型，长度为2个字节。</para>
        /// </param>
        /// <param name="crc32">
        /// 消息体CRC32校验。
        /// <para>uint类型，长度为4个字节。</para>
        /// </param>
        public MessageHead(MessageType type, uint seqNumber, ushort length, uint crc32)
                : this(type, seqNumber)
        {
            Length = length;
            Crc32 = crc32;
        }

        /// <summary>
        /// 获取消息头字节数组。
        /// </summary>
        /// <returns></returns>
        public byte[] GetHead()
        {
            List<byte> mh = new List<byte>();
            mh.Add((byte)(Type));

            for (int i = 24; i >= 0; i -= 8)
            {
                mh.Add((byte)(SeqNumber >> i));
            }

            mh.Add((byte)(Length >> 8));
            mh.Add((byte)(Length));

            for (int j = 0; j < 5; j++)
            {
                mh.Add(0x00);
            }

            for (int k = 24; k >= 0; k -= 8)
            {
                mh.Add((byte)(Crc32 >> k));
            }

            return mh.ToArray();
        }

        /// <summary>
        /// 获取消息头十六进制字符串。
        /// </summary>
        /// <param name="separator">
        /// 分隔符。
        /// <para>默认为空字符。</para>
        /// </param>
        /// <returns></returns>
        public string ToHexString(string separator = " ")
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in GetHead())
            {
                sb.Append(item.ToString("X2") + separator);
            }

            List<char> trimCharList = new List<char>(separator.ToCharArray());
            trimCharList.AddRange(new char[] { '0', ' ' });
            return sb.ToString().TrimEnd(trimCharList.ToArray());
        }

        /// <summary>
        /// 获取消息头字符串。
        /// </summary>
        /// <returns></returns>
        public override string ToString() => Encoding.UTF8.GetString(GetHead());
    }
}