using System;
using System.Linq;
using System.Collections.Generic;

namespace HillCipher
{
    public class Keys
    {
        public int[,] ara = new int[2, 2];              
    }

    class Program
    {
        static string TextConveter(string str, Keys var)
        {
            string CipherText = null;

            if (Convert.ToBoolean(str.Length % 2))
            {
                str += 'Z';
            }

            List<int> value = new List<int>();

            for (int i = 0; i < str.Length; i++)
            {
                int val = (int)str[i] - 65;
                value.Add(val);
            }

            int[] textValue = new int[value.Count()];

            for (int i = 0; i < value.Count(); i += 2)
            {
                int a = var.ara[0, 0] * value[i];
                int b = var.ara[0, 1] * value[i + 1];
                int c = var.ara[1, 0] * value[i];
                int d = var.ara[1, 1] * value[i + 1];
                int val1 = (a + b) % 26;
                int val2 = (c + d) % 26;
                char c1 = (char)(65 + val1);
                char c2 = (char)(65 + val2);
                CipherText += c1;
                CipherText += c2;
            }

            return CipherText;
        }

        static Keys DecryptKey(Keys keys)
        {
            int d = (keys.ara[0, 0] * keys.ara[1, 1]) - (keys.ara[0, 1] * keys.ara[1, 0]);

            if (d < 0) d += 26;

            Keys decrypt = new Keys();

            decrypt = keys;

            Swap(ref decrypt.ara[0, 0], ref decrypt.ara[1, 1]);
            
            decrypt.ara[0, 1] = (decrypt.ara[0, 1] * -1) % 26;
            decrypt.ara[1, 0] = (decrypt.ara[1, 0] * -1) % 26;

            if (decrypt.ara[0, 1] < 0) decrypt.ara[0, 1] += 26;
            if (decrypt.ara[1, 0] < 0) decrypt.ara[1, 0] += 26;

            int x = 0;

            for (int i = 1; i < 26; i++)
            {
                if (Convert.ToBoolean((d * i) % 26 == 1))
                {
                    x = i;
                    break;
                }
            }

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    decrypt.ara[i,j] *= x;
                    decrypt.ara[i,j] %= 26;

                    if (decrypt.ara[i,j] < 0)
                    {
                        decrypt.ara[i, j] += 26;
                    } 
                }
            }

            return decrypt;
        }

        static void Swap(ref int num1, ref int num2)
        {
            int newnum;
            newnum = num1;
            num1 = num2;
            num2 = newnum;
        }

        static void Main(string[] args)
        {
            Console.Write("Enter Your text : ");
            var plainText = Console.ReadLine().ToUpper();
            Keys key = new Keys();

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    key.ara[i, j] = int.Parse(Console.ReadLine());
                }
            }

            string cipherText = TextConveter(plainText, key);

            Console.WriteLine($"Cipher Text : {cipherText}");

            Keys decryptKey = new Keys();

            decryptKey = DecryptKey(key);

            string decryptText = TextConveter(cipherText, decryptKey);
            Console.WriteLine($"Decrypt Text : {decryptText}");
        }
    }
}