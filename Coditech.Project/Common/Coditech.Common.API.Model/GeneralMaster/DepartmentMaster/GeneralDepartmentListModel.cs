﻿namespace Coditech.Common.API.Model
{
    public class GeneralDepartmentListModel : BaseListModel
    {
        public List<GeneralDepartmentModel> GeneralDepartmentList { get; set; }
        public GeneralDepartmentListModel()
        {
            GeneralDepartmentList = new List<GeneralDepartmentModel>();
        }

        public string SelectedDepartmentId { get; set; }
    }
}
