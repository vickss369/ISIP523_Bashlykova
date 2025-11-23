using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP523_Bashlykova.Model
{
    internal class Weapon
    {
        public string weaponName;
        public int weaponAttack;

        public Weapon(string weaponName, int weaponAttack)
        {
            this.weaponName = weaponName;
            this.weaponAttack = weaponAttack;
        }

        static void TakeWeapon(Player player, string newWeaponName, double newWeaponAttack)
        {
            Console.WriteLine($"\nВаше текущее оружие: {player.weaponName} ({player.playerAttack} атаки)");
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
    }
}
