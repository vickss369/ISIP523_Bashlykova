using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISIP523_Bashlykova.FabricMethod;

namespace ISIP523_Bashlykova.Model
{
    internal class Game
    {
        enum Syndyk
        {
            Лечебное_зелье = 1,
            Деревянный_меч,
            Металлический_меч,
            Деревянные_доспехи,
            Железные_доспехи
        }

        static void OpenSyndyk(Player player)
        {
            Syndyk item = (Syndyk)Randoms.GetRandomChoice(1, 6);
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

        public static void Battle(Player player, Enemy enemy)
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
        public void playGame()
        {
            Player player = new Player(100, 20, 10);
            int turn = 0;

            while (true)
            {
                turn++;
                Console.WriteLine($"\n~~~ Ход {turn} ~~~");

                if (turn % 10 == 0)
                {
                    Enemy boss = Factory.GenerateBoss();
                    Console.WriteLine("ВНИМАНИЕ!!! БОСС!!!!!");
                    Battle(player, boss);
                }
                else
                {
                    if (Randoms.GetRandomChoice(1, 101) < 50)
                    {
                        Enemy enemy = Factory.GenerateEnemy();
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
