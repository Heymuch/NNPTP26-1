using NNPTPZ1.Mathematics;

namespace NNPTPZ1.Fractals;

public readonly record struct NewtonResult(Complex Point, int Iterations);

public sealed class NewtonSolver
{
    public const int DefaultRequiredSmallSteps = 30;

    public const double DefaultSmallStepThreshold = 0.5;

    public const int DefaultMaxIterations = 1000;

    private readonly Polynomial _polynomial;
    private readonly Polynomial _derivative;

    public NewtonSolver(Polynomial polynomial)
    {
        ArgumentNullException.ThrowIfNull(polynomial);
        _polynomial = polynomial;
        _derivative = polynomial.Derive();
    }

    public int RequiredSmallSteps { get; init; } = DefaultRequiredSmallSteps;
    public double SmallStepThreshold { get; init; } = DefaultSmallStepThreshold;
    public int MaxIterations { get; init; } = DefaultMaxIterations;

    public NewtonResult Solve(Complex initialGuess)
    {
        var point = initialGuess;
        var iterations = 0;
        var smallSteps = 0;

        while (smallSteps < RequiredSmallSteps && iterations < MaxIterations)
        {
            var step = _polynomial.Evaluate(point) / _derivative.Evaluate(point);
            point -= step;
            iterations++;

            var isLargeStep = step.MagnitudeSquared >= SmallStepThreshold;
            if (!isLargeStep)
            {
                smallSteps++;
            }
        }

        return new NewtonResult(point, iterations);
    }
}
