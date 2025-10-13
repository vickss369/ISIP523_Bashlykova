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

            public string GetFullName()
            {
                return $"{name} {surname}";
            }

            public string GetSurname()
            {
                return surname;
            }

            public Person(string name, string surname, int age, string email)
            {
                this.name = name;
                this.surname = surname;
                this.age = age;
                this.email = email;
            }

            public virtual void vyvodInfo()
            {
                Console.WriteLine($"Имя: {name}\nФамилия: {surname}\nВозраст: {age}\nКорпоративная почта: {email}");
            }
        }

        class Student : Person
        {
            private string group;
            private int godobych;

            public Student(string name, string surname, int age, string email, string group, int godobych)
                : base(name, surname, age, email)
            {
                this.group = group;
                this.godobych = godobych;
            }

            public override void vyvodInfo()
            {
                base.vyvodInfo();
                Console.WriteLine($"Номер группы: {group}\nКурс: {godobych}");
            }
        }

        class Teacher : Person
        {
            private string salary;
            private int experience;

            public Teacher(string name, string surname, int age, string email, string salary, int experience)
                : base(name, surname, age, email)
            {
                this.salary = salary;
                this.experience = experience;
            }

            public override void vyvodInfo()
            {
                base.vyvodInfo();
                Console.WriteLine($"Зарплата: {salary}\nСтаж работы: {experience}");
            }
        }

        class Course
        {
            private string title;
            private Teacher teacher;
            private string duration;
            private List<Student> Students = new List<Student>();

            public Course(string title, string duration)
            {
                this.title = title;
                this.duration = duration;
            }

            public void GetTeacher(Teacher teacher)
            {
                this.teacher = teacher;
            }

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

        static List<Student> allStud = new List<Student>();
        static void addStudent()
        {
            Console.WriteLine("Введите имя: ");
            string name = Console.ReadLine();
            Console.WriteLine("Введите фамилию: ");
            string surname = Console.ReadLine();
            Console.WriteLine("Введите корпоративную почту: ");
            string email = Console.ReadLine();
            Console.WriteLine("Введите возраст: ");
            int age = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите группу: ");
            string group = Console.ReadLine();
            Console.WriteLine("Введите год обучения: ");
            int year = Convert.ToInt32(Console.ReadLine());
            allStud.Add(new Student(name, surname, age, email, group, year));
            Console.WriteLine("\nСтудент добавлен!");
        }

        static List<Teacher> allTeach = new List<Teacher>();
        static void addTeacher()
        {
            Console.WriteLine("Введите имя: ");
            string name = Console.ReadLine();
            Console.WriteLine("Введите фамилию: ");
            string surname = Console.ReadLine();
            Console.WriteLine("Введите возраст: ");
            int age = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите корпоративную почту: ");
            string email = Console.ReadLine();
            Console.WriteLine("Введите зарплатy: ");
            string salary = Console.ReadLine();
            Console.WriteLine("Введите стаж работы: ");
            int experience = Convert.ToInt32(Console.ReadLine());
            allTeach.Add(new Teacher(name, surname, age, email, salary, experience));
            Console.WriteLine("\nПреподаватель добавлен!");
        }

        static List<Course> AllCourses = new List<Course>();
        static void addCourse()
        {
            Console.Write("Введите название курса: ");
            string title = Console.ReadLine();
            Console.Write("Введите длительность курса: ");
            string duration = Console.ReadLine();

            Course newCourse = new Course(title, duration);

            if (allTeach.Count == 0)
            {
                Console.WriteLine("Нет доступных преподавателей. Сначала добавьте хотя бы одного.");
            }
            else
            {
                Console.WriteLine("\nСписок преподавателей:");
                foreach (var t in allTeach)
                {
                    t.vyvodInfo();
                    Console.WriteLine();
                }

                Console.Write("Введите фамилию преподавателя для назначения на курс: ");
                string surnameSearch = Console.ReadLine();

                Teacher foundTeacher = allTeach.Find(t => t.GetSurname().Equals(surnameSearch, StringComparison.OrdinalIgnoreCase));

                if (foundTeacher != null)
                {
                    newCourse.GetTeacher(foundTeacher);
                    Console.WriteLine($"Преподаватель {foundTeacher.GetFullName()} назначен на курс.");
                }
                else
                {
                    Console.WriteLine("Преподаватель с такой фамилией не найден.");
                }
            }

            if (allStud.Count == 0)
            {
                Console.WriteLine("\nНет студентов для добавления.");
            }
            else
            {
                Console.WriteLine("\nДобавление студентов на курс (введите '0', чтобы закончить):");
                foreach (var s in allStud)
                {
                    s.vyvodInfo();
                    Console.WriteLine();
                }

                bool adding = true;
                while (adding)
                {
                    Console.Write("Введите фамилию студента для добавления (или '0' для выхода): ");
                    string studSurname = Console.ReadLine();

                    if (studSurname == "0")
                    {
                        adding = false;
                        continue;
                    }

                    Student foundStudent = allStud.Find(s => s.GetSurname().Equals(studSurname, StringComparison.OrdinalIgnoreCase));

                    if (foundStudent != null)
                    {
                        newCourse.AddStudent(foundStudent);
                        Console.WriteLine($" Студент {foundStudent.GetFullName()} добавлен на курс.");
                    }
                    else
                    {
                        Console.WriteLine(" Студент с такой фамилией не найден.");
                    }
                }
            }

            AllCourses.Add(newCourse);
            Console.WriteLine("\n Курс добавлен!");
        }

            static void Main(string[] args)
            {
            bool outt = true;
            while (outt)
            {
                Console.WriteLine("\nМЕНЮ:");
                Console.WriteLine("1. Добавить студента");
                Console.WriteLine("2. Добавить преподавателя");
                Console.WriteLine("3. Создать курс");
                Console.WriteLine("4. Вывод списков");
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
                        addStudent();
                        break;

                    case 2:
                        Console.WriteLine();
                        addTeacher();
                        break;

                    case 3:
                        Console.WriteLine();
                        addCourse();    
                        break;

                    case 4:
                        Console.WriteLine("\nВЫБОР СПИСКА:");
                        Console.WriteLine("1. Список студентов");
                        Console.WriteLine("2. Список преподавателей");
                        Console.WriteLine("3. Список курсов");

                        Console.Write("\nВыберите действие: ");
                        int choicepoisk = Convert.ToInt32(Console.ReadLine());
                        switch (choicepoisk)
                        {
                            case 1:
                                Console.WriteLine("\nСТУДЕНТЫ\n");
                                foreach(var stud in allStud)
                                {
                                    stud.vyvodInfo();
                                }
                                break;

                            case 2:
                                Console.WriteLine("\nПРЕПОДАВАТЕЛИ\n");
                                foreach (var prep in allTeach)
                                {
                                    prep.vyvodInfo();
                                }
                                break;

                            case 3:
                                Console.WriteLine("\nКУРСЫ\n");
                                break;

                            default: break;
                        }
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
