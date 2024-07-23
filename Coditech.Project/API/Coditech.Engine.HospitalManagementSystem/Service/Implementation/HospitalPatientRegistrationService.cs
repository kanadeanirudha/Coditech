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
    public class HospitalPatientRegistrationService : BaseService, IHospitalPatientRegistrationService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<HospitalPatientRegistration> _hospitalPatientRegistrationRepository;
        public HospitalPatientRegistrationService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _hospitalPatientRegistrationRepository = new CoditechRepository<HospitalPatientRegistration>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual HospitalPatientRegistrationListModel GetPatientRegistrationList(string selectedCentreCode, FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<HospitalPatientRegistrationModel> objStoredProc = new CoditechViewRepository<HospitalPatientRegistrationModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@CentreCode", selectedCentreCode, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<HospitalPatientRegistrationModel> PatientRegistrationList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetHospitalPatientRegistrationList @CentreCode,@WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 5, out pageListModel.TotalRowCount)?.ToList();
            HospitalPatientRegistrationListModel listModel = new HospitalPatientRegistrationListModel();

            listModel.HospitalPatientRegistrationList = PatientRegistrationList?.Count > 0 ? PatientRegistrationList : new List<HospitalPatientRegistrationModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Get PatientRegistration by PatientRegistration id.
        public virtual HospitalPatientRegistrationModel GetPatientRegistrationOtherDetail(long hospitalPatientRegistrationId)
        {
            if (hospitalPatientRegistrationId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "HospitalPatientRegistrationId"));

            //Get the PatientRegistration Details based on id.
            HospitalPatientRegistration hospitalPatientRegistration = _hospitalPatientRegistrationRepository.Table.Where(x => x.HospitalPatientRegistrationId == hospitalPatientRegistrationId)?.FirstOrDefault();
            HospitalPatientRegistrationModel hospitalPatientRegistrationModel = IsNotNull(hospitalPatientRegistration) ? hospitalPatientRegistration?.FromEntityToModel<HospitalPatientRegistrationModel>() : new HospitalPatientRegistrationModel();
            if (IsNotNull(hospitalPatientRegistrationModel))
            {
                GeneralPersonModel generalPersonModel = GetGeneralPersonDetails(hospitalPatientRegistrationModel.PersonId);
                if (IsNotNull(hospitalPatientRegistrationModel))
                {
                    hospitalPatientRegistrationModel.FirstName = generalPersonModel.FirstName;
                    hospitalPatientRegistrationModel.LastName = generalPersonModel.LastName;

                }
            }
            return hospitalPatientRegistrationModel;
        }

        //Delete PatientRegistration.
        public virtual bool DeletePatientRegistration(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "PatientRegistrationID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("HospitalPatientRegistrationId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteHospitalPatientRegistration @HospitalPatientRegistrationId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Protected Method
        //Check if Person code is already present or not.
        protected virtual bool IsPersonCodeAlreadyExist(string personCode, long hospitalPatientRegistrationId = 0)
         => _hospitalPatientRegistrationRepository.Table.Any(x => x.UAHNumber == personCode && (x.HospitalPatientRegistrationId != hospitalPatientRegistrationId || hospitalPatientRegistrationId == 0));

        #endregion
    }
}
