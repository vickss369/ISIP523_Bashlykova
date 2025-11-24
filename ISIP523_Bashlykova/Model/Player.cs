using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP523_Bashlykova.Model
{
    internal class Player
    {
        public double playerHP;
        public double maxHP;
        public double playerAttack;
        public double playerProtect;
        public bool isFrozen;

        public string weaponName;
        public string protectionName;

        public Player(double hp, double attack, double protect)
        {
            maxHP = hp;
            playerHP = hp;

            weaponName = "Кулаки";
            playerAttack = 15;
            protectionName = "Одежда";
            playerProtect = 10;
            isFrozen = false;
        }

        public void TryEvade()
        {
            if (Randoms.Evade()) // шанс полностью уклониться
            {
                Console.WriteLine("\nВы успешно уклонились от атаки!");
            }
            else
            {
                int blockPercent = Randoms.BlockPercent(); // 70–100%
                double blockValue = playerProtect * (blockPercent / 100.0);
                Console.WriteLine($"\nВы не уклонились, но заблокировали {blockPercent}% ({blockValue} ед.) урона!");
            }
        }

        public double DamageToEnemy(Enemy enemy)
        {
            if (isFrozen)
            {
                Console.WriteLine("\nВы заморожены и не можете атаковать!");
                return 0;
            }

            double damage = playerAttack - enemy.enemyProtect;
            if (damage < 0) damage = 0;

            if (enemy is Slime)
            {
                damage -= 2;
                if (damage < 0) damage = 0;
            }

            return damage;
        }

        public void TakeDamage(double damage)
        {
            playerHP -= damage;
            if (playerHP < 0) playerHP = 0;
        }
    }
}
