using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ISIP523_Bashlykova
{
    internal class Program
    {
        static Random random = new Random();

        enum Syndyk
        {
            Лечебное_зелье = 1,
            Деревянный_меч,
            Металлический_меч,
            Деревянные_доспехи,
            Железные_доспехи
        }

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
            public string enemyName;
            public double enemyHP;
            public double enemyAttack;
            public double enemyProtect;

            public Enemy(string enemyName, double enemyHP, double enemyAttack, double enemyProtect)
            {
                this.enemyName = enemyName;
                this.enemyHP = enemyHP;
                this.enemyAttack = enemyAttack;
                this.enemyProtect = enemyProtect;
            }

            public virtual double DamageToPlayer(Player player, bool protection)
            {
                double yron = enemyAttack - player.playerProtect;
                if (yron < 0) yron = 0;
                return yron;
            }
        }

        class Goblin : Enemy
        {
            public double chanceYron;

            public Goblin()
                : base("Гоблин", 20, 15, 10)
            {
                this.chanceYron = 20;
            }

            public Goblin(string name, double hp, double attack, double protect) // конструктор для босса
                : base(name, hp, attack, protect)
            {
                this.chanceYron = 22; // + 10% к значению обычного Гоблина
            }

            public override double DamageToPlayer(Player player, bool protection)
            {
                double yron = enemyAttack - player.playerProtect;
                if (random.Next(100) < chanceYron)
                {
                    yron *= 1.5;
                    Console.WriteLine("Гоблин нанёс критический удар!");
                }

                if (yron < 0) yron = 0;

                return yron;
            }
        }

        class Skelet : Enemy
        {
            public Skelet()
                : base("Скелет", 25, 20, 15) { }

            public Skelet(string name, double hp, double attack, double protect) // конструктор для босса
                : base(name, hp, attack, protect) { }

            public override double DamageToPlayer(Player player, bool protection)
            {
                double yron = enemyAttack;
                if (yron < 0) yron = 0;

                return yron;
            }
        }

        class Mag : Enemy
        {
            public double chanseMoroz;

            public Mag()
                : base("Маг", 30, 25, 20)
            {
                this.chanseMoroz = 30;
            }

            public Mag(string name, double hp, double attack, double protect) // конструктор для босса
                : base(name, hp, attack, protect)
            {
                this.chanseMoroz = 33; // + 10% к значению обычного Мага
            }

            public bool FreezePlayer()
            {
                return random.Next(100) < chanseMoroz;
            }
        }

        class Pestov : Skelet
        {
            public double chanseMoroz;

            public Pestov()
                : base()
            {
                this.enemyName = "Пестов (ААААА)";
                this.enemyHP = 25 * 1.3;
                this.enemyAttack = 20 * 1.8;
                this.enemyProtect = 15 * 0.6;
                this.chanseMoroz = 34.5; // + 15% к значению обычного Мага
            }
        }

        static Enemy GenerateEnemy()
        {
            int enemy = random.Next(3);
            switch (enemy)
            {
                case 0: return new Goblin();
                case 1: return new Skelet();
                case 2: return new Mag();
                default: return new Goblin();
            }
        }

        static Enemy GenerateBoss()
        {
            int boss = random.Next(4);
            switch (boss)
            {
                case 0: return new Goblin("ВВГ (босс гоблинов)", 20 * 2, 15 * 1.5, 10 * 1.2);
                case 1: return new Skelet("Ковальский (босс скелетов)", 25 * 2.5, 20 * 1.3, 15 * 1.4);
                case 2: return new Mag("Архимаг С++ (босс магов)", 30 * 1.8, 25 * 1.6, 15 * 1.1);
                case 3: return new Pestov();
                default: return new Pestov();
            }
        }


        static void Main(string[] args)
        {
        }
    }
}
