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
                        AddProductToBasket();
                        break;

                    case 2:
                        ShowBasket();
                        break;

                    case 3:
                        CreateOrder();
                        break;
    
                    case 4:
                        ShowOrderHistory();
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

                Baskets basket = new Baskets
                {
                    UserID = dbUser.ID,
                    Quantity = 0
                };
                Core.Context.Baskets.Add(basket);
                Core.Context.SaveChanges();


                Console.WriteLine("\n✅ Вы успешно зарегистрировались!\nУ вас есть пустая корзина - начните наполнять её товарами!");
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
            Console.WriteLine("\n🛍 НАШИ ТОВАРЫ");
            foreach (var p in Core.Context.Products) 
            {
                Console.WriteLine($"\n{p.ID}. {p.Name}\n{p.Description}\n{p.Price}₽");
            }
        }

        static void AddProductToBasket()
        {
            Console.Write("\nХотите добавить в корзину какой-то товар? (да/нет): ");
            string ans = Console.ReadLine();

            if (ans == "да")
            {
                Console.Write("\nВведите название товара, который хотите добавить в корзину: ");
                string addProduct = Console.ReadLine();

                var p = Core.Context.Products.FirstOrDefault(pr => pr.Name.ToLower().Contains(addProduct.ToLower()));
                if (p == null)
                {
                    Console.WriteLine("\n❌ Неверное название товара.");
                    return;
                }
                else
                {
                    Console.Write("Введите количество товара, который хотите добавить в корзину: ");
                    int kolvo = Convert.ToInt32(Console.ReadLine());

                    var basket = Core.Context.Baskets.FirstOrDefault(b => b.UserID == currentUser.ID);
                    if (basket == null)
                    {
                        basket = new Baskets 
                        { 
                            UserID = currentUser.ID,
                            Quantity = 0,
                        };
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
                        BasketProduct newBP = new BasketProduct
                        {
                            BasketID = basket.ID,
                            ProductID = p.ID,
                            Quantity = kolvo,
                            Price = p.Price
                        };
                        Core.Context.BasketProduct.Add(newBP);
                        Console.WriteLine($"✅ {p.Name} x{kolvo} добавлен в корзину!");

                        basket.Quantity = Core.Context.BasketProduct.Where(bp => bp.BasketID == basket.ID).Sum(bp => bp.Quantity);

                        Core.Context.SaveChanges();
                    }
                }
            }
        }

        static void ShowBasket()
        {
            var basket = Core.Context.Baskets.FirstOrDefault(b => b.ID == currentUser.ID );
            var products = Core.Context.BasketProduct.Where(bp => bp.BasketID == basket.ID).ToList();
            if (basket == null && products.Count == 0)
            {
                Console.WriteLine("\n🧺 Корзина пуста!");
                return;
            }
            else
            {
                Console.WriteLine("\n🛒 КОРЗИНА");
                double totalSum = 0;
                foreach (var pr in products)
                {
                    var prod = Core.Context.Products.First(p => p.ID == pr.ProductID);
                    Console.WriteLine($"{prod.Name} — {pr.Price}₽ × {pr.Quantity} = {pr.Price * pr.Quantity}₽");
                    totalSum += pr.Price * pr.Quantity;
                }
                Console.WriteLine($"💰 Итого: {totalSum}₽");
            }
        }

        static void CreateOrder()
        {
            Console.WriteLine("\nВыберите, что хотите заказать:");
            Console.WriteLine("1. Купить конкретный товар");
            Console.WriteLine("2. Купить всю корзину");
            Console.WriteLine("0. Отмена");

            Console.Write("Введите выбор: ");
            int orderchoice = Convert.ToInt32(Console.ReadLine());

            switch (orderchoice)
            {
                case 1:
                    BuyOneProd();
                    break;

                case 2:
                    BuyAllBasket();
                    break;

                case 0: Console.WriteLine("🚫 Отменено."); break;

                default: Console.WriteLine("❌ Неверный пункт меню."); break;
            }
        }

        static void BuyOneProd()
        {
            var basket = Core.Context.Baskets.FirstOrDefault(b => b.UserID == currentUser.ID);
            if (basket == null)
            {
                Console.WriteLine("🧺 У вас нет корзины.");
                return;
            }

            var items = Core.Context.BasketProduct.Where(bp => bp.BasketID == basket.ID)
                .ToList();

            if (items.Count == 0)
            {
                Console.WriteLine("🧺 Корзина пуста!");
                return;
            }

            Console.WriteLine("\n🛒 Товары в вашей корзине:");
            foreach (var i in items)
            {
                var prod = Core.Context.Products.First(p => p.ID == i.ProductID);
                Console.WriteLine($"{i.ID}. {prod.Name} — {i.Price} ₽ × {i.Quantity} = {i.Price * i.Quantity}");
            }

            Console.Write("Введите ID товара из корзины для покупки: ");
            int basketProductId = Convert.ToInt32(Console.ReadLine());

            var basketItem = items.FirstOrDefault(bp => bp.ID == basketProductId);
            if (basketItem == null)
            {
                Console.WriteLine("❌ Нет такого товара в корзине!");
                return;
            }

            var productToBuy = Core.Context.Products.First(p => p.ID == basketItem.ProductID);

            Console.Write("Введите количество для покупки: ");
            if (!int.TryParse(Console.ReadLine(), out int qty) || qty <= 0 || qty > basketItem.Quantity)
            {
                Console.WriteLine("❌ Некорректное количество!");
                return;
            }

            Console.WriteLine("\n📦 Доступные ПВЗ:");
            foreach (var p in Core.Context.PVZ)
            {
                Console.WriteLine($"{p.ID}. {p.Name} ({p.Address})");
            }

            Console.Write("Выберите ПВЗ: ");
            int pvzId = Convert.ToInt32(Console.ReadLine());
            var pvz = Core.Context.PVZ.FirstOrDefault(p => p.ID == pvzId);
            if (pvz == null)
            {
                Console.WriteLine("❌ Нет такого ПВЗ!");
                return;
            }

            Orders order = new Orders
            {
                UserID = currentUser.ID,
                PVZID = pvz.ID,
                OrderDate = DateTime.Now,
                Status = "Создан",
                TotalSum = productToBuy.Price * qty
            };
            Core.Context.Orders.Add(order);
            Core.Context.SaveChanges();

            OrderItems orderItem = new OrderItems
            {
                OrderID = order.ID,
                ProductID = productToBuy.ID,
                Quantity = qty,
                PriceAtBuyMoment = productToBuy.Price
            };
            Core.Context.OrderItems.Add(orderItem);

            if (basketItem.Quantity > qty)
            {
                basketItem.Quantity -= qty;
            }
            else
            {
                Core.Context.BasketProduct.Remove(basketItem);
            }

            basket.Quantity = Core.Context.BasketProduct.Where(bp => bp.BasketID == basket.ID).Sum(bp => bp.Quantity);

            Core.Context.SaveChanges();

            Console.WriteLine($"✅ Заказ №{order.ID} оформлен!\nТовар '{productToBuy.Name}' x{qty} куплен! Стоимость: {productToBuy.Price * qty}₽\nЗабрать в '{pvz.Name}'.");
        }

        static void BuyAllBasket()
        {
            var basket = Core.Context.Baskets.FirstOrDefault(b => b.UserID == currentUser.ID);
            if (basket == null)
            {
                Console.WriteLine("🧺 У вас нет корзины.");
                return;
            }

            var items = Core.Context.BasketProduct.Where(bp => bp.BasketID == basket.ID).ToList();
            if (items.Count == 0)
            {
                Console.WriteLine("🧺 Корзина пуста!");
                return;
            }

            Console.WriteLine("\n📦 Доступные ПВЗ:");
            foreach (var p in Core.Context.PVZ)
            {
                Console.WriteLine($"\n{p.ID}. {p.Name} \n({p.Address})");
            }

            Console.Write("Выберите ПВЗ: ");
            int pvzId = Convert.ToInt32(Console.ReadLine());
            var pvz = Core.Context.PVZ.FirstOrDefault(p => p.ID == pvzId);
            if (pvz == null)
            {
                Console.WriteLine("❌ Нет такого ПВЗ!");
                return;
            }

            Orders order = new Orders
            {
                UserID = currentUser.ID,
                PVZID = pvz.ID,
                OrderDate = DateTime.Now,
                Status = "Создан"
            };
            Core.Context.Orders.Add(order);
            Core.Context.SaveChanges();

            double totalSum = 0;

            foreach (var i in items)
            {
                var product = Core.Context.Products.First(p => p.ID == i.ProductID);

                OrderItems oi = new OrderItems
                {
                    OrderID = order.ID,
                    ProductID = product.ID,
                    Quantity = i.Quantity,
                    PriceAtBuyMoment = i.Price
                };
                Core.Context.OrderItems.Add(oi);

                totalSum += i.Price * i.Quantity;
                Core.Context.BasketProduct.Remove(i);
            }

            order.TotalSum = totalSum;
            basket.Quantity = 0;

            Core.Context.SaveChanges();
            Console.WriteLine($"✅ Заказ №{order.ID} оформлен! \nОбщая сумма: {totalSum} ₽. \nЗабрать в ПВЗ '{pvz.Name}'. \nКорзина очищена.");
        }

        static void ShowOrderHistory()
        {

        }

        static void AddProductsAndPVZ()
        {
            if (!Core.Context.Products.Any())
            {
                Core.Context.Products.Add(new Products { Name = "Свитер вязаный", Description = "Oversize-модель, молочный с принтом в красно-синюю клеточку.", Price = 1431.43, StockQuantity = 9 });
                Core.Context.Products.Add(new Products { Name = "Брелок Козочка", Description = "Мягкая плюшевая козочка из мультика, на карабине.", Price = 420.03, StockQuantity = 15 });
                Core.Context.Products.Add(new Products { Name = "Блеск для губ Art-visage", Description = "Питательная кремовая текстура, Красно-розовый глянцевый оттенок.", Price = 317.21, StockQuantity = 33 });
                Core.Context.Products.Add(new Products { Name = "Кулон Анатомическое сердце", Description = "Подвеска ручной работы с переливающейся красной жидкостью внутри.", Price = 437.9, StockQuantity = 13 });
                Core.Context.Products.Add(new Products { Name = "Набор значков 'Эксклюзивная классика'", Description = "Значки по известным классическим произведениям мировых авторов.", Price = 253.12, StockQuantity = 15 });
                Core.Context.SaveChanges();
            }

            if (!Core.Context.PVZ.Any())
            {
                Core.Context.PVZ.Add(new PVZ { Name = "МоскваGMWOG", Address = "г. Москва, ул. Бро, 5", Phone = "+7 963 656 0992" });
                Core.Context.PVZ.Add(new PVZ { Name = "МытищиGMWOG", Address = "г. Мытищи, наб. Чилл, 12", Phone = "+7 916 402 5560" });
                Core.Context.PVZ.Add(new PVZ { Name = "КазаньGMWOG", Address = "г. Казань, пр. Вайб, 9", Phone = "+7 963 128 5251" });
                Core.Context.PVZ.Add(new PVZ { Name = "ЧернянкаGMWOG", Address = "пос. Чернянка, Кронштадтский б-р, 19", Phone = "+7 905 678 1889" });
                Core.Context.PVZ.Add(new PVZ { Name = "ВолоколамскGMWOG", Address = "г. Волоколамск, ул. Смольная, 51", Phone = "+7 985 198 7679" });
                Core.Context.SaveChanges();
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
            AddProductsAndPVZ();

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
