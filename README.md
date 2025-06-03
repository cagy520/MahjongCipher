# MahjongCipher
麻将哈希加密算法
using System.Security.Cryptography;
using System.Text;

namespace EncryptTest
{
    internal class Program
    {
        static void Main(string[] args)
        {

            MahjongCipher mc = new MahjongCipher();
            string mi=mc.Encrypt("JKHJHDHSD54545424");
            Console.WriteLine(mi);//简单加密的结果
            string old=mc.Decrypt(mi);
            Console.WriteLine(old);//原文

            //复杂加密
            string fmi=mc.EncryptAESBASE64("JKHJHDHSD54545424", "123456789");
            fmi = mc.Encrypt(fmi);
            Console.WriteLine(fmi);
            //复杂解密
            string fold = mc.Decrypt(fmi);
            fold = mc.DecryptAESBASE64(fold, "123456789");
            Console.WriteLine(fold);//原文

        }
    }
}





