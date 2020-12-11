using System.ComponentModel;

namespace GBT35255.ProtoBase
{
    /// <summary>
    /// 参数类型枚举。
    /// </summary>
    [Description("参数类型")]
    public enum ParameterType : ushort
    {
        #region 配置命令参数
        /// <summary>
        /// 网关ID。
        /// <para>参数值最大字节数：10。</para>
        /// <para>用十进制的数字表示，由服务器分配。</para>
        /// <para>十进制范围为1～4294967295(2^32-1)。</para>
        /// <para>十六进制形式范围为0x00000001~0xffffffff。</para>
        /// </summary>
        [Description("网关ID")]
        GatewayId = 0x0001,

        /// <summary>
        /// 服务器IP地址。
        /// <para>参数值最大字节数：16。</para>
        /// <para>也可以使用域名的方式。</para>
        /// </summary>
        [Description("服务器IP地址")]
        ServerIP = 0x0002,

        /// <summary>
        /// 服务器端口号。
        /// <para>参数值最大字节数：4。</para>
        /// </summary>
        [Description("服务器端口号")]
        ServerPort = 0x0003,

        /// <summary>
        /// 通信协议。
        /// <para>参数值最大字节数：1。</para>
        /// <para>“01”：UDP。</para>
        /// <para>“02”：TCP。</para>
        /// </summary>
        [Description("通信协议")]
        Protocol = 0x0004,

        /// <summary>
        /// 日志级别。
        /// <para>参数值最大字节数：1。</para>
        /// <para>“01”：Debug。</para>
        /// <para>“02”：Error。</para>
        /// </summary>
        [Description("日志级别")]
        LogLevel = 0x0005,

        /// <summary>
        /// 日志类别。
        /// <para>参数值最大字节数：32。</para>
        /// <para>“COMM” ：通信传输的日志类别。</para>
        /// <para>“APP” ：应用层的日志类别。</para>
        /// <para>灯具厂商可以自己定义日志类别。</para>
        /// </summary>
        [Description("日志类别")]
        LogCategory = 0x0006,

        /// <summary>
        /// 命令响应时间。
        /// <para>参数值最大字节数：4。，</para>
        /// <para>命令响应的最大时间，单位为秒。</para>
        /// </summary>
        [Description("命令响应时间")]
        CommandACKTimeout = 0x0007,

        /// <summary>
        /// 命令重试次数。
        /// <para>参数值最大字节数：2。</para>
        /// <para>接收不到命令响应或者命令发送失败以后重发的次数。</para>
        /// </summary>
        [Description("命令重试次数")]
        CommandRetryTimes = 0x0008,

        /// <summary>
        /// 命令结果时间。
        /// <para>参数值最大字节数：4。</para>
        /// <para>等待命令执行结果的最大时间，单位为秒。</para>
        /// </summary>
        [Description("命令结果时间")]
        CommandResultTimeout = 0x0009,

        /// <summary>
        /// 事件响应时间。
        /// <para>参数值最大字节数：4，</para>
        /// <para>事件的最大响应时间，单位为秒。</para>
        /// </summary>
        [Description("事件响应时间")]
        EventACKTimeout = 0x00A,

        /// <summary>
        /// 事件重试次数。
        /// <para>参数值最大字节数：2。</para>
        /// <para>接受不到事件响应或者事件发送失败后重发的次数。</para>
        /// </summary>
        [Description("事件重试次数")]
        EventRetryTimes = 0x00B,

        /// <summary>
        /// 链路空闲时间。
        /// <para>参数值最大字节数：4。</para>
        /// <para>通讯链路上发送心跳包的空闲时间，单位为秒。</para>
        /// </summary>
        [Description("链路空闲时间")]
        LinkIdleTime = 0x000C,

        /// <summary>
        /// 心跳包响应超时时间。
        /// <para>参数值最大字节数：4。</para>
        /// <para>心跳包响应超时时间。</para>
        /// </summary>
        [Description("心跳包响应超时时间")]
        HeartbeatACKTimeout = 0x000D,

