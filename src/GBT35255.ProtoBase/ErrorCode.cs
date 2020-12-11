using System.ComponentModel;

namespace GBT35255.ProtoBase
{
    /// <summary>
    /// 错误代码。
    /// <para>uint类型，长度为4个字节。</para>
    /// </summary>
    [Description("错误代码")]
    public enum ErrorCode : uint
    {
        #region 系统保留(0000~0099和9999)
        /// <summary>
        /// 成功。
        /// </summary>
        [Description("成功")]
        Succeed = 0x30303030,

        /// <summary>
        /// 消息解析错误。
        /// </summary>
        [Description("消息解析错误")]
        MessageParseError = 0x30303031,

        /// <summary>
        /// 校验和错误。
        /// </summary>
        [Description("校验和错误")]
        ChecksumError = 0x30303032,

        /// <summary>
        /// 消息ID未定义。
        /// </summary>
        [Description("消息ID未定义")]
        MessageIdUndefined = 0x30303033,

        /// <summary>
        /// 命令暂时不能执行。
        /// </summary>
        [Description("命令暂时不能执行")]
        NotExecuted = 0x30303034,

        /// <summary>
        /// 参数个数错误。
        /// </summary>
        [Description("参数个数错误")]
        ParameterCountError = 0x30303035,

        /// <summary>
        /// 参数格式错误。
        /// </summary>
        [Description("参数格式错误")]
        ParameterFormatError = 0x30303036,

        /// <summary>
        /// 参数范围错误。
        /// </summary>
        [Description("参数范围错误")]
        ParameterScopeError = 0x30303037,

        /// <summary>
        /// 参数类型未定义。
        /// </summary>
        [Description("参数类型未定义")]
        ParameterTypeUndefined = 0x30303038,

        /// <summary>
        /// 未知错误。
        /// </summary>
        [Description("未知错误")]
        Unknown = 0x39393939,
        #endregion

        #region 厂商自定义(0100~9998)
        /// <summary>
        /// 不支持该消息类型。
        /// </summary>
        [Description("不支持该消息类型")]
        NotSupportedMessageType = 0x30313030,

        /// <summary>
        /// 网关ID不存在。
        /// </summary>
        [Description("网关ID不存在")]
        NoExistsGatewayId = 0x30313031,

        /// <summary>
        /// 序列号不存在。
        /// </summary>
        [Description("序列号不存在")]
        NoExistsSerialNumber = 0x30313032,

        /// <summary>
        /// 安全验证码错误。
        /// </summary>
        [Description("安全验证码错误")]
        AccessCodeError = 0x30313033,

        /// <summary>
        /// 网关尚未完成初始化。
        /// </summary>
        [Description("网关尚未完成初始化")]
        NoInitialized = 0x30313034,

        /// <summary>
        /// 网关尚未完成入网控制。
        /// </summary>
        [Description("网关尚未完成入网控制")]
        NoAuthorized = 0x30313035,

        /// <summary>
        /// 网关已经完成入网控制。
        /// <para>无需重复进行入网控制操作。</para>
        /// </summary>
        [Description("网关已经完成入网控制")]
        IsAuthorized = 0x30313036,

        /// <summary>
        /// 网关不在线。
        /// </summary>
        [Description("网关不在线")]
        Offline = 0x30313037,

        /// <summary>
        /// 命令超时。
        /// </summary>
        [Description("命令超时")]
        CommandTimeout = 0x30313038,

        /// <summary>
        /// 参数长度错误。
        /// </summary>
        [Description("参数长度错误")]
        ParameterLengthError = 0x30313039,

        /// <summary>
        /// 灯具尚未绑定到网关。
        /// </summary>
        [Description("灯具尚未绑定到网关")]
        NotBinding = 0x30313041,

        /// <summary>
        /// 设备正在升级。
        /// </summary>
        [Description("设备正在升级")]
        DeviceUpgrading = 0x30313130,
        #endregion
    }
}