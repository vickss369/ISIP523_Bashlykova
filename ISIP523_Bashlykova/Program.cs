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
            public string name { get; set; }
            public double price { get; set; }
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
            public Details neededPart;
            public double cost;
            public string status;

            public RepairOrder(int id, Clientt client, Details neededPart, double cost, string status)
            {
                this.id = id;
                this.client = client;
                this.neededPart = neededPart;
                this.cost = cost;
                this.status = status;
            }

            public double CalculateRepairCost()
            {
                double detailCost = 0;

                if (neededPart != null)
                {
                    detailCost = neededPart.price > 0
                        ? neededPart.price
                        : GetDetailPriceFromDatabase(neededPart.name);
                }

                double workCost = 1000;
                double total = detailCost + workCost;
                return total;
            }

            private double GetDetailPriceFromDatabase(string name)
            {
                var dbDetail = Core.Context.Detail.FirstOrDefault(d => d.Name == name);
                return dbDetail?.Price ?? 0;
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
                Console.WriteLine($"Сломанная деталь: {order.neededPart.name}");
                Console.WriteLine($"Стоимость ремонта: {order.CalculateRepairCost()}");

                var dbOrder = new RepairOrders
                {
                    ClientID = dbClientID,
                    Cost = order.CalculateRepairCost(),
                    Status = "Принят"
                };
                Core.Context.RepairOrders.Add(dbOrder);
                Core.Context.SaveChanges();

                bool hasPart = sklad.CheckDetailOnSklad(order.neededPart.name);

                if (hasPart)
                {
                    RepairCar(order, dbOrder);
                }
                else
                {
                    Console.WriteLine($"\n❌ На складе нет нужной детали «{order.neededPart.name}».");
                    Console.WriteLine("Выберите действие:");
                    Console.WriteLine("1. Отказать клиенту (штраф)");
                    Console.WriteLine("2. Поставить другую деталь (риск)");

                    Console.Write("Ваш выбор: ");
                    int choiseorder = Convert.ToInt32(Console.ReadLine());

                    if (choiseorder == 1)
                    {
                        RejectOrder(order, dbOrder);
                    }
                    else if (choiseorder == 2)
                    {
                        Console.WriteLine("\n🔧 Вы решили рискнуть и поставить другую деталь...");

                        var randomPart = Core.Context.Detail.FirstOrDefault();
                        if (randomPart != null)
                        {
                            sklad.TakeAndRemoveDetail(randomPart.Name);
                            double damage = order.CalculateRepairCost() * 2;
                            balance -= damage;
                            dbOrder.Status = "Неудачный ремонт (риск)";
                            Console.WriteLine($"\n💥 Клиент недоволен! Деталь не подошла. Возмещение ущерба: {damage}. \nБаланс: {balance}");
                            Core.Context.SaveChanges();
                            UpdateBalanceInDB();
                        }
                        else
                        {
                            Console.WriteLine("\n⚠️ На складе вообще нет деталей, отказано в заказе.");
                            RejectOrder(order, dbOrder);
                        }
                    }
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
                var part = order.neededPart;

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

        public static void ClearDatabase()
        {
            Console.WriteLine("⚠️ Очистка базы данных...");

            Core.Context.RepairOrders.RemoveRange(Core.Context.RepairOrders);
            Core.Context.Client.RemoveRange(Core.Context.Client);
            Core.Context.Car.RemoveRange(Core.Context.Car);
            Core.Context.Detail.RemoveRange(Core.Context.Detail);
            Core.Context.Sklad.RemoveRange(Core.Context.Sklad);
            Core.Context.Autoservice.RemoveRange(Core.Context.Autoservice);

            Core.Context.SaveChanges();

            Console.WriteLine("✅ Все данные из базы успешно удалены!");
        }

        static void Main(string[] args)
        {
            //ClearDatabase();

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Skladd mySklad = new Skladd(1, "Склад №1", new List<Details>());
            AutoService service = new AutoService(1, "Автосервис PR7", 100000, mySklad);

            mySklad.AddDetail("Двигатель", 5000, 1);
            mySklad.AddDetail("Колесо", 2000, 1);
            mySklad.AddDetail("Тормоза", 1000, 1);

            Console.WriteLine("🚗 Добро пожаловать в «Автосервис PR7»!");
            Console.WriteLine("У тебя есть 100000 монет и склад с набором деталей (двигатель, колесо, тормоза)");
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

                            Detail dbDetail = Core.Context.Detail.FirstOrDefault(d => d.Name == problem);
                            double price = 0;
                            int quantity = 1;

                            if (dbDetail != null)
                            {
                                price = dbDetail.Price;
                                quantity = dbDetail.QuantityOnSklad;
                            }
                            else
                            {
                                Console.WriteLine($"\n⚠️ Деталь '{problem}' не найдена в базе. Цена установлена по умолчанию: 3000");
                                price = 3000;
                            }

                            Details needed = new Details(problem, price, quantity);

                            RepairOrder order = new RepairOrder(1, client, needed, 0, "В ожидании");

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