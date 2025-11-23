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

        public override double DamageToPlayer(Player player, bool protection)
        {
            double yron = enemyAttack - player.playerProtect;
            if (Randoms.Critical(chanceKritYron))
            {
                yron *= 1.5;
                Console.WriteLine("\nГоблин нанёс критический удар!");
            }

            if (yron < 0) yron = 0;

            if (protection)
            {
                if (Randoms.Evade())
                {
                    Console.WriteLine("\nВы успешно уклонились от атаки!");
                    return 0;
                }
                else
                {
                    int blockPercent = Randoms.BlockPercent();
                    double blockValue = player.playerProtect * (blockPercent / 100.0);
                    yron -= blockValue;
                    Console.WriteLine($"\nВы не уклонились, но заблокировали {blockPercent}% ({blockValue} ед.) урона!");
                    if (yron < 0) yron = 0;
                }
            }

            return yron;
        }
    }
}
