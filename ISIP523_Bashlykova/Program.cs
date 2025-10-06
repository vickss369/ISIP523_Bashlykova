using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP523_Bashlykova
{
    internal class Program
    {
        ///Создайте консольное приложение C# для учёта книг в библиотеке.
        ///У книги должны быть следующие параметры: 
        /// * Уникальный идентификатор (генерируется автоматически при добавлении). 
        /// * Название. 
        /// * Автор. 
        /// * Жанр (можно выбрать из заданных в коде вариантов, не менее трёх). 
        /// * Год издания. 
        /// * Цена.

        ///Мы можем работать с книгами через команды: 
        /// * Добавить книгу (запросить все параметры у пользователя, идентификатор назначается автоматически). 
        /// * Удалить книгу по идентификатору. 
        /// * Найти книги (по названию, автору, жанру, должны быть все варианты поиска книги) и выводить полную информацию. 
        /// * Отсортировать книги по названию или году (должны быть обе команды).
        /// * Вывести самую дорогую и самую дешёвую книгу. 
        /// * Сгруппировать книги по авторам и вывести количество книг каждого автора.


        enum JanrKnigi
        {
            Фэнтези = 1,
            Детектив,
            Роман,
            НаучнаяФантастика,
            Приключения
        }

        class Kniga
        {
            private static int nextId = 1;
            private int id;
            private string nazv;
            private string avtor;
            private JanrKnigi janr;
            private int godizd;
            private double cena;

            public string Name() { return nazv; }
            public string Autor() { return avtor; }
            public JanrKnigi Janr() { return janr; }
            public int Year() { return godizd; }
            public double Cena() { return cena; }
            public int Id() { return id; }

            public void VvodInfo()
            {
                Console.Write("Введите название книги: ");
                nazv = Console.ReadLine();

                Console.Write("Введите автора книги: ");
                avtor = Console.ReadLine();

                Console.WriteLine("Выберите жанр (введите номер): ");
                foreach (var j in Enum.GetValues(typeof(JanrKnigi)))
                {
                    Console.WriteLine($"{(int)j}. {j}");
                }

                int n;
                while (!int.TryParse(Console.ReadLine(), out n) || !Enum.IsDefined(typeof(JanrKnigi), n))
                {
                    Console.Write("Ошибка! Введите корректный номер жанра: ");
                }
                janr = (JanrKnigi)n;

                Console.Write("Введите год издания книги: ");
                while (!int.TryParse(Console.ReadLine(), out godizd))
                {
                    Console.Write("Ошибка! Введите корректный год: ");
                }

                Console.Write("Введите цену книги: ");
                while (!double.TryParse(Console.ReadLine(), out cena) || cena < 0)
                {
                    Console.Write("Ошибка! Введите корректную цену: ");
                }

                id = nextId++;
            }

            public void VyvodInfo()
            {
                Console.WriteLine($"ID: {id}\nНазвание: '{nazv}'\nАвтор: {avtor}\nЖанр: {janr}\nГод: {godizd}\nЦена: {cena} руб.\n");
            }
        }

        static void Main(string[] args)
        {
            bool outt = true;
            while (outt)
            {
                Console.WriteLine("\nМЕНЮ:");
                Console.WriteLine("1. Добавить книгу");
                Console.WriteLine("2. Удалить книгу по ID");
                Console.WriteLine("3. Вывести все книги");
                Console.WriteLine("4. Поиск книг");
                Console.WriteLine("5. Сортировка книг");
                Console.WriteLine("6. Самая дорогая и самая дешёвая книга");
                Console.WriteLine("7. Группировка книг по авторам");
                Console.WriteLine("0. Выход");

                Console.Write("\nВыберите действие: ");
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.WriteLine();
                        //DobavitKnigu();
                        break;

                    case 2:
                        Console.WriteLine();
                        //UdalitKnigu();
                        break;

                    case 3:
                        Console.WriteLine();
                        //VyvestiBiblioteku();
                        break;

                    case 4:
                        Console.WriteLine("\nВЫБОР ПОИСКА:");
                        Console.WriteLine("1. Поиск книги по автору");
                        Console.WriteLine("2. Поиск книги по названию");
                        Console.WriteLine("3. Поиск книги по жанру");

                        Console.Write("\nВыберите действие: ");
                        int choicepoisk = Convert.ToInt32(Console.ReadLine());
                        switch (choicepoisk)
                        {
                            case 1:
                                //PoiskAvtor();
                                break;

                            case 2:
                                //PoiskNazvanie();
                                break;

                            case 3:
                                //PoiskJanr();
                                break;

                            default: break;
                        }
                        break;

                    case 5:
                        Console.WriteLine("\nВЫБОР СОРТИРОВКИ:");
                        Console.WriteLine("1. Сортирвка книг по названию");
                        Console.WriteLine("2. Сортировка книг по году издания");

                        Console.Write("\nВыберите действие: ");
                        int choicesort = Convert.ToInt32(Console.ReadLine());
                        switch (choicesort)
                        {
                            case 1:
                                //SortNazvanie();
                                break;

                            case 2:
                                //SortGod();
                                break;

                            default: break;
                        }
                        break;

                    case 6:
                        Console.WriteLine();
                        //CenaMaxMin();
                        break;

                    case 7:
                        Console.WriteLine();
                        //GruppirovkaAvtor();
                        break;

                    case 0: outt = false; break;
                    default: Console.WriteLine("Неправильный пункт меню."); break;
                }
            }
        }
    }
}