        /// <summary>
        /// 心跳包重试次数。
        /// <para>参数值最大字节数：2。</para>
        /// <para>收不到心跳包响应重发的次数-100。</para>
        /// </summary>
        [Description("心跳包重试次数")]
        HeatBeatRetryTimes = 0x000E,

        /// <summary>
        /// 灯具ID。
        /// <para>参数值最大字节数：10。</para>
        /// <para>网关所管理的灯具ID，用十进制的数字表示，由服务器分配，范围为0x00000001~0xffffff00。</para>
        /// <para>这个参数在命令中可以包含一个或者多个。</para>
        /// </summary>
        [Description("灯具ID")]
        LuminaireId = 0x000F,
        #endregion

        #region 操作维护命令参数
        /// <summary>
        /// 操作维护信息。
        /// <para>参数值最大字节数：128。</para>
        /// <para>操作维护的信息。</para>
        /// </summary>
        [Description("操作维护信息")]
        MaintainanceInfo = 0x0011,
        #endregion

        #region 控制命令参数
        /// <summary>
        /// 时间。
        /// <para>参数值最大字节数：2。</para>
        /// <para>使用24时的HHmm表示。</para>
        /// </summary>
        [Description("时间")]
        Time = 0x0020,

        /// <summary>
        /// 亮度。
        /// <para>即灯具的照度</para>
        /// <para>参数值最大字节数：3。</para>
        /// <para>亮度值，使用十进制表示，00-100。</para>
        /// </summary>
        [Description("灯具亮度")]
        Luminance = 0x0021,

        /// <summary>
        /// 开始日期。
        /// <para>参数值最大字节数：8。</para>
        /// <para>开始日期，yyyyMMdd。</para>
        /// </summary>
        [Description("开始日期")]
        BeginDate = 0x0022,

        /// <summary>
        /// 结束日期。
        /// <para>参数值最大字节数：8。</para>
        /// <para>结束日期，yyyyMMdd。</para>
        /// </summary>
        [Description("结束日期")]
        EndDate = 0x0023,

        /// <summary>
        /// 资源值。
        /// <para>参数值最大字节数：5。</para>
        /// <para>资源数值。</para>
        /// </summary>
        [Description("资源值")]
        ResourceValue = 0x0024,

        /// <summary>
        /// 门限值。
        /// <para>参数值最大字节数：8。</para>
        /// <para>阈值范围。</para>
        /// </summary>
        [Description("门限值")]
        ThresholdValue = 0x0026,

        /// <summary>
        /// 表示 <see cref="ResourceType"/> 资源类型。
        /// <para>参数值最大字节数：2。</para>
        /// <para>温度 <see cref="ResourceType.Temperature"/> 为：“01”。</para>
        /// <para>湿度 <see cref="ResourceType.Humidity"/> 为：“02”。</para>
        /// <para>电流 <see cref="ResourceType.Current"/> 为：“03”。</para>
        /// <para>电压 <see cref="ResourceType.Voltage"/> 为：“04”。</para>
        /// <para>亮度 <see cref="ResourceType.Luminance"/> 为：“05”。</para>
        /// <para>环境亮度 <see cref="ResourceType.Brightness"/> 为：“06”。</para>
        /// <para>是否有人 <see cref="ResourceType.是否有人"/> 为：“07”。</para>
        /// <para>以下为厂商自定义资源类型值：</para>
        /// <para>灯具好坏 <see cref="ResourceType.灯具好坏"/> 为：“10”。</para>
        /// <para>功率 <see cref="ResourceType.Power"/> 为：“11”。</para>
        /// <para>功率因素 <see cref="ResourceType.PowerFactor"/> 为：“12”。</para>
        /// <para>电能 <see cref="ResourceType.Energy"/> 为：“13”。</para>
        /// <para>是否在线 <see cref="ResourceType.是否在线"/> 为：“14”。</para>
        /// <para>经度 <see cref="ResourceType.Longitude"/> 为：“15”。</para>
        /// <para>纬度 <see cref="ResourceType.Latitude"/> 为：“16”。</para>
        /// <para>海拔 <see cref="ResourceType.Altitude"/> 为：“17”。</para>
        /// <para>移动传感器是否存在 <see cref="ResourceType.移动传感器是否存在"/> 为：“18”。</para>
        /// <para>运行时间 <see cref="ResourceType.RunTime"/> 为：“19”。</para>
        /// <para>所有：“00”。</para>
        /// </summary>
        [Description("资源类型")]
        ResourceType = 0x0025,

