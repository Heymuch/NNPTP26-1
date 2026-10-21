using System.Globalization;
using NNPTPZ1.CommandLine;

namespace NNPTPZ1Tests.CommandLine;

[TestClass]
public sealed class CommandLineOptionsTests
{
    [TestMethod]
    public void Parse_ValidArguments_ReturnsOptions()
    {
        var options = CommandLineOptions.Parse(["800", "600", "-2", "2", "-1.5", "1.5", "fractal.png"]);

        Assert.AreEqual(800, options.Width);
        Assert.AreEqual(600, options.Height);
        Assert.AreEqual(-2, options.Viewport.XMin);
        Assert.AreEqual(2, options.Viewport.XMax);
        Assert.AreEqual(-1.5, options.Viewport.YMin);
        Assert.AreEqual(1.5, options.Viewport.YMax);
        Assert.AreEqual("fractal.png", options.OutputPath);
    }

    [TestMethod]
    public void Parse_WithoutOutputPath_UsesDefault()
    {
        var options = CommandLineOptions.Parse(["10", "10", "-1", "1", "-1", "1"]);

        Assert.AreEqual(CommandLineOptions.DefaultOutputPath, options.OutputPath);
    }

    [TestMethod]
    public void Parse_IgnoresCurrentCulture()
    {
        var originalCulture = CultureInfo.CurrentCulture;
        try
        {
            // Czech culture uses ',' as decimal separator
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("cs-CZ");

            var options = CommandLineOptions.Parse(["10", "10", "-1.5", "1.5", "-1", "1"]);

            Assert.AreEqual(-1.5, options.Viewport.XMin);
        }
        finally
        {
            CultureInfo.CurrentCulture = originalCulture;
        }
    }

    [TestMethod]
    [DataRow(new[] { "10", "10", "-1", "1", "-1" }, DisplayName = "too few arguments")]
    [DataRow(new[] { "10", "10", "-1", "1", "-1", "1", "a.png", "extra" }, DisplayName = "too many arguments")]
    [DataRow(new[] { "0", "10", "-1", "1", "-1", "1" }, DisplayName = "zero width")]
    [DataRow(new[] { "10", "-5", "-1", "1", "-1", "1" }, DisplayName = "negative height")]
    [DataRow(new[] { "ten", "10", "-1", "1", "-1", "1" }, DisplayName = "non-numeric width")]
    [DataRow(new[] { "10", "10", "abc", "1", "-1", "1" }, DisplayName = "non-numeric coordinate")]
    [DataRow(new[] { "10", "10", "NaN", "1", "-1", "1" }, DisplayName = "NaN coordinate")]
    [DataRow(new[] { "10", "10", "1", "-1", "-1", "1" }, DisplayName = "inverted x range")]
    public void Parse_InvalidArguments_Throws(string[] args)
    {
        Assert.ThrowsExactly<ArgumentException>(() => CommandLineOptions.Parse(args));
    }
}
