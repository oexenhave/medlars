namespace Medlars.Core
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    public class Encryption
    {
        #region Simple Encryption
        private static readonly byte[] Tempkeysimple = { 11, 65, 23, 140, 178 };
        private static readonly byte[] Tempivsimple = { 150, 120, 85, 35, 121, 100, 25, 140 };

        public static string EncryptSimple(string originalstring)
        {
            if (!string.IsNullOrEmpty(originalstring))
            {
                originalstring = originalstring.Replace("å", ":aa:");
                originalstring = originalstring.Replace("ø", ":oe:");
                originalstring = originalstring.Replace("æ", ":ae:");

                var textEncoder = new ASCIIEncoding();
                var crypto = new RC2CryptoServiceProvider { Key = Tempkeysimple, IV = Tempivsimple };

                ICryptoTransform encrypto = crypto.CreateEncryptor(Tempkeysimple, Tempivsimple);

                var memory = new MemoryStream();
                var cryptstream = new CryptoStream(memory, encrypto, CryptoStreamMode.Write);

                byte[] toEncrypt = textEncoder.GetBytes(originalstring);

                cryptstream.Write(toEncrypt, 0, toEncrypt.Length);
                cryptstream.FlushFinalBlock();

                return Convert.ToBase64String(memory.ToArray());
            }

            return string.Empty;
        }

        public static string DecryptSimple(string encryptedstring)
        {
            if (!string.IsNullOrEmpty(encryptedstring))
            {
                var textEncoder = new ASCIIEncoding();
                var crypto = new RC2CryptoServiceProvider { Key = Tempkeysimple, IV = Tempivsimple };

                ICryptoTransform decrypto = crypto.CreateDecryptor(Tempkeysimple, Tempivsimple);

                var memory = new MemoryStream(Convert.FromBase64String(encryptedstring));
                var cryptstream = new CryptoStream(memory, decrypto, CryptoStreamMode.Read);

                var fromEncrypt = new byte[encryptedstring.Length];
                cryptstream.Read(fromEncrypt, 0, fromEncrypt.Length);

                string ret = textEncoder.GetString(fromEncrypt).Replace("\0", string.Empty);

                ret = ret.Replace(":aa:", "å");
                ret = ret.Replace(":oe:", "ø");
                ret = ret.Replace(":ae:", "æ");

                return ret;
            }

            return string.Empty;
        }

        #endregion

        /// <summary>
        /// Generate a MD5 hash from the string
        /// </summary>
        /// <param name="str">String to generate MD5 over</param>
        /// <returns>A MD5 hash</returns>
        public static string GenerateMd5Hash(string str)
        {
            string hash = string.Empty;
            MD5 md5Hasher = MD5.Create();

            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(str));
            foreach (byte t in data)
            {
                hash += t.ToString("x2"); // Loop through each byte of the hashed data and format each one as a hexadecimal string.
            }

            return hash;
        }

        /// <summary>
        /// Generates a new unique password salt
        /// </summary>
        /// <returns>A string value</returns>
        public static string GeneratePasswordSalt()
        {
            return Guid.NewGuid().ToString().Replace("-", string.Empty);
        }

        public static string GenerateRandomPassword()
        {
            var tempPassword = GeneratePasswordSalt();
            return tempPassword.Substring(0, Math.Min(12, tempPassword.Length));
        }

        /// <summary>
        /// Generates a hash over the salt and the clear text password
        /// </summary>
        /// <param name="salt">Password salt value</param>
        /// <param name="password">Clear text password</param>
        /// <returns>A string value</returns>
        public static string GeneratePasswordHash(string salt, string password)
        {
            return GenerateMd5Hash(string.Format("{0}Medl4rs{1}", salt, password));
        }
    }
}