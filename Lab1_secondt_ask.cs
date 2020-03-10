using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lab1_part2
{
    class Program
    {
        static readonly char[] base64Table = {'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O',
                                                       'P','Q','R','S','T','U','V','W','X','Y','Z','a','b','c','d',
                                                       'e','f','g','h','i','j','k','l','m','n','o','p','q','r','s',
                                                       't','u','v','w','x','y','z','0','1','2','3','4','5','6','7',
                                                       '8','9','+','/','=' };
        static byte[] Take_file(string path)
        {
            string text;
            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                text = sr.ReadToEnd();
            }
            byte[] mass = Encoding.UTF8.GetBytes(text);
            return mass;
        }

        public static void ConvertToBase64(byte[] mass)
        {
            string binaryfortext = string.Join("", mass.Select(x => Convert.ToString(x, 2).PadLeft(8, '0')));
            int countsbinary = binaryfortext.Count();
            string concate = countsbinary % 3 == 1 ? "=" : countsbinary % 3 == 2 ? "==" : "";
            int removedivision = countsbinary % 6;
            if(removedivision != 0)
                for(int i = 0; i < 6 - removedivision; i++) 
                {
                    binaryfortext = binaryfortext.Insert(countsbinary, "0");
                    countsbinary++;
                }
            List<string> newList = Enumerable.Range(0, countsbinary / 6).Select(x => binaryfortext.Substring(x * 6, 6)).ToList();
            string finaltext = string.Join("", newList.Select(x => base64Table[Convert.ToByte(x,2)]))+concate;
            Console.WriteLine($"\n\n\n{finaltext}");
        }
        static void Converttobase64(string path)
        {
            string text = Convert.ToBase64String(Take_file(path));
            Console.WriteLine($"Compare with default method \n\n\n{text}");
            using (StreamWriter sw = new StreamWriter(path.Replace(".rar", "_base64.txt"), false))
            {
                sw.WriteLine(text);
            }
            
        }
        static void Main(string[] args)
        {
            string first_path = @"C:\Users\maksi\Desktop\КС\FirstLab\first.rar";
            string second_path = @"C:\Users\maksi\Desktop\КС\FirstLab\second.rar";
            string third_path = @"C:\Users\maksi\Desktop\КС\FirstLab\third.rar";
            ConvertToBase64(Take_file(first_path));
            Converttobase64(first_path);
            ConvertToBase64(Take_file(second_path));
            Converttobase64(second_path);
            ConvertToBase64(Take_file(third_path));
            Converttobase64(third_path);
        }
    }
}
