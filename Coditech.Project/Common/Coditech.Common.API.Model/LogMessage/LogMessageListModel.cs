namespace Coditech.Common.API.Model
{
    public class LogMessageListModel : BaseListModel
    {
        public List<LogMessageModel> LogMessageList { get; set; }
        public LogMessageListModel()
        {
            LogMessageList = new List<LogMessageModel>();
        }
    }
}
