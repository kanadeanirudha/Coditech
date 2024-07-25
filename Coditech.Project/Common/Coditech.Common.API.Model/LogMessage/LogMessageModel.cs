namespace Coditech.Common.API.Model
{
    public class LogMessageModel : BaseModel
    {
        public long LogMessageId { get; set; }
        public string ErrorMessageType { get; set; }
        public string ExceptionMessage { get; set; }
        public string ComponentName { get; set; }
        public string TraceLevel { get; set; }
        public string Exception { get; set; }
        public string MethodName { get; set; }
        public string FileName { get; set; }
        public string LineNumber { get; set; }
    }
}
