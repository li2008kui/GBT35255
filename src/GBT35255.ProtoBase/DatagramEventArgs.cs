using System;
using System.Collections.Generic;

namespace GBT35255.ProtoBase
{
    /// <summary>
    /// 消息报文事件参数类。
    /// </summary>
    public class DatagramEventArgs : EventArgs
    {
        /// <summary>
        /// 数据数组。
        /// </summary>
        public byte[] DataArray { get; private set; }

        /// <summary>
        /// 消息报文对象列表。
        /// </summary>
        public List<Datagram> DatagramList { get; private set; }

        /// <summary>
        /// 初始化 DatagramEventArgs 类的新实例。
        /// </summary>
        private DatagramEventArgs() : base() { }

        /// <summary>
        /// 通过报文字节数组初始化 DatagramEventArgs 类的新实例。
        /// </summary>
        /// <param name="dataArray">消息报文字节数组。</param>
        /// <param name="desKey">
        /// DES 密钥，默认不加密。
        /// <para>该密钥运算模式采用 ECB 模式。</para>
        /// </param>
        /// <param name="isTcpOrUdp">报文承载方式是否是 TCP 或 UDP，默认为 False。</param>
        /// <param name="isCheckCrc">是否校验 CRC。</param>
        public DatagramEventArgs(byte[] dataArray, byte[] desKey = null, bool isTcpOrUdp = false, bool isCheckCrc = true)
            : base()
        {
            DataArray = dataArray;
            DatagramList = Datagram.GetDatagramList(dataArray, desKey, isTcpOrUdp, isCheckCrc);
        }
    }
}