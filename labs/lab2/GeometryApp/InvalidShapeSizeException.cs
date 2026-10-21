namespace GeometryApp;

/// <summary>
/// Виняток для недопустимих розмірів геометричної фігури.
/// </summary>
public class InvalidShapeSizeException : ArgumentException
{
    public InvalidShapeSizeException(string message) : base(message)
    {
    }
}
