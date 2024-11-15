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
        //private readonly ICoditech _dBTMDeviceMasterRepository;
        public DBTMNewRegistrationService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            //_dBTMNewRegistration = dBTMNewRegistration;
            //_dBTMDeviceMasterRepository = new CoditechRepository<DBTMDeviceMaster>(_serviceProvider.GetService<Coditech_Entities>());
        }

        //Create DBTMDevice.
        public virtual DBTMNewRegistrationModel DBTMNewRegistration(DBTMNewRegistrationModel dBTMNewRegistrationModel)
        {
            if (IsNull(dBTMNewRegistrationModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

           // DBTMDeviceMaster dBTMDeviceMaster = dBTMDeviceModel.FromModelToEntity<DBTMDeviceMaster>();

            //Create new DBTMDevice and return it.
            //DBTMDeviceMaster dBTMDeviceData = _dBTMDeviceMasterRepository.Insert(dBTMDeviceMaster);
            //if (dBTMDeviceData?.DBTMDeviceMasterId > 0)
            //{
            //    dBTMDeviceModel.DBTMDeviceMasterId = dBTMDeviceData.DBTMDeviceMasterId;
            //}
            //else
            //{
            //    dBTMDeviceModel.HasError = true;
            //    dBTMDeviceModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            //}
            return dBTMNewRegistrationModel;
        }
    }
}
