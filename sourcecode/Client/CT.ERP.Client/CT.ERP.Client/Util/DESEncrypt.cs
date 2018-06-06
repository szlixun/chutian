using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace CT.ERP.Client.Util
{
    public class DESEncrypt
    {
        /// <summary>
        /// DES加密算法必须使用Base64的Byte对象
        /// </summary>
        /// <param name="data">待加密的字符数据</param>
        /// <param name="key">密匙，长度必须为64位（byte[8]）)</param>
        /// <param name="iv">iv向量，长度必须为64位（byte[8]）</param>
        /// <returns>加密后的字符</returns>
        public static string EnDES(string data, byte[] key, byte[] iv)
        {
            DES des = DES.Create();
            //这行代码很重要,需要根据不同的字符串选择不同的转换格式
            byte[] tmp = Encoding.Unicode.GetBytes(data);
            Byte[] encryptoData;

            ICryptoTransform encryptor = des.CreateEncryptor(key, iv);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (var cs = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter writer = new StreamWriter(cs))
                    {
                        writer.Write(data);
                        writer.Flush();
                    }
                }
                encryptoData = memoryStream.ToArray();
            }
            des.Clear();

            return Convert.ToBase64String(encryptoData);

        }

        /// <summary>
        /// DES解密算法
        /// </summary>
        /// <param name="data">待加密的字符数据</param>
        /// <param name="key">密匙，长度必须为64位（byte[8]）)</param>
        /// <param name="iv">iv向量，长度必须为64位（byte[8]）</param>
        /// <returns>加密后的字符</returns>
        public static string DeDes(string data, Byte[] key, Byte[] iv)
        {
            string resultData = string.Empty;
            //这行代码很重要
            Byte[] tmpData = Convert.FromBase64String(data);//转换的格式挺重要
            DES des = DES.Create();

            ICryptoTransform decryptor = des.CreateDecryptor(key, iv);
            using (var memoryStream = new MemoryStream(tmpData))
            {
                using (var cs = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                {

                    StreamReader reader = new StreamReader(cs);
                    resultData = reader.ReadLine();
                }

            }

            return resultData;


        }
    }
}
