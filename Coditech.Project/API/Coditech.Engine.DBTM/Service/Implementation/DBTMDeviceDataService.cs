using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Logger;
using Coditech.Resources;

using static Coditech.Common.Helper.HelperUtility;
namespace Coditech.API.Service
{
    public class DBTMDeviceDataService : IDBTMDeviceDataService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<DBTMDeviceData> _dBTMDeviceDataRepository;
        private readonly ICoditechRepository<DBTMDeviceDataDetails> _dBTMDeviceDataDetailsRepository;

        public DBTMDeviceDataService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider) 
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _dBTMDeviceDataRepository = new CoditechRepository<DBTMDeviceData>(_serviceProvider.GetService<Coditech_Entities>());
            _dBTMDeviceDataDetailsRepository = new CoditechRepository<DBTMDeviceDataDetails>(_serviceProvider.GetService<Coditech_Entities>());
        }

        //Add DBTMDeviceData.
        public virtual DBTMDeviceDataModel InsertDeviceData(DBTMDeviceDataModel dBTMDeviceDataModel)
        {
            if (IsNull(dBTMDeviceDataModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            DBTMDeviceData dBTMDeviceData = new DBTMDeviceData()
            {
                DeviceSerialCode = dBTMDeviceDataModel.DeviceSerialCode,
                PersonCode = dBTMDeviceDataModel.PersonCode,
               TestCode= dBTMDeviceDataModel.TestCode,
                Comments= dBTMDeviceDataModel.Comments
            };

            //Add new DBTMDeviceData and return it.
            DBTMDeviceData DBTMDeviceDataDetails = _dBTMDeviceDataRepository.Insert(dBTMDeviceData);
            if (DBTMDeviceDataDetails?.DBTMDeviceDataId > 0)
            {
                dBTMDeviceDataModel.DBTMDeviceDataId = DBTMDeviceDataDetails.DBTMDeviceDataId;

                DBTMDeviceDataDetails dBTMDeviceDataDetails = new DBTMDeviceDataDetails()
                {
                    DBTMDeviceDataId = dBTMDeviceData.DBTMDeviceDataId,
                    Weight= dBTMDeviceDataModel.Weight,
                    Height= dBTMDeviceDataModel.Height,
                    Time= dBTMDeviceDataModel.Time,
                    Distance= dBTMDeviceDataModel.Distance,
                    Force= dBTMDeviceDataModel.Force,
                    Acceleration= dBTMDeviceDataModel.Acceleration,
                    Angle= dBTMDeviceDataModel.Angle
                };

                dBTMDeviceDataDetails = _dBTMDeviceDataDetailsRepository.Insert(dBTMDeviceDataDetails);
            }
            else
            {
                dBTMDeviceDataModel.HasError = true;
                dBTMDeviceDataModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return dBTMDeviceDataModel;
        }
    }
}
