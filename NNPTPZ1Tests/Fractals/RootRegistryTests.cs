using NNPTPZ1.Fractals;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1Tests.Fractals;

[TestClass]
public sealed class RootRegistryTests
{
    [TestMethod]
    public void GetOrAddRootIndex_NumbersNewRootsFromZero()
    {
        var registry = new RootRegistry();

        Assert.AreEqual(0, registry.GetOrAddRootIndex(new Complex(-1, 0)));
        Assert.AreEqual(1, registry.GetOrAddRootIndex(new Complex(0.5, 0.87)));
        Assert.AreEqual(2, registry.GetOrAddRootIndex(new Complex(0.5, -0.87)));
        Assert.HasCount(3, registry.Roots);
    }

    [TestMethod]
    public void GetOrAddRootIndex_PointWithinTolerance_ReturnsKnownRoot()
    {
        var registry = new RootRegistry();
        registry.GetOrAddRootIndex(new Complex(-1, 0));
        registry.GetOrAddRootIndex(new Complex(0.5, 0.87));

        Assert.AreEqual(0, registry.GetOrAddRootIndex(new Complex(-1.05, 0.05)));
        Assert.AreEqual(1, registry.GetOrAddRootIndex(new Complex(0.45, 0.9)));
        Assert.HasCount(2, registry.Roots);
    }

    [TestMethod]
    public void GetOrAddRootIndex_PointOutsideTolerance_AddsNewRoot()
    {
        var registry = new RootRegistry(toleranceSquared: 0.01);
        registry.GetOrAddRootIndex(Complex.Zero);

        Assert.AreEqual(1, registry.GetOrAddRootIndex(new Complex(0.2, 0)));
    }
}
