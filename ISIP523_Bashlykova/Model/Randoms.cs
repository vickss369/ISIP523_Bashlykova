using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP523_Bashlykova.Model
{
    internal class Randoms
    {
        private static readonly Random rand = new Random();
        public static int GetRandomChoice(int start, int end) => rand.Next(start, end);

        private static bool Choice(double chance)
        {
            return rand.NextDouble() < chance ;
        }

        public static bool FreezeChance()
        {
            return Choice(0.2);
        }
    }
}
