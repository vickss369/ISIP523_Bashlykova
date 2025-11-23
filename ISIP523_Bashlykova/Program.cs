using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using ISIP523_Bashlykova.Model;

namespace ISIP523_Bashlykova
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Game game1 = new Game();
            game1.playGame();
        }
    }
}
