using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Core.Util
{
    /// <summary>
    /// The encrypt decrypt angular.
    /// </summary>
    public static class EncryptDecryptAngular
    {
        public static string passwordkey = "itztek!@#98Books21";

        /// <summary>
        /// Encrypts the string.
        /// </summary>
        /// <param name="plainText">The plain text.</param>
        /// <returns>A string.</returns>
        public static string EncryptString(string plainText)
        {
            return EncryptString(plainText, passwordkey);
        }

        /// <summary>
        /// Encrypts the string.
        /// </summary>
        /// <param name="plainText">The plain text.</param>
        /// <param name="pwd">The pwd.</param>
        /// <returns>A string.</returns>
        public static string EncryptString(string plainText, string pwd)
        {
            try
            {
                var keybytes = Encoding.UTF8.GetBytes(pwd);
                var iv = Encoding.UTF8.GetBytes(passwordkey);

                var encryoFromJavascript = EncryptStringToBytes(plainText, keybytes, iv);
                return Convert.ToBase64String(encryoFromJavascript);
            }
            catch
            {
                return plainText;
            }
        }

        private static byte[] EncryptStringToBytes(string plainText, byte[] key, byte[] iv)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
            {
                throw new ArgumentNullException("plainText");
            }
            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            byte[] encrypted;
            // Create a RijndaelManaged object
            // with the specified key and IV.
            using (var rijAlg = new RijndaelManaged())
            {
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;

                rijAlg.Key = key;
                rijAlg.IV = iv;

                // Create a decrytor to perform the stream transform.
                var encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for encryption.
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }

        /// <summary>
        /// Decrypts the string.
        /// </summary>
        /// <param name="cipherText">The cipher text.</param>
        /// <returns>A string.</returns>
        public static string DecryptString(string cipherText)
        {
            return DecryptString(cipherText, passwordkey);
        }

        /// <summary>
        /// Decrypts the string.
        /// </summary>
        /// <param name="cipherText">The cipher text.</param>
        /// <param name="pwd">The pwd.</param>
        /// <returns>A string.</returns>
        public static string DecryptString(string cipherText, string pwd)
        {
            try
            {
                cipherText = UtilFunction.Base64Decode(cipherText);
                //var keybytes = Encoding.UTF8.GetBytes(pwd);
                //var iv = Encoding.UTF8.GetBytes(passwordkey);

                //var encrypted = Convert.FromBase64String(cipherText);
                //var decriptedFromJavascript = DecryptStringFromBytes(encrypted, keybytes, iv);
                //return decriptedFromJavascript;
                return cipherText;
            }
            catch
            {
                return cipherText;
            }
        }

        private static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
            {
                throw new ArgumentNullException("cipherText");
            }
            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an RijndaelManaged object
            // with the specified key and IV.
            using (var rijAlg = new RijndaelManaged())
            {
                //Settings
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;

                rijAlg.Key = key;
                rijAlg.IV = iv;
                // Create a decrytor to perform the stream transform.
                var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);
                try
                {
                    // Create the streams used for decryption.
                    using (var msDecrypt = new MemoryStream(cipherText))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                // Read the decrypted bytes from the decrypting stream
                                // and place them in a string.
                                plaintext = srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
                catch
                {
                    plaintext = "keyError";
                }
            }
            return plaintext;
        }
    }
}