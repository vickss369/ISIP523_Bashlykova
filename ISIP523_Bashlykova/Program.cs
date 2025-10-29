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
                Console.WriteLine("2. Просмотр корзины");
                Console.WriteLine("3. Оформить заказ");
                Console.WriteLine("4. История заказов");
                Console.WriteLine("0. Выход из аккуанта");

                Console.Write("Введите выбор: ");
                int userchoice = Convert.ToInt32(Console.ReadLine());

                switch (userchoice)
                {
                    case 1:
                        WatchProducts();
                        break;

                    case 2:
                        break;

                    case 3:
                        break;
    
                    case 4:
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
            Console.Write("Имя пользователя: ");
            string username = Console.ReadLine();
            Console.Write("Пароль: ");
            string password = Console.ReadLine();

            var user = Core.Context.Users.FirstOrDefault(x => x.Username == username && x.Password == password);
            if (user == null)
            {
                Console.WriteLine("\n❌ Пользователь с таким именем не найден.\nПроверьте корректность введённых данных или зарегистрируйтесь.");
                return;
            }
            else
            {
                Console.WriteLine($"\n✅ Вы успешно вошли в аккаунт, {user.Username}!");
                currentUser = user;
                UserMenu();
            }
        }

        static void WatchProducts()
        {
            Console.WriteLine("\nНАШИ ТОВАРЫ");
            foreach (var p in Core.Context.Products) 
            {
                Console.WriteLine($"{p.ID}. {p.Name}\n{p.Description}\n{p.Price}₽");
            }
        }

        static void AddProductToBasket()
        {
            Console.Write("\nХотите добавить в корзину какой-то товар? (да/нет): ");
            string ans = Console.ReadLine();

            if (ans == "да")
            {
                Console.Write("Введите название товара, который хотите добавить в корзину:");
                string addProduct = Console.ReadLine();

                var p = Core.Context.Products.FirstOrDefault(pr => pr.Name.ToLower() == addProduct.ToLower());
                if (p == null)
                {
                    Console.WriteLine("\n❌ Неверное название товара");
                    return;
                }
                else
                {
                    Console.Write("Введите количество товара, который хотите добавить в корзину:");
                    int kolvo = Convert.ToInt32(Console.ReadLine());

                    var basket = Core.Context.Baskets.FirstOrDefault(b => b.UserID == currentUser.ID);
                    if (basket == null)
                    {
                        basket = new Baskets { UserID = currentUser.ID };
                        Core.Context.Baskets.Add(basket);
                        Core.Context.SaveChanges();
                    }

                    var existing = Core.Context.BasketProduct.FirstOrDefault(b => b.BasketID == basket.ID && b.ProductID == p.ID);

                    if (existing != null)
                    {
                        existing.Quantity += kolvo;
                        Console.WriteLine($"🔁 Обновлено количество {p.Name}: теперь {existing.Quantity} шт.");
                    }
                    else
                    {
                        BasketProduct newItem = new BasketProduct
                        {
                            BasketID = basket.ID,
                            ProductID = p.ID,
                            Quantity = kolvo,
                            Price = p.Price
                        };
                        Core.Context.BasketProduct.Add(newItem);
                        Console.WriteLine($"✅ {p.Name} x{kolvo} добавлен в корзину!");
                        Core.Context.SaveChanges();
                    }
                }
            }
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
                        WatchProducts();
                        break;

                    case 0: outt = false; break;

                    default: Console.WriteLine("Неправильный пункт меню."); break;
                }
            }
        }
    }
}
