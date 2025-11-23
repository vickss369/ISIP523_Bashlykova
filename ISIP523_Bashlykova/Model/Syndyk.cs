using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP523_Bashlykova.Model
{
    internal class Syndyk
    {
        public string name;
        public int health;
        public int attack;
        public int protect;

        public Syndyk(string name, int health, int attack, int protect)
        {
            this.name = name;
            this.health = health;
            this.attack = attack;
            this.protect = protect;
        }

        public static Syndyk GetRandomThing()
        {
            int n = Randoms.GetRandomChoice(1, 6);
            switch(n)
            {
                case 1: return new Syndyk("Лечебное зелье", health: 100, attack: 0, protect:0);
                case 2: return new Syndyk("Деревянный меч", health: 0, attack: 20, protect: 0);
                case 3: return new Syndyk("Металлический меч", health: 0, attack: 30, protect: 0);
                case 4: return new Syndyk("Деревянная броня", health: 0, attack: 0, protect: 20);
                case 5: return new Syndyk("Железная броня", health: 0, attack: 0, protect: 30);
                default: return new Syndyk("Лечебное зелье", health: 100, attack: 0, protect: 0);
            }
        }

        public void Lyt(Player player)
        {
            if (health > 0)
            {
                player.playerHP = player.maxHP;
                Console.WriteLine("Вы полностью восстановили здоровье!");
                return;
            }

            if (attack > 0)
            {
                Console.WriteLine($"\nВаше текущее оружие: {player.weaponName} ({player.playerAttack} атаки)");
                Console.WriteLine($"Новое оружие: {name} ({attack} атаки)");
                Console.Write("\nВзять новое оружие? (yes/no да/нет): ");
                string input = Console.ReadLine();
                if (input.ToLower() == "yes" || input.ToLower() == "да")
                {
                    player.weaponName = name;
                    player.playerAttack = attack;
                    Console.WriteLine($"Вы экипировали {name}.");
                }
                else
                {
                    Console.WriteLine("Вы выбросили предмет.");
                }
            }

            if (protect > 0)
            {
                Console.WriteLine($"\nВаша текущая броня: {player.protectionName} ({player.playerProtect} защиты)");
                Console.WriteLine($"Новая броня: {name} ({protect} защиты)");
                Console.Write("\nВзять новую броню? (yes/no да/нет): ");
                string input = Console.ReadLine();
                if (input.ToLower() == "yes" || input.ToLower() == "да")
                {
                    player.protectionName = name;
                    player.playerProtect = protect;
                    Console.WriteLine($"Вы экипировали {name}.");
                }
                else
                {
                    Console.WriteLine("Вы выбросили предмет.");
                }
            }
        }
    }
}
