using NNPTPZ1.Mathematics;

namespace NNPTPZ1.Fractals;

public sealed record Viewport
{
    public double XMin { get; }
    public double XMax { get; }
    public double YMin { get; }
    public double YMax { get; }

    public Viewport(double xMin, double xMax, double yMin, double yMax)
    {
        if (!(xMin < xMax))
        {
            throw new ArgumentException($"xMin ({xMin}) must be less than xMax ({xMax}).", nameof(xMin));
        }

        if (!(yMin < yMax))
        {
            throw new ArgumentException($"yMin ({yMin}) must be less than yMax ({yMax}).", nameof(yMin));
        }

        XMin = xMin;
        XMax = xMax;
        YMin = yMin;
        YMax = yMax;
    }

    public Complex ToComplex(int column, int row, int width, int height)
    {
        var xStep = (XMax - XMin) / width;
        var yStep = (YMax - YMin) / height;
        return new Complex(XMin + column * xStep, YMin + row * yStep);
    }
}
