using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Application.Utils
{
    public class UtilsSingleton
    {
        private static UtilsSingleton? instance;
        private static readonly object lockObject = new();

        private UtilsSingleton()
        {
        }

        public static UtilsSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObject)
                    {
                        instance ??= new UtilsSingleton();
                    }
                }

                return instance;
            }
        }

        public string Encrypt(string input)
        {
            string hash = "coding con c";
            byte[] data = UTF8Encoding.UTF8.GetBytes(input);
            TripleDES triple = TripleDES.Create();

            triple.Key = MD5.HashData(UTF8Encoding.UTF8.GetBytes(hash));
            triple.Mode = CipherMode.ECB;

            ICryptoTransform transform = triple.CreateEncryptor();
            byte[] result = transform.TransformFinalBlock(data, 0, data.Length);

            return Convert.ToBase64String(result);
        }

        public string Decrypt(string input)
        {
            byte[] data = Convert.FromBase64String(input);
            TripleDES triple = TripleDES.Create();

            triple.Key = MD5.HashData(UTF8Encoding.UTF8.GetBytes("coding con c"));
            triple.Mode = CipherMode.ECB;

            ICryptoTransform transform = triple.CreateDecryptor();
            byte[] result = transform.TransformFinalBlock(data, 0, data.Length);

            return UTF8Encoding.UTF8.GetString(result);
        }

        public bool ValidateEmail(string email)
        {
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            bool isFormatValid = Regex.IsMatch(email, emailPattern);
            return isFormatValid;
        }
    }
}