        /// <summary>
        /// 运行模式。
        /// <para>参数值最大字节数：2。</para>
        /// <para>0x01：自动运行模式。</para>
        /// <para>0x02：手工模式。</para>
        /// </summary>
        [Description("运行模式")]
        OperationMode = 0x0027,

        /// <summary>
        /// 延时时长。
        /// <para>参数值最大字节数：5。</para>
        /// <para>延迟的时间，单位：秒钟。</para>
        /// </summary>
        [Description("延时时长")]
        DelayTime = 0x0028,

        /// <summary>
        /// 采集周期。
        /// <para>采集周期（分钟为单位）。</para>
        /// </summary>
        [Description("采集周期")]
        Interval = 0x0029,

        /// <summary>
        /// 分组组号。
        /// <para>参数值最大字节数：2。</para>
        /// <para>组号（1~32）。</para>
        /// </summary>
        [Description("分组组号")]
        Group = 0x002A,

        /// <summary>
        /// 调光场景。
        /// <para>参数值最大字节数：2。</para>
        /// <para>场景值。</para>
        /// </summary>
        [Description("调光场景")]
        Scene = 0x002B,

        /// <summary>
        /// 访问码。
        /// <para>参数值最大字节数：16。</para>
        /// <para>安全认证码。</para>
        /// </summary>
        [Description("访问码")]
        AccessCode = 0x0030,

        /// <summary>
        /// 序列号。
        /// <para>参数值最大字节数：32。</para>
        /// <para>灯具的序列号。</para>
        /// </summary>
        [Description("序列号")]
        SerialNumber = 0x0031,

        /// <summary>
        /// RSA密钥。
        /// <para>该密钥为私钥。</para>
        /// <para>参数值最大字节数：128。</para>
        /// <para>使用base64转码的密钥。</para>
        /// </summary>
        [Description("RSA密钥")]
        RSAKey = 0x0040,

        /// <summary>
        /// DES密钥。
        /// <para>该密钥使用RSA公钥进行加密。</para>
        /// <para>参数值最大字节数：128。</para>
        /// <para>使用base64转码的密钥。</para>
        /// </summary>
        [Description("DES密钥")]
        DESKey = 0x0041,

        /// <summary>
        /// 系统时间。
        /// <para>参数值最大字节数：14。</para>
        /// <para>yyyyMMddHHmmss。</para>
        /// </summary>
        [Description("系统时间")]
        SystemTime = 0x0042,

        /// <summary>
        /// 文件大小。
        /// <para>参数值最大字节数：5。</para>
        /// <para>升级文件的大小。</para>
        /// </summary>
        [Description("文件大小")]
        FileSize = 0x0050,

        /// <summary>
        /// 文件段大小。
        /// <para>参数值最大字节数：2。</para>
        /// <para>每段文件大小。</para>
        /// </summary>
        [Description("文件段大小")]
        SegmentSize = 0x0051,

        /// <summary>
        /// 段的数量。
        /// <para>参数值最大字节数：2。</para>
        /// <para>总段数。</para>
        /// </summary>
        [Description("段的数量")]
        SegmentCount = 0x0052,
        #endregion

