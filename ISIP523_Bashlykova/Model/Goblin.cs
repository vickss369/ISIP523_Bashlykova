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

        public Goblin()
            : base("Гоблин", 20, 15, 10)
        {
            chanceKritYron = 20;
        }

        public Goblin(string name, double hp, double attack, double protect)
            : base(name, hp, attack, protect)
        {
            chanceKritYron = 22;
        }

        public override double DamageToPlayer(Player player, bool protection)
        {
            double yron = enemyAttack - player.playerProtect;
            if (Randoms.GetRandomChoice(1, 100) < chanceKritYron)
            {
                yron *= 1.5;
                Console.WriteLine("\nГоблин нанёс критический удар!");
            }

            if (yron < 0) yron = 0;

            if (protection)
            {
                int chanseToEvede = Randoms.GetRandomChoice(1, 100);
                if (chanseToEvede < 40)
                {
                    Console.WriteLine("\nВы успешно уклонились от атаки!");
                    return 0;
                }
                else
                {
                    int blockPercent = Randoms.GetRandomChoice(70, 101);
                    double blockValue = player.playerProtect * (blockPercent / 100.0);
                    yron -= blockValue;
                    Console.WriteLine($"\nВы не уклонились, но заблокировали {blockPercent}% ({blockValue}) урона!");
                    if (yron < 0) yron = 0;
                }
            }
            return yron;
        }
    }
}
