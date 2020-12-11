using System;
using System.Runtime.Serialization;

namespace GBT35255.ProtoBase
{
    /// <summary>
    /// 表示发生错误时引发的 GBT35255 异常。
    /// </summary>
    [Serializable]
    public class DatagramException : ApplicationException
    {
        /// <summary>
        /// 错误代码。
        /// <para><see cref="ErrorCode"/>类型，长度为4个字节。</para>
        /// </summary>
        public ErrorCode Code { get; private set; }

        /// <summary>
        /// 初始化 GBT35255 Exception 类的新实例。
        /// </summary>
        /// <param name="code">
        /// 错误代码。
        /// <para><see cref="ErrorCode"/>类型，长度为4个字节。</para>
        /// </param>
        public DatagramException(ErrorCode code = ErrorCode.Succeed)
            : base()
        {
            Code = code;
        }

        /// <summary>
        /// 使用指定错误消息初始化 GBT35255 Exception 类的新实例。
        /// </summary>
        /// <param name="message">解释异常原因的错误信息。</param>
        /// <param name="code">
        /// 错误代码。
        /// <para><see cref="ErrorCode"/>类型，长度为4个字节。</para>
        /// </param>
        public DatagramException(string message, ErrorCode code = ErrorCode.Succeed)
            : base(message)
        {
            Code = code;
        }

        /// <summary>
        /// Initializes a new instance of the GBT35255 Exception class with
        /// serialized data.
        /// </summary>
        /// <param name="info">保存序列化对象数据的对象。</param>
        /// <param name="context">有关源或目标的上下文信息。</param>
        /// <param name="code">
        /// 错误代码。
        /// <para><see cref="ErrorCode"/>类型，长度为4个字节。</para>
        /// </param>
        public DatagramException(SerializationInfo info, StreamingContext context, ErrorCode code = ErrorCode.Succeed)
            : base(info, context)
        {
            Code = code;
        }

        /// <summary>
        /// Initializes a new instance of the GBT35255 Exception class with
        /// a specified error message and a reference to the inner exception that is
        /// the cause of this exception.
        /// </summary>
        /// <param name="message">解释异常原因的错误信息。</param>
        /// <param name="innerException">导致当前异常的异常。如果 innerException 参数不为空引用，则在处理内部异常的 catch 块中引发当前异常。</param>
        /// <param name="code">
        /// 错误代码。
        /// <para><see cref="ErrorCode"/>类型，长度为4个字节。</para>
        /// </param>
        public DatagramException(string message, Exception innerException, ErrorCode code = ErrorCode.Succeed)
            : base(message, innerException)
        {
            Code = code;
        }
    }
}