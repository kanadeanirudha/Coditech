using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class GeneralOccupationModel : BaseModel
    {
        public GeneralOccupationModel()
        {

        }
        public short GeneralOccupationMasterId { get; set; }
        public string OccupationName { get; set; }
        public short DisplayOrder { get; set; }
    }
}
