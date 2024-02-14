namespace Coditech.Common.API.Model
{ 
    public class GeneralPersonFollowUpListModel : BaseListModel
{
    public List<GeneralPersonFollowUpModel> GeneralPersonFollowUpList { get; set; }
    public GeneralPersonFollowUpListModel()
    {
        GeneralPersonFollowUpList = new List<GeneralPersonFollowUpModel>();
    }

}
}
