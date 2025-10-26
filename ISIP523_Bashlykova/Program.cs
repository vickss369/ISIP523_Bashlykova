using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP523_Bashlykova
{
    internal class Program
    {
        class Carr
        {
            public int id { get; set; }
            public string mark;
            public string problem;

            public Carr(string mark, string problem)
            {
                this.mark = mark;
                this.problem = problem;
            }
        }

        class Clientt
        {
            public int id { get; set; }
            public string fio;
            public Carr car;

            public Clientt(string fio, Carr car)
            {
                this.fio = fio;
                this.car = car;
            }
        }

        class Details
        {
            public int id { get; set; }
            public string name;
            public double price;
            public int quantity;

            public Details(string name, double price, int quantity)
            {
                this.name = name;
                this.price = price;
                this.quantity = quantity;
            }

            public void ShowDetInfo()
            {
                Console.WriteLine($"\nНазвание: {name}\nЦена: {price}\nКоличество на складе: {quantity}");
            }
        }

        class Skladd
        {
            public int id;
            public string name;
            public List<Details> allDetails = new List<Details>();

            public Skladd(int id, string name, List<Details> allDetails)
            {
                this.id = id;
                this.name = name;
                this.allDetails = allDetails;

                var dbSklad = Core.Context.Sklad.FirstOrDefault(s => s.Address == name);
                if (dbSklad == null)
                {
                    Core.Context.Sklad.Add(new Sklad
                    {
                        Address = name
                    });
                    Core.Context.SaveChanges();
                }
            }

            public bool CheckDetailOnSklad(string partName)
            {
                var detail = Core.Context.Detail.FirstOrDefault(d => d.Name == partName);
                return detail != null && detail.QuantityOnSklad > 0;
            }

            public void ShowAllDetails()
            {
                if (Core.Context.Detail.Count() == 0)
                {
                    Console.WriteLine("На складе пока нет деталей.");
                }
                else
                {
                    Console.WriteLine("Детали на складе:");
                    foreach (var d in Core.Context.Detail)
                    {
                        Console.WriteLine($"\nНазвание: {d.Name}\nЦена: {d.Price}\nКоличество на складе: {d.QuantityOnSklad}");
                    }
                }
            }

            public void AddDetail(string name, double price, int quantity)
            {
                Details det = new Details(name, price, quantity);
                allDetails.Add(det);

                var existing = Core.Context.Detail.FirstOrDefault(d => d.Name == name);
                if (existing != null)
                {
                    existing.QuantityOnSklad += quantity;
                }
                else
                {
                    Core.Context.Detail.Add(new Detail
                    {
                        Name = name,
                        Price = price,
                        QuantityOnSklad = quantity
                    });
                }
                Core.Context.SaveChanges();
            }

            public void TakeAndRemoveDetail(string nameNeededDetail)
            {
                Detail detail = Core.Context.Detail.FirstOrDefault(d => d.Name == nameNeededDetail);

                if (detail == null)
                {
                    Console.WriteLine("Деталь не найдена на складе.");
                    return;
                }
                else if (detail.QuantityOnSklad == 1)
                {
                    Core.Context.Detail.Remove(detail);
                    Console.WriteLine($"\nДеталь {nameNeededDetail} закончилась на складе.");
                }
                else
                {
                    detail.QuantityOnSklad--;
                    Console.WriteLine($"\nВы взяли деталь {nameNeededDetail} со склада.");
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
                this.client = client;
                this.neededParts = neededParts;
                this.cost = cost;
                this.status = status;
            }

            public double CalculateRepairCost()
            {
                double detailCost = neededParts.Sum(d => d.price);
                double workCost = 1000;
                return detailCost + workCost;
            }
        }

        class AutoService
        {
            public int id;
            public string name;
            public double balance;
            public Skladd sklad;

            public int clientsServed = 0;
            public List<(string name, double price, int quantity)> pendingDeliveries = new List<(string, double, int)>();

            public AutoService(int id, string name, double balance, Skladd sklad)
            {
                this.id = id;
                this.name = name;
                this.balance = balance;
                this.sklad = sklad;

                var dbService = Core.Context.Autoservice.FirstOrDefault(s => s.Name == name);
                if (dbService == null)
                {
                    Core.Context.Autoservice.Add(new Autoservice
                    {
                        Name = name,
                        Balance = balance,
                        SkladID = Core.Context.Sklad.First(s => s.Address == sklad.name).ID
                    });
                    Core.Context.SaveChanges();
                }
            }

            public void UpdateBalanceInDB()
            {
                var dbService = Core.Context.Autoservice.FirstOrDefault(s => s.Name == name);
                if (dbService != null)
                {
                    dbService.Balance = balance;
                    Core.Context.SaveChanges();
                }
            }

            public void ShowAutoserviceInfo()
            {
                Console.WriteLine($"Название автосервиса: {name}");
                Console.WriteLine($"Баланс: {balance} монет");
                sklad.ShowAllDetails();
            }

            public void CheckPendingDeliveries()
            {
                if (clientsServed >= 2 && pendingDeliveries.Count > 0)
                {
                    foreach (var p in pendingDeliveries)
                    {
                        sklad.AddDetail(p.name, p.price, p.quantity);
                        Console.WriteLine($"\n📦 Поставка прибыла: {p.quantity} шт. детали {p.name} добавлены на склад!");
                    }
                    pendingDeliveries.Clear();
                    clientsServed = 0;
                }
            }

            public void TakeOrder(RepairOrder order, int dbClientID)
            {
                Console.WriteLine($"\nПринят заказ от клиента {order.client.fio} на ремонт {order.client.car.mark}");
                Console.WriteLine($"Сломанная деталь: {order.neededParts[0].name}");
                Console.WriteLine($"Стоимость ремонта: {order.CalculateRepairCost()}");

                var dbOrder = new RepairOrders
                {
                    ClientID = dbClientID,
                    Cost = order.CalculateRepairCost(),
                    Status = "Принят"
                };
                Core.Context.RepairOrders.Add(dbOrder);
                Core.Context.SaveChanges();

                bool hasPart = sklad.CheckDetailOnSklad(order.neededParts[0].name);
                if (hasPart)
                {
                    RepairCar(order, dbOrder);
                }
                else
                {
                    Console.WriteLine("\nНа складе нет нужной детали.");
                    RejectOrder(order, dbOrder);
                }
            }

            public void RejectOrder(RepairOrder order, RepairOrders dbOrder)
            {
                double penalty = order.CalculateRepairCost() * 0.8;
                balance -= penalty;
                dbOrder.Status = "Отказ";
                Core.Context.SaveChanges();

                UpdateBalanceInDB();
                Console.WriteLine($"\nКлиент недоволен. Штраф: {penalty}. Баланс: {balance}");
            }

            public void RepairCar(RepairOrder order, RepairOrders dbOrder)
            {
                foreach (var part in order.neededParts)
                {
                    if (sklad.CheckDetailOnSklad(part.name))
                    {
                        sklad.TakeAndRemoveDetail(part.name);
                    }
                    else
                    {
                        var randomPart = Core.Context.Detail.FirstOrDefault();
                        if (randomPart != null)
                        {
                            sklad.TakeAndRemoveDetail(randomPart.Name);
                            double damage = order.CalculateRepairCost() * 1.5;
                            balance -= damage;
                            dbOrder.Status = "Неудачный ремонт";
                            Core.Context.SaveChanges();
                            UpdateBalanceInDB();
                            Console.WriteLine($"\nИспользована другая деталь {randomPart.Name}. Клиент недоволен! Штраф: {damage}. Баланс: {balance}");
                            return;
                        }
                        else
                        {
                            RejectOrder(order, dbOrder);
                            return;
                        }
                    }
                }

                double payment = order.CalculateRepairCost();
                balance += payment;
                dbOrder.Status = "Выполнен";
                Core.Context.SaveChanges();

                UpdateBalanceInDB();
                Console.WriteLine($"\n✅ Ремонт выполнен успешно! Клиент оплатил {payment}. Баланс: {balance}");
            }

            public void FinishClient()
            {
                clientsServed++;
                CheckPendingDeliveries();
            }

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
                    Console.WriteLine($"\nНедостаточно средств. Стоимость покупки: {totalCost}, Баланс: {balance}");
                    return;
                }
                balance -= totalCost;
                UpdateBalanceInDB();

                pendingDeliveries.Add((dName, pricePerUnit, quantity));
                Console.WriteLine($"\n🕒 Вы купили {quantity} шт. детали «{dName}» за {totalCost} монет. Поставка прибудет через 2 клиента.");
            }
        }
            static void Main(string[] args)
            {
                Console.OutputEncoding = System.Text.Encoding.UTF8;

                Skladd mySklad = new Skladd(1, "Склад №1", new List<Details>());
                AutoService service = new AutoService(1, "Автосервис PR7", 10000, mySklad);

                Console.WriteLine("🚗 Добро пожаловать в «Автосервис PR7»!");
                Console.WriteLine("У тебя есть 10000 монет и склад БЕЗ ДЕТАЛЕЙ");
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
                        Console.WriteLine("\n📋 МЕНЮ:");
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
                                Console.Write("\nВведите ФИО клиента: ");
                                string fio = Console.ReadLine();
                                Console.Write("Введите марку машины: ");
                                string mark = Console.ReadLine();
                                Console.Write("Введите сломанную деталь: ");
                                string problem = Console.ReadLine();

                                Car dbCar = new Car
                                {
                                    Mark = mark,
                                    Problem = problem
                                };
                                Core.Context.Car.Add(dbCar);
                                Core.Context.SaveChanges();
                                Client dbClient = new Client
                                {
                                    FIO = fio,
                                    CarID = dbCar.ID
                                };
                                Core.Context.Client.Add(dbClient);
                                Core.Context.SaveChanges();

                                Carr car = new Carr(mark, problem);
                                Clientt client = new Clientt(fio, car);
                                Details needed = new Details(problem, 3000, 1);
                                List<Details> parts = new List<Details> { needed };
                                RepairOrder order = new RepairOrder(1, client, parts, 0, "В ожидании");

                                service.TakeOrder(order, dbClient.ID);
                                service.FinishClient();
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
