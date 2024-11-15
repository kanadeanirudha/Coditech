namespace Coditech.Common.API.Model.Response
{
    public class GeneralBatchUserListResponse : BaseListResponse
    {
        public List<GeneralBatchUserModel> GeneralBatchUserList { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        public int GeneralBatchMasterId { get; set; }
        public string BatchName { get; set; }
    }
}
