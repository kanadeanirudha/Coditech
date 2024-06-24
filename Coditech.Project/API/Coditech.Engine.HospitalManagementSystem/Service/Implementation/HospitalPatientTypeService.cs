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
    public class HospitalPatientTypeService : BaseService, IHospitalPatientTypeService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<HospitalPatientType> _hospitalPatientTypeRepository;
        public HospitalPatientTypeService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _hospitalPatientTypeRepository = new CoditechRepository<HospitalPatientType>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual HospitalPatientTypeListModel GetHospitalPatientTypeList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<HospitalPatientTypeModel> objStoredProc = new CoditechViewRepository<HospitalPatientTypeModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<HospitalPatientTypeModel> hospitalPatientTypeList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetHospitalPatientTypeList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            HospitalPatientTypeListModel listModel = new HospitalPatientTypeListModel();

            listModel.HospitalPatientTypeList = hospitalPatientTypeList?.Count > 0 ? hospitalPatientTypeList : new List<HospitalPatientTypeModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Create Hospital PatientType.
        public virtual HospitalPatientTypeModel CreateHospitalPatientType(HospitalPatientTypeModel hospitalPatientTypeModel)
        {
            if (IsNull(hospitalPatientTypeModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsPatientTypeAlreadyExist(hospitalPatientTypeModel.PatientType))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Hospital Patient Type"));

            HospitalPatientType hospitalPatientType = hospitalPatientTypeModel.FromModelToEntity<HospitalPatientType>();

            //Create new Hospital PatientType and return it.
            HospitalPatientType hospitalPatientTypeData = _hospitalPatientTypeRepository.Insert(hospitalPatientType);
            if (hospitalPatientTypeData?.HospitalPatientTypeId > 0)
            {
                hospitalPatientTypeModel.HospitalPatientTypeId = hospitalPatientTypeData.HospitalPatientTypeId;
            }
            else
            {
                hospitalPatientTypeModel.HasError = true;
                hospitalPatientTypeModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return hospitalPatientTypeModel;
        }


        //Get HospitalPatientType by  hospital PatientType id.
        public virtual HospitalPatientTypeModel GetHospitalPatientType(byte hospitalPatientTypeId)
        {
            if (hospitalPatientTypeId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "HospitalPatientTypeId"));

            //Get the HospitalPatientType Details based on id.
            HospitalPatientType hospitalPatientType = _hospitalPatientTypeRepository.Table.FirstOrDefault(x => x.HospitalPatientTypeId == hospitalPatientTypeId);
            HospitalPatientTypeModel hospitalPatientTypeModel = hospitalPatientType?.FromEntityToModel<HospitalPatientTypeModel>();           
            return hospitalPatientTypeModel;
        }


        //Update HospitalPatientType.
        public virtual bool UpdateHospitalPatientType(HospitalPatientTypeModel hospitalPatientTypeModel)
        {
            if (IsNull(hospitalPatientTypeModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (hospitalPatientTypeModel.HospitalPatientTypeId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "HospitalPatientTypeId"));

            if (IsPatientTypeAlreadyExist(hospitalPatientTypeModel.PatientType, hospitalPatientTypeModel.HospitalPatientTypeId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Patient Type"));

            HospitalPatientType hospitalPatientType = hospitalPatientTypeModel.FromModelToEntity<HospitalPatientType>();

            //Update Hospital PatientType
            bool isHospitalPatientTypeUpdated = _hospitalPatientTypeRepository.Update(hospitalPatientType);
            if (!isHospitalPatientTypeUpdated)
            {
                hospitalPatientTypeModel.HasError = true;
                hospitalPatientTypeModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isHospitalPatientTypeUpdated;
        }

        //Delete Hospital PatientType.
        public virtual bool DeleteHospitalPatientType(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "HospitalPatientTypeId"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("HospitalPatientTypeId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteHospitalPatientType @HospitalPatientTypeId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Protected Method
        // Check if HospitalPatientTypeId is already present or not.
        protected virtual bool IsPatientTypeAlreadyExist(string patientType, byte hospitalPatientTypeId = 0)
         => _hospitalPatientTypeRepository.Table.Any(x => x.PatientType == patientType && (x.HospitalPatientTypeId != hospitalPatientTypeId || hospitalPatientTypeId == 0));
        #endregion
    }
}
