using System;

class Program
{
    static void Main()
    {
        // Початкове заповнення випадковими балами через Random.Shared.Next(0, 101)
        int[] scores = new int[10];
        for (int i = 0; i < scores.Length; i++)
        {
            scores[i] = Random.Shared.Next(0, 101);
        }

        // Для перевірки граничного випадку (порожній масив) можна стартувати з порожнього масиву:
        // int[] scores = Array.Empty<int>();

        bool running = true;
        while (running)
        {
            PrintMenu();
            Console.Write("Ваш вибір: ");
            string? choice = Console.ReadLine()?.Trim();

            switch (choice)
            {
                case "1":
                    AddScore(ref scores);
                    break;
                case "2":
                    ShowAllScores(scores);
                    break;
                case "3":
                    ShowAverage(scores);
                    break;
                case "4":
                    ShowMinMax(scores);
                    break;
                case "5":
                    ShowDistribution(scores);
                    break;
                case "0":
                    running = false;
                    Console.WriteLine("Завершення роботи.");
                    break;
                default:
                    Console.WriteLine("Невірний вибір. Введіть число від 0 до 5.");
                    break;
            }

            Console.WriteLine();
        }
    }

    static void PrintMenu()
    {
        Console.WriteLine("=== Статистика результатів тестування (Варіант 1) ===");
        Console.WriteLine("1. Додати результат");
        Console.WriteLine("2. Показати всі результати");
        Console.WriteLine("3. Показати середній бал");
        Console.WriteLine("4. Показати найвищий і найнижчий бал");
        Console.WriteLine("5. Показати розподіл за категоріями");
        Console.WriteLine("0. Вихід");
    }

    static bool HasData(int[] scores)
    {
        if (scores.Length == 0)
        {
            Console.WriteLine("Немає даних");
            return false;
        }
        return true;
    }

    // Додати результат — розширення масиву за зразком прикладу №3 (Array.Resize)
    static void AddScore(ref int[] scores)
    {
        Console.Write("Введіть бал (ціле число від 0 до 100): ");
        string? input = Console.ReadLine()?.Trim();
        if (!int.TryParse(input, out int score) || score < 0 || score > 100)
        {
            Console.WriteLine("Помилка: бал має бути цілим числом від 0 до 100.");
            return;
        }

        Array.Resize(ref scores, scores.Length + 1);
        scores[scores.Length - 1] = score;
        Console.WriteLine($"Додано результат: {score} (позиція {scores.Length - 1}).");
    }

    static void ShowAllScores(int[] scores)
    {
        if (!HasData(scores)) return;

        Console.WriteLine("Всі результати:");
        for (int i = 0; i < scores.Length; i++)
        {
            Console.WriteLine($"[{i}] {scores[i]} ({GetCategory(scores[i])})");
        }
    }

    static void ShowAverage(int[] scores)
    {
        if (!HasData(scores)) return;

        double avg = CalculateAverage(scores);
        Console.WriteLine($"Середній бал: {avg:F2}");
    }

    // Середній бал (приклад коду №2): приймає масив, повертає double
    static double CalculateAverage(int[] scores)
    {
        long sum = 0;
        for (int i = 0; i < scores.Length; i++)
        {
            sum += scores[i];
        }
        return (double)sum / scores.Length;
    }

    static void ShowMinMax(int[] scores)
    {
        if (!HasData(scores)) return;

        int maxIndex = FindMaxIndex(scores);
        int minIndex = FindMinIndex(scores);
        Console.WriteLine($"Найвищий бал: {scores[maxIndex]} (позиція {maxIndex})");
        Console.WriteLine($"Найнижчий бал: {scores[minIndex]} (позиція {minIndex})");
    }

    // Пошук найвищого бала: повертає індекс першого знайденого максимуму
    static int FindMaxIndex(int[] scores)
    {
        int maxIndex = 0;
        for (int i = 1; i < scores.Length; i++)
        {
            if (scores[i] > scores[maxIndex])
            {
                maxIndex = i;
            }
        }
        return maxIndex;
    }

    // Пошук найнижчого бала: повертає індекс першого знайденого мінімуму
    static int FindMinIndex(int[] scores)
    {
        int minIndex = 0;
        for (int i = 1; i < scores.Length; i++)
        {
            if (scores[i] < scores[minIndex])
            {
                minIndex = i;
            }
        }
        return minIndex;
    }

    static void ShowDistribution(int[] scores)
    {
        if (!HasData(scores)) return;

        int excellent = 0;      // 90+
        int good = 0;           // 75–89
        int satisfactory = 0;   // 60–74
        int unsatisfactory = 0; // <60

        foreach (int score in scores)
        {
            switch (GetCategoryKey(score))
            {
                case 0: excellent++; break;
                case 1: good++; break;
                case 2: satisfactory++; break;
                default: unsatisfactory++; break;
            }
        }

        Console.WriteLine("Розподіл за категоріями:");
        Console.WriteLine($"«відмінно» (90+): {excellent}");
        Console.WriteLine($"«добре» (75–89): {good}");
        Console.WriteLine($"«задовільно» (60–74): {satisfactory}");
        Console.WriteLine($"«незадовільно» (<60): {unsatisfactory}");
    }

    // Категорія за балом через switch expression
    static string GetCategory(int score) => score switch
    {
        >= 90 => "відмінно",
        >= 75 => "добре",
        >= 60 => "задовільно",
        _ => "незадовільно"
    };

    // Допоміжний ключ категорії для підрахунку розподілу
    static int GetCategoryKey(int score) => score switch
    {
        >= 90 => 0,
        >= 75 => 1,
        >= 60 => 2,
        _ => 3
    };
}
