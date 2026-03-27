using System.Drawing;

namespace TMMCAssignment.Core;

public interface IImageLoader
{
    Bitmap Load(string absolutePath);
}
