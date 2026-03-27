using System.Drawing;
using System.Drawing.Imaging;
using TMMCAssignment.Core;

namespace TMMCAssignment.Infrastructure;

//Loads images and normalizes it
public sealed class JpegImageLoader : IImageLoader
{
    public Bitmap Load(string absolutePath)
    {
        if (string.IsNullOrWhiteSpace(absolutePath))
            throw new ArgumentException(nameof(absolutePath));

        if (!File.Exists(absolutePath))
        {
            throw new FileNotFoundException($"Image file not found: {absolutePath}", absolutePath);
        }

        using var decoded = Image.FromFile(absolutePath);
        using var source = new Bitmap(decoded);

        if (source.PixelFormat == PixelFormat.Format24bppRgb)
        {
            return new Bitmap(source);
        }

        var converted = new Bitmap(source.Width, source.Height, PixelFormat.Format24bppRgb);
        using (var g = Graphics.FromImage(converted))
        {
            g.DrawImage(source, 0, 0);
        }

        return converted;
    }
}
