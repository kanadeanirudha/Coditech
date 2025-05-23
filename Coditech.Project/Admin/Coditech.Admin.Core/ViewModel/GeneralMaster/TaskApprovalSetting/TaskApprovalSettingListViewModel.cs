﻿using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class TaskApprovalSettingListViewModel : BaseViewModel
    {
        public List<TaskApprovalSettingViewModel> TaskApprovalSettingList { get; set; }
        public TaskApprovalSettingListViewModel()
        {
            TaskApprovalSettingList = new List<TaskApprovalSettingViewModel>();
        }
        public string SelectedCentreCode { get; set; } = string.Empty;
    }
}
