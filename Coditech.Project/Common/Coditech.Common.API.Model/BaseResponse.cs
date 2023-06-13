namespace Coditech.Common.API.Model.Response
{
    public class BaseResponse : BaseModel
    {
        public int? ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public bool HasError { get; set; }
        public Dictionary<string, string> CustomModelState { get; set; }
        public Dictionary<string, string> ErrorDetailList { get; set; }
    }
}
