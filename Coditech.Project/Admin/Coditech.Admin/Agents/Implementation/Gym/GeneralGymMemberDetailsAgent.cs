using Coditech.Admin.ViewModel;
using Coditech.API.Client;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Resources;

using System.Diagnostics;

using static Coditech.Common.Helper.HelperUtility;

namespace Coditech.Admin.Agents
{
    public class GeneralGymMemberDetailsAgent : BaseAgent, IGeneralGymMemberDetailsAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IGeneralGymMemberDetailsClient _generalGymMemberDetailsClient;
        #endregion

        #region Public Constructor
        public GeneralGymMemberDetailsAgent(ICoditechLogging coditechLogging, IGeneralGymMemberDetailsClient generalGymMemberDetailsClient)
        {
            _coditechLogging = coditechLogging;
            _generalGymMemberDetailsClient = GetClient<IGeneralGymMemberDetailsClient>(generalGymMemberDetailsClient);
        }
        #endregion

        #region Public Methods
        public virtual GymMemberDetailsListViewModel GetGymMemberDetailsList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("FirstName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("LastName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "FirstName" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            GymMemberDetailsListResponse response = _generalGymMemberDetailsClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            GymMemberDetailsListModel gymMemberDetailsList = new GymMemberDetailsListModel { GymMemberDetailsList = response?.GymMemberDetailsList };
            GymMemberDetailsListViewModel listViewModel = new GymMemberDetailsListViewModel();
            listViewModel.GymMemberDetailsList = gymMemberDetailsList?.GymMemberDetailsList?.ToViewModel<GymMemberDetailsViewModel>().ToList();
            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.GymMemberDetailsList.Count, BindColumns());
            return listViewModel;
        }
        #endregion

        #region protected
        protected virtual List<DatatableColumns> BindColumns()
        {
            List<DatatableColumns> datatableColumnList = new List<DatatableColumns>();
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "First Name",
                ColumnCode = "FirstName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Last Name",
                ColumnCode = "LastName",
                IsSortable = true,
            });
            return datatableColumnList;
        }
        #endregion
    }
}
