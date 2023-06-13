using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Coditech.Common.Logger
{
    public class CoditechLogging : ICoditechLogging
    {
        public void WriteLog(string message, string componentName, TraceLevel traceLevel = TraceLevel.Info, Exception ex = null)
        {
        }

        public void LogMessage(Exception ex, string componentName = "", TraceLevel traceLevel = TraceLevel.Info, object obj = null)
        {
        }

        public void LogMessage(string message, string componentName = "", TraceLevel traceLevel = TraceLevel.Info, object obj = null, [CallerMemberName] string methodName = "", [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0)
        {
        }

        public void LogObject(Type objectType, Object objectInstance, string componentName = "")
        {
        }
    }
}
