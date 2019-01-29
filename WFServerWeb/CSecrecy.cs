using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security.Cryptography;
using System.IO;
class CSecrecy
{
    public static string MD5Encrypt(string InitString)
    {
        MD5 md5 = new MD5CryptoServiceProvider();
        byte[] palindata = Encoding.Default.GetBytes(InitString);//将要加密的字符串转换为字节数组
        byte[] encryptdata=md5.ComputeHash(palindata);//将字符串加密后也转换为字符数组
        return Convert.ToBase64String(encryptdata);//将加密后的字节数组转换为加密字符串
    }

    public static string RSAEncrypt(string InitString)
    {
        CspParameters param = new CspParameters();
        param.KeyContainerName = "CommonContain";//密匙容器的名称，保持加密解密一致才能解密成功
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(param))
        {
            byte[] plaindata = Encoding.Default.GetBytes(InitString);//将要加密的字符串转换为字节数组
            byte[] encryptdata = rsa.Encrypt(plaindata, false);//将加密后的字节数据转换为新的加密字节数组
            return Convert.ToBase64String(encryptdata);//将加密后的字节数组转换为字符串
        }
    }

    //解密
    public static string RSADecrypt(string CipherString)
    {
        CspParameters param = new CspParameters();
        param.KeyContainerName = "CommonContain";
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(param))
        {
            byte[] encryptdata = Convert.FromBase64String(CipherString);
            byte[] decryptdata = rsa.Decrypt(encryptdata, false);
            return Encoding.Default.GetString(decryptdata);
        }
    }

    /// <summary>
    /// C# DES加密方法
    /// </summary>
    /// <param name="encryptedValue">要加密的字符串</param>
    /// <param name="key">密钥</param>
    /// <param name="iv">向量</param>
    /// <returns>加密后的字符串</returns>
    public static string DESEncrypt(string originalValue, string key, string iv)
    {
        using (DESCryptoServiceProvider sa
            = new DESCryptoServiceProvider { Key = Encoding.UTF8.GetBytes(key), IV = Encoding.UTF8.GetBytes(iv) })
        {
            using (ICryptoTransform ct = sa.CreateEncryptor())
            {
                byte[] by = Encoding.UTF8.GetBytes(originalValue);
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, ct,CryptoStreamMode.Write))
                    {
                        cs.Write(by, 0, by.Length);
                        cs.FlushFinalBlock();
                    }
                    return Convert.ToBase64String(ms.ToArray());
                    //return System.Text.Encoding.Default.GetString(ms.ToArray());
                }
            }
        }
    }

    /// <summary>
    /// C# DES解密方法
    /// </summary>
    /// <param name="encryptedValue">待解密的字符串</param>
    /// <param name="key">密钥</param>
    /// <param name="iv">向量</param>
    /// <returns>解密后的字符串</returns>
    public static string DESDecrypt(string encryptedValue, string key, string iv)
    {
        using (DESCryptoServiceProvider sa =
            new DESCryptoServiceProvider { Key = Encoding.UTF8.GetBytes(key), IV = Encoding.UTF8.GetBytes(iv) })
        {
            using (ICryptoTransform ct = sa.CreateDecryptor())
            {
                byte[] byt = Convert.FromBase64String(encryptedValue);
                //byte[] byt = System.Text.Encoding.Default.GetBytes(encryptedValue);

                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, ct, CryptoStreamMode.Write))
                    {
                        cs.Write(byt, 0, byt.Length);
                        cs.FlushFinalBlock();
                    }
                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
        }
    }

}