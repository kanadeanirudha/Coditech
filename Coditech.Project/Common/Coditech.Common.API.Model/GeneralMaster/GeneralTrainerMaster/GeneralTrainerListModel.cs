namespace Coditech.Common.API.Model
{
    public class GeneralTrainerListModel : BaseListModel
    {
        public List<GeneralTrainerModel> GeneralTrainerList { get; set; }
        public GeneralTrainerListModel()
        {
            GeneralTrainerList = new List<GeneralTrainerModel>();
        }

    }
}
