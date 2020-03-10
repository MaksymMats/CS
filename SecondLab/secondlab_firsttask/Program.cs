using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static string format(String str, int desiredLength)
        {
            StringBuilder builder = new StringBuilder(str);

            if (str.Length < desiredLength)
            {
                int padding = desiredLength - str.Length;
                for (int i = 0; i < padding; i++)
                {
                    builder.Insert(0, "0");
                }
                return builder.ToString();
            }
            else
            {
                return str.Substring(str.Length - desiredLength);
            }
        }
        static string Shift_Right(string k,int shift)
        {
            StringBuilder str = new StringBuilder(k.Substring(0,k.Length-shift));
            for (int i = 0; i < shift; i++)
            {
                if (k.Substring(0, 1).Equals("0"))
                {
                    str.Insert(0, "0");
                }
                else
                {
                    str.Insert(0, "1");
                }
            }
            return str.ToString();
        }
        static string generate_product(int a)
        {
            string binarytext1 = Convert.ToString(a,2);
            binarytext1 = binarytext1.PadLeft(16, '0');
            binarytext1 += "0";
            return binarytext1;
        }
        static string result(int a, string multiplicand)
        {
            string stroka = generate_product(a);
            string left = string.Empty;
            for (int i = 1; i < 9; i++)
            {
                int addingpart = Convert.ToInt32(stroka.Substring(0, 8), 2);
                if (stroka.Substring(stroka.Length - 2, 2) == "01")
                {
                    addingpart += Convert.ToInt32(multiplicand,2);
                    stroka = format(Convert.ToString(addingpart, 2), 8) + stroka.Substring(stroka.Length - 9);
                }
               else  if (stroka.Substring(stroka.Length - 2, 2) == "10")
                {
                    addingpart -= Convert.ToInt32(multiplicand, 2);
                   stroka = format(Convert.ToString(addingpart, 2), 8) + stroka.Substring(stroka.Length - 9);
                }
               stroka = Shift_Right(stroka, 1);
                Console.WriteLine($"Iteration \n {i}           temporary result  {stroka}   ");
            }
            if (stroka.Substring(0, 1) == "0")
            {
                return stroka.Remove(stroka.Length - 1);
            }
            else
            {
                string positivePortion = stroka.Substring(stroka.IndexOf("0"), 16 - stroka.IndexOf("0"));
                return positivePortion+"+";
            }
        }
        static string generate_number(int a)
        {
            string binaryfortext1 = Convert.ToString(a, 2);
            if (binaryfortext1.Length > 8)
            {
                binaryfortext1 = binaryfortext1.Substring(binaryfortext1.Length - 8);
            }
            else
            {
                binaryfortext1 = binaryfortext1.PadLeft(8, '0');
            }
            return binaryfortext1;
        }
        static void Main(string[] args)
        {
            Console.WriteLine( "Enter a first number" );
            int m = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter a second number");
            int n = Convert.ToInt32(Console.ReadLine());
            string A = string.Empty;
            string S = string.Empty;   
            A=generate_number(m);
            S = generate_number(n); 
            Console.WriteLine($"multiplicand={A}\nmultiplication={S}");
            string f=result(n,A);
            if (f.Substring(f.Length-1) == "+")
            {
               string g= f.Remove(f.Length-1);
                int value = Convert.ToInt32(g, 2) - (int)Math.Pow(2,g.Length);
                Console.WriteLine(value);
               
            }
            else
            {
                Console.WriteLine(Convert.ToInt32(f, 2));
            }
        }
    }
}
 