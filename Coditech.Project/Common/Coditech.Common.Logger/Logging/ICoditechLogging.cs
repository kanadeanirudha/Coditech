﻿using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Coditech.Common.Logger
{
    public interface ICoditechLogging
    {
        void LogMessage(Exception ex, string componentName = "", TraceLevel traceLevel = TraceLevel.Info, string errorMessageType = null);

        void LogMessage(string message, string componentName = "", TraceLevel traceLevel = TraceLevel.Info, object obj = null, string errorMessageType = null, [CallerMemberName] string methodName = "", [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0);
    }
}
