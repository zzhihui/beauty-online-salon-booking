using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Core.Common.Utility
{
    public interface ITokenEncryptor
    {
        string Decrypt(string data);
        string Encrypt(string data);
    }


    public class TokenEncryptor: ITokenEncryptor
    {
        private const string PrivateKey = "REPLACE_ME";
        private const string PublicKey = "REPLACE_ME";


        public string Decrypt(string data)
        {

            var rsa = new RSACryptoServiceProvider();
            var dataArray = data.Split(new char[] { ',' });
            var dataByte = new byte[dataArray.Length];
            for (var i = 0; i < dataArray.Length; i++)
            {
                dataByte[i] = Convert.ToByte(dataArray[i]);
            }

            rsa.FromXmlString(PrivateKey);
            var decryptedByte = rsa.Decrypt(dataByte, false);
            return new UnicodeEncoding().GetString(decryptedByte);

        }

        public string Encrypt(string data)
        {
            //var rsa = new RSACryptoServiceProvider();
            //rsa.FromXmlString(PublicKey);
            //var dataToEncrypt = new UnicodeEncoding().GetBytes(data);
            //var encryptedByteArray = rsa.Encrypt(dataToEncrypt, false).ToArray();
            //var length = encryptedByteArray.Count();
            //var item = 0;
            //var sb = new StringBuilder();
            //foreach (var x in encryptedByteArray)
            //{
            //    item++;
            //    sb.Append(x);

            //    if (item < length)
            //        sb.Append(",");
            //}

            //return sb.ToString();
            return "ZhihuiTest";
        }
    }
}
