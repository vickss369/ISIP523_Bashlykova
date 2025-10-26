using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP523_Bashlykova
{
    internal class Program
    {
        class Car
        {
            public int id;
            public string mark;
            public string problem;

            public Car(int id, string mark, string problem)
            {
                this.id = id;      
                this.mark = mark;
                this.problem = problem;
            }
        }

        class Client
        {
            public int id;
            public string fio;
            public Car car;

            public Client(int id, string fio, Car car)
            {
                this.id = id;
                this.fio = fio;
                this.car = car;
            }
        }

        class Detail
        {
            public int id;
            public string name;
            public double price;
            public int quantity;

            public Detail(int id, string name, double price, int quantity)
            {
                this.id = id;
                this.name = name;
                this.price = price;
                this.quantity = quantity;
            }

            /*public bool IsOnSklad()
            {
                Console.Write("Введите название необходимой детали для проверки наличия на складе: ");
                string nameNeededDetail = Console.ReadLine();
                var detail = Core.Context.Detail.FirstOrDefault(d => d.Name == nameNeededDetail);
                return detail != null && detail.QuantityOnSklad > 0;
            }*/
        }

        class Sklad
        {
            public int id;
            public List<Detail> allDetails = new List<Detail>();

            public Sklad(int id, List<Detail> allDetails)
            {
                this.id = id;
                this.allDetails = allDetails;
            }

            public bool CheckDetailOnSklad(string partName)
            {
                Console.Write("Введите название необходимой детали для проверки наличия на складе: ");
                string nameNeededDetail = Console.ReadLine();
                var detail = Core.Context.Detail.FirstOrDefault(d => d.Name == nameNeededDetail);
                return detail != null && detail.QuantityOnSklad > 0;
            }

            /*public void AddDetail(Detail part)
            {
            }*/

            /*public void TakeAndRemoveDetail(string partName, int quantity)
            {
            }*/
        }

        class RepairOrder
        {
            public int id;
            public Client client;
            public List<Detail> neededParts = new List<Detail>();
            public double cost;
            public string status;

            public RepairOrder(int id, Client client, List<Detail> neededParts, double cost, string status)
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
            public Sklad sklad;
            public List<RepairOrder> orders = new List<RepairOrder>();


            /*public void TakeOrder(RepairOrder order)
            {
            }*/

            /*public void RejectOrder(RepairOrder order)
            {
            }*/

            /*public void RepairCar(RepairOrder order)
            {
            }*/

            /*public void BuyDetails(Detail part, int quantity)
            {
            }*/

            /*public void ShowInfo()
            {
            }*/
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8; //для смайликов, надеюсь сработает:(

            AutoService service = new AutoService
            {
                id = 1,
                name = "Автосервис PR7",
                balance = 1000,
                sklad = new Sklad(1, new List<Detail>())
            };

            Console.WriteLine("🚗 Добро пожаловать в «Автосервис PR7»!");
            Console.WriteLine("У тебя есть 1000 монет и склад с деталями");
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
                    Console.WriteLine("2. Купить детали");
                    Console.WriteLine("3. Показать информацию о сервисе");
                    Console.WriteLine("0. Выход");

                    Console.Write("Введите выбор: ");
                    int choice = Convert.ToInt32(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("\n~~~ Новый клиент ~~~");
                            Console.Write("Введите ФИО клиента: ");
                            string fio = Console.ReadLine();
                            Console.Write("Введите марку машины: ");
                            string mark = Console.ReadLine();
                            Console.Write("Введите описание проблемы: ");
                            string problem = Console.ReadLine();

                            Client newClient = new Client(1, fio, new Car(1, mark, problem));

                            break;

                        case 2:
                            break;

                        case 3:
                            Console.WriteLine("\n~~~ Информация о автосервисе: ~~~");
                            //service.ShowInfo();
                            break;

                        case 0: outt = false; break;

                        default: Console.WriteLine("Неправильный пункт меню."); break;
                    }
                }
            }
        }
    }
}



