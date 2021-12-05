using System;
using System.IO.Compression;
using System.Text;

namespace lab1
{
    internal class Program
    {
        private static readonly string Text = "dobriy vechir";
        private const string Key = "AABB09182736CCDD";

        private static void Main(string[] args)
        {
            var des = new DES();
            Console.WriteLine("Encryption:\n");
            var encryptedText = des.Encrypt(Text, Key);
            Console.WriteLine($"\nEncrypted Text: {encryptedText.ToUpper()}\n");
            Console.WriteLine("Decryption\n");
            var decryptedText = des.Decrypt(encryptedText, Key);
            Console.WriteLine($"\nHex Text: {decryptedText.ToUpper()}");
        }
    }
}