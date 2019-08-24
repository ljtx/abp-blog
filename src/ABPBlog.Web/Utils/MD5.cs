using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABPBlog.Web.Utils
{
    public static class MD5
    {
        public static string ToMD5String(this string str, string salt)
        {
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bytResult = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(string.Concat(str, salt)));
            //转换成字符串，32位 
            string strResult = BitConverter.ToString(bytResult);
            //BitConverter转换出来的字符串会在每个字符中间产生一个分隔符，需要去除掉 
            strResult = strResult.Replace("-", "");
            return strResult.ToLower();
        }
    }
}
