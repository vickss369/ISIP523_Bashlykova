using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP523_Bashlykova.Model
{
    internal class Slime : Enemy
    {
        public Slime(string name, double hp, double attack, double protect)
            : base(name, hp, attack, protect) { }

        public override double TakeDamage(double rawDamage)
        {
            rawDamage -= 2;
            if (rawDamage < 0) rawDamage = 0;

            double damage = rawDamage - enemyProtect;
            if (damage < 0) damage = 0;

            enemyHP -= damage;
            return damage;
        }
    }
}
