using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP523_Bashlykova
{
    internal class Program
    {
        static Random random = new Random();

        class Player
        {
            public double playerHP;
            public double playerAttack;
            public double playerProtect;

            public Player(double playerHP, double playerAttack, double playerProtect)
            {
                this.playerHP = playerHP;
                this.playerAttack = playerAttack;
                this.playerProtect = playerProtect;
            }
        }

        class Enemy
        {
            public double enemyHP;
            public double enemyAttack;
            public double enemyProtect;

            public Enemy(double enemyHP, double enemyAttack, double enemyProtect)
            {
                this.enemyHP = enemyHP;
                this.enemyAttack = enemyAttack;
                this.enemyProtect = enemyProtect;
            }
        }

        class Goblin : Enemy
        {
            public double chanceYron;

            public Goblin(double enemyHP, double enemyAttack, double enemyProtect, double chanceYron) 
                : base(enemyHP, enemyAttack, enemyProtect)
            {
                this.chanceYron = chanceYron;
            }
        }

        class Skelet : Enemy
        {
            public Skelet(double enemyHP, double enemyAttack, double enemyProtect)
                : base(enemyHP, enemyAttack, enemyProtect) { }
        }

        class Mag : Enemy
        {
            public double chanseMoroz;

            public Mag(double enemyHP, double enemyAttack, double enemyProtect, double chanseMoroz)
                : base(enemyHP, enemyAttack, enemyProtect)
            {
                this.chanseMoroz = chanseMoroz;
            }
        }

        class Pestov : Skelet
        {
            public double chanseMoroz;

            public Pestov(double enemyHP, double enemyAttack, double enemyProtect, double chanseMoroz) 
                : base(enemyHP, enemyAttack, enemyProtect)
            {
                this.chanseMoroz = chanseMoroz;
            }
        }

        static void Main(string[] args)
        {
        }
    }
}
