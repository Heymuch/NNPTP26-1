using System.Globalization;

namespace NNPTPZ1.Mathematics;

public readonly record struct Complex(double Real, double Imaginary)
{
    public static readonly Complex Zero = new(0, 0);
    public static readonly Complex One = new(1, 0);

    public double MagnitudeSquared => Real * Real + Imaginary * Imaginary;

    public double Magnitude => Math.Sqrt(MagnitudeSquared);

    public double Phase => Math.Atan2(Imaginary, Real);

    public Complex Conjugate => new(Real, -Imaginary);

    public static Complex FromReal(double real) => new(real, 0);

    public static implicit operator Complex(double real) => FromReal(real);

    public static Complex operator +(Complex a, Complex b) =>
        new(a.Real + b.Real, a.Imaginary + b.Imaginary);

    public static Complex operator -(Complex a, Complex b) =>
        new(a.Real - b.Real, a.Imaginary - b.Imaginary);

    public static Complex operator -(Complex value) =>
        new(-value.Real, -value.Imaginary);

    public static Complex operator *(Complex a, Complex b) =>
        new(a.Real * b.Real - a.Imaginary * b.Imaginary,
            a.Real * b.Imaginary + a.Imaginary * b.Real);

    public static Complex operator /(Complex a, Complex b)
    {
        var numerator = a * b.Conjugate;
        var denominator = b.MagnitudeSquared;
        return new(numerator.Real / denominator, numerator.Imaginary / denominator);
    }

    public static Complex Add(Complex a, Complex b) => a + b;

    public static Complex Subtract(Complex a, Complex b) => a - b;

    public static Complex Multiply(Complex a, Complex b) => a * b;

    public static Complex Divide(Complex a, Complex b) => a / b;

    public static Complex Negate(Complex value) => -value;

    public override string ToString() =>
        string.Create(CultureInfo.InvariantCulture, $"({Real} + {Imaginary}i)");
}
