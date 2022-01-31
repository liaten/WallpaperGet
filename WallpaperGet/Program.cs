using System.Net;
namespace WallpaperGet
{
    public static class Program
    {
        public static string path = AppDomain.CurrentDomain.BaseDirectory;
        public static string GetTimeStamp()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
        }
        private static string GetURL(short width, short height)
        {
            return $"https://source.unsplash.com/random/{width}x{height}";
        }
        private static string URL = "";
        private static string TimeStamp = "";
        private static string ImageDir = "";
        private static string ImageFullName = "";
        public static void Download(WebClient OuterWebClient, short width, short height)
        {
            URL = GetURL(width, height);
            TimeStamp = GetTimeStamp();
            ImageDir = path + "Image";
            ImageFullName = $"{ImageDir}/{width}x{height}_{TimeStamp}.jpg";
            if (Directory.Exists(ImageDir))
            {
                //Console.WriteLine($"Directory already exists at directory: {ImageDir}");
                using WebClient client = OuterWebClient;
                client.DownloadFile(new Uri(URL), ImageFullName);
                //Console.WriteLine("The image was saved successfully at {0}.", Directory.GetCreationTime(ImageFullName));
            }
            else
            {
                Directory.CreateDirectory(ImageDir);
                //Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(ImageDir));
                using WebClient client = OuterWebClient;
                client.DownloadFile(new Uri(URL), ImageFullName);
                //Console.WriteLine("The image was saved successfully at {0}.", Directory.GetCreationTime(ImageFullName));
            }
        }
        public static void Main()
        {
            /*
             * System.Windows.SystemParameters.PrimaryScreenWidth
             * System.Windows.SystemParameters.PrimaryScreenHeight
            */
            short width = (short)System.Windows.SystemParameters.PrimaryScreenWidth;
            short height = (short)System.Windows.SystemParameters.PrimaryScreenHeight;
            try { Download(OuterWebClient: new(), height, width); }
            catch { }
            try
            {
                WallpaperManager.Set(ImageFullName, WallpaperManager.Style.Tiled);
                //Console.WriteLine("The wallpaper was set successfully.");
            }
            catch { }
            while (File.Exists(ImageFullName))
            {
                try
                {
                    File.Delete(ImageFullName);
                    //Console.WriteLine("The image was deleted successfully.");
                }
                catch { }
            }
            /*
            Console.Write("Press the Q Key to escape out of program: ");
            while (Console.ReadKey(true).Key != ConsoleKey.Q)
            {
                continue;
            }
            */
        }
    }
}