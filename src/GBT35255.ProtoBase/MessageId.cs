using System.ComponentModel;

namespace GBT35255.ProtoBase
{
    /// <summary>
    /// 消息ID枚举。
    /// </summary>
    [Description("消息ID")]
    public enum MessageId : ushort
    {
        #region 配置命令
        /// <summary>
        /// 配置命令。
        /// </summary>
        [Description("配置")]
        ConfigurationCommand = 0x1001,
        #endregion

        #region 操作维护
        /// <summary>
        /// 操作维护。
        /// </summary>
        [Description("维护")]
        OperationMaintenance = 0x1002,
        #endregion

        #region 控制命令
        /// <summary>
        /// 设置默认开灯时间。
        /// </summary>
        [Description("设置默认开灯时间")]
        SettingDefaultTurnOnTime = 0x1201,

        /// <summary>
        /// 设置默认关灯时间。
        /// </summary>
        [Description("设置默认关灯时间")]
        SettingDefaultTurnOffTime = 0x1202,

        /// <summary>
        /// 设置默认调整亮度时间。
        /// </summary>
        [Description("设置默认调整亮度时间")]
        SettingDefaultAdjustBrightnessTime = 0x1203,

        /// <summary>
        /// 设置计划开灯时间。
        /// </summary>
        [Description("设置计划开灯时间")]
        SettingPlanTurnOnTime = 0x1204,

        /// <summary>
        /// 设置计划关灯时间。
        /// </summary>
        [Description("设置计划关灯时间")]
        SettingPlanTurnOffTime = 0x1205,

        /// <summary>
        /// 设置灯具调光计划。
        /// </summary>
        [Description("设置灯具调光计划")]
        SettingLuminaireDimmingPlan = 0x1206,

        /// <summary>
        /// 设置触发告警阈值。
        /// </summary>
        [Description("设置触发告警阈值")]
        SettingTriggerAlarmThreshold = 0x1207,

        /// <summary>
        /// 实时开/关灯和调整亮度。
        /// </summary>
        [Description("实时开/关灯和调整亮度")]
        RealTimeControlLuminaire = 0x1208,

        /// <summary>
        /// 实时查询灯具状态。
        /// </summary>
        [Description("实时查询灯具状态")]
        RealTimeQueryLuminaireStatus = 0x1209,

        /// <summary>
        /// 设置灯具数据采集周期。
        /// </summary>
        [Description("设置灯具数据采集周期")]
        SettingLuminaireDataCollectionPeriod = 0x120A,

        /// <summary>
        /// 设置灯具分组。
        /// </summary>
        [Description("设置灯具分组")]
        SettingLuminaireGroup = 0x120B,

        /// <summary>
        /// 删除灯具分组。
        /// </summary>
        [Description("删除灯具分组")]
        RemoveLuminaireGroup = 0x120C,

        /// <summary>
        /// 设置灯具场景。
        /// </summary>
        [Description("设置灯具场景")]
        SettingLuminaireScene = 0x120D,

        /// <summary>
        /// 删除灯具场景。
        /// </summary>
        [Description("删除灯具场景")]
        RemoveLuminaireScene = 0x120E,

        /// <summary>
        /// 设置灯具运行模式。
        /// <para>自动或手动。</para>
        /// </summary>
        [Description("设置灯具运行模式")]
        SettingRunningMode = 0x120F,

        /// <summary>
        /// 要求上传灯具日志。
        /// </summary>
        [Description("要求上传灯具日志")]
        RequireReportLuminaireLog = 0x1210,

        /// <summary>
        /// 灯具恢复出厂状态。
        /// </summary>
        [Description("灯具恢复出厂状态")]
        LuminaireFactoryReset = 0x1211,

        /// <summary>
        /// 更新RSA密钥。
        /// </summary>
        [Description("更新RSA密钥")]
        UpdateRSAKey = 0x1212,

        /// <summary>
        /// 更新DES密钥。
        /// </summary>
        [Description("更新DES密钥")]
        UpdateDESKey = 0x1213,

        /// <summary>
        /// 同步时间。
        /// </summary>
        [Description("同步时间")]
        SynchronizeTime = 0x1214,

        /// <summary>
        /// 设置通信故障下灯具默认亮度。
        /// </summary>
        [Description("设置通信故障下灯具默认亮度")]
        SettingCommunicationFailureDefaultBrightness = 0x1215,

