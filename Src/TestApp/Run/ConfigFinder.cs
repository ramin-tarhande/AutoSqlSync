namespace TestApp.Run
{
    public class ConfigFinder
    {
        const string folder = "ConfigFiles";
        
        public static string MainConfig()
        {
            return MakePath("AutoSqlSync.config");
        }

        public static string LogConfig()
        {
            return MakePath("TestApp.log.config");
        }

        static string MakePath(string file)
        {
            return System.IO.Path.Combine(folder, file);
        }
    }
}
