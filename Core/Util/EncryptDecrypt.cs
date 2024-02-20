using System;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;

namespace Core.Util
{
    /// <summary>
    /// The encrypt decrypt.
    /// </summary>
    public static class EncryptDecrypt
    {
        private static readonly byte[] _key = { 111, 222, 33, 11, 24, 26, 85, 45, 114, 184, 27, 162, 37, 112, 222, 209, 241, 24, 175, 144, 173, 53, 196, 29, 24, 26, 17, 218, 131, 236, 53, 209 };
        private static readonly byte[] _iv = { 155, 68, 222, 111, 23, 3, 113, 119, 231, 121, 221, 112, 79, 32, 114, 156 };

        /// <summary>
        /// Encrypts the.
        /// </summary>
        /// <param name="plainText">The plain text.</param>
        /// <returns>A string.</returns>
        public static string Encrypt(this string plainText)
        {
            byte[] ciphertext = EncryptInner(plainText, _key, _iv);
            return Convert.ToBase64String(ciphertext);
        }

        /// <summary>
        /// Encrypts the.
        /// </summary>
        /// <param name="encryptedText">The encrypted text.</param>
        /// <returns>A string.</returns>
        public static string Decrypt(this string encryptedText)
        {
            byte[] bytes = Convert.FromBase64String(encryptedText);
            return DecryptInner(bytes, _key, _iv);
        }

        public static byte[] EncryptInner(string plaintext, byte[] key, byte[] iv)
        {
            using Aes aesAlg = Aes.Create();
            aesAlg.Key = key;
            aesAlg.IV = iv;
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            byte[] encryptedBytes;
            using (var msEncrypt = new MemoryStream())
            {
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    byte[] plainBytes = Encoding.UTF8.GetBytes(plaintext);
                    csEncrypt.Write(plainBytes, 0, plainBytes.Length);
                }
                encryptedBytes = msEncrypt.ToArray();
            }
            return encryptedBytes;
        }

        public static string DecryptInner(byte[] ciphertext, byte[] key, byte[] iv)
        {
            using Aes aesAlg = Aes.Create();
            aesAlg.Key = key;
            aesAlg.IV = iv;
            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
            byte[] decryptedBytes;
            using (var msDecrypt = new MemoryStream(ciphertext))
            {
                using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
                using var msPlain = new MemoryStream();
                csDecrypt.CopyTo(msPlain);
                decryptedBytes = msPlain.ToArray();
            }
            return Encoding.UTF8.GetString(decryptedBytes);
        }

        public static string Compress(string plaintext)
        {
            string strResult = "";
            using (MemoryStream output = new())
            {
                using (DeflateStream gzip = new(output, CompressionMode.Compress))
                {
                    using StreamWriter writer = new(gzip, Encoding.UTF8);
                    writer.Write(plaintext);
                }
                byte[] obyte = output.ToArray();
                strResult = Convert.ToBase64String(obyte);
            }
            return strResult;
        }

        public static string Decompress(string compressedstring)
        {
            string strResult = "";
            byte[] input = Convert.FromBase64String(compressedstring);
            using (MemoryStream inputStream = new(input))
            {
                using DeflateStream gzip = new(inputStream, CompressionMode.Decompress);
                using StreamReader reader = new(gzip, Encoding.UTF8);
                strResult = reader.ReadToEnd();
            }
            return strResult;
        }
    }
}