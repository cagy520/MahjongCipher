using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace OMFORM
{
    public class MahjongCipher
    {
        private Dictionary<char, string> encodingMap;
        private Dictionary<string, string> decodingMap;

        public MahjongCipher()
        {


            // 定义映射规则
            encodingMap = new Dictionary<char, string>()
            {
                {'A', "东"},
                {'B', "南"},
                {'C', "西"},
                {'D', "北"},
                {'E', "中"},
                {'F', "发"},
                {'G', "白"},
                {'H', "春"},
                {'I', "夏"},
                {'J', "秋"},
                {'K', "冬"},
                {'L', "梅"},
                {'M', "兰"},
                {'N', "竹"},
                {'O', "菊"},
                {'P', "帅"},
                {'Q', "仕"},
                {'R', "相"},
                {'S', "马"},
                {'T', "车"},
                {'U', "炮"},
                {'V', "兵"},
                {'W', "国王"},
                {'X', "皇后"},
                {'Y', "战车"},
                {'Z', "主教"},
                {'a', "骑士"},
                {'b', "兵卒"},
                {'c', "梅花"},
                {'d', "方块"},
                {'e', "红桃"},
                {'f', "黑桃"},
                {'g', "大王"},
                {'h', "小王"},
                {'i', "一筒"},
                {'j', "二筒"},
                {'k', "三筒"},
                {'l', "四筒"},
                {'m', "五筒"},
                {'n', "六筒"},
                {'o', "七筒"},
                {'p', "八筒"},
                {'q', "九筒"},
                {'r', "一万"},
                {'s', "二万"},
                {'t', "三万"},
                {'u', "四万"},
                {'v', "五万"},
                {'w', "六万"},
                {'x', "七万"},
                {'y', "八万"},
                {'z', "九万"},
                {'0', "一条"},
                {'1', "二条"},
                {'2', "三条"},
                {'3', "四条"},
                {'4', "五条"},
                {'5', "六条"},
                {'6', "七条"},
                {'7', "八条"},
                {'8', "九条"},
                {'9', "碰"},
                {'+', "杠"},
                {'/', "听"},
                {'=', "胡"}
            };

            // 创建解码映射
            decodingMap = new Dictionary<string, string>();
            foreach (var pair in encodingMap)
            {
                decodingMap.Add(pair.Value, pair.Key.ToString());
            }
        }

        // 加密函数
        public string Encrypt(string base64Str)
        {
            char[] chars = base64Str.ToCharArray();
            string mstr = "";
            for (int i = 0; i < chars.Length; i++)
            {
                if (encodingMap.ContainsKey(chars[i]))
                {
                    mstr += encodingMap[chars[i]] + " ";
                }
            }
            return mstr;
        }

        // 解密函数
        public string Decrypt(string encodedStr)
        {
            string lowerEncodedStr = encodedStr.ToLower().Trim();
            string[] chars = lowerEncodedStr.Split(' ');
            string str = "";
            for (int i = 0; i < chars.Length; i++)
            {
                string value = chars[i].ToString();
                if (decodingMap.ContainsKey(value))
                {
                    str += decodingMap[value];
                }
            }
            return str;
        }

        public string EncryptAESBASE64(string clearText, string private_key)
        {
            string EncryptionKey = private_key; // 修改为您的加密密钥
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6E, 0x20, 0x4D, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public string DecryptAESBASE64(string encryptedText, string private_key)
        {
            string EncryptionKey = private_key;
            byte[] cipherBytes = Convert.FromBase64String(encryptedText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6E, 0x20, 0x4D, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    encryptedText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return encryptedText;
        }
    }
}
