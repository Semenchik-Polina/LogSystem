using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace LogSystem.Common.Helpers
{
    // based on sha1
    public class HashHelper
    {
        // less then 20 chars (size of sha1 hash), can include non-printable characters 
        private const byte MaxPasswordLengthConst = 17;
        private const byte MinPasswordLengthConst = 7;

        // finds non-printable and whitespace characters
        private const string OutOfPasswordPattern = "[^#-~]+";

        // min password length for 
        private byte minPasswordLength = 10;

        public string StaticSalt { get; set; }

        public HashHelper(string staticSalt)
        {
            StaticSalt = staticSalt;
        }

        public byte MinPasswordLength
        {
            get { return minPasswordLength; }
            set
            {
                if (value < MinPasswordLengthConst)
                    minPasswordLength = MinPasswordLengthConst;
                else if (value > MaxPasswordLengthConst)
                    minPasswordLength = MaxPasswordLengthConst;
                else
                    minPasswordLength = value;
            }
        }

        // get hash of user's password
        public string GetPasswordHash(string password, string dynamicSalt)
        {
            string hash = "";

            if (password.Length < 1)
            {
                throw new NullReferenceException("Empty string as a paramater of GetPasswordHash function");
            }
            byte[] dynamicSaltInBytes = Encoding.UTF8.GetBytes(dynamicSalt);
            byte[] data = Encoding.UTF8.GetBytes(password);

            byte[] hashByteArr = GetHashByteArr(data);
            hashByteArr = GetHashByteArrWithSalt(hashByteArr, dynamicSaltInBytes);
            hashByteArr = GetHashByteArrWithSalt(hashByteArr, Encoding.UTF8.GetBytes(StaticSalt));

            hash = BitConverter.ToString(hashByteArr);

            return hash;
        }


        // generate password for user's profile using hashing it's content
        public string GeneratePassword(string email)
        {
            string password = "";

            if (email.Length < 1)
            {
                throw new NullReferenceException("Null as a paramater of GeneratePassword function");
            }
            using (SHA1 sha = new SHA1CryptoServiceProvider())
            {
                Random random = new Random();
                while (password.Length < MinPasswordLength)
                {
                    string data = $"{email}.{random.Next()}";
                    byte[] hashedData = sha.ComputeHash(Encoding.UTF8.GetBytes(data));
                    string hashedDataStr = Encoding.UTF8.GetString(hashedData);
                    password = TrimNonPrintableChars(hashedDataStr);
                }
            }

            return password;
        }

        public string GetDynamicSalt(string hashParameter)
        {
            string salt = "";
            if (hashParameter.Length < 1)
            {
                throw new NullReferenceException("Empty string as a paramater of GetHash function");
            }
            salt = BitConverter.ToString(GetDynamicSaltByteArr(hashParameter));

            return salt;
        }

        // generate dynamic salt for hashing user's data
        private byte[] GetDynamicSaltByteArr(string hashParameter)
        {
            byte[] hashedSalt;
            Random random = new Random();
            string salt = $"{hashParameter}.{random.Next()}";

            using (SHA1 sha = new SHA1CryptoServiceProvider())
            {
                hashedSalt = sha.ComputeHash(Encoding.UTF8.GetBytes(salt));
            }

            return hashedSalt;
        }

        // delete all non-printable and whitespace characters
        private string TrimNonPrintableChars(string str)
        {
            Regex reg_exp = new Regex(OutOfPasswordPattern);
            return reg_exp.Replace(str, "");
        }

        // get hash of any object in bytes
        private byte[] GetHashByteArr(Object obj)
        {
            byte[] hashByteArr;

            using (SHA1 sha = new SHA1CryptoServiceProvider())
            {
                hashByteArr = sha.ComputeHash(ByteArrayHelper.ObjectToByte(obj));
            }
            return hashByteArr;
        }


        // get hash with salt
        private byte[] GetHashByteArrWithSalt(byte[] data, byte[] salt)
        {
            byte[] hash;
            byte[] rawData = ByteArrayHelper.ConcatByteArrays(data, salt);

            using (SHA1 sha = new SHA1CryptoServiceProvider())
            {
                hash = sha.ComputeHash(rawData);
            }

            return hash;
        }
    }
}
