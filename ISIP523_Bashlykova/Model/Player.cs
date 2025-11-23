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
        public string protectionName;

        public Player(double hp, double attack, double protect)
        {
            playerHP = hp;
            maxHP = hp;

            weaponName = "Кулаки";
            playerAttack = 15;
            protectionName = "Одежда";
            playerProtect = 10;
        }
    }
}
