using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP523_Bashlykova.Model
{
    internal class Enemy
    {
        public string enemyName;
        public double enemyHP;
        public double enemyAttack;
        public double enemyProtect;

        public Enemy(string enemyName, double enemyHP, double enemyAttack, double enemyProtect)
        {
            this.enemyName = enemyName;
            this.enemyHP = enemyHP;
            this.enemyAttack = enemyAttack;
            this.enemyProtect = enemyProtect;
        }

        public virtual double DamageToPlayer(Player player, bool protection)
        {
            double damage = enemyAttack - player.playerProtect;
            if (damage < 0) damage = 0;

            if (protection)
            {
                int evadeChance = Randoms.GetRandomChoice(1, 100);
                if (evadeChance < 40)
                {
                    Console.WriteLine("\nВы успешно уклонились от атаки!");
                    return 0;
                }
                else
                {
                    int blockPercent = Randoms.GetRandomChoice(70, 101);
                    double blockValue = player.playerProtect * (blockPercent / 100.0);
                    damage -= blockValue;
                    Console.WriteLine($"\nВы не уклонились, но заблокировали {blockPercent}% ({blockValue}) урона!");
                    if (damage < 0) damage = 0;
                }
            }

            return damage;
        }
    }
}
