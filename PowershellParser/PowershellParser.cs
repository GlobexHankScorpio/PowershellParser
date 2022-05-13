using System;
using System.Linq;
using System.Text;
using System.Management.Automation;
using System.IO;
using System.Collections.ObjectModel;
using PowershellParser.Logs;

namespace PowershellParser
{
    public class PowershellParser
    {
        #region public

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">The <T> class to be serialized</typeparam>
        /// <param name="paramsAndValues">The object to be serialized with its values.</param>
        /// <param name="powershellPath">The powershell path to be executed.</param>
        /// <returns></returns>
        public static bool ExecuteScript<T>(T paramsAndValues, string powershellPath)
        {
            return PrepareAndExecute<T>(JsonParser.ObjectToJson(paramsAndValues), GetPowershellScript(powershellPath));
        }
        #endregion public

        #region private
        private static byte[] GetPowershellScript(string psPath)
        {
            return File.ReadAllBytes(psPath);
        }

        /// <summary>
        /// Will iterate over all properties of the class and will add its values as parameters in the powershell.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Properties"></param>
        /// <param name="ps"></param>
        private static void AddParams<T>(T Properties, PowerShell ps)
        {
            foreach (var param in Properties.GetType().GetProperties())
            {
                if (param.PropertyType.IsArray)
                {
                    object[] array = (object[])param.GetValue(Properties);
                    ps.AddParameter(param.Name, array);
                }

                else
                    ps.AddParameter(param.Name, param.GetValue(Properties).ToString());
            }
        }

        /// <summary>
        /// Executes the powershell
        /// </summary>
        /// <param name="ps"></param>
        /// <returns></returns>
        private static bool Execute(PowerShell ps)
        {
            try
            {
                Collection<PSObject> PSOutput = ps.Invoke();

                if (ps.Streams.Error.Any())
                {
                    Collection<ErrorRecord> errors = ps.Streams.Error.ReadAll();

                    foreach (ErrorRecord error in errors)                    
                        Log4NetManager.SaveTraceError($"Error message: {error.Exception.Message}");

                    return false;
                }

                return true;
            }

            catch (RuntimeException ex)
            {
                Log4NetManager.SaveTraceError($"Runtime execution exception message: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Log4NetManager.SaveTraceError($"Execution exception message: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Prepares the powershell values and params to be executed.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonConfig"></param>
        /// <param name="powershellScript"></param>
        /// <returns></returns>
        private static bool PrepareAndExecute<T>(string jsonConfig, byte[] powershellScript)
        {
            //Serialize json and create powershell script
            T json = JsonParser.JsonToObject<T>(jsonConfig);

            PowerShell Powershell = PowerShell.Create().AddScript(Encoding.ASCII.GetString(powershellScript));

            //Add params and execute
            AddParams(json, Powershell);
            return Execute(Powershell);
        }

        #endregion private
    }
}