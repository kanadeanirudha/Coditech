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

namespace Coditech.Admin.Agents
{
    public class GeneralTrainerAgent : BaseAgent, IGeneralTrainerAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IGeneralTrainerClient _generalTrainerClient;
        #endregion

        #region Public Constructor
        public GeneralTrainerAgent(ICoditechLogging coditechLogging, IGeneralTrainerClient generalTrainerClient, IUserClient userClient)
        {
            _coditechLogging = coditechLogging;
            _generalTrainerClient = GetClient<IGeneralTrainerClient>(generalTrainerClient);
        }
        #endregion

        #region Public Methods
        #region Trainer

        public virtual GeneralTrainerListViewModel GetTrainerList(string selectedCentreCode, short selectedDepartmentId, bool isAssociated, DataTableViewModel dataTableModel)
        {
            FilterCollection filters = new FilterCollection();
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters.Add("FirstName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("LastName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("MobileNumber", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("EmailId", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("PersonCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            GeneralTrainerListResponse response = _generalTrainerClient.List(selectedCentreCode, selectedDepartmentId, true, null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            GeneralTrainerListModel trainerList = new GeneralTrainerListModel { GeneralTrainerList = response?.GeneralTrainerList };
            GeneralTrainerListViewModel listViewModel = new GeneralTrainerListViewModel();
            listViewModel.GeneralTrainerList = trainerList?.GeneralTrainerList?.ToViewModel<GeneralTrainerViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.GeneralTrainerList.Count, BindColumns());
            return listViewModel;
        }

        //Create Trainer.
        public virtual GeneralTrainerViewModel CreateTrainer(GeneralTrainerViewModel generalTrainerViewModel)
        {
            try
            {
                GeneralTrainerModel generalTrainerModel = generalTrainerViewModel.ToModel<GeneralTrainerModel>();
                GeneralTrainerResponse response = _generalTrainerClient.CreateTrainer(generalTrainerModel);
                generalTrainerModel = response?.GeneralTrainerModel;
                return HelperUtility.IsNotNull(generalTrainerModel) ? generalTrainerModel.ToViewModel<GeneralTrainerViewModel>() : new GeneralTrainerViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Trainer.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (GeneralTrainerViewModel)GetViewModelWithErrorMessage(generalTrainerViewModel, ex.ErrorMessage);
                    default:
                        return (GeneralTrainerViewModel)GetViewModelWithErrorMessage(generalTrainerViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Trainer.ToString(), TraceLevel.Error);
                return (GeneralTrainerViewModel)GetViewModelWithErrorMessage(generalTrainerViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get Trainer by general Trainer id.
        public virtual GeneralTrainerViewModel GetTrainer(long generalTrainerId)
        {
            GeneralTrainerResponse response = _generalTrainerClient.GetTrainer(generalTrainerId);
            return response?.GeneralTrainerModel.ToViewModel<GeneralTrainerViewModel>();
        }

        //Update  Trainer.
        public virtual GeneralTrainerViewModel UpdateTrainer(GeneralTrainerViewModel generalTrainerViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.Trainer.ToString(), TraceLevel.Info);
                GeneralTrainerResponse response = _generalTrainerClient.UpdateTrainer(generalTrainerViewModel.ToModel<GeneralTrainerModel>());
                GeneralTrainerModel generalTrainerModel = response?.GeneralTrainerModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.Trainer.ToString(), TraceLevel.Info);
                return HelperUtility.IsNotNull(generalTrainerModel) ? generalTrainerModel.ToViewModel<GeneralTrainerViewModel>() : (GeneralTrainerViewModel)GetViewModelWithErrorMessage(new GeneralTrainerViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Trainer.ToString(), TraceLevel.Error);
                return (GeneralTrainerViewModel)GetViewModelWithErrorMessage(generalTrainerViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete Trainer.
        public virtual bool DeleteTrainer(string generalTrainerIds, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.Trainer.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _generalTrainerClient.DeleteTrainer(new ParameterModel { Ids = generalTrainerIds });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Trainer.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteTrainerDetails;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Trainer.ToString(), TraceLevel.Error);
                errorMessage = GeneralResources.ErrorFailedToDelete;
                return false;
            }
        }
        #endregion
        #endregion

        #region protected
        protected virtual List<DatatableColumns> BindColumns()
        {
            List<DatatableColumns> datatableColumnList = new List<DatatableColumns>();
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Image",
                ColumnCode = "Image",
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Trainer Code",
                ColumnCode = "PersonCode",
                IsSortable = true,
            });
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
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Contact",
                ColumnCode = "MobileNumber",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Email Id",
                ColumnCode = "EmailId",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Trainer Specialization",
                ColumnCode = "TrainerSpecializationEnumId",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Number Of Trainee Associated",
                ColumnCode = "NumberOfTraineeAssociated",
            });
            return datatableColumnList;
        }
    }
}
#endregion