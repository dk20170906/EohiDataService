using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EohiDataServer.Util
{
    public class MD5
    {
        public static string Get(string value)
        {
            //获取加密服务
            System.Security.Cryptography.MD5CryptoServiceProvider md5CSP = new System.Security.Cryptography.MD5CryptoServiceProvider();
            //获取要加密的字段，并转化为Byte[]数组
            byte[] testEncrypt = System.Text.Encoding.UTF8.GetBytes(value);
            //byte[] testEncrypt = value.get("ISO-8859-1");
            //加密Byte[]数组
            byte[] resultEncrypt = md5CSP.ComputeHash(testEncrypt);
            //将加密后的数组转化为字段(普通加密)
            string testResult = System.Text.Encoding.UTF8.GetString(resultEncrypt);
            //作为密码方式加密 
            string EncryptValue = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(value, "MD5");
            return EncryptValue;
        }
    }
}