using NNPTPZ1.Mathematics;

namespace NNPTPZ1.Fractals;

public sealed class NewtonFractalRenderer
{
    private const double AxisOffset = 0.0001;

    private static Complex AvoidAxes(Complex point) => new(
        point.Real == 0 ? AxisOffset : point.Real,
        point.Imaginary == 0 ? AxisOffset : point.Imaginary);

    private readonly NewtonSolver _solver;
    private readonly ColorPalette _palette;

    public NewtonFractalRenderer(Polynomial polynomial, ColorPalette? palette = null)
        : this(new NewtonSolver(polynomial), palette)
    {
    }

    public NewtonFractalRenderer(NewtonSolver solver, ColorPalette? palette = null)
    {
        ArgumentNullException.ThrowIfNull(solver);
        _solver = solver;
        _palette = palette ?? ColorPalette.Default;
    }

    public FractalImage Render(int width, int height, Viewport viewport)
    {
        ArgumentNullException.ThrowIfNull(viewport);
        var image = new FractalImage(width, height);
        var results = SolveAllPixels(width, height, viewport);

        var roots = new RootRegistry();
        for (var row = 0; row < height; row++)
        {
            for (var column = 0; column < width; column++)
            {
                var result = results[row * width + column];
                var rootIndex = roots.GetOrAddRootIndex(result.Point);
                image[column, row] = _palette.GetColor(rootIndex, result.Iterations);
            }
        }

        return image;
    }

    private NewtonResult[] SolveAllPixels(int width, int height, Viewport viewport)
    {
        var results = new NewtonResult[checked(width * height)];
        Parallel.For(0, height, row =>
        {
            for (var column = 0; column < width; column++)
            {
                var start = AvoidAxes(viewport.ToComplex(column, row, width, height));
                results[row * width + column] = _solver.Solve(start);
            }
        });

        return results;
    }
}
