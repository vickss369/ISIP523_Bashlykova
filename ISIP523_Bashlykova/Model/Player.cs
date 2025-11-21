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
        public string weaponName;
        public string armorName;

        public Player(double hp, double attack, double protect)
        {
            playerHP = hp;
            maxHP = hp;
            playerAttack = attack;
            playerProtect = protect;

            weaponName = "Кулаки";
            playerAttack = 20;
            armorName = "Одежда";
            playerProtect = 10;
        }
    }
}
