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

        public static bool Evade(int percentChance = 40) //уклон от следующей атаки врага
        {
            return GetRandomChoice(1, 101) <= percentChance;
        }

        public static int BlockPercent(int min = 70, int max = 100) //процент уменьшения получаемого урона на 70–100% от характеристики защиты 
        {
            return GetRandomChoice(min, max + 1);
        }

        public static bool Critical(double chancePercent) //шанс критического урона
        {
            return GetRandomChoice(1, 101) <= chancePercent;
        }

        public static bool Freeze(double chancePercent) //шанс заморозки
        {
            return GetRandomChoice(1, 101) <= chancePercent;
        }
    }
}
