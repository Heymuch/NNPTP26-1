namespace NNPTPZ1.Fractals;

public sealed class ColorPalette
{
    public const int DefaultDarkeningPerIteration = 2;

    private readonly RgbColor[] _rootColors;

    public ColorPalette(IEnumerable<RgbColor> rootColors, int darkeningPerIteration = DefaultDarkeningPerIteration)
    {
        ArgumentNullException.ThrowIfNull(rootColors);
        _rootColors = [.. rootColors];
        if (_rootColors.Length == 0)
        {
            throw new ArgumentException("At least one colour is required.", nameof(rootColors));
        }

        ArgumentOutOfRangeException.ThrowIfNegative(darkeningPerIteration);
        DarkeningPerIteration = darkeningPerIteration;
    }

    public static ColorPalette Default { get; } = new(
    [
        RgbColor.Red, RgbColor.Blue, RgbColor.Green, RgbColor.Yellow, RgbColor.Orange,
        RgbColor.Fuchsia, RgbColor.Gold, RgbColor.Cyan, RgbColor.Magenta,
    ]);

    public int DarkeningPerIteration { get; }

    public RgbColor GetColor(int rootIndex, int iterations)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(rootIndex);
        ArgumentOutOfRangeException.ThrowIfNegative(iterations);

        var baseColor = _rootColors[rootIndex % _rootColors.Length];
        var darkening = (int)Math.Min((long)iterations * DarkeningPerIteration, byte.MaxValue);
        return baseColor.Darken(darkening);
    }
}
