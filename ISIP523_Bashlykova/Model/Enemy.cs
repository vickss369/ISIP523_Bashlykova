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

        public virtual double DamageToPlayer(Player player)
        {
            if (player.isFrozen)
            {
                Console.WriteLine("Вы заморожены и не можете действовать!");
                return 0;
            }

            double damage = enemyAttack - player.playerProtect;
            if (damage < 0) damage = 0;

            return damage;
        }
        public virtual void TakeDamage(double rawDamage)
        {
            double damage = rawDamage - enemyProtect;
            if (damage < 0) damage = 0;
            enemyHP -= damage;
        }
    }
}