        #region 厂商自定义参数
        /// <summary>
        /// 设备种类。
        /// <para>参数值最大字节数：2。</para>
        /// <para>网关：“01”。</para>
        /// <para>灯具：“02”。</para>
        /// <para>其他：“03”。</para>
        /// <para>所有：“00”。</para>
        /// </summary>
        [Description("设备种类")]
        DeviceCategory = 0x0060,

        /// <summary>
        /// 设备类型。
        /// <para>参数值最大字节数：2。</para>
        /// <para>根据 <see cref="DeviceCategory"/> 进行复用。</para>
        /// <para>当 <see cref="DeviceCategory"/> 为“01”时：</para>
        /// <para>A型网关：“01”。</para>
        /// <para>B型网关：“02”。</para>
        /// <para>...</para>
        /// <para>当 <see cref="DeviceCategory"/> 为“02”时：</para>
        /// <para>A型灯具：“01”。</para>
        /// <para>B型灯具：“02”。</para>
        /// <para>...</para>
        /// </summary>
        [Description("设备类型")]
        DeviceType = 0x0061,

        /// <summary>
        /// 设备名称。
        /// </summary>
        [Description("设备名称")]
        DeviceName = 0x0062,

        /// <summary>
        /// 设备描述。
        /// </summary>
        [Description("设备描述")]
        Description = 0x0063,

        /// <summary>
        /// 多组组号。
        /// </summary>
        [Description("多组组号")]
        MultiGroup = 0x0064,

        /// <summary>
        /// 次数。
        /// </summary>
        [Description("次数")]
        次数 = 0x0065,

        /// <summary>
        /// 半径。
        /// <para>单位：米。</para>
        /// </summary>
        [Description("半径")]
        Radius = 0x0066,

        /// <summary>
        /// 数量。
        /// <para>单位：个。</para>
        /// </summary>
        [Description("数量")]
        Quantity = 0x0067,

        /// <summary>
        /// 持续时长。
        /// <para>单位：秒钟。</para>
        /// </summary>
        [Description("持续时长")]
        Duration = 0x0068,

        /// <summary>
        /// 灵敏度。
        /// </summary>
        [Description("灵敏度")]
        Sensitivity = 0x0069,

        /// <summary>
        /// 数字滤波参数。
        /// </summary>
        [Description("数字滤波参数")]
        DigitFilterParameter = 0x006A,

        /// <summary>
        /// 白天最小临界值。
        /// </summary>
        [Description("白天最小临界值")]
        DayMinThreshold = 0x006B,

        /// <summary>
        /// 无线频点。
        /// <para>ZigBee无线通信的信道。</para>
        /// <para>取值范围：[01,16]。</para>
        /// </summary>
        [Description("无线频点")]
        FrequencyPoint = 0x006C,

        /// <summary>
        /// 小时。
        /// </summary>
        [Description("小时")]
        Hour = 0x006D,

        /// <summary>
        /// 按星期重复。
        /// <para>用十六进制的字符串表示，使用时直接转换为二进制形式，从低位到高位分别表示周一到周日，最高位表示启用或禁用。</para>
        /// <para>如：16进制字符串"81"->2进制0x10000001表示每周1执行。</para>
        /// <para>16进制字符串"9F"->2进制0x10011111表示每个工作日执行。</para>
        /// <para>16进制字符串"D5"->2进制0x11010101表示每周1、3、5和周日执行。</para>
        /// <para>16进制字符串"E0"->2进制0x11100000表示每个周末执行。</para>
        /// <para>16进制字符串"FF"->2进制0x11111111表示每天执行。</para>
        /// <para>16进制字符串"80"->2进制0x10000000表示只执行一次。</para>
        /// </summary>
        [Description("按星期重复")]
        WeekRepeat = 0x006E,

        /// <summary>
        /// 编号。
        /// </summary>
        [Description("编号")]
        Number = 0x006F,

