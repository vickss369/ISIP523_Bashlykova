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

        public static bool Chance(double chance)
        {
            return rand.NextDouble() < chance;
        }

        public static bool Evade() //уклон от следующей атаки врага
        {
            return Chance(0.4);
        }

        public static int BlockPercent(int min = 70, int max = 100) //процент уменьшения получаемого урона на 70–100% от характеристики защиты 
        {
            return GetRandomChoice(min, max + 1);
        }

        public static bool Critical() //шанс критического урона
        {
            return Chance(0.3);
        }

        public static bool Freeze() //шанс заморозки
        {
            return Chance(0.3);
        }
    }
}
