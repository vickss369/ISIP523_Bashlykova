using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// Вам необходимо создать систему управления университетом. Система должна позволять управлять информацией о студентах, преподавателях и курсах через консоль. 
/// В университете есть студенты, которые могут записываться на различные курсы. У каждого курса есть преподаватель, который его ведет. Система должна хранить информацию обо всех участниках учебного процесса и позволять выполнять различные операции с ними.
/// Пользователь должен иметь возможность добавлять в систему новых студентов и просматривать информацию о них. Также необходимо реализовать функциональность записи студентов на курсы и просмотра списка всех курсов, на которые записан конкретный студент.
/// Система должна позволять добавлять преподавателей и просматривать информацию о каждом из них. Преподаватели могут быть назначены на различные курсы, которые они будут вести.
/// Для управления курсами нужно реализовать возможность создания новых курсов, просмотра детальной информации о каждом курсе и вывода списка всех студентов, записанных на конкретный курс.
/// Дополнительно программа должна предоставлять возможность вывода полных списков: всех студентов в системе, всех преподавателей и всех доступных курсов.
/// Ваша задача — спроектировать архитектуру приложения, используя принципы ООП, и реализовать консольное меню для удобного взаимодействия со всеми описанными функциями системы.
/// Требования к проектированию: При разработке системы вы обязаны применить следующие принципы ООП:
/// 1.Инкапсуляция - Данные объектов должны быть защищены от прямого доступа. Подумайте, какие поля должны быть приватными, а какие методы публичными.
/// 2. Наследование - Студенты и преподаватели имеют общие характеристики (имя, возраст, контактная информация и т.д.). Используйте наследование, чтобы избежать дублирования кода.
/// 3. Полиморфизм - Разные типы людей в университете могут иметь разное представление своей информации. Реализуйте возможность работы с объектами через общий интерфейс или базовый класс.
/// 4. Абстракция - Выделите общие характеристики и поведение для похожих сущностей. Используйте абстрактные классы или интерфейсы там, где это необходимо.


namespace ISIP523_Bashlykova
{
    internal class Program
    {
        class Person
        {
            private string name;
            private string surname;
            private int age;
            private string email;

            public Person(string name, string surname, int age, string email)
            {
                this.name = name;
                this.surname = surname;
                this.age = age;
                this.email = email;
            }

            public virtual void printInfo()
            {
                Console.WriteLine($"Имя: {name}\nФамилия: {surname}\nВозраст: {age}\nКорпоративная почта: {email}");
            }
        }

        class Student : Person
        {
            private string group;
            private string course;

            public Student(string name, string surname, int age, string email, string group, string course)
                : base(name, surname, age, email)
            {
                this.group = group;
                this.course = course;

            }

            public override void printInfo()
            {
                //Console.WriteLine("ИНФОРМАЦИЯ О С");
                base.printInfo();
                Console.WriteLine($"Номер группы: {group}\nКурс: {course}");
            }
        }

        class Teacher : Person
        {
            private string salary;
            private string course;
            private int experience;

            public Teacher(string name, string surname, int age, string email, string salary, string course, int experience)
                : base(name, surname, age, email)
            {
                this.salary = salary;
                this.course = course;
                this.experience = experience;
            }

            public override void printInfo()
            {
                //Console.WriteLine("ИНФОРМАЦИЯ О С");
                base.printInfo();
                Console.WriteLine($"Зарплата: {salary}\nКурс: {course}\nСтаж работы: {experience}");
            }
        }

        class Course
        {
            private string title;
            private Teacher teacher;
            private string duration;
            private List<Student> Students = new List<Student>();

            public void AddStudent(Student student)
            {
                if (!Students.Contains(student))
                {
                    Students.Add(student);
                }
            }

            private void Print()
            {
                Console.WriteLine($"Название: {title}\nУчитель: {teacher}\nДлительность: {duration}");
            }
        }


        static void Main(string[] args)
        {
            bool outt = true;
            while (outt)
            {
                Console.WriteLine("\nМЕНЮ:");
                Console.WriteLine("1. Добавить студента");
                Console.WriteLine("2. Добавить преподавателя");
                Console.WriteLine("3. Вывод списков");
                Console.WriteLine("4. Создать курс");
                Console.WriteLine("5. Записать студента на курс");
                Console.WriteLine("6. Назначить преподавателя на курс");
                Console.WriteLine("7. Список курсов студента");
                Console.WriteLine("8. Список студентов на курсе");
                Console.WriteLine("0. Выход");

                Console.Write("\nВыберите действие: ");
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.WriteLine();
                        break;

                    case 2:
                        Console.WriteLine();
                        break;

                    case 3:
                        Console.WriteLine("\nВЫБОР СПИСКА:");
                        Console.WriteLine("1. Список студентов");
                        Console.WriteLine("2. Список преподавателей");
                        Console.WriteLine("3. Список курсов");

                        Console.Write("\nВыберите действие: ");
                        int choicepoisk = Convert.ToInt32(Console.ReadLine());
                        switch (choicepoisk)
                        {
                            case 1:
                                break;

                            case 2:
                                break;

                            case 3:
                                break;

                            default: break;
                        }
                        break;

                    case 4:
                        Console.WriteLine();
                        break;

                    case 5:
                        Console.WriteLine();
                        break;

                    case 6:
                        Console.WriteLine();
                        break;

                    case 7:
                        Console.WriteLine();
                        break;

                    case 0: outt = false; break;

                    default: Console.WriteLine("Неправильный пункт меню."); break;
                }
            }
        }
    }
}
