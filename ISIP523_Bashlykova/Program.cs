using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP523_Bashlykova
{
    internal class Program
    {
        static Users currentUser = null;

        static void UserMenu()
        {
            bool useroutt = true;
            while (useroutt)
            {
                Console.WriteLine("\n📋 МЕНЮ:");
                Console.WriteLine("1. Просмотр товаров");
                Console.WriteLine("2. Оформить заказ");
                Console.WriteLine("3. История заказов");
                Console.WriteLine("0. Выход из программы(в целом всё)");

                Console.Write("Введите выбор: ");
                int userchoice = Convert.ToInt32(Console.ReadLine());

                switch (userchoice)
                {
                    case 1:
                        break;

                    case 2:
                        break;

                    case 3:
                        break;

                    case 0: useroutt = false; break;

                    default: Console.WriteLine("Неправильный пункт меню."); break;
                }
            }
        }

        static void Registration()
        {
            Console.WriteLine("\n~~~ Регистрация ~~~");
            Console.Write("Введите имя пользователя: ");
            string username = Console.ReadLine();

            var exist = Core.Context.Users.FirstOrDefault(x => x.Username == username);
            if (exist != null)
            {
                Console.WriteLine("\n❌ Пользователь с таким именем уже существует.\nНеобходимо войти в аккаунт, а не зарегистрироваться.");
            }
            else
            {
                Console.Write("Введите пароль: ");
                string password = Console.ReadLine();
                Console.Write("Повторите пароль: ");
                string passwordRepeat = Console.ReadLine();

                if (password != passwordRepeat)
                {
                    Console.WriteLine("\n❌ Пароли не совпадают!");
                    return;
                }

                Users dbUser = new Users
                {
                    Username = username,
                    Password = password,
                    DateOfRegistration = DateTime.Now,
                };
                Core.Context.Users.Add(dbUser);
                Core.Context.SaveChanges();

                Console.WriteLine("\n✅ Вы успешно зарегистрировались!");
                currentUser = dbUser;
                UserMenu();
            }
        }

        static void Login()
        {
            Console.WriteLine("\n~~~ Войдите в аккаунт ~~~");
            Console.Write("Введите имя пользователя: ");
            string username = Console.ReadLine();
            Console.Write("Введите пароль: ");
            string password = Console.ReadLine();

            var user = Core.Context.Users.FirstOrDefault(x => x.Username == username && x.Password == password);
            if (user == null)
            {
                Console.WriteLine("\n❌ Пользователь с таким именем не найден.\nПроверьте корректность введённых данных или зарегистрируйтесь.");
                return;
            }

            Console.WriteLine($"\n✅ Вы успешно вошли в аккаунт, {user.Username}!");
            UserMenu();
        }

        public static void ClearDatabase()
        {
            Console.WriteLine("⚠️ Очистка базы данных...");

            Core.Context.Users.RemoveRange(Core.Context.Users);
            Core.Context.Products.RemoveRange(Core.Context.Products);
            Core.Context.Baskets.RemoveRange(Core.Context.Baskets);
            Core.Context.Orders.RemoveRange(Core.Context.Orders);
            Core.Context.PVZ.RemoveRange(Core.Context.PVZ);
            Core.Context.OrderItems.RemoveRange(Core.Context.OrderItems);
            Core.Context.BasketProduct.RemoveRange(Core.Context.BasketProduct);

            Core.Context.SaveChanges();

            Console.WriteLine("✅ Все данные из базы успешно удалены!");
        }


        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            //ClearDatabase();

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
                        Registration();
                        break;

                    case 2:
                        Login();
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
