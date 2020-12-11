using GBT35255.Extensions;
using GBT35255.Utilities;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace GBT35255.ProtoBase
{
    /// <summary>
    /// 消息体结构体。
    /// </summary>
    public struct MessageBody
    {
        /// <summary>
        /// 消息ID。
        /// <para><see cref="MessageId"/>类型，长度为2个字节。</para>
        /// </summary>
        public MessageId MessageId { get; set; }

        /// <summary>
        /// 网关ID。
        /// <para>uint类型，长度为4个字节。</para>
        /// </summary>
        public uint GatewayId { get; set; }

        /// <summary>
        /// 灯具ID。
        /// <para>uint类型，长度为4个字节。</para>
        /// <para>0x00000000为保留地址，对于只需下发到网关的命令可以使用该地址。</para>
        /// <para>0x00000001~0xFFFFFF00分别对应入网的单灯的具体地址。</para>
        /// <para>0xFFFFFF01~0xFFFFFF20分别对应回路地址1～32路的地址。</para>
        /// <para>0xFFFFFF21~0xFFFFFF40分别对应组地址(组号)1～32。</para>
        /// <para>0xFFFFFF41~0xFFFFFFFE为保留地址。</para>
        /// <para>0xFFFFFFFF为广播地址，命令将下发到指定网关下的所有灯具设备。</para>
        /// </summary>
        public uint LuminaireId { get; set; }

        /// <summary>
        /// 参数列表。
        /// <para>长度可变。</para>
        /// <para>可选字段，对“请求”和“事件和告警”类型的消息有效。</para>
        /// </summary>
        public List<Parameter> ParameterList { get; set; }

        /// <summary> 
        /// 错误代码。
        /// <para><see cref="ErrorCode"/>类型，长度为4个字节。</para> 
        /// <para>可选字段，对“命令结果”类型的消息有效。</para> 
        /// </summary> 
        public ErrorCode ErrorCode { get; set; }

        /// <summary> 
        /// 错误信息。
        /// <para>string类型，长度可变。</para> 
        /// <para>可选字段，对“命令结果”类型的消息有效。</para> 
        /// </summary> 
        public string ErrorInfo { get; set; }

        /// <summary>
        /// DES 密钥。
        /// <para>该密钥运算模式采用 ECB 模式。</para>
        /// </summary>
        internal byte[] DESKey { get; }

        /// <summary>
        /// 通过“消息ID”、“网关ID”、“灯具ID”和“参数列表”初始化消息体对象实例。
        /// </summary>
        /// <param name="messageId">
        /// 消息ID。
        /// <para><see cref="MessageId"/>类型，长度为2个字节。</para>
        /// </param>
        /// <param name="gatewayId">
        /// 网关ID。
        /// <para>uint类型，长度为4个字节。</para>
        /// </param>
        /// <param name="luminaireId">
        /// 灯具ID。
        /// <para>uint类型，长度为4个字节。</para>
        /// <para>0x00000000为保留地址，对于只需下发到网关的命令可以使用该地址。</para>
        /// <para>0x00000001~0xFFFFFF00分别对应入网的单灯的具体地址。</para>
        /// <para>0xFFFFFF01~0xFFFFFF20分别对应回路地址1～32路的地址。</para>
        /// <para>0xFFFFFF21~0xFFFFFF40分别对应组地址(组号)1～32，</para>
        /// <para>0xFFFFFF41~0xFFFFFFFE为保留地址。</para>
        /// <para>0xFFFFFFFF为广播地址，命令将下发到指定网关下的所有灯具设备。</para>
        /// </param>
        /// <param name="parameterList">
        /// 参数列表。
        /// <para>长度可变。</para>
        /// <para>可选字段，对“请求”和“事件和告警”类型的消息有效。</para>
        /// </param>
        /// <param name="desKey">
        /// DES 密钥，默认不加密。
        /// <para>该密钥运算模式采用 ECB 模式。</para>
        /// </param>
        public MessageBody(MessageId messageId, uint gatewayId, uint luminaireId, List<Parameter> parameterList, byte[] desKey = null)
            : this()
        {
            MessageId = messageId;
            GatewayId = gatewayId;
            LuminaireId = luminaireId;
            ParameterList = parameterList;
            DESKey = desKey;
        }

        /// <summary>
        /// 通过“消息ID”、“网关ID”、“灯具ID”、“错误代码”和可选的“错误信息”初始化消息体对象实例。
        /// </summary>
        /// <param name="messageId">
        /// 消息ID。
        /// <para><see cref="MessageId"/>类型，长度为2个字节。</para>
        /// </param>
        /// <param name="gatewayId">
        /// 网关ID。
        /// <para>uint类型，长度为4个字节。</para>
        /// </param>
        /// <param name="luminaireId">
        /// 灯具ID。
        /// <para>uint类型，长度为4个字节。</para>
        /// <para>0x00000000为保留地址，对于只需下发到网关的命令可以使用该地址。</para>
        /// <para>0x00000001~0xFFFFFF00分别对应入网的单灯的具体地址。</para>
        /// <para>0xFFFFFF01~0xFFFFFF20分别对应回路地址1～32路的地址。</para>
        /// <para>0xFFFFFF21~0xFFFFFF40分别对应组地址(组号)1～32。</para>
        /// <para>0xFFFFFF41~0xFFFFFFFE为保留地址。</para>
        /// <para>0xFFFFFFFF为广播地址，命令将下发到指定网关下的所有灯具设备。</para>
        /// </param>
        /// <param name="errorCode">
        /// 错误代码。
        /// <para><see cref="ErrorCode"/>类型，长度为4个字节。</para>
        /// <para>可选字段，对“命令结果”类型的消息有效。</para>
        /// </param>
        /// <param name="errorInfo">
        /// 错误信息。
        /// <para>string类型，长度可变。</para> 
        /// <para>可选字段，对“命令结果”类型的消息有效。</para>
        /// </param>
        /// <param name="desKey">
        /// DES 密钥，默认不加密。
        /// <para>该密钥运算模式采用 ECB 模式。</para>
        /// </param>
        public MessageBody(MessageId messageId, uint gatewayId, uint luminaireId, ErrorCode errorCode, string errorInfo = null, byte[] desKey = null)
            : this()
        {
            MessageId = messageId;
            GatewayId = gatewayId;
            LuminaireId = luminaireId;
            ErrorCode = errorCode;
            ErrorInfo = errorInfo;
            DESKey = desKey;
        }

        /// <summary>
        /// 获取消息体字节数组。
        /// <para>如果 <see cref="DESKey"/> 不为空，则使用 <see cref="DES"/> 密钥加密消息体。</para>
        /// </summary>
        /// <returns></returns>
        public byte[] GetBody()
        {
            List<byte> mb = new List<byte>{
                (byte)((ushort)(MessageId) >> 8),
                (byte)(MessageId)
            };

            for (int i = 24; i >= 0; i -= 8)
            {
                mb.Add((byte)(GatewayId >> i));
            }

            for (int j = 24; j >= 0; j -= 8)
            {
                mb.Add((byte)(LuminaireId >> j));
            }

            if (ParameterList != null)
            {
                foreach (var pmt in ParameterList)
                {
                    mb.AddRange(pmt.GetParameter());
                }
            }
            else
            {
                for (int k = 24; k >= 0; k -= 8)
                {
                    mb.Add((byte)(((uint)ErrorCode) >> k));
                }

                if (ErrorInfo != null)
                {
                    mb.AddRange(ErrorInfo.ToByteArray());
                }
            }

            if (DESKey != null)
            {
                if (DESKey.Length != 8)
                {
                    throw new ArgumentNullException(nameof(DESKey), $"{typeof(DES)} 密钥长度不正确。");
                }
                else
                {
                    DESHelper des = new DESHelper();
                    return des.Encrypt(DESKey, mb.ToArray());
                }
            }
            else
            {
                return mb.ToArray();
            }
        }

        /// <summary>
        /// 获取消息体十六进制字符串。
        /// </summary>
        /// <param name="separator">
        /// 分隔符。
        /// <para>默认为空字符。</para>
        /// </param>
        /// <returns></returns>
        public string ToHexString(string separator = " ")
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in GetBody())
            {
                sb.Append(item.ToString("X2") + separator);
            }

            List<char> trimCharList = new List<char>(separator.ToCharArray());
            trimCharList.AddRange(new char[] { '0', ' ' });
            return sb.ToString().TrimEnd(trimCharList.ToArray());
        }

        /// <summary>
        /// 获取消息体字符串。
        /// </summary>
        /// <returns></returns>
        public override string ToString() => Encoding.UTF8.GetString(GetBody());
    }
}