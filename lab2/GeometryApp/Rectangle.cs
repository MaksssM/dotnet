namespace GeometryApp;

/// <summary>
/// Прямокутник із заданими шириною та висотою.
/// </summary>
public class Rectangle : Shape
{
    public Rectangle(double width, double height) : base("Прямокутник")
    {
        if (double.IsNaN(width) || double.IsInfinity(width) || width <= 0)
        {
            throw new InvalidShapeSizeException("Ширина має бути додатним числом.");
        }

        if (double.IsNaN(height) || double.IsInfinity(height) || height <= 0)
        {
            throw new InvalidShapeSizeException("Висота має бути додатним числом.");
        }

        Width = width;
        Height = height;
    }

    public double Width { get; }

    public double Height { get; }

    public override double GetArea() => Width * Height;

    public override double GetPerimeter() => 2 * (Width + Height);

    public override string ToString() => $"{Name} ({Width:0.##} × {Height:0.##})";
}
