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
    public class HospitalPathologyTestGroupService : IHospitalPathologyTestGroupService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<HospitalPathologyTestGroup> _hospitalPathologyTestGroupRepository;
        public HospitalPathologyTestGroupService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _hospitalPathologyTestGroupRepository = new CoditechRepository<HospitalPathologyTestGroup>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual HospitalPathologyTestGroupListModel GetHospitalPathologyTestGroupList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<HospitalPathologyTestGroupModel> objStoredProc = new CoditechViewRepository<HospitalPathologyTestGroupModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<HospitalPathologyTestGroupModel> hospitalPathologyTestGroupList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetHospitalPathologyTestGroupList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            HospitalPathologyTestGroupListModel listModel = new HospitalPathologyTestGroupListModel();

            listModel.HospitalPathologyTestGroupList = hospitalPathologyTestGroupList?.Count > 0 ? hospitalPathologyTestGroupList : new List<HospitalPathologyTestGroupModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }
        //Create HospitalPathologyTestGroup.
        public virtual HospitalPathologyTestGroupModel CreateHospitalPathologyTestGroup(HospitalPathologyTestGroupModel hospitalPathologyTestGroupModel)
        {
            if (IsNull(hospitalPathologyTestGroupModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            HospitalPathologyTestGroup hospitalPathologyTestGroup = hospitalPathologyTestGroupModel.FromModelToEntity<HospitalPathologyTestGroup>();

            //Create new HospitalPathologyTestGroup and return it.
            HospitalPathologyTestGroup hospitalPathologyTestGroupData = _hospitalPathologyTestGroupRepository.Insert(hospitalPathologyTestGroup);
            if (hospitalPathologyTestGroupData?.HospitalPathologyTestGroupId > 0)
            {
                hospitalPathologyTestGroupModel.HospitalPathologyTestGroupId = hospitalPathologyTestGroupData.HospitalPathologyTestGroupId;
            }
            else
            {
                hospitalPathologyTestGroupModel.HasError = true;
                hospitalPathologyTestGroupModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return hospitalPathologyTestGroupModel;
        }

        //Get HospitalPathologyTestGroup by HospitalPathologyTestGroup id.
        public virtual HospitalPathologyTestGroupModel GetHospitalPathologyTestGroup(int hospitalPathologyTestGroupId)
        {
            if (hospitalPathologyTestGroupId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "HospitalPathologyTestGroupID"));

            //Get the HospitalPathologyTestGroup Details based on id.
            HospitalPathologyTestGroup hospitalPathologyTestGroup = _hospitalPathologyTestGroupRepository.Table.FirstOrDefault(x => x.HospitalPathologyTestGroupId == hospitalPathologyTestGroupId);
            HospitalPathologyTestGroupModel hospitalPathologyTestGroupModel = hospitalPathologyTestGroup?.FromEntityToModel<HospitalPathologyTestGroupModel>();
            return hospitalPathologyTestGroupModel;
        }

        //Update HospitalPathologyTestGroup.
        public virtual bool UpdateHospitalPathologyTestGroup(HospitalPathologyTestGroupModel hospitalPathologyTestGroupModel)
        {
            if (IsNull(hospitalPathologyTestGroupModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (hospitalPathologyTestGroupModel.HospitalPathologyTestGroupId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "HospitalPathologyTestGroupID"));

            HospitalPathologyTestGroup hospitalPathologyTestGroup = hospitalPathologyTestGroupModel.FromModelToEntity<HospitalPathologyTestGroup>();

            //Update HospitalPathologyTestGroup
            bool isHospitalPathologyTestGroupUpdated = _hospitalPathologyTestGroupRepository.Update(hospitalPathologyTestGroup);
            if (!isHospitalPathologyTestGroupUpdated)
            {
                hospitalPathologyTestGroupModel.HasError = true;
                hospitalPathologyTestGroupModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isHospitalPathologyTestGroupUpdated;
        }

        //Delete HospitalPathologyTestGroup.
        public virtual bool DeleteHospitalPathologyTestGroup(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "HospitalPathologyTestGroupID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("HospitalPathologyTestGroupId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteHospitalPathologyTestGroup @HospitalPathologyTestGroupId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Protected Method
        #endregion
    }
}
