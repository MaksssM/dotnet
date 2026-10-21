namespace GeometryApp;

/// <summary>
/// Коло із заданим радіусом.
/// </summary>
public class Circle : Shape
{
    public Circle(double radius) : base("Коло")
    {
        if (double.IsNaN(radius) || double.IsInfinity(radius) || radius <= 0)
        {
            throw new InvalidShapeSizeException("Радіус має бути додатним числом.");
        }

        Radius = radius;
    }

    public double Radius { get; }

    public override double GetArea() => Math.PI * Radius * Radius;

    public override double GetPerimeter() => 2 * Math.PI * Radius;

    public override string ToString() => $"{Name} (радіус = {Radius:0.##})";
}
