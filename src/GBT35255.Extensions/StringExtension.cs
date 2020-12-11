using System;
using System.Text;
using System.Text.RegularExpressions;

namespace GBT35255.Extensions
{
    /// <summary>
    /// 字符串扩展类。
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// 指示指定的字符串是 null 还是 System.String.Empty 字符串。
        /// </summary>
        /// <param name="value">要测试的字符串。</param>
        /// <returns>如果 value 参数为 null 或空字符串 ("")，则为 true；否则为 false。</returns>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// 指示所指定的正则表达式在指定的输入字符串中是否找到了匹配项。
        /// </summary>
        /// <param name="value">要搜索匹配项的字符串。</param>
        /// <param name="pattern">要匹配的正则表达式模式。</param>
        /// <returns>如果正则表达式找到匹配项，则为 true；否则，为 false。</returns>
        public static bool IsMatch(this string value, string pattern)
        {
            return Regex.IsMatch(value, pattern);
        }

        /// <summary>
        /// 判断所指定的字符串是否是十六进制形式的字符串。
        /// </summary>
        /// <param name="value">要判断的字符串。</param>
        /// <returns></returns>
        public static bool IsHexString(this string value)
        {
            return value.IsMatch("^[0-9A-Fa-f]+$");
        }

        /// <summary>
        /// 将字符串转换为字节数组。
        /// </summary>
        /// <param name="value">要转换的字符串。</param>
        /// <param name="isHex">该字符串是否是十六进制形式,默认为false。</param>
        /// <returns></returns>
        public static byte[] ToByteArray(this string value, bool isHex = false)
        {
            if (!value.IsNullOrEmpty())
            {
                if (isHex)
                {
                    value = value.Replace(" ", "");

                    if (value.StartsWith("0x", true, null))
                    {
                        value = value[2..];
                    }

                    if (value.IsHexString())
                    {
                        if (value.Length % 2 > 0)
                        {
                            value = "0" + value;
                        }

                        byte[] byteArray = new byte[value.Length / 2];

                        for (int i = 0; i < byteArray.Length; i++)
                        {
                            byteArray[i] = Convert.ToByte(value.Substring(i * 2, 2), 16);
                        }

                        return byteArray;
                    }
                    else
                    {
                        return Encoding.UTF8.GetBytes(value);
                    }
                }
                else
                {
                    return Encoding.UTF8.GetBytes(value);
                }
            }
            else
            {
                return new byte[] { };
            }
        }
    }
}