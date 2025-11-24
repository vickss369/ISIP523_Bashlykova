using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP523_Bashlykova.Model
{
    internal class Goblin : Enemy
    {
        public double chanceKritYron;

        public Goblin(string name, double hp, double attack, double protect, double chanceKritYron)
            : base(name, hp, attack, protect)
        {
            this.chanceKritYron = chanceKritYron;
        }

        public override double DamageToPlayer(Player player)
        {
            double yron = enemyAttack - player.playerProtect;
            if (Randoms.Critical())
            {
                yron *= 1.5;
                Console.WriteLine("\nГоблин нанёс критический удар!");
            }

            if (yron < 0) yron = 0;
           
            return yron;
        }
    }
}

