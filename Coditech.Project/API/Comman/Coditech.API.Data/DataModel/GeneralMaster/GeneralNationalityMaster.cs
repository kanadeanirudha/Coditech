namespace Coditech.API.Data
{
    public partial class GeneralNationalityMaster : BaseDataModel
    {
        public short GeneralNationalityMasterId { get; set; }
        public string Description { get; set; }
        public bool DefaultFlag { get; set; }
        public Nullable<bool> IsUserDefined { get; set; }
    }
}

