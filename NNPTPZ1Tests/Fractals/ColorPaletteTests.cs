using NNPTPZ1.Fractals;

namespace NNPTPZ1Tests.Fractals;

[TestClass]
public sealed class ColorPaletteTests
{
    private static readonly ColorPalette TwoColors = new([RgbColor.Red, RgbColor.Cyan]);

    [TestMethod]
    public void GetColor_WithoutIterations_ReturnsBaseColor()
    {
        Assert.AreEqual(RgbColor.Red, TwoColors.GetColor(rootIndex: 0, iterations: 0));
        Assert.AreEqual(RgbColor.Cyan, TwoColors.GetColor(rootIndex: 1, iterations: 0));
    }

    [TestMethod]
    public void GetColor_WrapsAroundWhenThereAreMoreRootsThanColors()
    {
        Assert.AreEqual(RgbColor.Red, TwoColors.GetColor(rootIndex: 2, iterations: 0));
    }

    [TestMethod]
    public void GetColor_DarkensByTwoPerIteration()
    {
        Assert.AreEqual(new RgbColor(195, 0, 0), TwoColors.GetColor(rootIndex: 0, iterations: 30));
    }

    [TestMethod]
    public void GetColor_ClampsToBlack()
    {
        var black = new RgbColor(0, 0, 0);

        Assert.AreEqual(black, TwoColors.GetColor(rootIndex: 0, iterations: 200));
        Assert.AreEqual(black, TwoColors.GetColor(rootIndex: 0, iterations: int.MaxValue));
    }

    [TestMethod]
    public void Constructor_WithoutColors_Throws()
    {
        Assert.ThrowsExactly<ArgumentException>(() => new ColorPalette([]));
    }
}
