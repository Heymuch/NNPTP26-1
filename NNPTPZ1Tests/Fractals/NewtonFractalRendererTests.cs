using NNPTPZ1.Fractals;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1Tests.Fractals;

[TestClass]
public sealed class NewtonFractalRendererTests
{
    private static readonly Polynomial CubicPlusOne = new(1, 0, 0, 1);
    private static readonly Viewport DefaultViewport = new(-2, 2, -2, 2);

    [TestMethod]
    public void Render_NonSquareImage_HasRequestedSize()
    {
        var image = new NewtonFractalRenderer(CubicPlusOne).Render(40, 20, DefaultViewport);

        Assert.AreEqual(40, image.Width);
        Assert.AreEqual(20, image.Height);
    }

    [TestMethod]
    public void Render_IsDeterministic()
    {
        var renderer = new NewtonFractalRenderer(CubicPlusOne);

        var first = renderer.Render(64, 64, DefaultViewport);
        var second = renderer.Render(64, 64, DefaultViewport);

        for (var row = 0; row < 64; row++)
        {
            for (var column = 0; column < 64; column++)
            {
                Assert.AreEqual(first[column, row], second[column, row], $"Pixel ({column}, {row}) differs.");
            }
        }
    }

    [TestMethod]
    public void Render_ColorsEachRootBasinDifferently()
    {
        var palette = new ColorPalette([RgbColor.Red, RgbColor.Green, RgbColor.Blue], darkeningPerIteration: 0);
        var image = new NewtonFractalRenderer(CubicPlusOne, palette).Render(3, 3, DefaultViewport);

        // The first pixel (-2, -2) lies in the basin of root ½ - (√3/2)i, which is found first.
        var firstRootColor = image[0, 0];
        Assert.AreEqual(RgbColor.Red, firstRootColor);

        var distinctColors = Enumerable.Range(0, 9).Select(i => image[i % 3, i / 3]).Distinct().Count();
        Assert.AreEqual(3, distinctColors);
    }
}
