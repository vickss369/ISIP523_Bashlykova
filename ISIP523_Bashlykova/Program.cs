using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP523_Bashlykova
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ///Задание: Создать консольное приложение для подсчета потраченных за день средств.
            ///Пользователь вводит количество операций, которые будут записаны. Можно внести от 2 до 40 операций.
            ///Дальше, пользователь по шаблону(Название услуги или товара; Количество денег) вводит траты. Валюта - рубли.
            ///Пример: (Влажные салфетки "Лента"; 235)
            ///После заполнения всех трат, пользователь должен увидеть следующее меню:
            ///1.Вывод данных 2.Статистика(среднее, максимальное, минимальное, сумма) 3.Сортировка по цене(пузырьковая сортировка)
            ///4.Конвертация валюты(пользователь вводит курс или выбирает из списка) 5.Поиск по названию 0.Выход
            ///Выбор пунктов меню осуществляется по соответствующей цифре.


            Console.Write("Введите количество зафиксированных операций (2-40): ");
            int kolvoop = Convert.ToInt32(Console.ReadLine());
            string[] pokypki = new string[kolvoop];
            int[] sums = new int[kolvoop];
            for (int i = 0; i < kolvoop; i++) 
            {
                Console.WriteLine();
                Console.WriteLine("Введите название купленного товара: ");
                pokypki[i] = Console.ReadLine();
                Console.WriteLine("Введите сумму покупки: ");
                sums[i] = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
            }

            /*for (int i = 0; i < kolvoop; i++)
            {
                Console.WriteLine(pokypki[i]);
            }*/
        }
    }
}
