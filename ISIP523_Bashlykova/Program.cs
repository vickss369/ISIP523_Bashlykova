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
        private static List<TextStatistics> statisticsHistory = new List<TextStatistics>();
        private static TextStatistics currentStats = null;

        static void vvodText()
        {
            Console.WriteLine("\nВведите текст (минимум 100 символов):");
            Console.WriteLine("(Для завершения ввода введите пустую строку - нажмите Enter дважды)\n");

            List<string> lines = new List<string>();
            string line;
            int totalLength = 0;

            while (true)
            {
                line = Console.ReadLine();
                if (string.IsNullOrEmpty(line) && totalLength >= 100) break;
                if (!string.IsNullOrEmpty(line))
                {
                    lines.Add(line);
                    totalLength += line.Length;
                }
            }

            string fullText = string.Join("\n", lines);

            if (fullText.Length < 100)
            {
                Console.WriteLine("Ошибка: текст слишком короткий!");
                return;
            }

            var stats = AnalyzeText(fullText);
            currentStats = stats;
            statisticsHistory.Add(stats);

            Console.WriteLine($"\nТекст успешно сохранен! Общая длина: {fullText.Length} символов");
        }

        static TextStatistics AnalyzeText(string text)
        {
            string[] words = text.Split(new char[] { ' ', ',', '.', '!', '?', ';', ':', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            int wordCount = words.Length;
            string shortest = "";
            string longest = "";

            if (wordCount > 0)
            {
                shortest = words[0];
                longest = words[0];

                foreach (string word in words)
                {
                    if (word.Length < shortest.Length) shortest = word;
                    if (word.Length > longest.Length) longest = word;
                }
            }

            int sentenceCount = 0;
            foreach (char c in text)
            {
                if (c == '.' || c == '!' || c == '?') sentenceCount++;
            }

            string glas = "аеёиоуыэюяaeiou";
            string sogl = "бвгджзйклмнпрстфхцчшщbcdfghjklmnpqrstvwxyz";

            int glasCount = 0;
            int soglCount = 0;
            Dictionary<char, int> chastota = new Dictionary<char, int>();

            foreach (char c in text.ToLower())
            {
                if (char.IsLetter(c))
                {
                    if (chastota.ContainsKey(c))
                        chastota[c]++;
                    else
                        chastota[c] = 1;

                    if (glas.Contains(c))
                        glasCount++;
                    else if (sogl.Contains(c))
                        soglCount++;
                }
            }

            return new TextStatistics
            {
                Text = text,
                WordCount = wordCount,
                ShortestWord = shortest,
                LongestWord = longest,
                SentencesCount = sentenceCount,
                GlasCount = glasCount,
                SoglCount = soglCount,
                LetterChastota = chastota
            };
        }

        static void Main(string[] args)
        {
            bool outt = true;
            while (outt)
            {
                Console.WriteLine("\nМЕНЮ");
                Console.WriteLine("1. Ввести текст");
                Console.WriteLine("2. Статистика по тексту");
                Console.WriteLine("3. Статистика по всем текстам");
                Console.WriteLine("0. Выход");

                Console.Write("Введите выбор: ");
                int choise = Convert.ToInt32(Console.ReadLine());
                switch (choise)
                {
                    case 1:
                        vvodText();
                        break;

                    case 2:
                        //showNowStat();
                        break;

                    case 3:
                        //showHistory();
                        break;

                    case 0: outt = false; break;

                    default: outt = false; break;
                }
            }
        }
    }
}
