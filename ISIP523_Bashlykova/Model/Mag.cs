using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP523_Bashlykova.Model
{
    internal class Mag : Enemy
    {
        public double chanseMoroz;

        public Mag()
            : base("Маг", 30, 25, 20)
        {
            chanseMoroz = 30;
        }

        public Mag(string name, double hp, double attack, double protect)
            : base(name, hp, attack, protect)
        {
            chanseMoroz = 33;
        }

        public bool FreezePlayer()
        {
            return Randoms.GetRandomChoice(1, 100) < chanseMoroz;
        }
    }
}
