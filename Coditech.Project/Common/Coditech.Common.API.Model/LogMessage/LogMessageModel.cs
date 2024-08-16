using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class LogMessageModel : BaseModel
    {
        public long LogMessageId { get; set; }
        [MaxLength(50)]
        public string ErrorMessageType { get; set; }
        public string ExceptionMessage { get; set; }
        [MaxLength(200)]
        public string ComponentName { get; set; }
        [MaxLength(20)]
        public string TraceLevel { get; set; }
        public string Exception { get; set; }
        [MaxLength(200)]
        public string MethodName { get; set; }
        [MaxLength(200)]
        public string FileName { get; set; }
        public string LineNumber { get; set; }
    }
}
