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

                weaponName = "Деревянный меч";
                playerAttack = 20;
                armorName = "Тканевая броня";
                playerProtect = 5;
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
                double damage = enemyAttack - player.playerProtect;
                if (damage < 0) damage = 0;

                if (protection)
                {
                    int evadeChance = random.Next(100);
                    if (evadeChance < 40)
                    {
                        Console.WriteLine("\nВы успешно уклонились от атаки!");
                        return 0;
                    }
                    else
                    {
                        int blockPercent = random.Next(70, 101);
                        double blockValue = player.playerProtect * (blockPercent / 100.0);
                        damage -= blockValue;
                        Console.WriteLine($"\nВы не уклонились, но заблокировали {blockPercent}% ({blockValue}) урона!");
                        if (damage < 0) damage = 0;
                    }
                }

                return damage;
            }
        }

        class Goblin : Enemy
        {
            public double chanceKritYron;

            public Goblin()
                : base("Гоблин", 20, 15, 10)
            {
                chanceKritYron = 20;
            }

            public Goblin(string name, double hp, double attack, double protect)
                : base(name, hp, attack, protect)
            {
                chanceKritYron = 22;
            }

            public override double DamageToPlayer(Player player, bool protection)
            {
                double yron = enemyAttack - player.playerProtect;
                if (random.Next(100) < chanceKritYron)
                {
                    yron *= 1.5;
                    Console.WriteLine("\nГоблин нанёс критический удар!");
                }

                if (yron < 0) yron = 0;

                if (protection)
                {
                    int chanseToEvede = random.Next(100);
                    if (chanseToEvede < 40)
                    {
                        Console.WriteLine("\nВы успешно уклонились от атаки!");
                        return 0;
                    }
                    else
                    {
                        int blockPercent = random.Next(70, 101);
                        double blockValue = player.playerProtect * (blockPercent / 100.0);
                        yron -= blockValue;
                        Console.WriteLine($"\nВы не уклонились, но заблокировали {blockPercent}% ({blockValue}) урона!");
                        if (yron < 0) yron = 0;
                    }
                }
                return yron;
            }
        }

        class Skelet : Enemy
        {
            public Skelet()
                : base("Скелет", 25, 20, 15) { }

            public Skelet(string name, double hp, double attack, double protect)
                : base(name, hp, attack, protect) { }
        }

        class Mag : Enemy
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
                return random.Next(100) < chanseMoroz;
            }
        }

        class Pestov : Skelet
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
        }

        static Enemy GenerateEnemy()
        {
            int numEnemy = random.Next(3);
            switch (numEnemy)
            {
                case 0: return new Goblin();
                case 1: return new Skelet();
                case 2: return new Mag();
                default: return new Goblin();
            }
        }

        static Enemy GenerateBoss()
        {
            int numBoss = random.Next(4);
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
