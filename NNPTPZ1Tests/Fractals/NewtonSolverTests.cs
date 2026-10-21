using NNPTPZ1.Fractals;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1Tests.Fractals;

[TestClass]
public sealed class NewtonSolverTests
{
    private const double Tolerance = 1e-9;

    // x³ + 1 has roots -1 and ½ ± (√3/2)i
    private static readonly Polynomial CubicPlusOne = new(1, 0, 0, 1);

    [TestMethod]
    [DataRow(-1.5, 0.1, -1.0, 0.0)]
    [DataRow(1.0, 1.0, 0.5, 0.8660254037844386)]
    [DataRow(1.0, -1.0, 0.5, -0.8660254037844386)]
    public void Solve_ConvergesToNearbyRoot(double startReal, double startImaginary, double rootReal, double rootImaginary)
    {
        var result = new NewtonSolver(CubicPlusOne).Solve(new Complex(startReal, startImaginary));

        Assert.AreEqual(rootReal, result.Point.Real, Tolerance);
        Assert.AreEqual(rootImaginary, result.Point.Imaginary, Tolerance);
    }

    [TestMethod]
    public void Solve_StartingCloseToRoot_TakesOnlyRequiredSmallSteps()
    {
        var result = new NewtonSolver(CubicPlusOne).Solve(new Complex(-1.01, 0.001));

        Assert.AreEqual(NewtonSolver.DefaultRequiredSmallSteps, result.Iterations);
    }

    [TestMethod]
    public void Solve_StartingFarAway_NeedsAdditionalLargeSteps()
    {
        var result = new NewtonSolver(CubicPlusOne).Solve(new Complex(100, 100));

        Assert.IsGreaterThan(NewtonSolver.DefaultRequiredSmallSteps, result.Iterations);
    }

    [TestMethod]
    public void Solve_WhenStepsNeverGetSmall_StopsAtMaxIterations()
    {
        var solver = new NewtonSolver(CubicPlusOne) { SmallStepThreshold = 0, MaxIterations = 50 };

        var result = solver.Solve(new Complex(1, 1));

        Assert.AreEqual(50, result.Iterations);
    }
}
