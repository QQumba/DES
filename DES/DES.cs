using System;
using System.Linq;
using System.Text;

namespace lab1
{
    public class DES
    {
        private string HexToBin(string input)
        {
            return string.Join(string.Empty,
                input.Select(
                    c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')
                )
            );
        }

        // binary to hexadecimal conversion
        private string binToHex(string input)
        {
            var n = input.Length / 4;
            input = Convert.ToInt64(input, 2).ToString("X");

            while (input.Length < n)
            {
                input = "0" + input;
            }

            return input;
        }

        // per-mutate input hexadecimal
        // according to specified sequence
        private string Permutation(int[] sequence, string input)
        {
            var output = "";
            input = HexToBin(input);
            foreach (var t in sequence)
            {
                output += input[t - 1];
            }

            output = binToHex(output);
            return output;
        }

        // xor 2 hexadecimal strings
        private static string Xor(string a, string b)
        {
            // hexadecimal to decimal(base 10)
            var aLong = Convert.ToInt64(a, 16);
            // hexadecimal to decimal(base 10)
            var bLong = Convert.ToInt64(b, 16);
            // xor
            aLong ^= bLong;
            // decimal to hexadecimal
            a = aLong.ToString("X");
            // prepend 0's to maintain length
            while (a.Length < b.Length)
                a = "0" + a;
            return a;
        }

        // left Circular Shifting bits
        private string LeftCircularShift(string input, int numBits)
        {
            var n = input.Length * 4;
            var perm = new int[n];
            for (var i = 0; i < n - 1; i++)
            {
                perm[i] = i + 2;
            }

            perm[n - 1] = 1;
            while (numBits-- > 0)
            {
                input = Permutation(perm, input);
            }

            return input;
        }

        // preparing 16 keys for 16 rounds
        private string[] GetKeys(string key)
        {
            var keys = new string[16];
            // first key permutation
            key = Permutation(DESValues.PC1, key);
            for (var i = 0; i < 16; i++)
            {
                key = LeftCircularShift(
                          key.Substring(0, 8), DESValues.ShiftBits[i])
                      + LeftCircularShift(key.Substring(7, 7),
                          DESValues.ShiftBits[i]);
                // second key permutation
                keys[i] = Permutation(DESValues.PC2, key);
            }

            return keys;
        }

        // s-box lookup
        private string SBox(string input)
        {
            var output = "";
            input = HexToBin(input);
            for (var i = 0; i < 48; i += 6)
            {
                var temp = input.Substring(i, 6);
                var num = i / 6;
                var row = Convert.ToInt32(
                    temp[0] + "" + temp[5], 2);
                var col = Convert.ToInt32(
                    temp.Substring(1, 4), 2);
                output += DESValues.SBox[num, row, col].ToString("X");
            }

            return output;
        }

        private string Round(string input, string key, int num)
        {
            // fk
            var left = input.Substring(0, 8);
            var temp = input.Substring(8, 8);
            var right = temp;
            // Expansion permutation
            temp = Permutation(DESValues.EP, temp);
            // xor temp and round key
            temp = Xor(temp, key);
            // lookup in s-box table
            temp = SBox(temp);
            // Straight D-box
            temp = Permutation(DESValues.P, temp);
            // xor
            left = Xor(left, temp);
            Console.WriteLine("Round "
                              + (num + 1) + " "
                              + right.ToUpper()
                              + " " + left.ToUpper() + " "
                              + key.ToUpper());

            // swapper
            return right + left;
        }

        private string EncryptBlock(string plainText, string key)
        {
            // get round keys
            var keys = GetKeys(key);

            // initial permutation
            plainText = Permutation(DESValues.IP, plainText);
            Console.WriteLine(
                "After initial permutation: "
                + plainText.ToUpper());
            Console.WriteLine(
                "After splitting: L0="
                + plainText.Substring(0, 8).ToUpper()
                + " R0="
                + plainText.Substring(8, 8).ToUpper() + "\n");

            // 16 rounds
            for (var i = 0; i < 16; i++)
            {
                plainText = Round(plainText, keys[i], i);
            }

            // 32-bit swap
            plainText = plainText.Substring(8, 8)
                        + plainText.Substring(0, 8);

            // final permutation
            plainText = Permutation(DESValues.IP1, plainText);
            return plainText;
        }

        private string DecryptBlock(string plainText, string key)
        {
            int i;
            // get round keys
            var keys = GetKeys(key);

            // initial permutation
            plainText = Permutation(DESValues.IP, plainText);
            Console.WriteLine(
                "After initial permutation: "
                + plainText.ToUpper());
            Console.WriteLine(
                "After splitting: L0="
                + plainText.Substring(0, 8).ToUpper()
                + " R0=" + plainText.Substring(8, 8).ToUpper()
                + "\n");

            // 16-rounds
            for (i = 15; i > -1; i--)
            {
                plainText = Round(plainText, keys[i], 15 - i);
            }

            // 32-bit swap
            plainText = plainText.Substring(8, 8)
                        + plainText.Substring(0, 8);
            plainText = Permutation(DESValues.IP1, plainText);
            return plainText;
        }

        public string Encrypt(string text, string key)
        {
            var hexText = PlainTextToHex(text);
            var remainingBytes = 16 - hexText.Length % 16;
            for (var i = 0; i < remainingBytes; i+=2)
            {
                hexText += "20";
            }

            var encryptedText = "";
            var blockCount = hexText.Length / 16;
            for (var i = 0; i < blockCount * 16; i += 16)
            {
                encryptedText += EncryptBlock(hexText.Substring(i, 16), key);
            }

            return encryptedText;
        }

        public string Decrypt(string text, string key)
        {
            var blockCount = text.Length / 16;
            var decryptedText = "";
            for (var i = 0; i < blockCount * 16; i += 16)
            {
                decryptedText += DecryptBlock(text.Substring(i, 16), key);
            }

            return HexToPlainText(decryptedText);
        }

        private static string PlainTextToHex(string text)
        {
            var bytes = Encoding.ASCII.GetBytes(text);
            var hexText = "";
            foreach (var b in bytes)
            {
                hexText += b.ToString("X");
            }

            return hexText;
        }

        private static string HexToPlainText(string hexText)
        {
            var sourceText = "";
            for (int i = 0; i < hexText.Length; i += 2)
            {
                var hexChar = hexText.Substring(i, 2);
                sourceText += (char) Convert.ToInt64(hexChar, 16);
            }

            return sourceText;
        }
    }
}