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

            bool protection = false;

            while (player.playerHP > 0 && enemy.enemyHP > 0)
            {
                if (!player.isFrozen)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"Ваш HP: {player.playerHP}, сила атаки: {player.playerAttack}, защита: {player.protectionName} ({player.playerProtect})");
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"HP врага: {enemy.enemyHP}, сила атаки: {enemy.enemyAttack}, защита: {enemy.enemyProtect}");
                    Console.ResetColor();

                    Console.WriteLine("\n1 — Атака\n2 — Защита");
                    string choice = Console.ReadLine();

                    if (choice == "1")
                    {
                        double dealt = player.DamageToEnemy(enemy);
                        Console.WriteLine($"\nВы нанесли {dealt} ед. урона врагу!");
                        protection = false;
                    }
                    else if (choice == "2")
                    {
                        protection = true;
                        player.TryEvade();
                    }
                }
                else
                {
                    Console.WriteLine("\nВы заморожены и пропускаете ход!");
                    player.isFrozen = false;
                }

                if (enemy.enemyHP <= 0) break;

                double enemyDmg = enemy.DamageToPlayer(player);
                player.playerHP -= enemyDmg;
                Console.WriteLine($"{enemy.enemyName} нанёс вам {enemyDmg} ед. урона!");

                if (enemy is Mag magEnemy && magEnemy.FreezePlayer())
                {
                    Console.WriteLine("\nВы заморожены магией врага! Пропускаете следующий ход.");
                    player.isFrozen = true;
                }
                else if (enemy is Pestov pestovEnemy && pestovEnemy.FreezePlayer())
                {
                    Console.WriteLine("\nПестов использовал свою особую способность! Пропускаете следующий ход.");
                    player.isFrozen = true;
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
