using System.Text;

namespace NNPTPZ1.Mathematics;

public sealed class Polynomial
{
    public IReadOnlyList<Complex> Coefficients => _coefficients;
    public int Degree => _coefficients.Length - 1;

    private readonly Complex[] _coefficients;

    public Polynomial(params IEnumerable<Complex> coefficients)
    {
        ArgumentNullException.ThrowIfNull(coefficients);
        _coefficients = [.. coefficients];
    }

    public Polynomial Derive() =>
        new(_coefficients.Skip(1).Select((coefficient, index) => coefficient * (index + 1)));

    public Complex Evaluate(Complex x)
    {
        var result = Complex.Zero;
        for (var power = _coefficients.Length - 1; power >= 0; power--)
        {
            result = result * x + _coefficients[power];
        }

        return result;
    }

    public override string ToString()
    {
        var builder = new StringBuilder();
        for (var power = 0; power < _coefficients.Length; power++)
        {
            if (power > 0)
            {
                builder.Append(" + ");
            }

            builder.Append(_coefficients[power]).Append('x', power);
        }

        return builder.ToString();
    }
}
