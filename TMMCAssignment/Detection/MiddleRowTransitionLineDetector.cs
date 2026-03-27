using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using TMMCAssignment.Core;

namespace TMMCAssignment.Detection;

//Check the middle row for white-to-black transitions
//Consider slightly darker pixels as black
public sealed class MiddleRowTransitionLineDetector : ILineDetector
{
    private const byte BrightnessThreshold = 128;

    public int CountVerticalLines(Bitmap bitmap)
    {
        if (bitmap == null) throw new ArgumentNullException(nameof(bitmap));

        if (bitmap.Width == 0 || bitmap.Height == 0)
        {
            return 0;
        }

        int midY = bitmap.Height / 2;
        bool[] isBlack = SampleBlackMaskRow(bitmap, midY);

        return CountBlackRunsFromLeft(isBlack);
    }

    private static int CountBlackRunsFromLeft(bool[] isBlack)
    {
        int count = 0;
        bool inBlack = false;
        bool anyWhite = false;

        foreach (bool black in isBlack)
        {
            if (!black)
            {
                anyWhite = true;
                inBlack = false;
                continue;
            }

            if (!inBlack)
            {
                count++;
                inBlack = true;
            }
        }

        //If cmoplete image is black
        if (!anyWhite && isBlack.Length > 0)
        {
            return 1;
        }

        return count;
    }

    private static bool[] SampleBlackMaskRow(Bitmap bitmap, int y)
    {
        int width = bitmap.Width;
        var mask = new bool[width];
        var bounds = new Rectangle(0, y, width, 1);

        BitmapData data = bitmap.LockBits(bounds, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

        try
        {
            int stride = data.Stride;
            int rowBytes = Math.Abs(stride);
            byte[] buffer = new byte[rowBytes];

            Marshal.Copy(data.Scan0, buffer, 0, rowBytes);

            int bytesPerPixel = 3;

            for (int x = 0; x < width; x++)
            {
                int i = x * bytesPerPixel;
                byte b = buffer[i];
                byte g = buffer[i + 1];
                byte r = buffer[i + 2];
            
                mask[x] = GetAverageBrightness(r, g, b) < BrightnessThreshold;
            }
        }
        finally
        {
            bitmap.UnlockBits(data);
        }

        return mask;
    }

    private static int GetAverageBrightness(byte r, byte g, byte b)
    {
        return (r + g + b) / 3;
    }
}
