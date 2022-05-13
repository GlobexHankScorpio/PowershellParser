using log4net;
using System;

namespace PowershellParser.Logs
{
    class Log4NetManager
    {
        private static readonly ILog Log = LogManager.GetLogger("PowershellParser");

        public static void SaveTrace(string info)
        {
            try
            {
                Console.WriteLine(info);
                Log.Info(info);
            }
            catch (Exception e)
            {
                SaveTraceError(e);
            }
        }

        public static void SaveTraceWarn(string warning)
        {
            try
            {
                Console.WriteLine(warning);
                Log.Warn(warning);
            }
            catch (Exception e)
            {
                SaveTraceError(e);
            }
        }

        public static void SaveTraceError(Exception error)
        {
            try
            {
                Log.Error(" Error Message: " + error.Message);
                Log.Error(" StackTrace: " + error.StackTrace);
                Log.Error(" InnerException: " + error.InnerException);
            }
            catch
            {

            }
        }

        public static void SaveTraceError(string ErrorDerscription, Exception error)
        {
            try
            {
                Log.Error(ErrorDerscription);
                Log.Error(" Error Message: " + error.Message);
                Log.Error(" StackTrace: " + error.StackTrace);
                Log.Error(" InnerException: " + error.InnerException);
            }
            catch
            {

            }
        }

        public static void SaveTraceError(string ErrorDerscription)
        {
            try
            {
                Log.Error(ErrorDerscription);
            }
            catch
            {

            }
        }
    }
}