using GBT35255.Extensions;
using GBT35255.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GBT35255.ProtoBase
{
    /// <summary>
    /// 报文结构体。
    /// </summary>
    public struct Datagram
    {
        /// <summary>
        /// 起始符。
        /// <para>值为0x02。</para>
        /// </summary>
        public const byte Stx = 0x02;

        /// <summary>
        /// 结束符。
        /// <para>值为0x03。</para>
        /// </summary>
        public const byte Etx = 0x03;

        /// <summary>
        /// 消息头。
        /// <para><see cref="MessageHead"/>类型，长度为16字节。</para>
        /// </summary>
        public MessageHead Head { get; set; }

        /// <summary>
        /// 消息体。
        /// <para><see cref="MessageBody"/>类型，长度可变。</para>
        /// </summary>
        public MessageBody Body { get; set; }

        /// <summary>
        /// 获取一个值，该值指示报文 <see cref="Datagram"/> 的消息体 <see cref="MessageBody"/> 是否采用 <see cref="DES"/> 密钥加密。
        /// </summary>
        public bool IsCryptographic { get; private set; }

        /// <summary>
        /// 通过消息头初始化报文对象实例。
        /// </summary>
        /// <param name="head">
        /// 消息头。
        /// <para><see cref="MessageHead"/>类型，长度为16字节。</para>
        /// </param>
        public Datagram(MessageHead head)
            : this()
        {
            Head = head;
        }

        /// <summary>
        /// 通过消息头和消息体初始化报文对象实例。
        /// </summary>
        /// <param name="head">
        /// 消息头。
        /// <para><see cref="MessageHead"/>类型，长度为16字节。</para>
        /// </param>
        /// <param name="body">
        /// 消息体。
        /// <para><see cref="MessageBody"/>类型，长度可变。</para>
        /// </param>
        public Datagram(MessageHead head, MessageBody body)
            : this()
        {
            Head = head;
            Body = body;

            // 设置一个值指示采用 DES 密钥加密。
            IsCryptographic = Body.DESKey?.Length == 8;
        }

        /// <summary>
        /// 获取消息报文字节数组。
        /// </summary>
        /// <returns>消息报文字节数组。</returns>
        public byte[] GetDatagram()
        {
            if (Head.Type == MessageType.HeartbeatData)
            {
                return new byte[] { 0xFF };
            }
            else if (Head.Type == MessageType.HeartbeatResponse)
            {
                return new byte[] { 0xFE };
            }
            else
            {
                List<byte> dg = new List<byte> { Stx };
                byte[] head = Head.GetHead();
                dg.AddRange(Escaping(head));

                // “命令响应”和“事件和告警响应”类型的消息类型无消息体。
                if (Head.Type != MessageType.CommandACK
                    && Head.Type != MessageType.EventACK)
                {
                    byte[] body = Body.GetBody();
                    dg.AddRange(Escaping(body));
                }

                dg.Add(Etx);
                return dg.ToArray();
            }
        }

        /// <summary>
        /// 获取消息报文对象列表。
        /// </summary>
        /// <param name="dataArray">消息报文字节数组。</param>
        /// <param name="desKey">
        /// DES 密钥，默认不加密。
        /// <para>该密钥运算模式采用 ECB 模式。</para>
        /// </param>
        /// <param name="isTcpOrUdp">报文承载方式是否是TCP或UDP，默认为false。</param>
        /// <param name="isCheckCrc">是否校验CRC。</param>
        /// <returns>消息报文对象列表。</returns>
        internal static List<Datagram> GetDatagramList(byte[] dataArray, byte[] desKey = null, bool isTcpOrUdp = false, bool isCheckCrc = true)
        {
            List<byte> dataList = new List<byte>(dataArray);

            for (int i = dataArray.Length - 1; i >= 0; i--)
            {
                if (dataList[i] > 0)
                {
                    break;
                }

                dataList.RemoveAt(i);
            }

            if (dataList.Count < 15)
            {
                foreach (var item in dataList)
                {
                    if (item == 0xFF)
                    {
                        return new List<Datagram>()
                        {
                            new Datagram
                            (
                                new MessageHead(MessageType.HeartbeatData)
                            )
                        };
                    }

                    if (item == 0xFE)
                    {
                        return new List<Datagram>()
                        {
                            new Datagram
                            (
                                new MessageHead(MessageType.HeartbeatResponse)
                            )
                        };
                    }
                }

                throw new DatagramException("消息解析错误。", ErrorCode.MessageParseError);
            }

            List<Datagram> datagramList = new List<Datagram>();
            List<byte[]> newByteArrayList;

            if (!isTcpOrUdp)
            {
                List<byte[]> byteArrayList = new List<byte[]>();
                GetByteArrayList(dataList.ToArray(), 0, ref byteArrayList);
                newByteArrayList = Descaping(byteArrayList);
            }
            else
            {
                newByteArrayList = new List<byte[]> { dataList.ToArray() };
            }

            foreach (var tempByteArray in newByteArrayList)
            {
                if (tempByteArray.Length > 15)
                {
                    if (!Enum.IsDefined(typeof(MessageType), tempByteArray[0]))
                    {
                        throw new DatagramException("参数类型未定义。", ErrorCode.ParameterTypeUndefined);
                    }

                    Datagram d = new Datagram();
                    MessageHead mh = new MessageHead
                    {
                        Type = (MessageType)tempByteArray[0],
                        SeqNumber = (uint)((tempByteArray[1] << 24) + (tempByteArray[2] << 16) + (tempByteArray[3] << 8) + tempByteArray[4]),
                        Length = (ushort)((tempByteArray[5] << 8) + tempByteArray[6]),
                        Reserved = (ulong)((tempByteArray[7] << 32) + (tempByteArray[8] << 24) + (tempByteArray[9] << 16) + (tempByteArray[10] << 8) + tempByteArray[11]),
                        Crc32 = (uint)((tempByteArray[12] << 24) + (tempByteArray[13] << 16) + (tempByteArray[14] << 8) + tempByteArray[15])
                    };

                    if (mh.Type == MessageType.Command
                        || mh.Type == MessageType.Event
                        || mh.Type == MessageType.CommandResult)
                    {
                        byte[] newByteArray = tempByteArray.Where((b, index) => index >= 16 && index < 16 + mh.Length).ToArray();
                        byte[] msgBody;

                        if (desKey?.Length == 8)
                        {
                            DESHelper des = new DESHelper();
                            msgBody = des.Decrypt(desKey, newByteArray);

                            // 设置一个值指示采用 DES 密钥加密。
                            d.IsCryptographic = true;
                        }
                        else
                        {
                            msgBody = newByteArray;
                        }

                        if (msgBody.Length >= 10)
                        {
                            if (!Enum.IsDefined(typeof(MessageId), (ushort)((msgBody[0] << 8) + msgBody[1])))
                            {
                                throw new DatagramException("消息ID未定义。", ErrorCode.MessageIdUndefined);
                            }

                            MessageBody mb = new MessageBody
                            {
                                MessageId = (MessageId)((msgBody[0] << 8) + msgBody[1]),
                                GatewayId = ((uint)msgBody[2] << 24) + ((uint)msgBody[3] << 16) + ((uint)msgBody[4] << 8) + msgBody[5],
                                LuminaireId = ((uint)msgBody[6] << 24) + ((uint)msgBody[7] << 16) + ((uint)msgBody[8] << 8) + msgBody[9]
                            };

                            if (mh.Type == MessageType.CommandResult)
                            {
                                mb.ErrorCode = (ErrorCode)((msgBody[10] << 24) + (msgBody[11] << 16) + (msgBody[12] << 8) + msgBody[13]);
                                List<byte> errorInfoArrayList = new List<byte>();

                                for (int i = 14; i < msgBody.Length; i++)
                                {
                                    errorInfoArrayList.Add(msgBody[i]);
                                }

                                if (errorInfoArrayList.Count > 0)
                                {
                                    mb.ErrorInfo = errorInfoArrayList.ToArray().ToString2();
                                }
                            }
                            else
                            {
                                List<Parameter> pmtList = new List<Parameter>();
                                Parameter.GetParameterList(msgBody, 10, ref pmtList);
                                mb.ParameterList = pmtList;
                            }

                            if (isCheckCrc && Crc32.GetCrc32(newByteArray) != mh.Crc32)
                            {
                                throw new DatagramException("消息体CRC校验错误。", ErrorCode.ChecksumError);
                            }

                            d.Body = mb;
                        }
                        else
                        {
                            throw new DatagramException("消息解析错误。", ErrorCode.MessageParseError);
                        }
                    }

                    d.Head = mh;
                    datagramList.Add(d);
                }
            }

            return datagramList;
        }

        /// <summary>
        /// 转义特殊字符。
        /// <para>STX转义为ESC和0xE7，即02->1BE7。</para>
        /// <para>ETX转义为ESC和0xE8，即03->1BE8。</para>
        /// <para>ESC转义为ESC和0x00，即1B->1B00。</para>
        /// </summary>
        /// <param name="byteArray">消息报文字节数组。</param>
        /// <returns>转义后的字节数组。</returns>
        private byte[] Escaping(byte[] byteArray)
        {
            List<byte> byteList = new List<byte>();

            foreach (var item in byteArray)
            {
                if (item == 0x02)
                {
                    byteList.Add(0x1B);
                    byteList.Add(0xE7);
                }
                else if (item == 0x03)
                {
                    byteList.Add(0x1B);
                    byteList.Add(0xE8);
                }
                else if (item == 0x1B)
                {
                    byteList.Add(0x1B);
                    byteList.Add(0x00);
                }
                else
                {
                    byteList.Add(item);
                }
            }

            return byteList.ToArray();
        }

        /// <summary>
        /// 去除转义特殊字符。
        /// </summary>
        /// <param name="byteArray">原消息报文字节数组。</param>
        /// <returns>去除转义字符的字节数组。</returns>
        private static byte[] Descaping(byte[] byteArray)
        {
            List<byte> byteList = new List<byte>();

            for (int i = 0; i < byteArray.Length; i++)
            {
                if (byteArray[i] == 0x1B)
                {
                    if (i + 1 < byteArray.Length)
                    {
                        switch (byteArray[i + 1])
                        {
                            case 0xE7:
                                byteList.Add(0x02);
                                break;
                            case 0xE8:
                                byteList.Add(0x03);
                                break;
                            case 0x00:
                                byteList.Add(0x1B);
                                break;
                            default:
                                byteList.Add(byteArray[i + 1]);
                                break;
                        }
                    }
                    else
                    {
                        break;
                    }

                    i++;
                }
                else
                {
                    byteList.Add(byteArray[i]);
                }
            }

            return byteList.ToArray();
        }

        /// <summary>
        /// 去除转义特殊字符。
        /// </summary>
        /// <param name="byteArrayList">原消息报文字节数组列表。</param>
        /// <returns>去除转义字符的字节数组列表。</returns>
        private static List<byte[]> Descaping(List<byte[]> byteArrayList)
        {
            List<byte[]> newByteArrayList = new List<byte[]>();
            byte[] byteArray;

            foreach (var item in byteArrayList)
            {
                byteArray = Descaping(item);
                newByteArrayList.Add(byteArray);
            }

            return newByteArrayList;
        }

        /// <summary>
        /// 获取消息报文字节数组列表。
        /// <para>此列表中的消息报文字节数组不包含起止符。</para>
        /// </summary>
        /// <param name="dataArray">消息报文字节数组。</param>
        /// <param name="index">数组索引。</param>
        /// <param name="byteArrayList">消息报文字节数组列表。</param>
        private static void GetByteArrayList(byte[] dataArray, int index, ref List<byte[]> byteArrayList)
        {
            bool isStx = false;
            List<byte> byteList = new List<byte>();

            for (int i = index; i < dataArray.Length; i++)
            {
                if (dataArray[i] == 0x02)
                {
                    byteList = new List<byte>();
                    isStx = true;
                }
                else if (dataArray[i] == 0x03)
                {
                    isStx = false;

                    if (byteList.Count > 0)
                    {
                        byteArrayList.Add(byteList.ToArray());
                    }

                    GetByteArrayList(dataArray, i + 1, ref byteArrayList);
                    break;
                }
                else if (isStx)
                {
                    byteList.Add(dataArray[i]);
                }
            }
        }

        /// <summary>
        /// 获取消息报文十六进制字符串。
        /// </summary>
        /// <param name="separator">
        /// 分隔符。
        /// <para>默认为空字符。</para>
        /// </param>
        /// <returns>消息报文十六进制字符串。</returns>
        public string ToHexString(string separator = " ")
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in GetDatagram())
            {
                sb.Append(item.ToString("X2") + separator);
            }

            List<char> trimCharList = new List<char>(separator.ToCharArray());
            trimCharList.AddRange(new char[] { '0', ' ' });
            return sb.ToString().TrimEnd(trimCharList.ToArray());
        }

        /// <summary>
        /// 获取消息报文字符串。
        /// </summary>
        /// <returns>消息报文字符串。</returns>
        public override string ToString() => Encoding.UTF8.GetString(GetDatagram());
    }
}