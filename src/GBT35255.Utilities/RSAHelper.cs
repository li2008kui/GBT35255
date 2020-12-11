using System;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace GBT35255.Utilities
{
    /// <summary>
    /// RSA 助手类。
    /// <para>采用 PKCS#1 填充方式进行填充。</para>
    /// <para>能够加密的明文长度（字节）= keySize / 8 - 11。</para>
    /// <para>该版本不支持分组加密。</para>
    /// </summary>
    public class RSAHelper
    {
        private int keySize;
        /// <summary>
        /// 获取或设置当前密钥的大小。
        /// </summary>
        /// <value>
        /// 密钥的大小（以位为单位）。
        /// <para>最小取值为384位，最大取值为16384位，以8的倍数递增。</para>
        /// </value>
        public int KeySize
        {
            get { return keySize; }
            set
            {
                if (value >= 384 && value <= 16384 && value % 8 == 0)
                {
                    keySize = value;
                }
                else
                {
                    keySize = 512;
                }
            }
        }

        /// <summary>
        /// 使用密钥大小初始化 RSAHelper 类的新实例。
        /// </summary>
        /// <param name="keySize">
        /// 密钥的大小（以位为单位）。
        /// <para>最小取值为384位，最大取值为16384位，以8的倍数递增。</para>
        /// </param>
        public RSAHelper(int keySize = 512)
        {
            if (keySize >= 384 && keySize <= 16384 && keySize % 8 == 0)
            {
                KeySize = keySize;
            }
            else
            {
                KeySize = 512;
            }
        }

        /// <summary>
        /// 通过 XML 字符串形式的密钥获取 RSA 私钥。
        /// </summary>
        /// <param name="xmlKeys">XML 字符串形式的密钥（包含公钥和私钥）。</param>
        /// <returns>RSA 私钥。</returns>
        public byte[] GetPrivateKey(string xmlKeys)
        {
            byte[] privateKey = new byte[] { };

            try
            {
                using RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(keySize);
                rsa.FromXmlString(xmlKeys);
                RSAParameters pmt = rsa.ExportParameters(true);

                // 私钥表示为 {d, n}。
                privateKey = pmt.D.Concat(pmt.Modulus).ToArray();
            }
            catch { }

            return privateKey;
        }

        /// <summary>
        /// 获取包含 RSA 公钥和私钥的 XML 形式的密钥。
        /// </summary>
        /// <returns>XML 字符串形式的密钥。</returns>
        public string GetXmlKeys()
        {
            string xmlKeys = string.Empty;

            try
            {
                using RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(keySize);
                xmlKeys = rsa.ToXmlString(true);
            }
            catch { }

            return xmlKeys;
        }

        /// <summary>
        /// 使用公钥加密明文字节数组。
        /// <para>采用PKCS#1填充方式进行填充，能够加密的明文长度（字节）= keySize / 8 - 11。</para>
        /// </summary>
        /// <param name="publicXmlKey">XML 字符串形式的公钥。</param>
        /// <param name="plainBytes">需要加密的字节数组。</param>
        /// <returns>密文字节数组。</returns>
        public byte[] Encrypt(string publicXmlKey, byte[] plainBytes)
        {
            byte[] cipherBytes = new byte[] { };

            try
            {
                using RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(keySize);
                rsa.FromXmlString(publicXmlKey);
                cipherBytes = rsa.Encrypt(plainBytes, false);
            }
            catch { }

            return cipherBytes;
        }

        /// <summary>
        /// 使用公钥加密明文字符串。
        /// <para>采用PKCS#1填充方式进行填充，能够加密的明文长度（字节）= keySize / 8 - 11。</para>
        /// </summary>
        /// <param name="publicXmlKey">XML 字符串形式的公钥。</param>
        /// <param name="plainText">需要加密的字符串。</param>
        /// <returns>密文字节数组。</returns>
        public byte[] Encrypt(string publicXmlKey, string plainText)
        {
            return Encrypt(publicXmlKey, Encoding.UTF8.GetBytes(plainText));
        }

        /// <summary>
        /// 使用私钥解密密文字节数组。
        /// </summary>
        /// <param name="privateKey">包含 （D 私钥指数） 和 （Modulus） 的私钥。</param>
        /// <param name="cipherBytes">需要解密的字节数组。</param>
        /// <returns></returns>
        public byte[] Decrypt(byte[] privateKey, byte[] cipherBytes)
        {
            if (privateKey.Length != 128)
            {
                return new byte[] { };
            }

            return Decrypt(cipherBytes,
                privateKey.Where((b, i) => i < 64).ToArray(),
                privateKey.Where((b, j) => j >= 64).ToArray(),
                false);
        }

        /// <summary>
        /// 使用私钥解密密文字符串。
        /// </summary>
        /// <param name="privateKey">包含 （D 私钥指数） 和 （Modulus） 的私钥。</param>
        /// <param name="cipherText">需要解密的字符串。</param>
        /// <returns></returns>
        public byte[] Decrypt(byte[] privateKey, string cipherText)
        {
            return Decrypt(privateKey, Encoding.UTF8.GetBytes(cipherText));
        }

        /// <summary>
        /// 使用密钥（包含公钥和私钥）解密密文字节数组。
        /// </summary>
        /// <param name="xmlKeys">XML 字符串形式的密钥（包含公钥和私钥）。</param>
        /// <param name="cipherBytes">需要解密的字节数组。</param>
        /// <returns>明文字节数组。</returns>
        public byte[] Decrypt(string xmlKeys, byte[] cipherBytes)
        {
            byte[] plainBytes = new byte[] { };

            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(keySize))
            {
                try
                {
                    rsa.FromXmlString(xmlKeys);
                    plainBytes = rsa.Decrypt(cipherBytes, false);
                }
                catch { }
            }

            return plainBytes;
        }

        /// <summary>
        /// 使用密钥（包含公钥和私钥）解密密文字符串。
        /// </summary>
        /// <param name="xmlKeys">XML 字符串形式的密钥（包含公钥和私钥）。</param>
        /// <param name="cipherText">需要解密的字符串。</param>
        /// <returns>明文字节数组。</returns>
        public byte[] Decrypt(string xmlKeys, string cipherText)
        {
            return Decrypt(xmlKeys, Encoding.UTF8.GetBytes(cipherText));
        }

        /// <summary>
        /// 通过字节数组初始化 <see cref="BigInteger"/> 结构的新实例。
        /// </summary>
        /// <param name="bytes">要使用的字节数组。</param>
        /// <returns></returns>
        private BigInteger FromBytes(byte[] bytes)
        {
            // 1、BigInteger的构造函数接受byte[]的格式是“低位在前（Litter Endian）”。所以以下两行是等价的：
            //    new BigInteger(new byte[]{1, 2, 3, 4})
            //    new BitInteger(new byte[]{1, 2, 3, 4, 0, 0, 0})
            // 2、BigInteger支持负数，如果byte[]的最高二进制位非零，则表示为负数，比如new byte[]{1,2,3, 0x80}就是负数。
            //    而RSA中的参数都是正整数，因此，Concat(0)用来保证正整数。
            // 3、如果输入的byte[]的格式是“高位在前(Big Endian)”，那么要先用Reverse翻转一次。
            return new BigInteger(bytes.Reverse().Concat(new byte[] { 0 }).ToArray());
        }

        /// <summary>
        /// 只用 d（<see cref="RSAParameters.D"/> 私钥指数） 和 n（<see cref="RSAParameters.Modulus"/>） 来进行RSA解密。
        /// </summary>
        /// <param name="cipherBytes">需要解密的字节数组。</param>
        /// <param name="D">d， <see cref="RSAParameters.D"/> 私钥指数。</param>
        /// <param name="Modulus">n， <see cref="RSAParameters.Modulus"/> 参数。</param>
        /// <param name="fOAEP">
        /// 如果为 true，则使用 OAEP 填充（仅在运行 Microsoft Windows XP 或更高版本的计算机上可用）执行直接的 <see cref="RSA"/> 解密；
        /// 否则，如果为 false，则使用 PKCS#1 1.5 版填充。
        /// </param>
        /// <returns></returns>
        private byte[] Decrypt(byte[] cipherBytes, byte[] D, byte[] Modulus, bool fOAEP = false)
        {
            BigInteger value = FromBytes(cipherBytes);
            BigInteger exponent = FromBytes(D);
            BigInteger modulus = FromBytes(Modulus);
            BigInteger b1 = BigInteger.ModPow(value, exponent, modulus);// 解密则定义为 m = (c ^ d) mod n。

            var b2 = b1.ToByteArray().Reverse();
            if (fOAEP) throw new NotImplementedException($"未实现 {nameof(fOAEP)} 为 True 时，使用 OAEP 填充（仅在运行 Microsoft Windows XP 或更高版本的计算机上可用）执行直接的 {typeof(RSA)} 解密。");
            else b2 = b2.SkipWhile(b => b != 0).Skip(1);// 去掉PKCS#1 V1.5铺垫，省略检验。

            return b2.ToArray();
        }
    }
}