using NNPTPZ1.Fractals;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1Tests.Fractals;

[TestClass]
public sealed class ViewportTests
{
    [TestMethod]
    public void ToComplex_MapsPixelGridOntoRegion()
    {
        var viewport = new Viewport(-2, 2, -1, 1);

        Assert.AreEqual(new Complex(-2, -1), viewport.ToComplex(0, 0, width: 4, height: 2));
        Assert.AreEqual(new Complex(1, 0), viewport.ToComplex(3, 1, width: 4, height: 2));
    }

    [TestMethod]
    [DataRow(1.0, 1.0, 0.0, 1.0)]
    [DataRow(2.0, 1.0, 0.0, 1.0)]
    [DataRow(0.0, 1.0, 1.0, 1.0)]
    [DataRow(0.0, 1.0, 2.0, 1.0)]
    public void Constructor_EmptyOrInvertedRange_Throws(double xMin, double xMax, double yMin, double yMax)
    {
        Assert.ThrowsExactly<ArgumentException>(() => new Viewport(xMin, xMax, yMin, yMax));
    }
}
