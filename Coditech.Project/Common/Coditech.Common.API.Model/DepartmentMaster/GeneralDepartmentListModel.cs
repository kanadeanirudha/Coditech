using System.Collections.Generic;

namespace Coditech.Common.API.Model
{
    public class GeneralDepartmentListModel : BaseListModel
    {
        public List<GeneralDepartmentMasterModel> GeneralDepartmentList { get; set; }
        public GeneralDepartmentListModel()
        {
            GeneralDepartmentList = new List<GeneralDepartmentMasterModel>();
        }

        public string SelectedDepartmentID { get; set; }
    }
}
