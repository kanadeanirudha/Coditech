
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
    public class GymMemberBodyMeasurementService : BaseService, IGymMemberBodyMeasurementService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<GymMemberBodyMeasurement> _gymMemberBodyMeasurementRepository;
        private readonly ICoditechRepository<GymBodyMeasurementType> _gymBodyMeasurementTypeRepository;
        private readonly ICoditechRepository<GeneralMeasurementUnitMaster> _generalMeasurementUnitMasterRepository;

        public GymMemberBodyMeasurementService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _gymBodyMeasurementTypeRepository = new CoditechRepository<GymBodyMeasurementType>(_serviceProvider.GetService<Coditech_Entities>());
            _generalMeasurementUnitMasterRepository = new CoditechRepository<GeneralMeasurementUnitMaster>(_serviceProvider.GetService<Coditech_Entities>());
            _gymMemberBodyMeasurementRepository = new CoditechRepository<GymMemberBodyMeasurement>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual GymMemberBodyMeasurementListModel GetBodyMeasurementTypeListByMemberId(int gymMemberDetailId, long personId, short pageSize)
        {

            GymMemberBodyMeasurementListModel listModel = new GymMemberBodyMeasurementListModel();
            listModel.GymMemberBodyMeasurementList = new List<GymMemberBodyMeasurementModel>();

            listModel.GymMemberBodyMeasurementList = (from a in _gymBodyMeasurementTypeRepository.Table
                                                      join b in _generalMeasurementUnitMasterRepository.Table
                                                      on a.GeneralMeasurementUnitMasterId equals b.GeneralMeasurementUnitMasterId
                                                      where a.IsActive
                                                      select new GymMemberBodyMeasurementModel
                                                      {
                                                          GymBodyMeasurementTypeId = a.GymBodyMeasurementTypeId,
                                                          BodyMeasurementType = a.BodyMeasurementType,
                                                          MeasurementUnitShortCode = b.MeasurementUnitShortCode,
                                                          MeasurementUnitDisplayName = b.MeasurementUnitDisplayName
                                                      })?.ToList();

            foreach (GymMemberBodyMeasurementModel item in listModel?.GymMemberBodyMeasurementList)
            {
                List<GymMemberBodyMeasurement> gymMemberBodyMeasurementValueList = _gymMemberBodyMeasurementRepository.Table.Where(x => x.GymMemberDetailId == gymMemberDetailId && x.GymBodyMeasurementTypeId == item.GymBodyMeasurementTypeId)?.OrderByDescending(y => y.CreatedDate)?.Take(pageSize)?.ToList();
                if (gymMemberBodyMeasurementValueList?.Count > 0)
                {
                    item.GymMemberBodyMeasurementValueList = new List<GymMemberBodyMeasurementValueModel>();
                    foreach (GymMemberBodyMeasurement gymMemberBodyMeasurement in gymMemberBodyMeasurementValueList)
                    {
                        item.GymMemberBodyMeasurementValueList.Add(new GymMemberBodyMeasurementValueModel()
                        {
                            BodyMeasurementType = item.BodyMeasurementType,
                            BodyMeasurementValue = gymMemberBodyMeasurement.BodyMeasurementValue,
                            MeasurementUnitDisplayName = item.MeasurementUnitDisplayName,
                            MeasurementUnitShortCode = item.MeasurementUnitShortCode,
                            CreatedDate = Convert.ToDateTime(gymMemberBodyMeasurement.CreatedDate)
                        });
                    }
                }
            }
            GeneralPerson generalPerson = GetGeneralPersonDetails(personId);
            if (IsNotNull(generalPerson))
            {
                listModel.FirstName = generalPerson.FirstName;
                listModel.LastName = generalPerson.LastName;
            }
            listModel.GymMemberDetailId = gymMemberDetailId;
            listModel.PersonId = personId;
            return listModel;
        }

        public virtual GymMemberBodyMeasurementListModel GetMemberBodyMeasurementList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<GymMemberBodyMeasurementModel> objStoredProc = new CoditechViewRepository<GymMemberBodyMeasurementModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<GymMemberBodyMeasurementModel> MemberBodyMeasurementList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetMemberBodyMeasurementList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            GymMemberBodyMeasurementListModel listModel = new GymMemberBodyMeasurementListModel();

            listModel.GymMemberBodyMeasurementList = MemberBodyMeasurementList?.Count > 0 ? MemberBodyMeasurementList : new List<GymMemberBodyMeasurementModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }
        //Create MemberBodyMeasurement.
        public virtual GymMemberBodyMeasurementModel CreateMemberBodyMeasurement(GymMemberBodyMeasurementModel GymMemberBodyMeasurementModel)
        {
            if (IsNull(GymMemberBodyMeasurementModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            //if (IsMemberBodyMeasurementCodeAlreadyExist(GymMemberBodyMeasurementModel.FirstName))
            //    throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "First Name"));

            GymMemberBodyMeasurement gymMemberBodyMeasurement = GymMemberBodyMeasurementModel.FromModelToEntity<GymMemberBodyMeasurement>();

            //Create new MemberBodyMeasurement and return it.
            GymMemberBodyMeasurement MemberBodyMeasurementData = _gymMemberBodyMeasurementRepository.Insert(gymMemberBodyMeasurement);
            if (MemberBodyMeasurementData?.GymMemberBodyMeasurementId > 0)
            {
                GymMemberBodyMeasurementModel.GymMemberBodyMeasurementId = MemberBodyMeasurementData.GymMemberBodyMeasurementId;
            }
            else
            {
                GymMemberBodyMeasurementModel.HasError = true;
                GymMemberBodyMeasurementModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return GymMemberBodyMeasurementModel;
        }

        //Get MemberBodyMeasurement by MemberBodyMeasurement id.
        public virtual GymMemberBodyMeasurementModel GetMemberBodyMeasurement(long MemberBodyMeasurementId)
        {
            if (MemberBodyMeasurementId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "MemberBodyMeasurementID"));

            //Get the MemberBodyMeasurement Details based on id.
            GymMemberBodyMeasurement gymMemberBodyMeasurement = _gymMemberBodyMeasurementRepository.Table.FirstOrDefault(x => x.GymMemberBodyMeasurementId == MemberBodyMeasurementId);
            GymMemberBodyMeasurementModel GymMemberBodyMeasurementModel = gymMemberBodyMeasurement?.FromEntityToModel<GymMemberBodyMeasurementModel>();
            return GymMemberBodyMeasurementModel;
        }

        //Update MemberBodyMeasurement.
        public virtual bool UpdateMemberBodyMeasurement(GymMemberBodyMeasurementModel GymMemberBodyMeasurementModel)
        {
            if (IsNull(GymMemberBodyMeasurementModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (GymMemberBodyMeasurementModel.GymMemberBodyMeasurementId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "MemberBodyMeasurementID"));

            //if (IsMemberBodyMeasurementCodeAlreadyExist(GymMemberBodyMeasurementModel.FirstName, GymMemberBodyMeasurementModel.GymMemberBodyMeasurementId))
            //    throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "FirstName"));

            GymMemberBodyMeasurement gymMemberBodyMeasurement = GymMemberBodyMeasurementModel.FromModelToEntity<GymMemberBodyMeasurement>();

            //Update MemberBodyMeasurement
            bool isMemberBodyMeasurementUpdated = _gymMemberBodyMeasurementRepository.Update(gymMemberBodyMeasurement);
            if (!isMemberBodyMeasurementUpdated)
            {
                GymMemberBodyMeasurementModel.HasError = true;
                GymMemberBodyMeasurementModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isMemberBodyMeasurementUpdated;
        }

        //Delete MemberBodyMeasurement.
        public virtual bool DeleteMemberBodyMeasurement(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "MemberBodyMeasurementID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("MemberBodyMeasurementId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteMemberBodyMeasurement @MemberBodyMeasurementId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Protected Method
        //Check if MemberBodyMeasurement code is already present or not.
        protected virtual bool IsMemberBodyMeasurementCodeAlreadyExist(int gymMemberDetailId, long gymMemberBodyMeasurementId = 0)
         => _gymMemberBodyMeasurementRepository.Table.Any(x => x.GymMemberDetailId == gymMemberDetailId && (x.GymMemberBodyMeasurementId != gymMemberBodyMeasurementId || gymMemberBodyMeasurementId == 0));
        #endregion
    }
}