        /// <summary>
        /// 设置灯具默认上电亮度。
        /// </summary>
        [Description("设置灯具默认上电亮度")]
        SettingPowerOnDefaultBrightness = 0x1216,

        /// <summary>
        /// 接入认证请求。
        /// <para>由网关向服务器发起。</para>
        /// </summary>
        [Description("接入认证请求")]
        AccessAuthenticationRequest = 0x1300,

        #region 厂商自定义
        /// <summary>
        /// 启用或禁用功能。
        /// <para>防盗。</para>
        /// <para>移动传感器。</para>
        /// <para>亮度传感器。</para>
        /// <para>天气状况。</para>
        /// <para>交通量。</para>
        /// <para>经纬度。</para>
        /// <para>光衰补偿。</para>
        /// </summary>
        [Description("启用或禁用功能")]
        SettingEnableFunction = 0x1400,

        /// <summary>
        /// 灯具绑定到网关。
        /// </summary>
        [Description("灯具绑定到网关")]
        BindingGateway = 0x1401,

        /// <summary>
        /// 灯具从网关解除绑定。
        /// </summary>
        [Description("灯具从网关解除绑定")]
        UnbindingGateway = 0x1402,

        /// <summary>
        /// 设置ZigBee无线网络参数。
        /// <para>无线频点，即ZigBee无线通信的信道，取值范围：[01,16]。</para>
        /// <para>其他参数待定。</para>
        /// </summary>
        [Description("设置ZigBee无线网络参数")]
        SettingZigBeeParameter = 0x1403,

        /// <summary>
        /// 设置光衰补偿参数。
        /// </summary>
        [Description("设置光衰补偿参数")]
        SettingAttenuationCompensationParameter = 0x1404,

        /// <summary>
        /// 搜索设备。
        /// </summary>
        [Description("搜索设备")]
        SearchDevice = 0x1405,

        /// <summary>
        /// 查询计划定时任务。
        /// </summary>
        [Description("查询计划定时任务")]
        QueryPlanTimingTask = 0x1406,

        /// <summary>
        /// 删除计划定时任务。
        /// </summary>
        [Description("删除计划定时任务")]
        RemovePlanTimingTask = 0x1407,

        /// <summary>
        /// 查询资源2状态参数。
        /// </summary>
        [Description("查询资源2状态参数")]
        QueryResource2Status = 0x1408,

        /// <summary>
        /// 设置移动传感器参数。
        /// </summary>
        [Description("设置移动传感器参数")]
        设置移动传感器参数 = 0x1409,

        /// <summary>
        /// 设置亮度传感器参数。
        /// </summary>
        [Description("设置亮度传感器参数")]
        设置亮度传感器参数 = 0x140A,

        /// <summary>
        /// 设置经纬度参数。
        /// </summary>
        [Description("设置经纬度参数")]
        设置经纬度参数 = 0x140B,

        /// <summary>
        /// 校准电参数。
        /// </summary>
        [Description("校准电参数")]
        校准电参数 = 0x140C,

        /// <summary>
        /// 设置电参数阈值。
        /// </summary>
        [Description("设置电参数阈值")]
        设置电参数阈值 = 0x140D,

        /// <summary>
        /// 准备下发调光策略。
        /// </summary>
        [Description("准备下发调光策略")]
        准备下发调光策略 = 0x140E,

        /// <summary>
        /// 查询通信网络参数。
        /// <para>如GPRS/3G/4G等网络参数。</para>
        /// </summary>
        [Description("查询通信网络参数")]
        查询通信网络参数 = 0x1410,

        /// <summary>
        /// 发送短信息。
        /// </summary>
        [Description("发送短信息")]
        发送短信息 = 0x1411,

        /// <summary>
        /// 读取短信息。
        /// </summary>
        [Description("读取短信息")]
        读取短信息 = 0x1412,

        /// <summary>
        /// 查询灯具分组。
        /// </summary>
        [Description("查询灯具分组")]
        查询灯具分组 = 0x1420,
        #endregion
        #endregion

        #region 事件列表
        #region 数据采集
        /// <summary>
        /// 数据采集。
        /// </summary>
        [Description("数据采集")]
        DataCollection = 0x2101,

