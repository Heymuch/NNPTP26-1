using NNPTPZ1.Mathematics;

namespace NNPTPZ1Tests.Mathematics;

[TestClass]
public sealed class ComplexTests
{
    private const double Tolerance = 1e-12;

    [TestMethod]
    public void Add_ReturnsComponentWiseSum()
    {
        Assert.AreEqual(new Complex(11, 22), new Complex(10, 20) + new Complex(1, 2));
        Assert.AreEqual(new Complex(1, -1), new Complex(1, -1) + Complex.Zero);
    }

    [TestMethod]
    public void Subtract_ReturnsComponentWiseDifference()
    {
        Assert.AreEqual(new Complex(9, 18), new Complex(10, 20) - new Complex(1, 2));
    }

    [TestMethod]
    public void Negate_FlipsBothSigns()
    {
        Assert.AreEqual(new Complex(-3, 4), -new Complex(3, -4));
    }

    [TestMethod]
    public void Multiply_FollowsComplexMultiplication()
    {
        // (1 + 2i)(3 + 4i) = 3 + 4i + 6i + 8i² = -5 + 10i
        Assert.AreEqual(new Complex(-5, 10), new Complex(1, 2) * new Complex(3, 4));
    }

    [TestMethod]
    public void Multiply_ImaginaryUnitSquared_IsMinusOne()
    {
        var i = new Complex(0, 1);
        Assert.AreEqual(new Complex(-1, 0), i * i);
    }

    [TestMethod]
    public void Divide_IsInverseOfMultiply()
    {
        var a = new Complex(-5, 10);
        var b = new Complex(3, 4);

        var quotient = a / b;

        Assert.AreEqual(1, quotient.Real, Tolerance);
        Assert.AreEqual(2, quotient.Imaginary, Tolerance);
    }

    [TestMethod]
    public void Divide_ByZero_ReturnsNaN()
    {
        var quotient = Complex.One / Complex.Zero;

        Assert.IsTrue(double.IsNaN(quotient.Real));
        Assert.IsTrue(double.IsNaN(quotient.Imaginary));
    }

    [TestMethod]
    public void Magnitude_OfThreeFourIsFive()
    {
        var value = new Complex(3, 4);

        Assert.AreEqual(25, value.MagnitudeSquared);
        Assert.AreEqual(5, value.Magnitude);
    }

    [TestMethod]
    [DataRow(1.0, 0.0, 0.0)]
    [DataRow(0.0, 1.0, Math.PI / 2)]
    [DataRow(-1.0, 0.0, Math.PI)]
    [DataRow(-1.0, -1.0, -3 * Math.PI / 4)]
    public void Phase_ReturnsAngleInRadiansInEveryQuadrant(double real, double imaginary, double expected)
    {
        Assert.AreEqual(expected, new Complex(real, imaginary).Phase, Tolerance);
    }

    [TestMethod]
    public void Conjugate_NegatesImaginaryPart()
    {
        Assert.AreEqual(new Complex(3, -4), new Complex(3, 4).Conjugate);
    }

    [TestMethod]
    public void ImplicitConversionFromDouble_CreatesRealNumber()
    {
        Complex value = 2.5;

        Assert.AreEqual(new Complex(2.5, 0), value);
    }

    [TestMethod]
    [DataRow(10.0, 20.0, "(10 + 20i)")]
    [DataRow(1.0, -1.0, "(1 + -1i)")]
    [DataRow(0.0, 0.0, "(0 + 0i)")]
    [DataRow(1.5, 0.25, "(1.5 + 0.25i)")]
    public void ToString_FormatsWithInvariantCulture(double real, double imaginary, string expected)
    {
        Assert.AreEqual(expected, new Complex(real, imaginary).ToString());
    }
}
