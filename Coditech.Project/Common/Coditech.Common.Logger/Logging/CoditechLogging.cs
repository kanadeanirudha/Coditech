using Coditech.Common.Helper;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace Coditech.Common.Logger
{
    public class CoditechLogging : ICoditechLogging
    {
        private static readonly string todaysDate = DateTime.Now.ToString("yyyy-MM-dd");
        private static readonly IHostingEnvironment _webHostEnvironment = CoditechDependencyResolver.GetService<IHostingEnvironment>();
        private static IConfigurationSection settings = CoditechDependencyResolver.GetService<IConfiguration>().GetSection("appsettings");

        #region Declarations

        public const string LogComponentFilePath = "data/logs/{yyyy-mm-dd}/Coditech_{ComponentName}_Log.log";
        public const string LogFilePath = "data/logs/{yyyy-mm-dd}/Coditech_Log.log";

        #endregion
        #region Enum Mode

        // Specifies the text file access modes.
        public enum Mode
        {
            // Indicates text file read mode operation.
            Read,

            // Indicates text file write mode operation. It deletes the previous content.
            Write,

            // Indicates text file append mode operation. it preserves the previous content.
            Append
        }

        #endregion

        public void LogMessage(Exception ex, string componentName = "", TraceLevel traceLevel = TraceLevel.Info, object obj = null)
        {
            if (traceLevel == TraceLevel.Error)
            {
                if (string.IsNullOrEmpty(componentName))
                {
                    WriteLogFiles(ex.ToString(), LogFilePath, componentName);
                }
                else
                {
                    WriteLogFiles(ex.ToString(), LogComponentFilePath, componentName);
                }
            }
        }

        public void LogMessage(string message, string componentName = "", TraceLevel traceLevel = TraceLevel.Info, object obj = null, [CallerMemberName] string methodName = "", [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0)
        {
            if (traceLevel == TraceLevel.Error)
            {
                if (string.IsNullOrEmpty(componentName))
                {
                    WriteLogFiles(message, LogFilePath, componentName);
                }
                else
                {
                    WriteLogFiles(message, LogComponentFilePath, componentName);
                }
            }
        }

        #region Private Methods


        // Checks the web.config to see if text file logging is enabled.
        // Returns True - If logging is enabled, False - If logging is disabled.</returns>
        private bool FileLoggingEnabled()
        {
            if (settings["EnableFileLogging"] != null)
            {
                return Convert.ToString(settings["EnableFileLogging"]).Equals("1");
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// This method will write the messages to the log file for Admin as well as for demo site.
        /// </summary>
        /// <param name="message">string message</param>
        /// <param name="logFilePath">string log file path</param>
        private void WriteLogFiles(string message, string logFilePath, string componentName)
        {
            if (FileLoggingEnabled())
            {
                StringBuilder errMsg = new StringBuilder();
                errMsg.AppendLine();
                errMsg.AppendLine();
                errMsg.AppendLine("*************************");
                errMsg.AppendLine("TimeStamp: " + DateTime.Now.ToString("yyyy-MM-dd"));
                errMsg.AppendLine(message);
                errMsg.AppendLine("*************************");

                string filePath = logFilePath.Replace("{yyyy-mm-dd}", todaysDate);
                if (!string.IsNullOrEmpty(componentName))
                {
                    filePath = filePath.Replace("{ComponentName}", componentName);
                }
                WriteTextStorage(errMsg.ToString(), filePath, Mode.Append);
            }
        }

        /// <summary>
        /// Writes text file to persistant storage.
        /// </summary>
        /// <param name="fileData">Specify the string that has the file content.</param>
        /// <param name="filePath">Specify the relative file path.</param>
        /// <param name="fileMode">Specify the file write mode operatation. </param>
        private static void WriteTextStorage(string fileData, string filePath, Mode fileMode)
        {
            try
            {
                // Create directory if not exists.
                FileInfo fileInfo = new FileInfo(Path.Combine(_webHostEnvironment.ContentRootPath, (filePath)));
                if (!fileInfo.Directory.Exists)
                {
                    fileInfo.Directory.Create();
                }

                // Check file write mode and write content.
                if (Equals(fileMode, Mode.Append))
                {
                    File.AppendAllText(Path.Combine(_webHostEnvironment.ContentRootPath, (filePath)), fileData);
                }
                else
                {
                    File.WriteAllText(Path.Combine(_webHostEnvironment.ContentRootPath, (filePath)), fileData);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
