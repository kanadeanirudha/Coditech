using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class LogMessageViewModel : BaseViewModel
    {
        public long LogMessageId { get; set; }

        [Display(Name = "Error Message Type")]
        public string ErrorMessageType { get; set; }

        [Display(Name = "Exception Message")]
        public string ExceptionMessage { get; set; }

        [Display(Name = "Component Name")]
        public string ComponentName { get; set; }
        [Display(Name = "Trace Level")]
        public string TraceLevel { get; set; }
        public string Exception { get; set; }
        public string MethodName { get; set; }
        public string FileName { get; set; }
        public int LineNumber { get; set; }
    }
}