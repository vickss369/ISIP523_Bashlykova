using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP523_Bashlykova
{
    internal class Program
    {
        /// Необходимо написать программу, которая будет принимать текст от пользователя и делать над ним определённые действия.
        /// Функциональные требования:
        /// * Программа принимает от пользователя минимум 100 символов
        /// * Подсчёт количества слов в тексте
        /// * Поиск самого короткого слова
        /// * Подсчёт количества предложений
        /// * Подсчёт количества гласных и согласных букв
        /// * Поиск самого длинного слова
        /// * Создание статистики по частоте встречаемости каждой буквы
        /// * Возможность продолжить работу с новым текстом
        /// * Сохранение всей статистики в список
        /// * Возможность вывести статистику по прошлым текстам


        class TextStatistics
        {
            public string Text;
            public int WordCount;
            public string ShortestWord;
            public string LongestWord;
            public int SoglCount;
            public int GlasCount;
            public int SentencesCount;
            public Dictionary<char, int> LetterChastota;
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
