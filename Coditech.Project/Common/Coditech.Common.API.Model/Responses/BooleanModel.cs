namespace Coditech.Common.API.Model.Response
{
    public class BooleanModel : BaseModel
    {
        public bool IsSuccess { get; set; }
        public string SuccessMessage { get; set; }
        public int MediaFolderMasterId { get; set; }
    }
}
