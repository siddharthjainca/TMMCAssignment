using System.Drawing;
using TMMCAssignment.Core;

namespace TMMCAssignment.Services;

public sealed class VerticalLineAnalysisService
{
    private readonly IImageLoader _imageLoader;
    private readonly ILineDetector _lineDetector;

    public VerticalLineAnalysisService(IImageLoader imageLoader, ILineDetector lineDetector)
    {
        _imageLoader = imageLoader ?? throw new ArgumentNullException(nameof(imageLoader));
        _lineDetector = lineDetector ?? throw new ArgumentNullException(nameof(lineDetector));
    }

    public int Analyze(string absoluteImagePath)
    {
        using Bitmap bitmap = _imageLoader.Load(absoluteImagePath);
        return _lineDetector.CountVerticalLines(bitmap);
    }
}
