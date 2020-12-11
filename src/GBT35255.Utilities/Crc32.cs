namespace GBT35255.Utilities
{
    /// <summary>
    /// CRC32校验类。
    /// </summary>
    public class Crc32
    {
        /// <summary>
        /// CRC32表。
        /// </summary>
        private static uint[] Crc32Table
        {
            get
            {
                uint item;
                uint[] crc32Table = new uint[256];

                for (int i = 0; i < 256; i++)
                {
                    item = (uint)i;

                    for (int j = 8; j > 0; j--)
                    {
                        if ((item & 1) == 1)
                        {
                            item = (item >> 1) ^ 0xEDB88320;
                        }
                        else
                        {
                            item >>= 1;
                        }
                    }

                    crc32Table[i] = item;
                }

                return crc32Table;
            }
            set { }
        }

        /// <summary>
        /// 获取字节数组的CRC32校验值。
        /// </summary>
        /// <param name="data">需要校验的字节数组。</param>
        /// <returns></returns>
        public static uint GetCrc32(byte[] data)
        {
            uint crc32 = 0xFFFFFFFF;

            foreach (var item in data)
            {
                crc32 = (crc32 >> 8) ^ Crc32Table[(crc32 & 0xFF) ^ item];
            }

            return crc32 ^ 0xFFFFFFFF;
        }
    }
}