namespace TMMCAssignment;

internal static class FriendlyError
{
    public static string Describe(Exception ex)
    {
        if (IsSystemDrawingExternal(ex))
        {
            return "Can't decode the image. The file may be damaged or not a supported format.";
        }

        switch (ex)
        {
            case FileNotFoundException:
                return "File doesn't exist. Check the path and try again.";
            case DirectoryNotFoundException:
                return "Folder doesn't exist or the path is wrong.";
            case ArgumentException:
                return "The path isn't valid.";
            case IOException:
                return "Can't read the file.";
            case OutOfMemoryException:
                return "Can't load the image.";
            default:
                return "Something went wrong while opening or reading the image.";
        }
    }

    private static bool IsSystemDrawingExternal(Exception ex)
    {
        return string.Equals(ex.GetType().FullName, "System.Drawing.ExternalException", StringComparison.Ordinal);
    }
}
