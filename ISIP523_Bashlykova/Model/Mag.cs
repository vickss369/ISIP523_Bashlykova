using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP523_Bashlykova.Model
{
    internal class Mag : Enemy
    {
        public double chanceFreeze;

        public Mag(string name, double hp, double attack, double protect, double chanceFreeze)
            : base(name, hp, attack, protect)
        {
            this.chanceFreeze = chanceFreeze;
        }

        public bool FreezePlayer()
        {
            return Randoms.Freeze(chanceFreeze);
        }
    }
}
