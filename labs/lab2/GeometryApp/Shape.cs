namespace GeometryApp;

/// <summary>
/// Абстрактна геометрична фігура.
/// </summary>
public abstract class Shape
{
    protected Shape(string name)
    {
        Name = name;
    }

    /// <summary>Назва фігури.</summary>
    public string Name { get; }

    /// <summary>Обчислює площу фігури.</summary>
    public abstract double GetArea();

    /// <summary>Обчислює периметр фігури.</summary>
    public abstract double GetPerimeter();

    public override string ToString() => Name;
}
