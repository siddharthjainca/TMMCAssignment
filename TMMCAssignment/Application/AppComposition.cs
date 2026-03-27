using TMMCAssignment.Core;
using TMMCAssignment.Detection;
using TMMCAssignment.Infrastructure;
using TMMCAssignment.Services;

namespace TMMCAssignment.Application;

//Glue interfaces to classes
public static class AppComposition
{
    public static VerticalLineAnalysisService CreateVerticalLineAnalysisService()
    {
        IImageLoader loader = new JpegImageLoader();
        ILineDetector detector = new MiddleRowTransitionLineDetector();
        return new VerticalLineAnalysisService(loader, detector);
    }
}
