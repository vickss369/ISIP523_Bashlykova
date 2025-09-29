using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP523_Bashlykova
{
    internal class Program
    {
        class TextStatistics
        {
            public string Text;
            public int WordCount;
            public string ShortestWord;
            public string LongestWord;
            public int SoglCount;
            public int GlasCount;
            public int SentencesCount;
            public Dictionary<char, int> LetterChastot;
        }

        static void Main(string[] args)
        {
            bool outt = true;
            while (outt)
            {
                Console.WriteLine("\n=== МЕНЮ ===");
                Console.WriteLine("1. Ввести текст");
                Console.WriteLine("2. Статистика по тексту");
                Console.WriteLine("3. Статистика по прошлому тексту");
                Console.WriteLine("0. Выход");

                Console.Write("Введите выбор: ");
                int choise = Convert.ToInt32(Console.ReadLine());
                switch (choise)
                {
                    case 1:
                        break;

                    case 2:
                        break;

                    case 3:
                        break;

                    case 0: outt = false; break;

                    default: outt = false; break;
                }
            }
        }
    }
}
