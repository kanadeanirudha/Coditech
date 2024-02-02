using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Resources;
using System.Collections.Specialized;
using System.Data;

namespace Coditech.API.Service
{
    public class GeneralRunningNumbersService : IGeneralRunningNumbersService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<GeneralRunningNumbers> _generalRunningNumbersRepository;

        public GeneralRunningNumbersService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _generalRunningNumbersRepository = new CoditechRepository<GeneralRunningNumbers>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual GeneralRunningNumbersListModel GetRunningNumbersList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            string selectedCentreCode = filters?.Find(x => string.Equals(x.FilterName, FilterKeys.SelectedCentreCode, StringComparison.CurrentCultureIgnoreCase))?.FilterValue;
            filters.RemoveAll(x => x.FilterName == FilterKeys.SelectedCentreCode);

            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<GeneralRunningNumbersModel> objStoredProc = new CoditechViewRepository<GeneralRunningNumbersModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@CentreCode", selectedCentreCode, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<GeneralRunningNumbersModel> generalRunningNumbersList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetGeneralRunningNumbersList @CentreCode,@WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 6, out pageListModel.TotalRowCount)?.ToList();
            GeneralRunningNumbersListModel listModel = new GeneralRunningNumbersListModel();

            listModel.GeneralRunningNumbersList = generalRunningNumbersList?.Count > 0 ? generalRunningNumbersList : new List<GeneralRunningNumbersModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Create GeneralRunningNumbers.
        public virtual GeneralRunningNumbersModel CreateRunningNumbers(GeneralRunningNumbersModel generalRunningNumbersModel)
        {
            if (HelperUtility.IsNull(generalRunningNumbersModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsCentreCodeAlreadyExist(generalRunningNumbersModel.CentreCode))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Centre Code"));

            GeneralRunningNumbers generalRunningNumbers = generalRunningNumbersModel.FromModelToEntity<GeneralRunningNumbers>();

            //Create new GeneralRunningNumbers and return it.
            GeneralRunningNumbers generalRunningNumbersData = _generalRunningNumbersRepository.Insert(generalRunningNumbers);
            if (generalRunningNumbersData?.GeneralRunningNumberId > 0)
            {
                generalRunningNumbersModel.GeneralRunningNumberId = generalRunningNumbersData.GeneralRunningNumberId;
            }
            else
            {
                generalRunningNumbersModel.HasError = true;
                generalRunningNumbersModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return generalRunningNumbersModel;
        }

        //Get GeneralRunningNumbers by generalRunningNumber id.
        public virtual GeneralRunningNumbersModel GetRunningNumbers(long generalRunningNumberId)
        {
            if (generalRunningNumberId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "GeneralRunningNumberId"));

            //Get the GeneralRunningNumbers Details based on id.
            GeneralRunningNumbers generalRunningNumbers = _generalRunningNumbersRepository.Table.FirstOrDefault(x => x.GeneralRunningNumberId == generalRunningNumberId);
            GeneralRunningNumbersModel generalRunningNumbersModel = generalRunningNumbers?.FromEntityToModel<GeneralRunningNumbersModel>();
            return generalRunningNumbersModel;
        }

        //Update GeneralRunningNumbers.
        public virtual bool UpdateRunningNumbers(GeneralRunningNumbersModel generalRunningNumbersModel)
        {
            if (HelperUtility.IsNull(generalRunningNumbersModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (generalRunningNumbersModel.GeneralRunningNumberId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "GeneralRunningNumberId"));

            if (IsCentreCodeAlreadyExist(generalRunningNumbersModel.CentreCode, generalRunningNumbersModel.GeneralRunningNumberId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Centre Code"));

            GeneralRunningNumbers generalRunningNumbers = generalRunningNumbersModel.FromModelToEntity<GeneralRunningNumbers>();

            //Update GeneralRunningNumbers
            bool isRunningNumbersUpdated = _generalRunningNumbersRepository.Update(generalRunningNumbers);
            if (!isRunningNumbersUpdated)
            {
                generalRunningNumbersModel.HasError = true;
                generalRunningNumbersModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isRunningNumbersUpdated;
        }

        //Delete GeneralRunningNumbers.
        public virtual bool DeleteRunningNumbers(ParameterModel parameterModel)
        {
            if (HelperUtility.IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "GeneralRunningNumberId"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("GeneralRunningNumberId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteGeneralRunningNumbers @GeneralRunningNumberId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }
        #region Protected Method
        //Check if Centre Code is already present or not.
        protected virtual bool IsCentreCodeAlreadyExist(string centreCode, long generalRunningNumberId = 0)
         => _generalRunningNumbersRepository.Table.Any(x => x.CentreCode == centreCode && (x.GeneralRunningNumberId != generalRunningNumberId || generalRunningNumberId == 0));
        #endregion
    }
}

