using System.ComponentModel;

namespace GBT35255.ProtoBase
{
    /// <summary>
    /// 策略类型。
    /// </summary>
    [Description("策略类型")]
    public enum StrategyType : ushort
    {
        /// <summary>
        /// 定时任务。
        /// </summary>
        [Description("定时任务")]
        TimedTask = 0x3031,

        /// <summary>
        /// 经纬度。
        /// </summary>
        [Description("经纬度")]
        LonLat = 0x3032,

        /// <summary>
        /// 亮度传感器。
        /// </summary>
        [Description("亮度传感器")]
        BrightnessSensor = 0x3033,

        /// <summary>
        /// 移动传感器。
        /// </summary>
        [Description("移动传感器")]
        MoveSensor = 0x3034
    }
}