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

            /*public bool IsAvailable()
            {
               
            }*/
        }

        static void Main(string[] args)
        {

        }
    }
}
