using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP523_Bashlykova
{
    internal class Program
    {
        /// Задание Учёт товаров в магазине
        /// У товара должны быть следующее параметры:
        /// * Уникальный код(начинается с "1", должен автоматически ставиться при пополнении списка товаров)
        /// * Название
        /// * Цена
        /// * Количество
        /// * Остался ли ещё товар на складе
        /// * Категория(выбирается из имеющихся, задаются в коде, сделайте как минимум 3)

        /// Мы можем работать с товаром через команды:
        /// * Добавить товар
        /// * Удалить товар
        /// * Заказать поставку товара
        /// * Продать товар
        /// * Поиск товаров(по коду, названию и категории). Необходимо выводить полную информацию о товаре.
        
        class Product
        {
            public int ProductID;
            public string Name;
            public double Price;
            public int Quantity;
            public string IsOnSklad;
            public string Category;

            public Product(int productID, string name, double price, int quantity, string isOnSklad, string category)
            {
                ProductID = productID;
                Name = name;
                Price = price;
                Quantity = quantity;
                IsOnSklad = isOnSklad;
                Category = category;
            }

            public void PrintInfo()
            {
                Console.WriteLine("Информация о товаре\n" + "Номер: " + this.ProductID + "\n" + "Название: " + this.Name + "\n" + "Цена: " + this.Price + "\n" + "Количество: " + this.Quantity + "\n" + "Есть ли на складе: " + this.IsOnSklad + "\n" + "Категория: " + this.Category + "\n");
            }
        }

        static void Main(string[] args)
        {
            List<Product> sklad = new List<Product>();
            bool outt = true;
            while (outt)
            {
                Console.WriteLine("\nМЕНЮ");
                Console.WriteLine("1. Добавление товара");
                Console.WriteLine("2. Удаление товара");
                Console.WriteLine("3. Вывод списка товаров");
                Console.WriteLine("4. Заказать товар");
                Console.WriteLine("5. Продать товар");
                Console.WriteLine("6. Поиск товаров(по коду, названию и категории)");
                Console.WriteLine("0. Выход");

                Console.Write("Введите выбор: ");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("\nВведите информацию о товаре для добавления: \n");
                        int addproductID = Convert.ToInt32(Console.ReadLine());
                        string addname = Console.ReadLine();
                        double addprice = Convert.ToDouble(Console.ReadLine());
                        int addquantity = Convert.ToInt32(Console.ReadLine());
                        string addonsklad = Console.ReadLine();
                        string addcat = Console.ReadLine();

                        Product productadd = new Product(addproductID, addname, addprice, addquantity, addonsklad, addcat);
                        sklad.Add(productadd);
                        Console.WriteLine("\nТовар успешно добавлен.");
                        break;

                    case 2:
                        if (sklad.Count == 0)
                        {
                            Console.WriteLine("\nСклад товаров пуст.");
                            break;
                        }

                        Console.Write("\nВведите ProductID товара для удаления: ");
                        int removeID = Convert.ToInt32(Console.ReadLine());

                        Product productToRemove = sklad.Find(p => p.ProductID == removeID);
                        if (productToRemove != null)
                        {
                            sklad.Remove(productToRemove);
                            Console.WriteLine("\nТовар удалён.");
                        }
                        else
                        {
                            Console.WriteLine("\nТовар с таким ProductID не найден.");
                        }
                        break;

                    case 3:
                        if (sklad.Count == 0)
                        {
                            Console.WriteLine("\nСписок товаров пуст.");
                        }
                        else
                        {
                            Console.WriteLine("\nСПИСОК ТОВАРОВ\n");
                            foreach (var prd in sklad)
                            {
                                prd.PrintInfo();
                            }
                        }
                        break;

                    case 4:
                        for (int i = 0; i < sklad.Count; i++)
                        {
                            if (sklad[i].IsOnSklad == "нет" || sklad[i].IsOnSklad == "Нет")
                            {
                                Console.Write("\nВведите количество товара '" + sklad[i].Name + "', который необходимо заказать: ");
                                int newQuantity = Convert.ToInt32(Console.ReadLine());
                                sklad[i].Quantity = newQuantity;
                                Console.WriteLine("\nТовар успешно заказан.");
                            }
                        }
                        break;

                    case 5:
                        Console.Write("");
                        string nazvforsale = Console.ReadLine();
                        for (int i = 0; i < sklad.Count; i++)
                        {
                            if (sklad[i].Name == nazvforsale)
                            {
                                Console.WriteLine();
                                Console.WriteLine("\nТовар '" + sklad[i].Name + "' успешно продан.");
                                sklad[i].Quantity--;
                            }
                        }
                        break;

                    case 6:
                        Console.WriteLine("\nМЕНЮ ПАРАМЕТРОВ ТОВАРА");
                        Console.WriteLine("1. ID");
                        Console.WriteLine("2. Название");
                        Console.WriteLine("3. Категория");
                        Console.Write("Введите выбор: ");
                        int choiseval = Convert.ToInt32(Console.ReadLine());
                        switch (choiseval)
                        {
                            case 1:
                                Console.Write("\nВведите ProductID искомого товара: ");
                                int poiskID = Convert.ToInt32(Console.ReadLine());
                                bool poiskid = false;
                                for (int i = 0; i < sklad.Count; i++)
                                {
                                    if (sklad[i].ProductID == poiskID)
                                    {
                                        Console.WriteLine();
                                        sklad[i].PrintInfo();
                                        poiskid = true;
                                    }
                                }
                                if (!poiskid)
                                {
                                    Console.WriteLine("Товар с ID " + poiskID + " не найден.");
                                }
                                break;

                            case 2:
                                Console.Write("\nВведите название искомого товара: ");
                                string nazvtovar = Console.ReadLine();
                                bool poisknazv = false;
                                for (int i = 0; i < sklad.Count; i++)
                                {
                                    if (sklad[i].Name == nazvtovar)
                                    {
                                        Console.WriteLine();
                                        sklad[i].PrintInfo();
                                        poisknazv = true;
                                    }
                                }
                                if (!poisknazv)
                                {
                                    Console.WriteLine("Товар " + nazvtovar + " не найден.");
                                }
                                break;

                            case 3:
                                Console.Write("\nВведите категорию искомого товара: ");
                                string categ = Console.ReadLine();
                                bool poiskcateg = false;
                                for (int i = 0; i < sklad.Count; i++)
                                {
                                    if (sklad[i].Category == categ)
                                    {
                                        Console.WriteLine();
                                        sklad[i].PrintInfo();
                                        poiskcateg = true;
                                    }
                                }
                                if (!poiskcateg)
                                {
                                    Console.WriteLine("Категория товара " + categ + " не найденa.");
                                }
                                break;

                            default: break;
                        }
                        break;

                    case 0: outt = false; break;

                    default: outt = false; break;
                }
            }
        }
    }
}
