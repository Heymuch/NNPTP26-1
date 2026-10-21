namespace NNPTPZ1.Fractals;

public sealed class FractalImage
{
    private readonly RgbColor[] _pixels;

    public FractalImage(int width, int height)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(width);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(height);

        Width = width;
        Height = height;
        _pixels = new RgbColor[checked(width * height)];
    }

    public int Width { get; }
    public int Height { get; }

    public RgbColor this[int column, int row]
    {
        get => _pixels[GetIndex(column, row)];
        set => _pixels[GetIndex(column, row)] = value;
    }

    private int GetIndex(int column, int row)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(column);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(column, Width);
        ArgumentOutOfRangeException.ThrowIfNegative(row);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(row, Height);
        return row * Width + column;
    }
}
