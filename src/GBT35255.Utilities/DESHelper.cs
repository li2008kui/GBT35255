using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace GBT35255.Utilities
{
    /// <summary>
    /// DES 助手类。
    /// <para>运算模式为 ECB 模式。</para>
    /// </summary>
    public class DESHelper
    {
        /// <summary>
        /// 获取 DES 密钥。
        /// </summary>
        /// <returns>DES 密钥。</returns>
        public byte[] GetSecretKey()
        {
            byte[] secretKey = new byte[] { };

            try
            {
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    des.Mode = CipherMode.ECB;
                    secretKey = des.Key;
                }
            }
            catch { }

            return secretKey;
        }

        /// <summary>
        /// 使用密钥加密明文字节数组。
        /// </summary>
        /// <param name="keyBytes">DES 密钥字节数组。</param>
        /// <param name="plainBytes">需要加密的字节数组。</param>
        /// <returns>密文字节数组。</returns>
        public byte[] Encrypt(byte[] keyBytes, byte[] plainBytes)
        {
            byte[] cipherBytes = new byte[] { };

            try
            {
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    des.Mode = CipherMode.ECB;
                    des.Key = keyBytes;

                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            using (StreamWriter sw = new StreamWriter(cs))
                            {
                                sw.WriteLine(Encoding.ASCII.GetChars(plainBytes));
                            }
                        }

                        cipherBytes = ms.ToArray();
                    }
                }
            }
            catch { }

            return cipherBytes;
        }

        /// <summary>
        /// 使用密钥加密明文字符串。
        /// </summary>
        /// <param name="keyBytes">DES 密钥字节数组。</param>
        /// <param name="plainText">需要加密的字符串。</param>
        /// <returns></returns>
        public byte[] Encrypt(byte[] keyBytes, string plainText)
        {
            return Encrypt(keyBytes, Encoding.UTF8.GetBytes(plainText));
        }

        /// <summary>
        /// 使用密钥解密密文字节数组。
        /// </summary>
        /// <param name="keyBytes">DES 密钥字节数组。</param>
        /// <param name="cipherBytes">需要解密的字节数组。</param>
        /// <returns>明文字节数组。</returns>
        public byte[] Decrypt(byte[] keyBytes, byte[] cipherBytes)
        {
            byte[] plainBytes = new byte[] { };

            try
            {
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    des.Mode = CipherMode.ECB;
                    des.Key = keyBytes;

                    using (MemoryStream ms = new MemoryStream(cipherBytes))
                    {
                        using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Read))
                        {
                            using (StreamReader sr = new StreamReader(cs))
                            {
                                plainBytes = Encoding.UTF8.GetBytes(sr.ReadLine());
                            }
                        }
                    }
                }
            }
            catch { }

            return plainBytes;
        }

        /// <summary>
        /// 使用密钥解密密文字符串。
        /// </summary>
        /// <param name="keyBytes">DES 密钥字节数组。</param>
        /// <param name="cipherText">需要解密的字符串。</param>
        /// <returns>明文字节数组。</returns>
        public byte[] Decrypt(byte[] keyBytes, string cipherText)
        {
            return Decrypt(keyBytes, Encoding.UTF8.GetBytes(cipherText));
        }
    }
}