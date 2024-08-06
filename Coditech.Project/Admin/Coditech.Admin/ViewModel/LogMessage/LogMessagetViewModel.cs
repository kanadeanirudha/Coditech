using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class LogMessageViewModel : BaseViewModel
    {
        public long LogMessageId { get; set; }

        [MaxLength(50)]
        [Display(Name = "Error Message Type :")]
        public string ErrorMessageType { get; set; }

        [Display(Name = "Exception Message :")]
        public string ExceptionMessage { get; set; }

        [MaxLength(200)]
        [Display(Name = "Component Name :")]
        public string ComponentName { get; set; }

        [MaxLength(20)]
        [Display(Name = "Trace Level :")]
        public string TraceLevel { get; set; }

        [Display(Name = "Exception :")]
        public string Exception { get; set; }

        [MaxLength(200)]
        [Display(Name = "Method Name :")]
        public string MethodName { get; set; }

        [MaxLength(200)]
        [Display(Name = "File Name :")]
        public string FileName { get; set; }

        [Display(Name = "Line Number :")]
        public int LineNumber { get; set; }

        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}