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
        }

        class Client
        {
            public int id;
            public string name;
            public Car car;
        }

        class Detail
        {
            public int id;
            public string name;
            public double price;
            public int quantity;

            /*public bool IsOnSklad()
            {
            }*/
        }

        class Sklad
        {
            public int id;
            public List<Detail> allDetails = new List<Detail>();

            /*public bool CheckDetailOnSklad(string partName)
            {
            }*/

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

        }
    }
}
