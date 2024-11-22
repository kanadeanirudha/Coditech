namespace Coditech.Common.API.Model
{
    public class TaskMasterModel : BaseModel
    {
        public short TaskMasterId { get; set; }
        public string TaskCode { get; set; }
        public string TaskDescription { get; set; }
        public bool IsActive { get; set; }
    }
}
