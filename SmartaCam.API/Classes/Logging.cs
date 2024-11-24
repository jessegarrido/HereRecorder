//using Serilog;
//using SeriLogThemesLibrary;
using System.Diagnostics;


namespace SmartaCam
{
    public class SetupLogging
    {
        public static void Development()
        {

            //Log.Logger = new LoggerConfiguration()
            //    .MinimumLevel.Verbose()
            //    .WriteTo.Console(theme: SeriLogCustomThemes.Theme1())
            //    .WriteTo.File(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", $"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}", "Log.txt"),
            //        rollingInterval: RollingInterval.Infinite,
            //        outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] {Message}{NewLine}{Exception}")
            //    .CreateLogger();
        }

        public static void Production()
        {
            //Log.Logger = new LoggerConfiguration()
            //    .WriteTo.File(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LogFiles", "Log.txt"),
            //        rollingInterval: RollingInterval.Day)
            //    .CreateBootstrapLogger();
        }
        public class DbContextToFileLogger
        {
            /// <summary>
            /// Log file name
            /// </summary>
            private readonly string _fileName =
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                    "Logs", $"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}", $"EF_Log.txt");

            /// <summary>
            /// Use to override log file name and path
            /// </summary>
            /// <param name="fileName"></param>
            public DbContextToFileLogger(string fileName)
            {
                _fileName = fileName;
            }

            /// <summary>
            /// Setup to use default file name for logging
            /// </summary>
            public DbContextToFileLogger()
            {

            }
            /// <summary>
            /// append message to the existing stream
            /// </summary>
            /// <param name="message"></param>
            [DebuggerStepThrough]
            public void Log(string message)
            {

                if (!File.Exists(_fileName))
                {
                    File.CreateText(_fileName).Close();
                }

                StreamWriter streamWriter = new(_fileName, true);

                streamWriter.WriteLine(message);

                streamWriter.WriteLine(new string('-', 40));

                streamWriter.Flush();
                streamWriter.Close();
            }
        }
    }
}