        #region 厂商自定义
        /// <summary>
        /// 上报搜索到的设备。
        /// </summary>
        [Description("上报搜索到的设备")]
        DeviceDiscovered = 0x2110,

        /// <summary>
        /// 上报计划定时任务。
        /// </summary>
        [Description("上报计划定时任务")]
        ReportPlanTimingTask = 0x2111,

        /// <summary>
        /// 上报移动传感器参数。
        /// </summary>
        [Description("上报移动传感器参数")]
        上报移动传感器参数 = 0x2112,

        /// <summary>
        /// 上报亮度传感器状态。
        /// </summary>
        [Description("上报亮度传感器参数")]
        上报亮度传感器参数 = 0x2113,

        /// <summary>
        /// 上报经纬度参数。
        /// </summary>
        [Description("上报经纬度参数")]
        上报经纬度参数 = 0x2114,

        /// <summary>
        /// 上报电参数。
        /// </summary>
        [Description("上报电参数")]
        上报电参数 = 0x2115,

        /// <summary>
        /// 上报电参数阈值。
        /// </summary>
        [Description("上报电参数阈值")]
        上报电参数阈值 = 0x2116,

        /// <summary>
        /// 上报灯具运行模式。
        /// </summary>
        [Description("上报灯具运行模式")]
        ReportRunningMode = 0x2117,

        /// <summary>
        /// 上报灯具开关调光次数。
        /// </summary>
        [Description("上报灯具开关调光次数")]
        上报灯具开关调光次数 = 0x2118,

        /// <summary>
        /// 上报通信网络参数。
        /// </summary>
        [Description("上报通信网络参数")]
        上报通信网络参数 = 0x2119,

        /// <summary>
        /// 上报短信内容。
        /// </summary>
        [Description("上报短信内容")]
        上报短信内容 = 0x211A,

        /// <summary>
        /// 请求下发调光策略。
        /// </summary>
        [Description("请求下发调光策略")]
        请求下发调光策略 = 0x211B,

        /// <summary>
        /// 上报灯具分组。
        /// </summary>
        [Description("上报灯具分组")]
        上报灯具分组 = 0x2120,
        #endregion
        #endregion

        #region 故障告警事件
        /// <summary>
        /// 灯具重新启动。
        /// </summary>
        [Description("灯具重新启动")]
        LuminaireRestart = 0x2200,

        /// <summary>
        /// 灯具临界告警消除。
        /// </summary>
        [Description("灯具临界告警消除")]
        LuminaireThresholdEliminateAlarm = 0x2202,

        /// <summary>
        /// 灯具临界告警。
        /// </summary>
        [Description("灯具临界告警")]
        LuminaireThresholdAlarm = 0x2302,

        /// <summary>
        /// 网关与灯具通信故障告警。
        /// </summary>
        [Description("网关与灯具通信故障告警")]
        CommunicationFailureAlarm = 0x2303,

        /// <summary>
        /// 网关与灯具通信故障告警消除。
        /// </summary>
        [Description("网关与灯具通信故障告警消除")]
        CommunicationFailureEliminateAlarm = 0x2203,

        /// <summary>
        /// 灯具未按控制设定工作告警。
        /// </summary>
        [Description("灯具未按控制设定工作告警")]
        LuminaireRunExceptionAlarm = 0x2304,

        /// <summary>
        /// 灯具未按控制设定工作告警消除。
        /// </summary>
        [Description("灯具未按控制设定工作告警消除")]
        LuminaireRunExceptionEliminateAlarm = 0x2204,

        /// <summary>
        /// 灯具防盗告警。
        /// </summary>
        [Description("灯具防盗告警")]
        LuminaireBurglarAlarm = 0x2305,
        #endregion
        #endregion

        #region 远程升级
        /// <summary>
        /// 远程升级。
        /// </summary>
        [Description("远程升级")]
        RemoteUpgrade = 0x1100,

        /// <summary>
        /// 请求第N段文件。
        /// </summary>
        [Description("请求第N段文件")]
        RequestNthSegmentFile = 0x1101,

        #region 厂商自定义
        /// <summary>
        /// 获取设备版本。
        /// </summary>
        [Description("获取设备版本")]
        获取设备版本 = 0x1110,

        /// <summary>
        /// 上报设备版本。
        /// </summary>
        [Description("上报设备版本")]
        上报设备版本 = 0x1111,
        #endregion
        #endregion
    }
}