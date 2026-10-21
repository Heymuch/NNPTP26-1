using System.Globalization;
using NNPTPZ1.Fractals;

namespace NNPTPZ1.CommandLine;

public sealed record CommandLineOptions(int Width, int Height, Viewport Viewport, string OutputPath)
{
    public const string DefaultOutputPath = "out.png";

    private const int RequiredArgumentCount = 6;
    private const int MaxArgumentCount = 7;

    public static CommandLineOptions Parse(IReadOnlyList<string> args)
    {
        ArgumentNullException.ThrowIfNull(args);
        if (args.Count is < RequiredArgumentCount or > MaxArgumentCount)
        {
            throw new ArgumentException(
                $"Expected {RequiredArgumentCount} or {MaxArgumentCount} arguments, got {args.Count}.", nameof(args));
        }

        var width = ParsePositiveInt(args[0], "width");
        var height = ParsePositiveInt(args[1], "height");
        var viewport = new Viewport(
            ParseDouble(args[2], "xMin"),
            ParseDouble(args[3], "xMax"),
            ParseDouble(args[4], "yMin"),
            ParseDouble(args[5], "yMax"));
        var outputPath = args.Count > RequiredArgumentCount ? args[RequiredArgumentCount] : DefaultOutputPath;

        return new CommandLineOptions(width, height, viewport, outputPath);
    }

    private static int ParsePositiveInt(string value, string name)
    {
        if (!int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var result) || result <= 0)
        {
            throw new ArgumentException($"{name} must be a positive integer, got '{value}'.", name);
        }

        return result;
    }

    private static double ParseDouble(string value, string name)
    {
        if (!double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var result)
            || !double.IsFinite(result))
        {
            throw new ArgumentException($"{name} must be a number, got '{value}'.", name);
        }

        return result;
    }
}
