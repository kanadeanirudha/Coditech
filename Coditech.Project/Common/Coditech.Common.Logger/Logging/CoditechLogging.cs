using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;

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
        public CoditechLogging()
        {
        }
        #region Enum Mode

        
        #endregion

        public void LogMessage(Exception ex, string componentName = "", TraceLevel traceLevel = TraceLevel.Info, string errorMessageType = null, object obj = null)
        {
            LogMessage(ex.Message, componentName, traceLevel, null, obj);
        }

        public void LogMessage(string message, string componentName = "", TraceLevel traceLevel = TraceLevel.Info, string errorMessageType = null, object obj = null, [CallerMemberName] string methodName = "", [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0)
        {
            if (traceLevel == TraceLevel.Error || traceLevel == TraceLevel.Warning)
            {
                WriteLogFiles(message, string.IsNullOrEmpty(componentName) ? LogFilePath : LogComponentFilePath, componentName);

                //LogMessage logMessage = new LogMessage()
                //{
                //    ErrorMessageType = !string.IsNullOrEmpty(errorMessageType) ? errorMessageType : ErrorMessageTypeEnum.Application.ToString(),
                //    ExceptionMessage = message,
                //    ComponentName = componentName,
                //    TraceLevel = traceLevel.ToString(),
                //    Exception = Convert.ToString(obj),
                //    MethodName = methodName,
                //    FileName = fileName,
                //    LineNumber = Convert.ToString(lineNumber),
                //    CreatedBy = 1,
                //    CreatedDate = DateTime.Now,
                //    ModifiedBy = 1,
                //    ModifiedDate = DateTime.Now,
                //};
                //_logMessageRepository.Insert(logMessage);
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
                WriteTextStorage(errMsg.ToString(), filePath, FileModeEnum.Append);
            }
        }

        /// <summary>
        /// Writes text file to persistant storage.
        /// </summary>
        /// <param name="fileData">Specify the string that has the file content.</param>
        /// <param name="filePath">Specify the relative file path.</param>
        /// <param name="fileMode">Specify the file write mode operatation. </param>
        private static void WriteTextStorage(string fileData, string filePath, FileModeEnum fileMode)
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
                if (Equals(fileMode, FileModeEnum.Append))
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
