using ISIP523_Bashlykova.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP523_Bashlykova.FabricMethod
{
    internal class Factory
    {
        public static Enemy GenerateEnemy()
        {
            int numEnemy = Randoms.GetRandomChoice(1, 3);
            switch (numEnemy)
            {
                case 0: return new Goblin();
                case 1: return new Skelet();
                case 2: return new Mag();
                default: return new Goblin();
            }
        }

        public static Enemy GenerateBoss()
        {
            int numBoss = Randoms.GetRandomChoice(1, 4);
            switch (numBoss)
            {
                case 0: return new Goblin("ВВГ (босс гоблинов)", 40, 22.5, 12);
                case 1: return new Skelet("Ковальский (босс скелетов)", 62.5, 26, 21);
                case 2: return new Mag("Архимаг С++ (босс магов)", 54, 40, 16.5);
                case 3: return new Pestov();
                default: return new Pestov();
            }
        }
    }
}
