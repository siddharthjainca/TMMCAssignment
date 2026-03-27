using System.Drawing;
using System.Drawing.Imaging;
using TMMCAssignment.Detection;

namespace TMMCAssignment.Tests
{
    public class MiddleRowTransitionLineDetectorTest
    {
        [Fact]
        public void CountVerticalLines_SingleVerticalBar_ReturnsOne()
        {
            using Bitmap bitmap = CreateBitmapWithVerticalBars(width: 100, height: 80, barXs: new[] { 50 }, barWidth: 6);
            var detector = new MiddleRowTransitionLineDetector();

            int result = detector.CountVerticalLines(bitmap);

            Assert.Equal(1, result);
        }

        [Fact]
        public void CountVerticalLines_ThreeVerticalBars_ReturnsThree()
        {
            using Bitmap bitmap = CreateBitmapWithVerticalBars(
                width: 150,
                height: 80,
                barXs: new[] { 20, 70, 120 },
                barWidth: 6);
            var detector = new MiddleRowTransitionLineDetector();

            int result = detector.CountVerticalLines(bitmap);

            Assert.Equal(3, result);
        }

        [Fact]
        public void CountVerticalLines_ThickBarCountsAsSingleLine()
        {
            using Bitmap bitmap = CreateBitmapWithVerticalBars(width: 120, height: 80, barXs: new[] { 60 }, barWidth: 18);
            var detector = new MiddleRowTransitionLineDetector();

            int result = detector.CountVerticalLines(bitmap);

            Assert.Equal(1, result);
        }

        private static Bitmap CreateBitmapWithVerticalBars(int width, int height, int[] barXs, int barWidth)
        {
            var bitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            using var g = Graphics.FromImage(bitmap);
            g.Clear(Color.White);

            foreach (int x in barXs)
            {
                g.FillRectangle(Brushes.Black, x, 0, barWidth, height);
            }

            return bitmap;
        }
    }
}
