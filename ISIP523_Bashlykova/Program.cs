using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP523_Bashlykova
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            bool outt = true;
            while (outt)
            {
                Console.WriteLine("\n📋 МЕНЮ:");
                Console.WriteLine("1. Регистрация");
                Console.WriteLine("2. Вход");
                Console.WriteLine("3. Просмотр товаров");
                Console.WriteLine("0. Выход из программы(в целом всё)");

                Console.Write("Введите выбор: ");
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("~~~ Регистрация ~~~");
                        Console.Write("Введите имя пользователя: ");
                        Console.Write("Введите пароль: ");
                        Console.Write("Повторите пароль: ");

                        Console.WriteLine("\n✅ Вы успешно зарегистрировались!");
                        bool regoutt = true;
                        while (regoutt)
                        {
                            Console.WriteLine("\n📋 МЕНЮ:");
                            Console.WriteLine("1. Просмотр товаров");
                            Console.WriteLine("2. Оформить заказ");
                            Console.WriteLine("3. История заказов");
                            Console.WriteLine("0. Выход из программы(в целом всё)");

                            Console.Write("Введите выбор: ");
                            int regchoice = Convert.ToInt32(Console.ReadLine());

                            switch (regchoice)
                            {
                                case 1:
                                    break;

                                case 2:
                                    break;

                                case 3:
                                    break;

                                case 0: regoutt = false; outt = false; break;

                                default: Console.WriteLine("Неправильный пункт меню."); break;
                            }
                        }

                        break;

                    case 2:
                        Console.WriteLine("~~~ Войдите в аккаунт ~~~");
                        Console.Write("Введите имя пользователя: ");
                        Console.Write("Введите пароль: ");

                        Console.WriteLine("\n✅ Вы успешно вошли в аккаунт!");
                        bool logoutt = true;
                        while (logoutt)
                        {
                            Console.WriteLine("\n📋 МЕНЮ:");
                            Console.WriteLine("1. Просмотр товаров");
                            Console.WriteLine("2. Оформить заказ");
                            Console.WriteLine("3. История заказов");
                            Console.WriteLine("0. Выход из программы(в целом всё)");

                            Console.Write("Введите выбор: ");
                            int logchoice = Convert.ToInt32(Console.ReadLine());

                            switch (logchoice)
                            {
                                case 1:
                                    break;

                                case 2:
                                    break;

                                case 3:
                                    break;

                                case 0: logoutt = false; outt = false; break;

                                default: Console.WriteLine("Неправильный пункт меню."); break;
                            }
                        }
                        break;

                    case 3:
                        break;

                    case 0: outt = false; break;

                    default: Console.WriteLine("Неправильный пункт меню."); break;
                }
            }
        }
    }
}
