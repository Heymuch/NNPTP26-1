using NNPTPZ1.CommandLine;
using NNPTPZ1.Fractals;
using NNPTPZ1.Imaging;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1;

/// <summary>
/// This program should produce Newton fractals.
/// See more at: https://en.wikipedia.org/wiki/Newton_fractal
/// </summary>
public static class Program
{
    private const int ExitSuccess = 0;
    private const int ExitInvalidArguments = 1;

    private static readonly Polynomial FractalPolynomial = new(Complex.One, Complex.Zero, Complex.Zero, Complex.One);

    public static int Main(string[] args)
    {
        CommandLineOptions options;
        try
        {
            options = CommandLineOptions.Parse(args);
        }
        catch (ArgumentException exception)
        {
            Console.Error.WriteLine(exception.Message);
            return ExitInvalidArguments;
        }

        var renderer = new NewtonFractalRenderer(FractalPolynomial);
        var image = renderer.Render(options.Width, options.Height, options.Viewport);
        PngImageWriter.Save(image, options.OutputPath);

        return ExitSuccess;
    }
}
