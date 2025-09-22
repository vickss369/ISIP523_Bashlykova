using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP523_Bashlykova
{
    internal class Program
    {
        enum Category
        {
            Еда = 1,
            Техника,
            Канцелярия,
            Одежда,
            Химия
        }

        class Product
        {
            public int ProductID;
            public string Name;
            public double Price;
            public int Quantity
            {
                get => quantity;
                set
                {
                    quantity = value < 0 ? 0 : value;
                }
            }
            private int quantity;

            public bool IsOnSklad => Quantity > 0;
            public Category Category;

            public Product(int productID, string name, double price, int quantity, Category category)
            {
                ProductID = productID;
                Name = name;
                Price = price;
                Quantity = quantity;
                Category = category;
            }

            public void PrintInfo()
            {
                Console.WriteLine("\nИнформация о товаре:");
                Console.WriteLine($"ID: {ProductID}");
                Console.WriteLine($"Название: {Name}");
                Console.WriteLine($"Цена: {Price:F2}");
                Console.WriteLine($"Количество: {Quantity}");
                Console.WriteLine($"В наличии на складе: {(IsOnSklad ? "Да" : "Нет")}");
                Console.WriteLine($"Категория: {Category}");
                Console.WriteLine(new string('-', 30));
            }
        }

        class Inventory
        {
            private List<Product> products = new List<Product>();
            private int nextProductID = 1;

            public void AddProduct()
            {
                Console.WriteLine("Добавление нового товара");

                Console.Write("Введите название: ");
                string name = Console.ReadLine();

                double price;
                while (true)
                {
                    Console.Write("Введите цену: ");
                    if (double.TryParse(Console.ReadLine(), out price) && price >= 0) break;
                    Console.WriteLine("Ошибка: введите корректное число для цены.");
                }

                int quantity;
                while (true)
                {
                    Console.Write("Введите количество: ");
                    if (int.TryParse(Console.ReadLine(), out quantity) && quantity >= 0) break;
                    Console.WriteLine("Ошибка: введите корректное неотрицательное целое число для количества.");
                }

                Console.WriteLine("Выберите категорию (введите цифру):");
                foreach (var catValue in Enum.GetValues(typeof(Category)))
                {
                    Console.WriteLine($"{(int)catValue}. {catValue}");
                }

                Category category;
                while (true)
                {
                    Console.Write("Категория: ");
                    if (Enum.TryParse<Category>(Console.ReadLine(), out category) && Enum.IsDefined(typeof(Category), category))
                        break;
                    Console.WriteLine("Ошибка: выберите категорию из списка.");
                }

                var newProduct = new Product(nextProductID++, name, price, quantity, category);
                products.Add(newProduct);
                Console.WriteLine("\nТовар успешно добавлен");
            }

            public void RemoveProduct()
            {
                if (products.Count == 0)
                {
                    Console.WriteLine("Склад пуст.");
                    return;
                }

                Console.Write("Введите ID товара для удаления: ");
                if (int.TryParse(Console.ReadLine(), out int id))
                {
                    var prod = products.Find(p => p.ProductID == id);
                    if (prod != null)
                    {
                        products.Remove(prod);
                        Console.WriteLine($"Товар с ID {id} удалён.");
                    }
                    else
                    {
                        Console.WriteLine("Товар с таким ID не найден.");
                    }
                }
                else
                {
                    Console.WriteLine("Ошибка: введён неверный ID.");
                }
            }

            public void ListProducts()
            {
                if (products.Count == 0)
                {
                    Console.WriteLine("Склад пуст.");
                    return;
                }

                Console.WriteLine("Список товаров:");
                foreach (var p in products)
                    p.PrintInfo();
            }

            public void OrderProduct()
            {
                if (products.Count == 0)
                {
                    Console.WriteLine("Склад пуст.");
                    return;
                }

                Console.Write("\nВведите ID товара для заказа поставки: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("Ошибка: неверный ID.");
                    return;
                }

                var product = products.Find(p => p.ProductID == id);
                if (product == null)
                {
                    Console.WriteLine("Товар не найден.");
                    return;
                }

                Console.Write($"Введите количество для заказа у товара '{product.Name}': ");
                if (!int.TryParse(Console.ReadLine(), out int orderQuantity) || orderQuantity <= 0)
                {
                    Console.WriteLine("Ошибка: нужно ввести положительное число.");
                    return;
                }

                product.Quantity += orderQuantity;
                Console.WriteLine($"Заказ поставки выполнен. \nНовое количество товара '{product.Name}': {product.Quantity}");
            }

            public void SellProduct()
            {
                if (products.Count == 0)
                {
                    Console.WriteLine("Склад пуст.");
                    return;
                }

                Console.Write("Введите название товара для продажи: ");
                string name = Console.ReadLine();

                var product = products.Find(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
                if (product == null)
                {
                    Console.WriteLine("Товар не найден.");
                    return;
                }

                if (product.Quantity == 0)
                {
                    Console.WriteLine($"Товар '{product.Name}' отсутствует на складе.");
                    return;
                }

                product.Quantity--;
                Console.WriteLine($"Товар '{product.Name}' успешно продан. \nОсталось на складе: {product.Quantity}");
            }

            public void SearchProducts()
            {
                if (products.Count == 0)
                {
                    Console.WriteLine("Склад пуст.");
                    return;
                }

                Console.WriteLine("Поиск товаров по параметру:");
                Console.WriteLine("1. ID");
                Console.WriteLine("2. Название");
                Console.WriteLine("3. Категория");
                Console.Write("Выберите параметр: ");
                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Ошибка: неверный ввод.");
                    return;
                }

                switch (choice)
                {
                    case 1:
                        Console.Write("\nВведите ID товара: ");
                        if (int.TryParse(Console.ReadLine(), out int id))
                        {
                            var prod = products.Find(p => p.ProductID == id);
                            if (prod != null)
                                prod.PrintInfo();
                            else
                                Console.WriteLine("\nТовар не найден.");
                        }
                        else Console.WriteLine("\nОшибка: неверный ID.");
                        break;

                    case 2:
                        Console.Write("\nВведите название товара: ");
                        string name = Console.ReadLine();
                        var foundByName = products.FindAll(p => p.Name.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0);
                        if (foundByName.Count == 0)
                            Console.WriteLine("\nТовары с таким названием не найдены.");
                        else
                            foundByName.ForEach(p => p.PrintInfo());
                        break;

                    case 3:
                        Console.WriteLine("\nВыберите категорию:");
                        foreach (var catValue in Enum.GetValues(typeof(Category)))
                        {
                            Console.WriteLine($"{(int)catValue}. {catValue}");
                        }
                        Console.Write("Категория: ");
                        if (Enum.TryParse<Category>(Console.ReadLine(), out Category cat) && Enum.IsDefined(typeof(Category), cat))
                        {
                            var foundByCategory = products.FindAll(p => p.Category == cat);
                            if (foundByCategory.Count == 0)
                                Console.WriteLine("\nТовары в данной категории не найдены.");
                            else
                                foundByCategory.ForEach(p => p.PrintInfo());
                        }
                        else
                        {
                            Console.WriteLine("\nНеверная категория.");
                        }
                        break;

                    default:
                        Console.WriteLine("Неверный выбор параметра.");
                        break;
                }
            }
        }
            static void Main()
            {
                Inventory sklad = new Inventory();
                bool running = true;

                while (running)
                {
                    Console.WriteLine("\n=== МЕНЮ ===");
                    Console.WriteLine("1. Добавить товар");
                    Console.WriteLine("2. Удалить товар");
                    Console.WriteLine("3. Вывести список товаров");
                    Console.WriteLine("4. Заказать поставку товара");
                    Console.WriteLine("5. Продать товар");
                    Console.WriteLine("6. Поиск товаров");
                    Console.WriteLine("0. Выход");
                    Console.Write("Введите выбор: ");

                    string input = Console.ReadLine();
                    Console.WriteLine();

                    switch (input)
                    {
                        case "1":
                            sklad.AddProduct();
                            break;
                        case "2":
                            sklad.RemoveProduct();
                            break;
                        case "3":
                            sklad.ListProducts();
                            break;
                        case "4":
                            sklad.OrderProduct();
                            break;
                        case "5":
                            sklad.SellProduct();
                            break;
                        case "6":
                            sklad.SearchProducts();
                            break;
                        case "0":
                            running = false;
                            break;
                        default:
                            Console.WriteLine("Некорректный выбор, попробуйте ещё раз.");
                            break;
                    }
                }
            }
        }
    }
