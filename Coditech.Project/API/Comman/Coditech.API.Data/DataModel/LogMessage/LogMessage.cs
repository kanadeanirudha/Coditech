using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class LogMessage
    {
        [Key]
        public long LogMessageId { get; set; }
        public string ErrorMessageType { get; set; }
        public string ExceptionMessage { get; set; }
        public string ComponentName { get; set; }
        public string TraceLevel { get; set; }
        public string Exception { get; set; }
        public string MethodName { get; set; }
        public string FileName { get; set; }
        public int LineNumber { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

