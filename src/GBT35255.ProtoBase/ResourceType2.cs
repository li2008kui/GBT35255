using System.ComponentModel;
using System.Text;

namespace GBT35255.ProtoBase
{
    /// <summary>
    /// 资源类型2枚举。
    /// <para>使用时需要将枚举值转换成字符串形式。</para>
    /// <para>转换方式如下：</para>
    /// <para><see cref="StringHelper.ToByteArray(string, bool)"/></para>
    /// <para><see cref="Encoding.UTF8"/></para>
    /// </summary>
    [Description("资源类型2")]
    public enum ResourceType2 : ushort
    {
        /// <summary>
        /// 移动传感器。
        /// <para>微波感应。</para>
        /// </summary>
        [Description("移动传感器")]
        移动传感器 = 0x3031,

        /// <summary>
        /// 亮度传感器。
        /// <para>光控感应。</para>
        /// </summary>
        [Description("亮度传感器")]
        亮度传感器 = 0x3032,

        /// <summary>
        /// 经纬度。
        /// </summary>
        [Description("经纬度")]
        经纬度 = 0x3033,

        /// <summary>
        /// 电参数。
        /// </summary>
        [Description("电参数")]
        电参数 = 0x3034,

        /// <summary>
        /// 电参数阈值。
        /// </summary>
        [Description("电参数阈值")]
        电参数阈值 = 0x3035,
    }
}