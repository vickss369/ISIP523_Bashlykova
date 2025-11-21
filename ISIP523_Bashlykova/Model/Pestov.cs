using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP523_Bashlykova.Model
{
    internal class Pestov : Skelet
    {
        public double chanceFreeze;

        public Pestov()
            : base()
        {
            this.enemyName = "Пестов (ААААА)";
            this.enemyHP = 25 * 1.3;
            this.enemyAttack = 20 * 1.8;
            this.enemyProtect = 15 * 0.6;
            this.chanceFreeze = 34.5;
        }

        public bool FreezePlayer()
        {
            return Randoms.GetRandomChoice(1, 100) < chanceFreeze;
        }
    }
}
