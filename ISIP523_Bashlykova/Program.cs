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
            }

            bool outt = true;
            while (outt)
            {
                Console.WriteLine("\nМЕНЮ");
                Console.WriteLine("1.Вывод данных");
                Console.WriteLine("2.Статистика(среднее, максимальное, минимальное, сумма)");
                Console.WriteLine("3.Сортировка по цене(пузырьковая сортировка)");
                Console.WriteLine("4.Конвертация валюты(пользователь вводит курс или выбирает из списка)");
                Console.WriteLine("5.Поиск по названию");
                Console.WriteLine("0.Выход");

                Console.Write("Введите выбор: ");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Console.WriteLine();
                        for (int i = 0; i < kolvoop; i++)
                        {
                            Console.WriteLine("Товар: " + pokypki[i] + ", цена: " + sums[i]);
                        }
                        break;

                    case 2:
                        double srar = 0;
                        int maxim = sums[0];
                        int minim = sums[1];
                        int summ = 0;
                        Console.WriteLine();
                        foreach (int i in sums)
                        {
                            summ += i;
                            if (i >  maxim) maxim = i;
                            if (i < minim) minim = i;
                        }
                        srar = (double)summ / kolvoop;
                        Console.WriteLine("Среднее значение: " + srar + "\n" + "Максимальная сумма: " + maxim + "\n" + "Минимальная сумма: " + minim + "\n" + "Всего: " + summ + "\n");
                        break;

                    case 3:
                        break;

                    case 4:
                        break;

                    case 5:
                        break;

                    case 0: outt = false; break;

                    default: outt = false; break;
                }
            }

            /*for (int i = 0; i < kolvoop; i++)
            {
                Console.WriteLine(pokypki[i]);
            }*/
        }
    }
}
