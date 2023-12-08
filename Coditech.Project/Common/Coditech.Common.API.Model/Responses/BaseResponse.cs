namespace Coditech.Common.API.Model.Responses
{
    public class BaseResponse : BaseModel
    {
        public Dictionary<string, string> CustomModelState { get; set; }
        public Dictionary<string, string> ErrorDetailList { get; set; }
    }
}
