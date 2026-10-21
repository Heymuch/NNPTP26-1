using NNPTPZ1.Mathematics;

namespace NNPTPZ1.Fractals;

public sealed class RootRegistry
{
    public const double DefaultToleranceSquared = 0.01;

    private readonly List<Complex> _roots = [];
    private readonly double _toleranceSquared;

    public RootRegistry(double toleranceSquared = DefaultToleranceSquared)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(toleranceSquared);
        _toleranceSquared = toleranceSquared;
    }

    public IReadOnlyList<Complex> Roots => _roots;

    public int GetOrAddRootIndex(Complex point)
    {
        var index = _roots.FindIndex(root => (root - point).MagnitudeSquared <= _toleranceSquared);
        if (index >= 0)
        {
            return index;
        }

        _roots.Add(point);
        return _roots.Count - 1;
    }
}
