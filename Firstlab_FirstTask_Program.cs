using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lab1
{
    static class myclass
    {
       static Dictionary<char, int> mylist = new Dictionary<char, int>();
        static char Low;
       public static void SymbolsCount(string text)
        {
            mylist.Clear();
            foreach (var ch in text)
            {
                Low = ch;
                if (mylist.ContainsKey(Low))
                    mylist[Low]++;
                else
                    mylist.Add(Low, 1);
            }
        }
        public static Dictionary<char, double> Showprobability(Dictionary<char, double> symbols,int size)
        {
            foreach(var item in mylist)
            {
                symbols.Add(item.Key, ((double)item.Value/size));             
            }
            return symbols;
        }
        public static double Entrophy(Dictionary<char,double> symbols)
        {
            double entrophy=0;
            foreach(var item in symbols)
            {
                entrophy += (item.Value * Math.Log(item.Value, 2));
            }
            return (entrophy * -1);
        }
      
    }
    class Program
    {
        static string Take_file(string path)
        {
            string text;
            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                text = sr.ReadToEnd();
            }
            return text;
        }
        static void Showdictionary(Dictionary<char,double> symbols)
        {
            foreach (var item in symbols)
            {
                Console.WriteLine(item.Key + "\t" + item.Value);
            }
        }
        static double Amountofinfo(int amount,double entro)
        {
            return amount * entro;
        }
        static void AmountOut(double amount, string path)
        {
            FileInfo file = new FileInfo(path);
            double size = file.Length;
            Console.WriteLine("Кількість інформації в тексті - {0} біт\nЗагальний розмір файлу - {1} біт\nРозмір файлу в - {2} рази більший за кількість інформації", amount, size * 8, size * 8 / amount);
        }
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.GetEncoding(1251);
            Dictionary<char, double> first = new Dictionary<char, double>();
            Dictionary<char, double> second = new Dictionary<char, double>();
            Dictionary<char, double> third = new Dictionary<char, double>();
            string first_path = @"C:\Users\maksi\Desktop\КС\FirstLab\first_base64.txt";
            string second_path = @"C:\Users\maksi\Desktop\КС\FirstLab\second_base64.txt";
            string third_path = @"C:\Users\maksi\Desktop\КС\FirstLab\third_base64.txt";
            myclass.SymbolsCount(Take_file(first_path));
            myclass.Showprobability(first, Take_file(first_path).Length);
            myclass.SymbolsCount(Take_file(second_path));
            myclass.Showprobability(second, Take_file(second_path).Length);
            myclass.SymbolsCount(Take_file(third_path));
            myclass.Showprobability(third, Take_file(third_path).Length);
            Showdictionary(first);
            Console.WriteLine($"Entrophy {myclass.Entrophy(first)}");
            AmountOut(Amountofinfo(Take_file(first_path).Length, myclass.Entrophy(first)), first_path);
            Showdictionary(second);
            Console.WriteLine($"Entrophy {myclass.Entrophy(second)}");
            AmountOut(Amountofinfo(Take_file(second_path).Length, myclass.Entrophy(second)), second_path);
            Showdictionary(third);
            Console.WriteLine($"Entrophy {myclass.Entrophy(third)}");
            AmountOut(Amountofinfo(Take_file(third_path).Length, myclass.Entrophy(third)), third_path);
        }
    }
}
