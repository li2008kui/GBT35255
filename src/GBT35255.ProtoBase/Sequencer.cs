using System;

namespace GBT35255.ProtoBase
{
    /// <summary>
    /// 消息序号计数器类。
    /// <para>此类只有一个对象。</para>
    /// </summary>
    public sealed class Sequencer
    {
        /// <summary>
        /// 延迟初始化对象。
        /// </summary>
        private static readonly Lazy<Sequencer> lazy = new Lazy<Sequencer>(() => new Sequencer());

        /// <summary>
        /// 通过静态字段获取消息序号计数器实例。
        /// </summary>
        public static Sequencer Instance { get { return lazy.Value; } }

        /// <summary>
        /// 私有默认构造方法。
        /// </summary>
        private Sequencer() { }

        /// <summary>
        /// 消息序号。
        /// </summary>
        public uint SeqNumber { get; set; }
    }
}