        /// <summary>
        /// 防盗开关。
        /// </summary>
        [Description("防盗开关")]
        SwitchBurglarAlarm = 0x0070,

        /// <summary>
        /// 移动传感器开关。
        /// <para>“00”表示禁用。</para>
        /// <para>“01”表示全天启用，灯具未打开时也可触发。</para>
        /// <para>“02”表示全天启用，灯具未打开时不能触发。</para>
        /// <para>“03”表示只在晚上开启，灯具未打开时也可触发。</para>
        /// <para>“04”表示只在晚上开启，灯具未打开时不能触发。</para>
        /// </summary>
        [Description("移动传感器开关")]
        SwitchMoveSensor = 0x0071,

        /// <summary>
        /// 亮度传感器开关。
        /// </summary>
        [Description("亮度传感器开关")]
        SwitchLightSensor = 0x0072,

        /// <summary>
        /// 天气状况开关。
        /// </summary>
        [Description("天气状况开关")]
        SwitchWeather = 0x0073,

        /// <summary>
        /// 交通量开关。
        /// </summary>
        [Description("交通量开关")]
        SwitchTrafficFlow = 0x0074,

        /// <summary>
        /// 经纬度开关。
        /// </summary>
        [Description("经纬度开关")]
        SwitchLongitudeLatitude = 0x0075,

        /// <summary>
        /// 光衰补偿开关。
        /// </summary>
        [Description("光衰补偿开关")]
        SwitchAttenuationCompensation = 0x0076,

        /// <summary>
        /// 经纬度开灯偏移时间。
        /// <para>正数表示延迟，负数表示提前，0表示立即。</para>
        /// <para>单位：秒钟。</para>
        /// </summary>
        [Description("经纬度开灯偏移时间")]
        LonLatTurnOnOffset = 0x0077,

        /// <summary>
        /// 经纬度关灯偏移时间。
        /// <para>正数表示延迟，负数表示提前，0表示立即。</para>
        /// <para>单位：秒钟。</para>
        /// </summary>
        [Description("经纬度关灯偏移时间")]
        LonLatTurnOffOffset = 0x0078,

        /// <summary>
        /// 段的文件内容。
        /// </summary>
        [Description("段的文件内容")]
        SegmentContent = 0x0079,

        /// <summary>
        /// CRC校验。
        /// <para>即循环冗余码校验。</para>
        /// </summary>
        [Description("CRC校验")]
        Crc = 0x007A,

        /// <summary>
        /// 版本号。
        /// <para>设备的软件（固件）或硬件版本号。</para>
        /// <para>可能由“主版本”、“次版本”、“生成号”和“修订号”四部分组成。</para>
        /// </summary>
        [Description("版本号")]
        VersionNumber = 0x007B,

        /// <summary>
        /// 表示 <see cref="StrategyType"/> 策略类型。
        /// <para><see cref="StrategyType.TimedTask"/>为：“01”</para>
        /// <para><see cref="StrategyType.BrightnessSensor"/>为：“02”</para>
        /// <para><see cref="StrategyType.MoveSensor"/>为：“03”</para>
        /// <para><see cref="StrategyType.LonLat"/>为：“04”</para>
        /// </summary>
        [Description("策略类型")]
        StrategyType = 0x007C,

        /// <summary>
        /// 表示 <see cref="ResourceType2"/> 资源类型2。
        /// <para><see cref="ResourceType2.移动传感器"/> 为：“01”</para>
        /// <para><see cref="ResourceType2.亮度传感器"/> 为：“02”</para>
        /// <para><see cref="ResourceType2.经纬度"/> 为：“03”</para>
        /// <para><see cref="ResourceType2.电参数"/> 为：“04”</para>
        /// </summary>
        [Description("资源类型2")]
        ResourceType2 = 0x0080,

        /// <summary>
        /// 黑夜最大临界值。
        /// </summary>
        [Description("黑夜最大临界值")]
        NightMaxThreshold = 0x0081,

