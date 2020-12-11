using System;
using System.Collections.Generic;
using System.Text;

namespace GBT35255.ProtoBase
{
    /// <summary>
    /// 参数结构体。
    /// </summary>
    public struct Parameter
    {
        /// <summary>
        /// 参数类型。
        /// <para><see cref="ParameterType"/>类型，长度为2个字节。</para>
        /// </summary>
        public ParameterType Type { get; set; }

        /// <summary>
        /// 参数值。
        /// <para><see cref="byte"/>类型的数组，长度可变。</para>
        /// </summary>
        public byte[] Value { get; set; }

        /// <summary>
        /// 参数值结束符。
        /// <para>值为0x00。</para>
        /// </summary>
        public const byte End = 0x00;

        /// <summary>
        /// 通过“参数类型”和字节类型的“参数值”初始化参数对象实例。
        /// </summary>
        /// <param name="type">
        /// 参数类型。
        /// <para><see cref="ParameterType"/>类型，长度为2个字节。</para>
        /// </param>
        /// <param name="value">字节类型的参数值。</param>
        /// <exception cref="DatagramException">表示发生错误时引发的 GBT35255 异常。</exception>
        public Parameter(ParameterType type, byte value)
            : this(type, new byte[] { value })
        { }

        /// <summary>
        /// 通过“参数类型”和字符串类型的“参数值”初始化参数对象实例。
        /// </summary>
        /// <param name="type">
        /// 参数类型。
        /// <para><see cref="ParameterType"/>类型，长度为2个字节。</para>
        /// </param>
        /// <param name="value">字符串类型的参数值。</param>
        /// <exception cref="DatagramException">表示发生错误时引发的 GBT35255 异常。</exception>
        public Parameter(ParameterType type, string value)
            : this(type, Encoding.UTF8.GetBytes(value))
        { }

        /// <summary>
        /// 通过“参数类型”和字节数组类型的“参数值”初始化参数对象实例。
        /// </summary>
        /// <param name="type">
        /// 参数类型。
        /// <para><see cref="ParameterType"/>类型，长度为2个字节。</para>
        /// </param>
        /// <param name="value">字节数组类型的参数值。</param>
        /// <exception cref="DatagramException">表示发生错误时引发的 GBT35255 异常。</exception>
        public Parameter(ParameterType type, byte[] value)
            : this()
        {
            CheckParameterValue(type, value);

            Type = type;
            Value = value;
        }

        /// <summary>
        /// 获取参数字节数组。
        /// </summary>
        /// <returns></returns>
        public byte[] GetParameter()
        {
            List<byte> pmt = new List<byte> {
                (byte)((ushort)(Type) >> 8),
                (byte)(Type)
            };

            pmt.AddRange(Value);
            pmt.Add(0x00);
            return pmt.ToArray();
        }

        private static void CheckParameterValue(ParameterType type, byte[] value)
        {
            if (type == ParameterType.GatewayId)
            {
                if (value.Length != 4)
                {
                    throw new DatagramException("参数长度错误。", ErrorCode.ParameterLengthError);
                }

                uint gatewayId = (uint)(value[0] << 24) + (uint)(value[1] << 16) + (uint)(value[2] << 8) + value[3];

                if (gatewayId < 1 || gatewayId > 0xFFFFFFFF)
                {
                    throw new DatagramException("参数范围错误。", ErrorCode.ParameterScopeError);
                }
            }
            else if (type == ParameterType.LuminaireId)
            {
                if (value.Length != 4)
                {
                    throw new DatagramException("参数长度错误。", ErrorCode.ParameterLengthError);
                }

                uint luminaireId = (uint)(value[0] << 24) + (uint)(value[1] << 16) + (uint)(value[2] << 8) + value[3];

                if (luminaireId < 1 || luminaireId > 0xFFFFFF00)
                {
                    throw new DatagramException("参数范围错误。", ErrorCode.ParameterScopeError);
                }
            }
            else
            {

            }
        }

        /// <summary>
        /// 获取参数对象列表。
        /// </summary>
        /// <param name="byteArray">消息报文字节数组。</param>
        /// <param name="index">数组索引。</param>
        /// <param name="pmtList">参数对象列表。</param>
        internal static void GetParameterList(byte[] byteArray, int index, ref List<Parameter> pmtList)
        {
            if (byteArray.Length > index + 2)
            {
                Parameter parameter = new Parameter();
                List<byte> byteList = new List<byte>();
                ushort pmtType = (ushort)((byteArray[index] << 8) + byteArray[index + 1]);

                if (!Enum.IsDefined(typeof(ParameterType), pmtType))
                {
                    throw new DatagramException("参数类型未定义。", new Exception("若参数类型确实已经定义，请查看参数值是否包含参数结束符0x00。"), ErrorCode.ParameterTypeUndefined);
                }

                parameter.Type = (ParameterType)(pmtType);

                // 由于网关ID和灯具ID可能包含参数结束符，故需要特殊处理
                if (parameter.Type == ParameterType.GatewayId || parameter.Type == ParameterType.LuminaireId)
                {
                    byteList.Add(byteArray[index + 2]);
                    byteList.Add(byteArray[index + 3]);
                    byteList.Add(byteArray[index + 4]);
                    byteList.Add(byteArray[index + 5]);
                    index += 4;
                }
                else
                {
                    byte indexByte = byteArray[index + 2];

                    while (indexByte != 0x00)
                    {
                        byteList.Add(indexByte);
                        index++;

                        if (byteArray.Length <= index + 2)
                        {
                            throw new DatagramException("参数格式错误。", ErrorCode.ParameterFormatError);
                        }

                        indexByte = byteArray[index + 2];
                    }
                }

                parameter.Value = byteList.ToArray();
                pmtList.Add(parameter);

                GetParameterList(byteArray, index + 3, ref pmtList);
            }
        }

        /// <summary>
        /// 获取参数十六进制字符串。
        /// </summary>
        /// <param name="separator">
        /// 分隔符。
        /// <para>默认为空字符。</para>
        /// </param>
        /// <returns></returns>
        public string ToHexString(string separator = " ")
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in GetParameter())
            {
                sb.Append(item.ToString("X2") + separator);
            }

            List<char> trimCharList = new List<char>(separator.ToCharArray());
            trimCharList.AddRange(new char[] { '0', ' ' });
            return sb.ToString().TrimEnd(trimCharList.ToArray());
        }

        /// <summary>
        /// 获取参数字符串。
        /// </summary>
        /// <returns></returns>
        public override string ToString() => Encoding.UTF8.GetString(GetParameter());
    }
}