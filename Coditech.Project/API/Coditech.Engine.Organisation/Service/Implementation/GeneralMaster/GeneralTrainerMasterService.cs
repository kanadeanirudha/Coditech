using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Common.Service;
using Coditech.Resources;

using System.Collections.Specialized;
using System.Data;

using static Coditech.Common.Helper.HelperUtility;
namespace Coditech.API.Service
{
    public class GeneralTrainerMasterService : BaseService, IGeneralTrainerMasterService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<GeneralTrainerMaster> _generalTrainerRepository;
        public GeneralTrainerMasterService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _generalTrainerRepository = new CoditechRepository<GeneralTrainerMaster>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual GeneralTrainerListModel GetTrainerList(string selectedCentreCode, short selectedDepartmentId, bool isAssociated, FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<GeneralTrainerModel> objStoredProc = new CoditechViewRepository<GeneralTrainerModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@CentreCode", selectedCentreCode, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@DepartmentId", selectedDepartmentId, ParameterDirection.Input, DbType.Int16);
            objStoredProc.SetParameter("@IsAssociated", isAssociated, ParameterDirection.Input, DbType.Boolean);
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<GeneralTrainerModel> trainerList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetGeneralTrainerList @CentreCode,@DepartmentId,@IsAssociated,@WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 7, out pageListModel.TotalRowCount)?.ToList();
            GeneralTrainerListModel listModel = new GeneralTrainerListModel();

            listModel.GeneralTrainerList = trainerList?.Count > 0 ? trainerList : new List<GeneralTrainerModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Create Trainer.
        public virtual GeneralTrainerModel CreateTrainer(GeneralTrainerModel generalTrainerModel)
        {
            if (IsNull(generalTrainerModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsEmployeeAlreadyExist(generalTrainerModel.EmployeeId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "EmployeeId"));

            GeneralTrainerMaster generalTrainerMaster = generalTrainerModel.FromModelToEntity<GeneralTrainerMaster>();

            //Create new Trainer Master and return it.
            GeneralTrainerMaster generalTrainerMasterData = _generalTrainerRepository.Insert(generalTrainerMaster);
            if (generalTrainerMasterData?.GeneralTrainerMasterId > 0)
            {
                generalTrainerModel.GeneralTrainerMasterId = generalTrainerMasterData.GeneralTrainerMasterId;
            }
            else
            {
                generalTrainerModel.HasError = true;
                generalTrainerModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return generalTrainerModel;
        }

        //Get Trainer by  general Trainer Id.
        public virtual GeneralTrainerModel GetTrainer(long generalTrainerId)
        {
            if (generalTrainerId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "GeneralTrainerMasterId"));

            //Get the Trainer Details based on id.
            GeneralTrainerMaster generalTrainerMaster = _generalTrainerRepository.Table.Where(x => x.GeneralTrainerMasterId == generalTrainerId)?.FirstOrDefault();
            GeneralTrainerModel generalTrainerModel = generalTrainerMaster?.FromEntityToModel<GeneralTrainerModel>();
            if (generalTrainerModel?.EmployeeId > 0)
            {
                GeneralPersonModel generalPersonModel = GetGeneralPersonDetailsByEntityType(generalTrainerModel.EmployeeId, UserTypeEnum.Employee.ToString());
                if (IsNotNull(generalPersonModel))
                {
                    generalTrainerModel.FirstName = generalPersonModel.FirstName;
                    generalTrainerModel.LastName = generalPersonModel.LastName;
                    generalTrainerModel.SelectedCentreCode = generalPersonModel.SelectedCentreCode;
                    generalTrainerModel.SelectedDepartmentId = generalPersonModel.SelectedDepartmentId;
                }
            }
            return generalTrainerModel;
        }

        //Update Trainer.
        public virtual bool UpdateTrainer(GeneralTrainerModel generalTrainerModel)
        {
            if (IsNull(generalTrainerModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (generalTrainerModel.GeneralTrainerMasterId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "GeneralTrainerMasterId"));

            if (IsEmployeeAlreadyExist(generalTrainerModel.EmployeeId, generalTrainerModel.GeneralTrainerMasterId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "EmployeeId"));

            GeneralTrainerMaster generalTrainerMaster = generalTrainerModel.FromModelToEntity<GeneralTrainerMaster>();
            //Update Trainer
            bool isTrainerUpdated = _generalTrainerRepository.Update(generalTrainerMaster);
            if (!isTrainerUpdated)
            {
                generalTrainerModel.HasError = true;
                generalTrainerModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isTrainerUpdated;
        }

        //Delete Trainer.
        public virtual bool DeleteTrainer(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "EmployeeID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("GeneralTrainerMasterId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteTrainer @GeneralTrainerMasterId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Protected Method
        //Check if EmployeeId is already present or not.
        protected virtual bool IsEmployeeAlreadyExist(long employeeId, long generalTrainerMasterId = 0)
         => _generalTrainerRepository.Table.Any(x => x.EmployeeId == employeeId && (x.GeneralTrainerMasterId != generalTrainerMasterId || generalTrainerMasterId == 0));
        #endregion
    }
}
