using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.IO;

public class SignCHBTC
{
    private static String encodingCharset = "UTF-8";
    private static string accesskey = "10823b7c-bf93-4dd0-b5af-83dc487a9f7e";
    private static string secretkey = "2ffdfa1e-ba2c-42c5-98fc-61679b8c30d4";
    private static string baseURL = "https://trade.chbtc.com/api/";
    /**
     *
     * @param aValue  要加密的文字
     * @param aKey  密钥
     * @return
     */
    public static String hmacSign(String aValue, String aKey) {
        byte[] k_ipad = new byte[64];
        byte[] k_opad = new byte[64];
        byte[] keyb;
        byte[] value;
        Encoding coding = Encoding.GetEncoding(encodingCharset);
        try {
            keyb = coding.GetBytes(aKey);// aKey.getBytes(encodingCharset);
            value = coding.GetBytes(aValue);// aValue.getBytes(encodingCharset);
        } catch (Exception e) {
            keyb = null;
            value = null;
            //throw;
        }

        for (int i = keyb.Length; i < 64; i++)
        {
            k_ipad[i] = (byte)54;
            k_opad[i] = (byte)92;
        }


        for (int i = 0; i < keyb.Length; i++) {
            k_ipad[i] = (byte)(keyb[i] ^ 0x36);
            k_opad[i] = (byte)(keyb[i] ^ 0x5c);
        }

        byte[] sMd5_1 = MakeMD5(k_ipad.Concat(value).ToArray());
        byte[] dg = MakeMD5(k_opad.Concat(sMd5_1).ToArray());

        return toHex(dg);
    }

    public static String toHex(byte[] input) {
        if (input == null)
            return null;
        StringBuilder output = new StringBuilder(input.Length * 2);
        for (int i = 0; i < input.Length; i++) {
            int current = input[i] & 0xff;
            if (current < 16)
                output.Append('0');
            output.Append(current.ToString("x"));
        }

        return output.ToString();
    }

    /**
     * 
     * @param args
     * @param key
     * @return
     */
    public static String getHmac(String[] args, String key) {
        if (args == null || args.Length == 0) {
            return (null);
        }
        StringBuilder str = new StringBuilder();
        for (int i = 0; i < args.Length; i++) {
            str.Append(args[i]);
        }
        return (hmacSign(str.ToString(), key));
    }


    /// <summary>
    /// 生成MD5摘要
    /// </summary>
    /// <param name="original">数据源</param>
    /// <returns>摘要</returns>
    public static byte[] MakeMD5(byte[] original)
    {
        MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
        byte[] keyhash = hashmd5.ComputeHash(original);
        hashmd5 = null;
        return keyhash;
    }

    /**
     * SHA加密
     * @param aValue
     * @return
     */

    public static String digest(String aValue) {
        aValue = aValue.Trim();
        byte[] value;
        SHA1 sha = null;
        Encoding coding = Encoding.GetEncoding(encodingCharset);
        try {
            value = coding.GetBytes(aValue);// aValue.getBytes(encodingCharset);
            HashAlgorithm ha = (HashAlgorithm)CryptoConfig.CreateFromName("SHA");
            value = ha.ComputeHash(value);
        } catch (Exception e) {
            //value = coding.GetBytes(aValue);
            throw;
        }

        return toHex(value);
    }


    /**
     * API调用示例
     * 其他方法调用及返回说明请查看API交易文档
     */
    public static string testGetAccountInfo() {

        string param = "method=getAccountInfo&accesskey=" + accesskey;

        secretkey = digest(secretkey);
        string sign = hmacSign(param, secretkey);

        DateTime timeStamp = new DateTime(1970, 1, 1); //得到1970年的时间戳
        long stamp = (DateTime.UtcNow.Ticks - timeStamp.Ticks) / 10000; //注意这里有时区问题，用now就要减掉8个小时

        baseURL += "getAccountInfo?" + param + "&sign=" + sign + "&reqTime=" + stamp;
        return baseURL;
    }
}