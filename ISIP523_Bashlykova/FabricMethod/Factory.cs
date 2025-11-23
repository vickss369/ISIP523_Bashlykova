using ISIP523_Bashlykova.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ISIP523_Bashlykova.FabricMethod
{
    internal class Factory
    {
        private static Goblin CreateRandomGoblin()
        {
            double hp = Randoms.GetRandomChoice(20, 31);     
            double attack = Randoms.GetRandomChoice(10, 21); 
            double protect = Randoms.GetRandomChoice(5, 11);
            double chanceKritYron = Randoms.GetRandomChoice(15, 26);

            return new Goblin("Гоблин", hp, attack, protect, chanceKritYron);
        }

        private static Skelet CreateRandomSkelet()
        {
            double hp = Randoms.GetRandomChoice(30, 41);      
            double attack = Randoms.GetRandomChoice(15, 26); 
            double protect = Randoms.GetRandomChoice(10, 16); 

            return new Skelet("Скелет", hp, attack, protect);
        }

        private static Mag CreateRandomMag()
        {
            double hp = Randoms.GetRandomChoice(40, 46);      
            double attack = Randoms.GetRandomChoice(20, 31);  
            double protect = Randoms.GetRandomChoice(10, 21);
            double chanceMoroz = Randoms.GetRandomChoice(25, 36);

            return new Mag("Маг", hp, attack, protect, chanceMoroz);
        }

        private static Slime CreateRandomSlime()
        {
            double hp = Randoms.GetRandomChoice(10, 21);  
            double attack = Randoms.GetRandomChoice(5, 16);  
            double protect = Randoms.GetRandomChoice(3, 11); 

            return new Slime("Слизень", hp, attack, protect);
        }

        public static Enemy GenerateEnemy()
        {
            int numEnemy = Randoms.GetRandomChoice(0, 4);
            switch (numEnemy)
            {
                case 0: return CreateRandomGoblin();
                case 1: return CreateRandomSkelet();
                case 2: return CreateRandomMag();
                case 3: return CreateRandomSlime();
                default: return CreateRandomGoblin();
            }
        }

        public static Enemy GenerateBoss()
        {
            int numBoss = Randoms.GetRandomChoice(1, 4);
            switch (numBoss)
            {
                case 0: return new Goblin("ВВГ (босс гоблинов)", 40, 22.5, 12, Randoms.GetRandomChoice(15, 26));
                case 1: return new Skelet("Ковальский (босс скелетов)", 62.5, 26, 21);
                case 2: return new Mag("Архимаг С++ (босс магов)", 54, 40, 16.5, Randoms.GetRandomChoice(25, 36));
                case 3: return new Pestov();
                default: return new Pestov();
            }
        }
    }
}
