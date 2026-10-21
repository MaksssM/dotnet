using System.Globalization;

namespace GeometryApp;

internal static class Program
{
    private static readonly List<Shape> Shapes = new();

    private static void Main()
    {
        bool running = true;

        while (running)
        {
            PrintMenu();
            Console.Write("Ваш вибір: ");
            string? choice = Console.ReadLine()?.Trim();

            switch (choice)
            {
                case "1":
                    AddShape();
                    break;
                case "2":
                    ShowAllShapes();
                    break;
                case "3":
                    SortByArea();
                    break;
                case "4":
                    ShowTotals();
                    break;
                case "0":
                    running = false;
                    Console.WriteLine("Завершення роботи.");
                    break;
                default:
                    Console.WriteLine("Невірний вибір. Введіть число від 0 до 4.");
                    break;
            }

            Console.WriteLine();
        }
    }

    private static void PrintMenu()
    {
        Console.WriteLine("=== Геометричні фігури (Варіант 1) ===");
        Console.WriteLine("1. Додати фігуру");
        Console.WriteLine("2. Показати всі фігури");
        Console.WriteLine("3. Відсортувати за площею");
        Console.WriteLine("4. Показати сумарну площу та периметр");
        Console.WriteLine("0. Вихід");
    }

    private static void AddShape()
    {
        Console.WriteLine("Тип фігури:");
        Console.WriteLine("  1 - коло");
        Console.WriteLine("  2 - прямокутник");
        Console.Write("Тип: ");
        string? type = Console.ReadLine()?.Trim();

        try
        {
            switch (type)
            {
                case "1":
                {
                    double radius = ReadDouble("Радіус: ");
                    Shapes.Add(new Circle(radius));
                    Console.WriteLine($"Додано: {Shapes[^1]}");
                    break;
                }
                case "2":
                {
                    double width = ReadDouble("Ширина: ");
                    double height = ReadDouble("Висота: ");
                    Shapes.Add(new Rectangle(width, height));
                    Console.WriteLine($"Додано: {Shapes[^1]}");
                    break;
                }
                default:
                    Console.WriteLine("Помилка: невірний тип фігури. Оберіть 1 або 2.");
                    break;
            }
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"Помилка вводу: {ex.Message}");
        }
        catch (InvalidShapeSizeException ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
    }

    private static double ReadDouble(string prompt)
    {
        Console.Write(prompt);
        string? input = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(input))
        {
            throw new FormatException("порожнє значення. Введіть число.");
        }

        bool parsed = double.TryParse(input, NumberStyles.Float, CultureInfo.CurrentCulture, out double value)
                      || double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out value);

        if (!parsed)
        {
            throw new FormatException($"«{input}» не є числом.");
        }

        return value;
    }

    private static void ShowAllShapes()
    {
        if (!EnsureNotEmpty())
        {
            return;
        }

        PrintShapes("Усі фігури:");
    }

    private static void SortByArea()
    {
        if (!EnsureNotEmpty())
        {
            return;
        }

        Shapes.Sort((first, second) => first.GetArea().CompareTo(second.GetArea()));
        PrintShapes("Фігури, відсортовані за площею (за зростанням):");
    }

    private static void ShowTotals()
    {
        if (!EnsureNotEmpty())
        {
            return;
        }

        double totalArea = 0;
        double totalPerimeter = 0;

        foreach (Shape shape in Shapes)
        {
            totalArea += shape.GetArea();
            totalPerimeter += shape.GetPerimeter();
        }

        Console.WriteLine($"Сумарна площа: {totalArea:F2}");
        Console.WriteLine($"Сумарний периметр: {totalPerimeter:F2}");
    }

    private static void PrintShapes(string title)
    {
        Console.WriteLine(title);
        for (int i = 0; i < Shapes.Count; i++)
        {
            Shape shape = Shapes[i];
            Console.WriteLine(
                $"{i + 1}. {shape} — площа: {shape.GetArea():F2}, периметр: {shape.GetPerimeter():F2}");
        }
    }

    private static bool EnsureNotEmpty()
    {
        if (Shapes.Count > 0)
        {
            return true;
        }

        Console.WriteLine("Колекція порожня.");
        return false;
    }
}
