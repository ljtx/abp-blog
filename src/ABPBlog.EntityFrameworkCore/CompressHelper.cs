using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;

namespace ABPBlog
{
    public class CompressHelper
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="encriyptString">要被加密的字符串</param>
        /// <param name="key">秘钥（44个字符）</param>
        /// <param name="ivString">向量长度（16个字符）</param>
        /// <returns></returns>
        public static string AES_Encrypt(string encriyptString, string key, string ivString)
        {
            key = key.PadRight(32, ' ');
            SymmetricAlgorithm aes = new RijndaelManaged();

            byte[] iv = Encoding.UTF8.GetBytes(ivString.Substring(0, 16));


            aes.Key = Encoding.UTF8.GetBytes(key.Substring(0, 32));
            aes.Mode = CipherMode.ECB;
            aes.IV = iv;
            aes.Padding = PaddingMode.PKCS7; //


            ICryptoTransform rijndaelEncrypt = aes.CreateEncryptor();
            byte[] inputData = Encoding.UTF8.GetBytes(encriyptString);
            byte[] encryptedData = rijndaelEncrypt.TransformFinalBlock(inputData, 0, inputData.Length);

            return Convert.ToBase64String(encryptedData);
        }
        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="decryptString">AES密文</param>
        /// <param name="key">秘钥（44个字符）</param>
        /// <param name="ivString">向量（16个字符）</param>
        /// <returns></returns>
        public static string AES_Decrypt(string decryptString, string key, string ivString)
        {
            try
            {

                key = key.PadRight(32, ' ');
                RijndaelManaged aes = new RijndaelManaged();

                byte[] iv = Encoding.UTF8.GetBytes(ivString.Substring(0, 16));
                aes.Key = Encoding.UTF8.GetBytes(key.Substring(0, 32));
                aes.Mode = CipherMode.ECB;
                aes.IV = iv;
                aes.Padding = PaddingMode.PKCS7;  //


                ICryptoTransform rijndaelDecrypt = aes.CreateDecryptor();
                byte[] inputData = Convert.FromBase64String(decryptString);
                byte[] xBuff = rijndaelDecrypt.TransformFinalBlock(inputData, 0, inputData.Length);

                return Encoding.UTF8.GetString(xBuff);
            }
            catch (Exception ex)
            {
                throw;

            }
        }

        /// <summary>
        /// dec加密
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] buffer)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes("ljtx");
                byte[] rgbIV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                using (MemoryStream mStream = new MemoryStream())
                {
                    CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                    cStream.Write(buffer, 0, buffer.Length);
                    cStream.FlushFinalBlock();
                    return mStream.ToArray();
                }
            }
            catch
            {
                return buffer;
            }
        }
        public static byte[] UnEncrypt(byte[] buffer)
        {
            byte[] rgbKey = Encoding.UTF8.GetBytes("ljtx");
            byte[] rgbIV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
            using (MemoryStream reader = new MemoryStream())
            {
                using (MemoryStream mStream = new MemoryStream(buffer))
                {
                    CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Read);
                    while (cStream.CanRead)
                    {
                        byte[] cache = new byte[1024];
                        int count = cStream.Read(cache, 0, cache.Length);
                        if (count < 1)
                            break;
                        reader.Write(cache, 0, count);
                    }
                }
                return reader.ToArray();
            }
        }
        public static string GZipCompress(byte[] buffer)
        {
            try
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    using (GZipStream gZipStream = new GZipStream(stream, CompressionMode.Compress))
                    {
                        gZipStream.Write(buffer, 0, buffer.Length);
                        gZipStream.Close();
                    }
                    return System.Convert.ToBase64String(stream.ToArray());
                }
            }
            catch
            {

                return System.Convert.ToBase64String(buffer);
            }
        }
    }
}
