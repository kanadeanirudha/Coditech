
using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Resources;
using System.Collections.Specialized;
using System.Data;
using static Coditech.Common.Helper.HelperUtility;
namespace Coditech.API.Service
{
    public class AccGLSetupNarrationService : IAccGLSetupNarrationService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<AccGLSetupNarration> _accGLSetupNarrationRepository;
        public AccGLSetupNarrationService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _accGLSetupNarrationRepository = new CoditechRepository<AccGLSetupNarration>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual AccGLSetupNarrationListModel GetNarrationList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<AccGLSetupNarrationModel> objStoredProc = new CoditechViewRepository<AccGLSetupNarrationModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<AccGLSetupNarrationModel> NarrationList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetAccGLSetupNarrationList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            AccGLSetupNarrationListModel listModel = new AccGLSetupNarrationListModel();

            listModel.AccGLSetupNarrationList = NarrationList?.Count > 0 ? NarrationList : new List<AccGLSetupNarrationModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }
        //Create Narration.
        public virtual AccGLSetupNarrationModel CreateNarration(AccGLSetupNarrationModel accGLSetupNarrationModel)
        {
            if (IsNull(accGLSetupNarrationModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            //if (IsNarrationIDAlreadyExist(AccGLSetupNarration.NarrationType))
            //    throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Narration Type"));

            AccGLSetupNarration accGLSetupNarration = accGLSetupNarrationModel.FromModelToEntity<AccGLSetupNarration>();

            //Create new Narration and return it.
            AccGLSetupNarration narrationData = _accGLSetupNarrationRepository.Insert(accGLSetupNarration);
            if (narrationData?.AccGLSetupNarrationId > 0)
            {
                accGLSetupNarrationModel.AccGLSetupNarrationId = narrationData.AccGLSetupNarrationId;
            }
            else
            {
                accGLSetupNarrationModel.HasError = true;
                accGLSetupNarrationModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return accGLSetupNarrationModel;
        }

        //Get Narration by Narration id.
        public virtual AccGLSetupNarrationModel GetNarration(int accGLSetupNarrationId)
        {
            if (accGLSetupNarrationId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "NarrationID"));

            //Get the Narration Details based on id.
            AccGLSetupNarration accGLSetupNarration = _accGLSetupNarrationRepository.Table.FirstOrDefault(x => x.AccGLSetupNarrationId == accGLSetupNarrationId);
            AccGLSetupNarrationModel accGLSetupNarrationModel = accGLSetupNarration?.FromEntityToModel<AccGLSetupNarrationModel>();
            return accGLSetupNarrationModel;
        }

        //Update NArration.
        public virtual bool UpdateNarration(AccGLSetupNarrationModel accGLSetupNarrationModel)
        {
            if (IsNull(accGLSetupNarrationModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (accGLSetupNarrationModel.AccGLSetupNarrationId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "NarrationID"));

            //if (IsNarrationIDAlreadyExist(accGLSetupNarrationModel.NarrationType, accGLSetupNarrationModel.AccGLSetupNarrationId))
            //    throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Narration Type"));

            AccGLSetupNarration accGLSetupNarration = accGLSetupNarrationModel.FromModelToEntity<AccGLSetupNarration>();

            //Update Narration
            bool isNarrationUpdated = _accGLSetupNarrationRepository.Update(accGLSetupNarration);
            if (!isNarrationUpdated)
            {
                accGLSetupNarrationModel.HasError = true;
                accGLSetupNarrationModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isNarrationUpdated;
        }

        //Delete Narration.
        public virtual bool DeleteNarration(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "NarrationID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("AccGLSetupNarrationId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteCountry @AccGLSetupNarrationId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Protected Method
        //Check if Narration ID is already present or not.
        //protected virtual bool IsNarrationIDAlreadyExist(int AccGLSetupNarrationId, int accGLSetupNarrationId = 0)
        // => _accGLSetupNarrationRepository.Table.Any(x => x.AccGLSetupNarrationId == accGLSetupNarrationId && (x.AccGLSetupNarrationId != accGLSetupNarrationId || accGLSetupNarrationId == 0));
        #endregion
    }
}
