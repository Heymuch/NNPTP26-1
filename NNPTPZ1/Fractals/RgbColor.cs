namespace NNPTPZ1.Fractals;

public readonly record struct RgbColor(byte R, byte G, byte B)
{
    public static readonly RgbColor Red = new(255, 0, 0);
    public static readonly RgbColor Blue = new(0, 0, 255);
    public static readonly RgbColor Green = new(0, 128, 0);
    public static readonly RgbColor Yellow = new(255, 255, 0);
    public static readonly RgbColor Orange = new(255, 165, 0);
    public static readonly RgbColor Fuchsia = new(255, 0, 255);
    public static readonly RgbColor Gold = new(255, 215, 0);
    public static readonly RgbColor Cyan = new(0, 255, 255);
    public static readonly RgbColor Magenta = new(255, 0, 255);

    public RgbColor Darken(int amount) => new(Subtract(R, amount), Subtract(G, amount), Subtract(B, amount));

    private static byte Subtract(byte channel, int amount) => (byte)Math.Clamp(channel - amount, 0, 255);
}
