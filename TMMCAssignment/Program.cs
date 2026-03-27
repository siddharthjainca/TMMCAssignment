using TMMCAssignment.Application;

namespace TMMCAssignment;

internal static class Program
{
    private static int Main(string[] args)
    {
        try
        {
            return Run(args);
        }
        catch (Exception ex)
        {
            Console.WriteLine(FriendlyError.Describe(ex));
            Console.WriteLine(ex);
            return 1;
        }
    }

    private static int Run(string[] args)
    {
        if (args.Length == 0)
        {
            RunInteractiveLoop();
            return 0;
        }

        if (args.Length != 1)
        {
            Console.WriteLine("Please provide full path to an image file.");
            return 1;
        }

        string path = args[0];
        if (string.IsNullOrWhiteSpace(path))
        {
            Console.WriteLine("Please provide full path for a image file.");
            return 1;
        }

        var service = AppComposition.CreateVerticalLineAnalysisService();

        try
        {
            int count = service.Analyze(path);
            Console.WriteLine(count);
            return 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine(FriendlyError.Describe(ex));
            Console.WriteLine(ex);
            return 1;
        }
    }

    private static void RunInteractiveLoop()
    {
        Console.WriteLine("TMMCAssignment — counts vertical black lines.");
        Console.WriteLine("Please provide full path for a image file, or press Enter / type exit to quit.");
        Console.WriteLine();

        var service = AppComposition.CreateVerticalLineAnalysisService();

        while (true)
        {
            Console.Write("Image path: ");
            string? line = Console.ReadLine();
            if (line == null)
            {
                break;
            }

            line = line.Trim();
            if (line.Length == 0)
            {
                break;
            }

            if (line.Equals("exit", StringComparison.OrdinalIgnoreCase) ||
                line.Equals("quit", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }

            try
            {
                int count = service.Analyze(line);
                Console.WriteLine($"Number of vertical lines: {count}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(FriendlyError.Describe(ex));
                Console.WriteLine(ex);
            }

            Console.WriteLine();
        }
    }
}
