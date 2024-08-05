namespace Coditech.Common.API.Model.Response
{
    public class LogMessageListResponse : BaseListResponse
    {
        public List<LogMessageModel> LogMessageList { get; set; }
    }
}
