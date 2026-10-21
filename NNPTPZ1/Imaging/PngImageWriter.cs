using NNPTPZ1.Fractals;
using SkiaSharp;

namespace NNPTPZ1.Imaging;

public static class PngImageWriter
{
    private const int PngQuality = 100;

    public static void Save(FractalImage image, string path)
    {
        ArgumentNullException.ThrowIfNull(image);
        ArgumentException.ThrowIfNullOrWhiteSpace(path);

        using var bitmap = new SKBitmap(image.Width, image.Height, SKColorType.Rgba8888, SKAlphaType.Opaque);
        for (var row = 0; row < image.Height; row++)
        {
            for (var column = 0; column < image.Width; column++)
            {
                var color = image[column, row];
                bitmap.SetPixel(column, row, new SKColor(color.R, color.G, color.B));
            }
        }

        var directory = Path.GetDirectoryName(Path.GetFullPath(path));
        if (!string.IsNullOrEmpty(directory))
        {
            Directory.CreateDirectory(directory);
        }

        using var data = bitmap.Encode(SKEncodedImageFormat.Png, PngQuality);
        using var file = File.Create(path);
        data.SaveTo(file);
    }
}
