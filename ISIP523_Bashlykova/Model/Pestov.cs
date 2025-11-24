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
            this.enemyHP = Randoms.GetRandomChoice(30, 41) * 1.3;
            this.enemyAttack = Randoms.GetRandomChoice(15, 26) * 1.8;
            this.enemyProtect = Randoms.GetRandomChoice(10, 16) * 0.6;
            this.chanceFreeze = Randoms.GetRandomChoice(25, 36) + (Randoms.GetRandomChoice(25, 36)/100*10);
        }

        public bool FreezePlayer()
        {
            return Randoms.Freeze();
        }
    }
}
