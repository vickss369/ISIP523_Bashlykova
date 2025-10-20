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

                weaponName = "Кулаки";
                playerAttack = 20;
                armorName = "Одежда";
                playerProtect = 10;
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

            public override double DamageToPlayer(Player player, bool protection)
            {
                double damage = enemyAttack;

                if (protection)
                {
                    int evadeChance = random.Next(100);
                    if (evadeChance < 40)
                    {
                        Console.WriteLine("\nВы успешно уклонились от атаки!");
                        return 0;
                    }
                    Console.WriteLine("\nСкелет проигнорировал вашу защиту!");
                }

                return damage;
            }
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

            public bool FreezePlayer()
            {
                return random.Next(100) < chanceFreeze;
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

        static void OpenSyndyk(Player player)
        {
            Syndyk item = (Syndyk)random.Next(1, 6);
            Console.WriteLine($"\nВы нашли сундук! Предмет: {item}");

            switch (item)
            {
                case Syndyk.Лечебное_зелье:
                    player.playerHP = player.maxHP;
                    Console.WriteLine("Вы полностью восстановили здоровье!");
                    break;

                case Syndyk.Деревянный_меч:
                    TakeWeapon(player, "Деревянный меч", 20);
                    break;

                case Syndyk.Металлический_меч:
                    TakeWeapon(player, "Металлический меч", 30);
                    break;

                case Syndyk.Деревянные_доспехи:
                    TakeProtection(player, "Деревянные доспехи", 20);
                    break;

                case Syndyk.Железные_доспехи:
                    TakeProtection(player, "Железные доспехи", 30);
                    break;
            }
        }

        static void TakeWeapon(Player player, string newWeaponName, double newWeaponAttack)
        {
            Console.WriteLine($"\nВаше текущее оружие: {player.weaponName} (+{player.playerAttack} атаки)");
            Console.WriteLine($"Новое оружие: {newWeaponName} (+{newWeaponAttack} атаки)");
            Console.Write("\nВзять новое оружие? (y/n): ");
            string input = Console.ReadLine();
            if (input.ToLower() == "y")
            {
                player.weaponName = newWeaponName;
                player.playerAttack = newWeaponAttack;
                Console.WriteLine($"Вы экипировали {newWeaponName}.");
            }
            else
            {
                Console.WriteLine("Вы выбросили предмет.");
            }
        }

        static void TakeProtection(Player player, string newArmorName, double newArmorProtect)
        {
            Console.WriteLine($"\nВаша текущая броня: {player.armorName} (+{player.playerProtect} защиты)");
            Console.WriteLine($"Новая броня: {newArmorName} (+{newArmorProtect} защиты)");
            Console.Write("\nВзять новую броню? (y/n): ");
            string input = Console.ReadLine();
            if (input.ToLower() == "y")
            {
                player.armorName = newArmorName;
                player.playerProtect = newArmorProtect;
                Console.WriteLine($"Вы экипировали {newArmorName}.");
            }
            else
            {
                Console.WriteLine("Вы выбросили предмет.");
            }
        }

        static void Battle(Player player, Enemy enemy)
        {
            Console.WriteLine($"Вы столкнулись с врагом: {enemy.enemyName}");

            bool playerFrozen = false;
            while (player.playerHP > 0 && enemy.enemyHP > 0)
            {
                bool protection = false;
                if (!playerFrozen)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"Ваш HP: {player.playerHP}");
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"HP врага: {enemy.enemyHP}");
                    Console.ResetColor();

                    Console.WriteLine("\n1 — Атака\n2 — Защита");
                    string choice = Console.ReadLine();

                    if (choice == "1")
                    {
                        double yron = player.playerAttack - enemy.enemyProtect;
                        if (yron < 1) yron = 5;
                        enemy.enemyHP -= yron;
                        Console.WriteLine($"\nВы нанесли {yron} урона врагу!");
                    }
                    else if (choice == "2")
                    {
                        protection = true;
                    }
                }
                else
                {
                    playerFrozen = false;
                }

                if (enemy.enemyHP <= 0) break;

                double enemyDmg = enemy.DamageToPlayer(player, protection);
                player.playerHP -= enemyDmg;
                Console.WriteLine($"{enemy.enemyName} нанёс вам {enemyDmg} урона!");

                if (enemy is Mag magEnemy && magEnemy.FreezePlayer())
                {
                    Console.WriteLine("\nВы заморожены магией врага!\nВы не можете ходить и пропускаете свой ход.");
                    playerFrozen = true;
                }
                else if (enemy is Pestov pestovEnemy && pestovEnemy.FreezePlayer())
                {
                    Console.WriteLine("\nПестов использовал свою особую способность!\nВы заморожены и пропускаете следующий ход.");
                    playerFrozen = true;
                }

                if (player.playerHP <= 0)
                {
                    Console.WriteLine("\nВы погибли...(");
                    Environment.Exit(0);
                }
            }

            Console.WriteLine($"\nВы победили врага {enemy.enemyName}!\n");
        }

        static void Main(string[] args)
        {
            Player player = new Player(100, 20, 10);
            int turn = 0;

            while (true)
            {
                turn++;
                Console.WriteLine($"\n~~~ Ход {turn} ~~~");

                if (turn % 10 == 0)
                {
                    Enemy boss = GenerateBoss();
                    Console.WriteLine("ВНИМАНИЕ!!! БОСС!!!!!");
                    Battle(player, boss);
                }
                else
                {
                    if (random.Next(101) < 50)
                    {
                        Enemy enemy = GenerateEnemy();
                        Battle(player, enemy);
                    }
                    else
                    {
                        OpenSyndyk(player);
                    }
                }
            }
        }
    }
}
