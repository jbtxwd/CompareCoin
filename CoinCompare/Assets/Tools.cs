using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using System.Security;
using System.Text;
public class Tools 
{
    public static string GetSHA256HashFromString(string strData)
    {
        byte[] tmpByte;
                 SHA256 sha256 = new SHA256Managed();
        
         tmpByte = sha256.ComputeHash(GetKeyByteArray(strData));
                 sha256.Clear();
        
         return GetStringValue(tmpByte);
    }

    private static string GetStringValue(byte[] Byte)
     {
         string tmpString = "";
         ASCIIEncoding Asc = new ASCIIEncoding();
         tmpString = Asc.GetString(Byte);

         return tmpString;
     }

    private static byte[] GetKeyByteArray(string strKey)
     {
         ASCIIEncoding Asc = new ASCIIEncoding();
 
         int tmpStrLen = strKey.Length;
         byte[] tmpByte = new byte[tmpStrLen - 1];
 
         tmpByte = Asc.GetBytes(strKey);
 
         return tmpByte;
 
     }
}
