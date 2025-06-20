namespace praktika4
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class Application
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Главное меню ===");
                Console.WriteLine("1) BLACKJACK");
                Console.WriteLine("2) База данных учебного заведения");
                Console.WriteLine("3) Выход");
                Console.Write("Выберите опцию: ");

                string userChoice = Console.ReadLine();
                if (userChoice == "1")
                {
                    CardGame.StartGame();
                }
                else if (userChoice == "2")
                {
                    PeopleDatabase.ShowRecords();
                }
                else if (userChoice == "3")
                {
                    Console.WriteLine("Выход из программы.");
                    break;
                }
                else
                {
                    Console.WriteLine("Некорректный ввод. Нажмите Enter и попробуйте снова.");
                    Console.ReadLine();
                }

                Console.WriteLine("\nНажмите Enter для продолжения...");
                Console.ReadLine();
            }
        }
    }

    // Модуль карточной игры
    class CardGame
    {
        static Random cardGenerator = new Random();

        public static void StartGame()
        {
            Console.Clear();
            Console.WriteLine("=== BLACKJACK ===");

            List<int> userCards = new List<int>();
            List<int> computerCards = new List<int>();

            userCards.Add(GetRandomCard());
            userCards.Add(GetRandomCard());
            computerCards.Add(GetRandomCard());
            computerCards.Add(GetRandomCard());

            while (true)
            {
                Console.WriteLine($"\nВАШИ карты: {string.Join(", ", userCards)} (Сумма: {CalculateTotal(userCards)})");
                Console.WriteLine($"Карты у ДИЛЛЕРА: {computerCards[0]}");

                if (CalculateTotal(userCards) > 21)
                {
                    Console.WriteLine("Вы проиграли! Сумма больше 21.");
                    return;
                }

                Console.Write("Взять карту (1) или пас (2)? ");
                string decision = Console.ReadLine();

                if (decision == "1")
                    userCards.Add(GetRandomCard());
                else if (decision == "2")
                    break;
                else
                    Console.WriteLine("Некорректный ввод. Введите 1 или 2.");
            }

            Console.WriteLine($"\nКарты ДИЛЛЕРА: {string.Join(", ", computerCards)}");

            while (CalculateTotal(computerCards) < 17)
            {
                computerCards.Add(GetRandomCard());
                Console.WriteLine($"ДИЛЛЕР взял карту: {computerCards[computerCards.Count - 1]} (Сумма: {CalculateTotal(computerCards)})");
            }

            int userScore = CalculateTotal(userCards);
            int computerScore = CalculateTotal(computerCards);

            Console.WriteLine($"\nВаш результат: {userScore}");
            Console.WriteLine($"Результат ДИЛЛЕРА: {computerScore}");

            if (computerScore > 21 || userScore > computerScore)
                Console.WriteLine("Поздравляем с победой!");
            else if (computerScore == userScore)
                Console.WriteLine("Ничья!");
            else
                Console.WriteLine("ДИЛЛЕР победил.");
        }

        static int GetRandomCard() => cardGenerator.Next(2, 12);

        static int CalculateTotal(List<int> cards)
        {
            int sum = 0;
            foreach (var card in cards)
                sum += card;
            return sum;
        }
    }

    // Модуль базы данных людей
    static class PeopleDatabase
    {
        public static void ShowRecords()
        {
            Console.Clear();
            Console.WriteLine("=== База данных учебного заведения ===");

            Person universityMember = new UniversityMember("Иван", 21, "МГТУ", "Информационные системы");
            Person companyWorker = new CompanyWorker("Артур", 32, "МГТУ", "Финансовый аналитик");

            Console.WriteLine(universityMember.GetDetails());
            Console.WriteLine();
            Console.WriteLine(companyWorker.GetDetails());
        }
    }

    abstract class Person
    {
        protected string FullName { get; set; }
        protected int Years { get; set; }

        public Person(string name, int age)
        {
            FullName = name;
            Years = age;
        }

        public abstract string GetDetails();
    }

    class UniversityMember : Person
    {
        private string EducationInstitution { get; set; }
        private string StudyProgram { get; set; }

        public UniversityMember(string name, int age, string university, string program)
            : base(name, age)
        {
            EducationInstitution = university;
            StudyProgram = program;
        }

        public override string GetDetails()
        {
            return $"[Студент]\nИмя: {FullName}\nВозраст: {Years}\nУчебное заведение: {EducationInstitution}\nНаправление: {StudyProgram}";
        }
    }

    class CompanyWorker : Person
    {
        private string Organization { get; set; }
        private string JobTitle { get; set; }

        public CompanyWorker(string name, int age, string company, string position)
            : base(name, age)
        {
            Organization = company;
            JobTitle = position;
        }

        public override string GetDetails()
        {
            return $"[Работник]\nИмя: {FullName}\nВозраст: {Years}\nОрганизация: {Organization}\nДолжность: {JobTitle}";
        }
    }
}