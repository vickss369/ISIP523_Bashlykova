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
                if (Randoms.Evade())
                {
                    Console.WriteLine("\nВы успешно уклонились от атаки!");
                    return 0;
                }
                else
                {
                    int blockPercent = Randoms.BlockPercent();
                    double blockValue = player.playerProtect * (blockPercent / 100.0);
                    damage -= blockValue;
                    Console.WriteLine($"\nВы не уклонились, но заблокировали {blockPercent}% ({blockValue}) урона!");
                    if (damage < 0) damage = 0;
                }
            }

            return damage;
        }

        public virtual double TakeDamage(double rawDamage)
        {
            double damage = rawDamage - enemyProtect;
            if (damage < 0) damage = 0;
            enemyHP -= damage;
            return damage;
        }
    }
}