        /// <summary>
        /// 是否被触发。
        /// </summary>
        [Description("是否被触发")]
        IsTriggered = 0x0082,

        /// <summary>
        /// 等待时长。
        /// <para>单位：秒钟。</para>
        /// </summary>
        [Description("等待时间")]
        WaitingTime = 0x0083,

        /// <summary>
        /// 环境亮度。
        /// </summary>
        [Description("环境亮度")]
        Brightness = 0x0084,

        /// <summary>
        /// 持续亮度。
        /// </summary>
        [Description("持续亮度")]
        KeepLuminance = 0x0085,

        /// <summary>
        /// 等待亮度。
        /// </summary>
        [Description("等待亮度")]
        WaitingLuminance = 0x0086,

        /// <summary>
        /// 设备好坏。
        /// </summary>
        [Description("设备好坏")]
        设备好坏 = 0x0087,

        /// <summary>
        /// GPS是否有效。
        /// </summary>
        [Description("GPS是否有效")]
        GpsValid = 0x0090,

        /// <summary>
        /// 经度。
        /// </summary>
        [Description("经度")]
        经度 = 0x0091,

        /// <summary>
        /// 纬度。
        /// </summary>
        [Description("纬度")]
        纬度 = 0x0092,

        /// <summary>
        /// 海拔。
        /// </summary>
        [Description("海拔")]
        海拔 = 0x0093,

        /// <summary>
        /// 时区。
        /// <para>例如：UTC+08:00</para>
        /// </summary>
        [Description("时区")]
        TimeZone = 0x0094,

        /// <summary>
        /// 温度。
        /// </summary>
        [Description("温度")]
        温度 = 0x00A0,

        /// <summary>
        /// 湿度。
        /// </summary>
        [Description("湿度")]
        湿度 = 0x00A1,

        /// <summary>
        /// 电流。
        /// </summary>
        [Description("电流")]
        电流 = 0x00A2,

        /// <summary>
        /// 电压。
        /// </summary>
        [Description("电压")]
        电压 = 0x00A3,

        /// <summary>
        /// 功率。
        /// </summary>
        [Description("功率")]
        功率 = 0x00A4,

        /// <summary>
        /// 功率因数。
        /// </summary>
        [Description("功率因数")]
        功率因数 = 0x00A5,

        /// <summary>
        /// 电能。
        /// </summary>
        [Description("电能")]
        电能 = 0x00A6,

        /// <summary>
        /// 开灯次数。
        /// </summary>
        [Description("开灯次数")]
        OpenCount = 0x00C0,

        /// <summary>
        /// 关灯次数。
        /// </summary>
        [Description("关灯次数")]
        CloseCount = 0x00C1,

        /// <summary>
        /// 调光次数。
        /// </summary>
        [Description("调光次数")]
        DimmingCount = 0x00C2,

        /// <summary>
        /// 国际移动设备身份码。
        /// </summary>
        [Description("国际移动设备身份码")]
        IMEI = 0x00D0,

        /// <summary>
        /// 国际移动用户识别码。
        /// </summary>
        [Description("国际移动用户识别码")]
        IMSI = 0x00D1,

        /// <summary>
        /// 信号强度。
        /// </summary>
        [Description("信号强度")]
        信号强度 = 0x00D2,

        /// <summary>
        /// 运营商名称。
        /// </summary>
        [Description("运营商名称")]
        运营商名称 = 0x00D3,

        /// <summary>
        /// 短信中心号码。
        /// </summary>
        [Description("短信中心号码")]
        短信中心号码 = 0x00D4,

        /// <summary>
        /// 手机号码。
        /// </summary>
        [Description("手机号码")]
        手机号码 = 0x00D5,

        /// <summary>
        /// 短信内容。
        /// </summary>
        [Description("短信内容")]
        短信内容 = 0x00D6,
        #endregion
    }
}