using NNPTPZ1.Mathematics;

namespace NNPTPZ1Tests.Mathematics;

[TestClass]
public sealed class PolynomialTests
{
    // 1 + x²
    private static readonly Polynomial OnePlusXSquared = new(Complex.One, Complex.Zero, Complex.One);

    [TestMethod]
    [DataRow(0.0, 1.0)]
    [DataRow(1.0, 2.0)]
    [DataRow(2.0, 5.0)]
    [DataRow(-3.0, 10.0)]
    public void Evaluate_AtRealPoint_ReturnsExpectedValue(double x, double expected)
    {
        Assert.AreEqual(Complex.FromReal(expected), OnePlusXSquared.Evaluate(x));
    }

    [TestMethod]
    public void Evaluate_AtRoot_ReturnsZero()
    {
        // 1 + x² has roots ±i
        Assert.AreEqual(Complex.Zero, OnePlusXSquared.Evaluate(new Complex(0, 1)));
        Assert.AreEqual(Complex.Zero, OnePlusXSquared.Evaluate(new Complex(0, -1)));
    }

    [TestMethod]
    public void Evaluate_WithoutCoefficients_ReturnsZero()
    {
        Assert.AreEqual(Complex.Zero, new Polynomial().Evaluate(new Complex(3, 4)));
    }

    [TestMethod]
    public void Derive_AppliesPowerRule()
    {
        // d/dx (1 + 2x + 3x²) = 2 + 6x
        var polynomial = new Polynomial(1, 2, 3);

        var derivative = polynomial.Derive();

        CollectionAssert.AreEqual(new Complex[] { 2, 6 }, derivative.Coefficients.ToArray());
    }

    [TestMethod]
    public void Derive_OfConstant_HasNoCoefficients()
    {
        var derivative = new Polynomial(7).Derive();

        Assert.AreEqual(-1, derivative.Degree);
        Assert.AreEqual(Complex.Zero, derivative.Evaluate(5));
    }

    [TestMethod]
    public void Degree_IsHighestPower()
    {
        Assert.AreEqual(2, OnePlusXSquared.Degree);
    }

    [TestMethod]
    public void Constructor_CopiesCoefficients()
    {
        var coefficients = new Complex[] { 1, 2 };
        var polynomial = new Polynomial(coefficients);

        coefficients[0] = 100;

        Assert.AreEqual(Complex.One, polynomial.Coefficients[0]);
    }

    [TestMethod]
    public void ToString_ListsTermsInAscendingPower()
    {
        Assert.AreEqual("(1 + 0i) + (0 + 0i)x + (1 + 0i)xx", OnePlusXSquared.ToString());
    }
}
