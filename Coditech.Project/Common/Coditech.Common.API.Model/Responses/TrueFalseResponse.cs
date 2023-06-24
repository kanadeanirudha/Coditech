
namespace Coditech.Common.API.Model.Response
{
    //Boolean response for Post
    public class TrueFalseResponse : BaseResponse
    {
        public BooleanModel booleanModel { get; set; }

        public bool IsSuccess { get; set; }
    }
}
