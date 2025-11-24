using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP523_Bashlykova.Model
{
    internal class Skelet : Enemy
    {
        public Skelet()
               : base("Скелет", 25, 20, 15) { }

        public Skelet(string name, double hp, double attack, double protect)
            : base(name, hp, attack, protect) { }

        public override double DamageToPlayer(Player player)
        {
            double damage = enemyAttack;
            int evadeChance = Randoms.GetRandomChoice(1, 100);
            if (evadeChance < 40)
            {
                Console.WriteLine("\nВы успешно уклонились от атаки!");
                return 0;
            }

            Console.WriteLine("\nСкелет проигнорировал вашу защиту!");
            return damage;
        }
    }
}
