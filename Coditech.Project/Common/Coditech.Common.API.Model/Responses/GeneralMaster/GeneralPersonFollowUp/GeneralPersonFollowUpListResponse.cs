using Coditech.Common.API.Model.GeneralPerson.GeneralPersonFollowUp;

namespace Coditech.Common.API.Model.Response
{
    public class GeneralPersonFollowUpListResponse : BaseListResponse
    {
        public List<GeneralPersonFollowUpModel> GeneralPersonFollowUpList { get; set; }
    }
}
