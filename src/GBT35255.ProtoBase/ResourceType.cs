using System.ComponentModel;
using System.Text;

namespace GBT35255.ProtoBase
{
    /// <summary>
    /// 资源类型枚举。
    /// <para>使用时需要将枚举值转换成字符串形式。</para>
    /// <para>转换方式如下：</para>
    /// <para><see cref="StringHelper.ToByteArray(string, bool)"/></para>
    /// <para><see cref="Encoding.UTF8"/></para>
    /// </summary>
    [Description("资源类型")]
    public enum ResourceType : ushort
    {
        #region 系统保留
        /// <summary>
        /// 温度。
        /// <para>灯具控制器的温度。</para>
        /// <para>单位：华氏度（非GBT35255规范）。</para>
        /// <para>摄氏度和华氏度的换算公式如下：</para>
        /// <para>℃＝5/9 * (℉－32)</para>
        /// <para>℉＝℃ * 9/5 ＋ 32</para>
        /// </summary>
        [Description("温度")]
        Temperature = 0x3031,

        /// <summary>
        /// 湿度。
        /// <para>湿度传感器检测到的环境湿度。</para>
        /// <para>取值范围：[0,100]。</para>
        /// </summary>
        [Description("湿度")]
        Humidity = 0x3032,

        /// <summary>
        /// 电流。
        /// <para>灯具控制器的输出电流。</para>
        /// <para>单位：A（非GBT35255规范）。</para>
        /// </summary>
        [Description("电流")]
        Current = 0x3033,

        /// <summary>
        /// 电压。
        /// <para>灯具控制器的输出电压。</para>
        /// <para>单位：V（非GBT35255规范）。</para>
        /// </summary>
        [Description("电压")]
        Voltage = 0x3034,

        /// <summary>
        /// 亮度
        /// <para>灯具的亮度，有时也称照度。</para>
        /// <para>取值范围：[0,100]。</para>
        /// </summary>
        [Description("灯具亮度")]
        Luminance = 0x3035,

        /// <summary>
        /// 环境亮度。
        /// <para>亮度传感器检测到的环境亮度。</para>
        /// <para>取值范围：[0,100]。</para>
        /// </summary>
        [Description("环境亮度")]
        Brightness = 0x3036,

        /// <summary>
        /// 是否有人。
        /// <para>红外传感器检测到环境中是否有人或其他生物。</para>
        /// <para>01表示有人，02表示无人（非GBT35255规范）。</para>
        /// </summary>
        [Description("是否有人")]
        是否有人 = 0x3037,
        #endregion

        #region 厂商自定义
        /// <summary>
        /// 灯具好坏。
        /// <para>01表示正常，02表示损坏。</para>
        /// </summary>
        [Description("灯具好坏")]
        灯具好坏 = 0x3130,

        /// <summary>
        /// 功率。
        /// <para>灯具的视在功率。</para>
        /// </summary>
        [Description("功率")]
        Power = 0x3131,

        /// <summary>
        /// 功率因素。
        /// </summary>
        [Description("功率因素")]
        PowerFactor = 0x3132,

        /// <summary>
        /// 电能。
        /// <para>灯具消耗的电量。</para>
        /// </summary>
        [Description("电能")]
        Energy = 0x3133,

        /// <summary>
        /// 是否在线。
        /// <para>灯具可能产生通信故障。</para>
        /// </summary>
        [Description("是否在线")]
        是否在线 = 0x3134,

        /// <summary>
        /// 经度。
        /// </summary>
        [Description("经度")]
        Longitude = 0x3135,

        /// <summary>
        /// 纬度。
        /// </summary>
        [Description("纬度")]
        Latitude = 0x3136,

        /// <summary>
        /// 海拔。
        /// </summary>
        [Description("海拔")]
        Altitude = 0x3137,

        /// <summary>
        /// 移动传感器是否存在。
        /// </summary>
        [Description("移动传感器是否存在")]
        移动传感器是否存在 = 0x3138,

        /// <summary>
        /// 运行时间。
        /// </summary>
        [Description("运行时间")]
        RunTime = 0x3139,

        /// <summary>
        /// 运行模式。
        /// </summary>
        [Description("运行模式")]
        RunModel = 0x3230,

        /// <summary>
        /// 灯具开关调光次数。
        /// </summary>
        [Description("灯具开关调光次数")]
        灯具开关调光次数 = 0x3231,
        #endregion
    }
}