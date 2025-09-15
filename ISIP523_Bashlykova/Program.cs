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
            string request = "";
            string[] words = new string[2];
            int kolvoop = Convert.ToInt32(Console.ReadLine());
            string[] pokypki = new string[kolvoop];
            double[] sums = new double[kolvoop];
            for (int i = 0; i < kolvoop; i++) 
            {
                Console.WriteLine();
                Console.WriteLine("Введите данные о покупке товара: ");
                request = Console.ReadLine();
                words = request.Split(new char[] { ';' },
                StringSplitOptions.RemoveEmptyEntries);
                words[1] = words[1].Trim();
                pokypki[i] = words[0];
                sums[i] = Convert.ToInt32(words[1]);
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
                            Console.WriteLine("( " + pokypki[i] + "; " + sums[i] + " )");
                        }
                        break;

                    case 2:
                        double srar = 0;
                        double maxim = sums[0];
                        double minim = sums[1];
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
                        for (int i = 0; i < kolvoop - 1; i++)
                        {
                            for (int j = kolvoop - 2; j >= i; j--)
                            {
                                if (sums[j] > sums[j + 1])
                                {
                                    double v = sums[j];
                                    string p = pokypki[j];
                                    sums[j] = sums[j + 1];
                                    pokypki[j] = pokypki[j + 1];
                                    sums[j + 1] = v;
                                    pokypki[j + 1] = p;
                                }
                            }
                        }
                        Console.WriteLine("\nОтсортировано.");
                        break;

                    case 4:
                        Console.WriteLine("\nМЕНЮ ВАЛЮТ");
                        Console.WriteLine("1.Доллары");
                        Console.WriteLine("2.Евро");
                        Console.WriteLine("3.Свой вариант");
                        Console.Write("Введите выбор: ");
                        int choiseval = Convert.ToInt32(Console.ReadLine());
                        switch(choiseval)
                        {
                            case 1:
                                for (int i = 0; i < kolvoop; i++)
                                {
                                    sums[i] = sums[i] * 0.012;
                                }
                                break;

                            case 2:
                                for (int i = 0; i < kolvoop; i++)
                                {
                                    sums[i] = sums[i] * 0.010;
                                }
                                break;

                            case 3:
                                Console.Write("Введите свой курс: ");
                                double kurs = Convert.ToDouble(Console.ReadLine()); 
                                for (int i = 0; i < kolvoop; i++)
                                {
                                    sums[i] = sums[i] * kurs;
                                }
                                break;

                            default: break;
                        }
                        Console.WriteLine("\nКонвертировано.");
                        break;

                    case 5:
                        Console.WriteLine();
                        Console.Write("Введите название искомого товара: ");
                        string nazvtovar = Console.ReadLine();
                        bool poisk = false;
                        for (int i = 0; i < kolvoop; i++)
                        {
                            if (pokypki[i] == nazvtovar)
                            {
                                Console.WriteLine("( " + pokypki[i] + "; " + sums[i] + " )");
                                poisk = true;
                            }
                        }
                        if (!poisk)
                        {
                            Console.WriteLine("Товар " + nazvtovar + " не найден.");
                        }
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
