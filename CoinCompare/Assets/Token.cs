using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using System.Text;
using System;
public class Token
{

	public static string GetSignature(string _priteKey,string _msg)
    {
        string md5 = GetMD5(_priteKey);//ok
        string signature = CreateToken(_msg, md5).ToLower();
        return signature;
    }

    public static string GetMD5(string str)
    {
        byte[] textBytes = Encoding.Default.GetBytes(str);
        try
        {
            MD5CryptoServiceProvider cryptHandler;
            cryptHandler = new MD5CryptoServiceProvider();
            byte[] hash = cryptHandler.ComputeHash(textBytes);
            string ret = "";
            foreach (byte a in hash)
            {
                if (a < 16)
                    ret += "0" + a.ToString("x");
                else
                    ret += a.ToString("x");
            }
            return ret;
        }
        catch
        {
            throw;
        }
    }

    public static string CreateToken(string message, string secret)
    {
        string result = "";
        var encoding = new System.Text.ASCIIEncoding();
        byte[] keyByte = encoding.GetBytes(secret);
        byte[] messageBytes = encoding.GetBytes(message);
        using (var hmacsha256 = new HMACSHA256(keyByte))
        {
            byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
            for (int i = 0; i < hashmessage.Length; i++)
            {
                result += hashmessage[i].ToString("X2");
            }
            return result;
        }
    }

    public static string GetNonce()
    {
        //string nonce = DateTime.Now.Ticks.ToString().Substring(0, 13);
        string curUnixTime = (DateTime.Now - new DateTime(1970, 1, 1).ToLocalTime()).TotalSeconds.ToString();
        string nonce = curUnixTime.Replace(".", "");
        while (nonce.Length < 14)
        {
            nonce += "0";
        }
        nonce = nonce.Substring(0, 14);
        return nonce;
    }

}
