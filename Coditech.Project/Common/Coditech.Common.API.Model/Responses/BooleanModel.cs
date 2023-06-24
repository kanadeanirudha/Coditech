namespace Coditech.Common.API.Model.Response
{
    public class BooleanModel : BaseModel
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public bool HasError { get; set; }
        public string SuccessMessage { get; set; }
    }
}
