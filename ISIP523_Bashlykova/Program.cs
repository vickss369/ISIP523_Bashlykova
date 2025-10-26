using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ISIP523_Bashlykova
{
    internal class Program
    {
        class Carr
        {
            private static int nextId = 1;
            public int id;
            public string mark;
            public string problem;

            public Carr(string mark, string problem)
            {
                this.id = nextId++; 
                this.mark = mark;
                this.problem = problem;
            }

            public void AddCar()
            {
                Console.Write("Введите марку машины: ");
                string mark = Console.ReadLine();
                Console.Write("Введите описание проблемы: ");
                string problem = Console.ReadLine();
                Carr car = new Carr(mark, problem);

                Core.Context.Car.Add(new Car
                {
                    Mark = mark,
                    Problem = problem
                });
                Core.Context.SaveChanges();
            }
        }

        class Clientt
        {
            private static int nextId = 1;
            public int id;
            public string fio;
            public Carr car;

            public Clientt(string fio, Carr car)
            {
                this.id = nextId++;
                this.fio = fio;
                this.car = car;
            }

            public void AddClient()
            {
                Console.Write("Введите ФИО клиента: ");
                string fio = Console.ReadLine();
                Console.Write("Введите марку машины: ");
                string mark = Console.ReadLine();
                Console.Write("Введите описание проблемы: ");
                string problem = Console.ReadLine();
                Clientt newClient = new Clientt(fio, new Carr(mark, problem));

                Core.Context.Client.Add(new Client
                {
                    FIO = fio,
                    CarID = this.id
                });
                Core.Context.SaveChanges();
            }
        }

        class Details
        {
            private static int nextId = 1;
            public int id;
            public string name;
            public double price;
            public int quantity;

            public Details(string name, double price, int quantity)
            {
                this.id = nextId++;
                this.name = name;
                this.price = price;
                this.quantity = quantity;
            }

            public void ShowDetInfo()
            {
                Console.WriteLine($"\nНазвание: {name}\nЦена: {price}\nКоличество на складе: {quantity}"); 
            }

            /*public bool IsOnSklad()
            {
                Console.Write("Введите название необходимой детали для проверки наличия на складе: ");
                string nameNeededDetail = Console.ReadLine();
                var detail = Core.Context.Detail.FirstOrDefault(d => d.Name == nameNeededDetail);
                return detail != null && detail.QuantityOnSklad > 0;
            }*/
        }

        class Skladd
        {
            public int id;
            public List<Details> allDetails = new List<Details>();

            public Skladd(int id, List<Details> allDetails)
            {
                this.id = id;
                this.allDetails = allDetails;
            }

            public bool CheckDetailOnSklad(string partName)
            {
                Console.Write("\nВведите название необходимой детали для проверки наличия на складе: ");
                string nameNeededDetail = Console.ReadLine();
                var detail = Core.Context.Detail.FirstOrDefault(d => d.Name == nameNeededDetail);
                return detail != null && detail.QuantityOnSklad > 0;
            }

            public void ShowAllDetails()
            {
                if (allDetails.Count == 0)
                {
                    Console.WriteLine("На складе пока нет деталей.");
                }
                else
                {
                    Console.WriteLine("Детали на складе:");
                    foreach (Details d in allDetails)
                    {
                        d.ShowDetInfo();
                    }
                }
            }

            public void AddDetail(string name, double price, int quantity)
            {
                Details det = new Details(name, price, quantity);
                allDetails.Add(det);

                Core.Context.Detail.Add(new Detail
                {
                    Name = name,
                    Price = price,
                    QuantityOnSklad = quantity
                });
                Core.Context.SaveChanges();
                Console.WriteLine($"\nДеталь «{name}» успешно добавлена на склад!");
            }

            public void TakeAndRemoveDetail()
            {
                Console.Write("\nВведите название необходимой детали: ");
                string nameNeededDetail = Console.ReadLine();
                Detail detail = Core.Context.Detail.FirstOrDefault(d => d.Name.Contains(nameNeededDetail));

               if (detail == null)
               {
                    Console.WriteLine("Деталь не найдена на складе.");
                    return;
               }
                else if (detail.QuantityOnSklad == 1)
                {
                    Core.Context.Detail.Remove(detail);
                    Console.WriteLine("\nДетали больше нет на складе(нужно купить)");
                }
                else
                {
                    detail.QuantityOnSklad--;
                    Console.WriteLine("\nВы взяли деталь со склада.");
                }

                Core.Context.SaveChanges();
            }
        }

        class RepairOrder
        {
            public int id;
            public Clientt client;
            public List<Details> neededParts = new List<Details>();
            public double cost;
            public string status;

            public RepairOrder(int id, Clientt client, List<Details> neededParts, double cost, string status)
            {
                this.id = id;
                this.client = client;
                this.neededParts = neededParts;
                this.cost = cost;
                this.status = status;
            }

            /*public double CalculateRepairCost()
            {
            }*/
        }

        class AutoService
        {
            public int id;
            public string name;
            public double balance;
            public Skladd sklad;
            public AutoService(int id, string name, double balance, Skladd sklad)
            {
                this.id = id;
                this.name = name;
                this.balance = balance;
                this.sklad = sklad;
            }

            public void ShowAutoserviceInfo()
            {
                Console.WriteLine($"Название автосервиса: {name}");
                Console.WriteLine($"Баланс: {balance} монет");
                sklad.ShowAllDetails();
            }

            /*public void TakeOrder(RepairOrder order)
            {
            }*/

            /*public void RejectOrder(RepairOrder order)
            {
            }*/

            /*public void RepairCar(RepairOrder order)
            {
            }*/

            public void BuyDetails()
            {
                Console.Write("\nВведите название детали, которую хотите купить: ");
                string dName = Console.ReadLine();
                Console.Write("Введите цену детали: ");
                double pricePerUnit;
                while (!double.TryParse(Console.ReadLine(), out pricePerUnit) || pricePerUnit < 0)
                {
                    Console.Write("Ошибка! Введите корректную цену: ");
                }
                Console.Write("Введите количество деталей для покупки: ");
                int quantity;
                while (!int.TryParse(Console.ReadLine(), out quantity) || quantity <= 0)
                {
                    Console.Write("Ошибка! Введите корректное количество: ");
                }

                double totalCost = pricePerUnit * quantity;

                if (balance < totalCost)
                {
                    Console.WriteLine($"Недостаточно средств. Стоимость покупки: {totalCost}, Баланс: {balance}");
                    return;
                }
                balance -= totalCost;

                sklad.AddDetail(dName, pricePerUnit, quantity);
                Console.WriteLine($"Вы купили {quantity} шт. детали «{dName}» за {totalCost} монет. Остаток баланса: {balance}");
            }
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8; //для смайликов, надеюсь сработает:(

            Skladd mySklad = new Skladd(1, new List<Details>());
            AutoService service = new AutoService(1, "Автосервис PR7", 1000, mySklad);

            Console.WriteLine("🚗 Добро пожаловать в «Автосервис PR7»!");
            Console.WriteLine("У тебя есть 100000 монет и склад БЕЗ ДЕТАЛЕЙ");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

            bool outt = true;
            while (outt)
            {
                if (service.balance <= 0)
                {
                    Console.WriteLine("\n💸 Вы обанкротились:(\nИгра окончена.");
                    Console.WriteLine("\nНажмите любую клавишу, чтобы выйти...");
                    Console.ReadKey();
                    break;
                }
                else
                {
                    Console.WriteLine("\n📋МЕНЮ:");
                    Console.WriteLine("1. Принять нового клиента");
                    Console.WriteLine("2. Купить детали на склад");
                    Console.WriteLine("3. Показать информацию об автосервисе");
                    Console.WriteLine("0. Выход");

                    Console.Write("Введите выбор: ");
                    int choice = Convert.ToInt32(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("\n~~~ Новый клиент ~~~");
                            break;

                        case 2:
                            service.BuyDetails();
                            break;

                        case 3:
                            Console.WriteLine("\n~~~ Информация о автосервисе: ~~~");
                            service.ShowAutoserviceInfo();
                            break;

                        case 0: outt = false; break;

                        default: Console.WriteLine("Неправильный пункт меню."); break;
                    }
                }
            }
        }
    }
}



