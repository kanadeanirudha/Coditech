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
    public class DBTMNewRegistrationService : IDBTMNewRegistrationService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<OrganisationCentreMaster> _organisationCentreMasterRepository;

        //private readonly ICoditech _dBTMDeviceMasterRepository;
        public DBTMNewRegistrationService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _organisationCentreMasterRepository = new CoditechRepository<OrganisationCentreMaster>(_serviceProvider.GetService<Coditech_Entities>());
            //_dBTMNewRegistration = dBTMNewRegistration;
            //_dBTMDeviceMasterRepository = new CoditechRepository<DBTMDeviceMaster>(_serviceProvider.GetService<Coditech_Entities>());
        }

        //Create DBTMDevice.
        public virtual DBTMNewRegistrationModel DBTMNewRegistration(DBTMNewRegistrationModel dBTMNewRegistrationModel)
        {
            if (IsNull(dBTMNewRegistrationModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsCentreNameAlreadyExist(dBTMNewRegistrationModel.CentreName))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Centre Name"));


            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@CentreCode", dBTMNewRegistrationModel.CentreCode, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@CentreName", dBTMNewRegistrationModel.CentreName, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@FirstName", dBTMNewRegistrationModel.FirstName, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@LastName", dBTMNewRegistrationModel.LastName, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@EmailId", dBTMNewRegistrationModel.EmailId, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@GeneralCityMasterId", dBTMNewRegistrationModel.GeneralCityMasterId, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@GeneralCountryMasterId", dBTMNewRegistrationModel.GeneralCountryMasterId, ParameterDirection.Input, DbType.SByte);
            objStoredProc.SetParameter("@GeneralRegionMasterId", dBTMNewRegistrationModel.GeneralRegionMasterId, ParameterDirection.Input, DbType.SByte);
            objStoredProc.SetParameter("@AddressLine1", dBTMNewRegistrationModel.AddressLine1, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@AddressLine2", dBTMNewRegistrationModel.AddressLine2, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@Pincode", dBTMNewRegistrationModel.Pincode, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@CellPhone", dBTMNewRegistrationModel.CellPhone, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@DeviceSerialCode", dBTMNewRegistrationModel.DeviceSerialCode, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@Password", dBTMNewRegistrationModel.Password, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@TermsAndCondition", dBTMNewRegistrationModel.TermsAndCondition, ParameterDirection.Input, DbType.Boolean);
            objStoredProc.SetParameter("@CallingCode", dBTMNewRegistrationModel.TermsAndCondition, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_InsertDBTMCenterRegistration @DBTMNewRegistrationId,  @Status OUT", 15, out status);
            if (status == 0)
            {
                dBTMNewRegistrationModel.HasError = true;
                dBTMNewRegistrationModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return dBTMNewRegistrationModel;
        }
        public virtual bool IsCentreNameAlreadyExist(string centreName)
        {
            if (string.IsNullOrWhiteSpace(centreName))
            {
                throw new ArgumentException("Centre name cannot be null or empty");
            }
            // Return true if the device code exists in the repository, false otherwise
            return _organisationCentreMasterRepository.Table.Any(x => x.CentreName == centreName);
        }
    }
}
