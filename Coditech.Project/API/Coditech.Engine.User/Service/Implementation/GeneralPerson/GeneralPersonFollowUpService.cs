
using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.GeneralPerson.GeneralPersonFollowUp;
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
    public class GeneralPersonFollowUpService : IGeneralPersonFollowUpService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<GeneralPersonFollowUp> _generalPersonFollowUpRepository;
        public GeneralPersonFollowUpService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _generalPersonFollowUpRepository = new CoditechRepository<GeneralPersonFollowUp>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual GeneralPersonFollowUpListModel GetPersonFollowUpList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<GeneralPersonFollowUpModel> objStoredProc = new CoditechViewRepository<GeneralPersonFollowUpModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<GeneralPersonFollowUpModel> PersonFollowUpList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetPersonFollowUpList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            GeneralPersonFollowUpListModel listModel = new GeneralPersonFollowUpListModel();

            listModel.GeneralPersonFollowUpList = PersonFollowUpList?.Count > 0 ? PersonFollowUpList : new List<GeneralPersonFollowUpModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }
        //Create Lead Generation.
        public virtual GeneralPersonFollowUpModel CreatePersonFollowUp(GeneralPersonFollowUpModel generalPersonFollowUpModel)
        {
            if (IsNull(generalPersonFollowUpModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            //if (IsPersonFollowUpCodeAlreadyExist(generalPersonFollowUpModel.PersonId))
            //    throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Person Id"));

            GeneralPersonFollowUp generalPersonFollowUp = generalPersonFollowUpModel.FromModelToEntity<GeneralPersonFollowUp>();

            //Create new PersonFollowUp and return it.
            GeneralPersonFollowUp PersonFollowUpData = _generalPersonFollowUpRepository.Insert(generalPersonFollowUp);
            if (PersonFollowUpData?.GeneralPersonFollowUpId > 0)
            {
                generalPersonFollowUpModel.GeneralPersonFollowUpId = PersonFollowUpData.GeneralPersonFollowUpId;
            }
            else
            {
                generalPersonFollowUpModel.HasError = true;
                generalPersonFollowUpModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return generalPersonFollowUpModel;
        }

        //Get PersonFollowUp by PersonFollowUp id.
        public virtual GeneralPersonFollowUpModel GetPersonFollowUp(long PersonFollowUpId)
        {
            if (PersonFollowUpId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "PersonFollowUpID"));

            //Get the PersonFollowUp Details based on id.
            GeneralPersonFollowUp generalPersonFollowUp = _generalPersonFollowUpRepository.Table.FirstOrDefault(x => x.GeneralPersonFollowUpId == PersonFollowUpId);
            GeneralPersonFollowUpModel generalPersonFollowUpModel = generalPersonFollowUp?.FromEntityToModel<GeneralPersonFollowUpModel>();
            return generalPersonFollowUpModel;
        }

        //Update PersonFollowUp.
        public virtual bool UpdatePersonFollowUp(GeneralPersonFollowUpModel generalPersonFollowUpModel)
        {
            if (IsNull(generalPersonFollowUpModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (generalPersonFollowUpModel.GeneralPersonFollowUpId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "PersonFollowUpID"));

            //if (IsPersonFollowUpCodeAlreadyExist(generalPersonFollowUpModel.PersonId, generalPersonFollowUpModel.GeneralPersonFollowUpId))
            //    throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Person Id"));

            GeneralPersonFollowUp generalPersonFollowUp = generalPersonFollowUpModel.FromModelToEntity<GeneralPersonFollowUp>();

            //Update PersonFollowUp
            bool isPersonFollowUpUpdated = _generalPersonFollowUpRepository.Update(generalPersonFollowUp);
            if (!isPersonFollowUpUpdated)
            {
                generalPersonFollowUpModel.HasError = true;
                generalPersonFollowUpModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isPersonFollowUpUpdated;
        }

        //Delete PersonFollowUp.
        public virtual bool DeletePersonFollowUp(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "PersonFollowUpID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("PersonFollowUpId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeletePersonFollowUp @PersonFollowUpId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Protected Method
        //Check if PersonFollowUp code is already present or not.
        protected virtual bool IsPersonFollowUpCodeAlreadyExist(string followtakenfor, long generalPersonFollowUpId = 0)
         => _generalPersonFollowUpRepository.Table.Any(x => x.FollowTakenFor == followtakenfor && (x.GeneralPersonFollowUpId != generalPersonFollowUpId || generalPersonFollowUpId == 0));
        #endregion
    }
}
