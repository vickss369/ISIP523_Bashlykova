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
        static void OpenSyndyk(Player player)
        {
            Syndyk item = Syndyk.GetRandomThing();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Вы нашли сундук!");
            Console.ResetColor();
            Console.WriteLine($"Предмет: {item.name}");
            item.Lyt(player);
        }

        public static bool Battle(Player player, Enemy enemy)
        {
            Console.WriteLine($"Вы столкнулись с врагом: {enemy.enemyName}");

            bool playerFrozen = false;

            while (player.playerHP > 0 && enemy.enemyHP > 0)
            {
                bool protection = false;

                if (!playerFrozen)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"Ваш HP: {player.playerHP}, ваша сила атаки: {player.playerAttack}, ваша защита: {player.protectionName} ({player.playerProtect} защиты)");
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"HP врага: {enemy.enemyHP}, сила атаки врага: {enemy.enemyAttack}, защита врага: {enemy.enemyProtect}");
                    Console.ResetColor();

                    Console.WriteLine("\n1 — Атака\n2 — Защита");
                    string choice = Console.ReadLine();

                    if (choice == "1")
                    {
                        double dealt = enemy.TakeDamage(player.playerAttack);
                        Console.WriteLine($"\nВы нанесли {dealt} ед. урона врагу!");
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
                Console.WriteLine($"{enemy.enemyName} нанёс вам {enemyDmg} ед. урона!");

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
                    return false;
                }
            }

            Console.WriteLine($"\nВы победили врага {enemy.enemyName}!\n");
            return true;
        }

        public void playGame()
        {
            Player player = new Player(100, 10, 10);
            int turn = 0;
            bool isPlaying = true;

            while (isPlaying)
            {
                turn++;
                Console.WriteLine($"\n~~~ Ход {turn} ~~~");

                Enemy enemy = null;

                if (turn % 10 == 0)
                {
                    enemy = Factory.GenerateBoss();
                    Console.WriteLine("ВНИМАНИЕ!!! БОСС!!!!!");
                }
                else
                {
                    if (Randoms.Chance(0.5))
                    {
                        enemy = Factory.GenerateEnemy();
                    }
                    else
                    {
                        OpenSyndyk(player);
                        continue;
                    }
                }

                bool survived = Battle(player, enemy);
                if (!survived)
                {
                    isPlaying = false;
                }
            }
            Console.WriteLine("Игра окончена. Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}
