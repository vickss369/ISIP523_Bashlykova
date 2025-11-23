using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP523_Bashlykova.Model
{
    internal class Protection
    {
        public string protectionName;
        public int protectionProtect;

        public Protection(string protectionName, int protectionProtect)
        {
            this.protectionName = protectionName;
            this.protectionProtect = protectionProtect;
        }

        static void TakeProtection(Player player, string newProtectName, double newPrProtect)
        {
            Console.WriteLine($"\nВаша текущая броня: {player.protectionName} ({player.playerProtect} защиты)");
            Console.WriteLine($"Новая броня: {newProtectName} (+{newPrProtect} защиты)");
            Console.Write("\nВзять новую броню? (y/n): ");
            string input = Console.ReadLine();
            if (input.ToLower() == "y")
            {
                player.protectionName = newProtectName;
                player.playerProtect = newPrProtect;
                Console.WriteLine($"Вы экипировали {newProtectName}.");
            }
            else
            {
                Console.WriteLine("Вы выбросили предмет.");
            }
        }
    }
}
