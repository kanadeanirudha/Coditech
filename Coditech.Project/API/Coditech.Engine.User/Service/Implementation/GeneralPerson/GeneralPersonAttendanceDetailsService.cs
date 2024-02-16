
using Coditech.API.Data;
using Coditech.API.Data.DataModel.Gym;
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
    public class GeneralPersonAttendanceDetailsService : BaseService, IGeneralPersonAttendanceDetailsService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<GeneralPersonAttendanceDetails> _generalPersonAttendanceDetailsRepository;
        public GeneralPersonAttendanceDetailsService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _generalPersonAttendanceDetailsRepository = new CoditechRepository<GeneralPersonAttendanceDetails>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual GeneralPersonAttendanceDetailsListModel GetPersonAttendanceList(long personId, FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<GeneralPersonAttendanceDetailsModel> objStoredProc = new CoditechViewRepository<GeneralPersonAttendanceDetailsModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@PersonId", personId, ParameterDirection.Input, DbType.Int64);
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<GeneralPersonAttendanceDetailsModel> PersonAttendanceList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetGeneralPersonAttendanceDetailsList @PersonId,@WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 5, out pageListModel.TotalRowCount)?.ToList();
            GeneralPersonAttendanceDetailsListModel listModel = new GeneralPersonAttendanceDetailsListModel();

            listModel.GeneralPersonAttendanceDetailsList = PersonAttendanceList?.Count > 0 ? PersonAttendanceList : new List<GeneralPersonAttendanceDetailsModel>();
            listModel.BindPageListModel(pageListModel);
            GeneralPerson generalPerson = GetGeneralPersonDetails(personId);
            if (IsNotNull(generalPerson))
            {
                listModel.FirstName = generalPerson.FirstName;
                listModel.LastName = generalPerson.LastName;
            }
            listModel.PersonId = personId;
            return listModel;
        }
        //Create Lead Generation.
        public virtual GeneralPersonAttendanceDetailsModel CreatePersonAttendance(GeneralPersonAttendanceDetailsModel generalPersonAttendanceDetailsModel)
        {
            if (IsNull(generalPersonAttendanceDetailsModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            GeneralPersonAttendanceDetails generalPersonAttendanceDetails = generalPersonAttendanceDetailsModel.FromModelToEntity<GeneralPersonAttendanceDetails>();

            //Create new PersonAttendance and return it.
            GeneralPersonAttendanceDetails personAttendanceData = _generalPersonAttendanceDetailsRepository.Insert(generalPersonAttendanceDetails);
            if (personAttendanceData?.GeneralPersonAttendanceDetailId > 0)
            {
                generalPersonAttendanceDetailsModel.GeneralPersonAttendanceDetailId = personAttendanceData.GeneralPersonAttendanceDetailId;
            }
            else
            {
                generalPersonAttendanceDetailsModel.HasError = true;
                generalPersonAttendanceDetailsModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return generalPersonAttendanceDetailsModel;
        }

        //Get PersonAttendance by PersonAttendance id.
        public virtual GeneralPersonAttendanceDetailsModel GetPersonAttendance(long PersonAttendanceId)
        {
            if (PersonAttendanceId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "PersonAttendanceID"));

            //Get the PersonAttendance Details based on id.
            GeneralPersonAttendanceDetails generalPersonAttendanceDetails = _generalPersonAttendanceDetailsRepository.Table.FirstOrDefault(x => x.GeneralPersonAttendanceDetailId == PersonAttendanceId);
            GeneralPersonAttendanceDetailsModel generalPersonAttendanceDetailsModel = generalPersonAttendanceDetails?.FromEntityToModel<GeneralPersonAttendanceDetailsModel>();
            return generalPersonAttendanceDetailsModel;
        }

        //Update PersonAttendance.
        public virtual bool UpdatePersonAttendance(GeneralPersonAttendanceDetailsModel generalPersonAttendanceDetailsModel)
        {
            if (IsNull(generalPersonAttendanceDetailsModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (generalPersonAttendanceDetailsModel.GeneralPersonAttendanceDetailId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "PersonAttendanceID"));

            GeneralPersonAttendanceDetails generalPersonAttendanceDetails = generalPersonAttendanceDetailsModel.FromModelToEntity<GeneralPersonAttendanceDetails>();

            //Update PersonAttendance
            bool isPersonAttendanceUpdated = _generalPersonAttendanceDetailsRepository.Update(generalPersonAttendanceDetails);
            if (!isPersonAttendanceUpdated)
            {
                generalPersonAttendanceDetailsModel.HasError = true;
                generalPersonAttendanceDetailsModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isPersonAttendanceUpdated;
        }

        //Delete PersonAttendance.
        public virtual bool DeletePersonAttendance(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "PersonAttendanceID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("PersonAttendanceId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeletePersonAttendance @PersonAttendanceId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Protected Method
        #endregion
    }
}
