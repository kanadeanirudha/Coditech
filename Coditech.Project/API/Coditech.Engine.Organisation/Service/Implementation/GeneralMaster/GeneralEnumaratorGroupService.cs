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
    public class GeneralEnumaratorGroupService : IGeneralEnumaratorGroupService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<GeneralEnumaratorGroup> _generalEnumaratorGroupRepository;
        //private readonly ICoditechRepository<GeneralRegionMaster> _generalRegionMasterRepository;
        public GeneralEnumaratorGroupService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _generalEnumaratorGroupRepository = new CoditechRepository<GeneralEnumaratorGroup>(_serviceProvider.GetService<Coditech_Entities>());
           // _generalRegionMasterRepository = new CoditechRepository<GeneralRegionMaster>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual GeneralEnumaratorGroupListModel GetEnumaratorGroupList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<GeneralEnumaratorGroupModel> objStoredProc = new CoditechViewRepository<GeneralEnumaratorGroupModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<GeneralEnumaratorGroupModel> EnumaratorGroupList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetEnumaratorGroupList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            GeneralEnumaratorGroupListModel listModel = new GeneralEnumaratorGroupListModel();

            listModel.GeneralEnumaratorGroupList = EnumaratorGroupList?.Count > 0 ? EnumaratorGroupList : new List<GeneralEnumaratorGroupModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Create EnumaratorGroup.
        public virtual GeneralEnumaratorGroupModel CreateEnumaratorGroup(GeneralEnumaratorGroupModel generalEnumaratorGroupModel)
        {
            if (IsNull(generalEnumaratorGroupModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);
            if (IsEnumaratorGroupNameAlreadyExist(generalEnumaratorGroupModel.EnumGroup))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "EnumaratorGroup Name"));
            GeneralEnumaratorGroup generalEnumaratorGroup = generalEnumaratorGroupModel.FromModelToEntity<GeneralEnumaratorGroup>();

            //Create new EnumaratorGroup and return it.
            GeneralEnumaratorGroup enumaratorGroupData = _generalEnumaratorGroupRepository.Insert(generalEnumaratorGroup);
            if (enumaratorGroupData?.GeneralEnumaratorGroupId > 0)
            {
                generalEnumaratorGroupModel.GeneralEnumaratorGroupId = enumaratorGroupData.GeneralEnumaratorGroupId;
            }
            else
            {
                generalEnumaratorGroupModel.HasError = true;
                generalEnumaratorGroupModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return generalEnumaratorGroupModel;
        }

        //Get EnumaratorGroup by EnumaratorGroup id.
        public virtual GeneralEnumaratorGroupModel GetEnumaratorGroup(int EnumaratorGroupId)
        {
            if (EnumaratorGroupId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "EnumaratorGroupID"));

            //Get the EnumaratorGroup Details based on id.
            GeneralEnumaratorGroup enumaratorGroupData = _generalEnumaratorGroupRepository.Table.FirstOrDefault(x => x.GeneralEnumaratorGroupId == EnumaratorGroupId);
            GeneralEnumaratorGroupModel generalEnumaratorGroupModel = enumaratorGroupData.FromEntityToModel<GeneralEnumaratorGroupModel>();
            if (IsNotNull(generalEnumaratorGroupModel))
            {
                generalEnumaratorGroupModel.GeneralEnumaratorGroupId = _generalEnumaratorGroupRepository.Table.FirstOrDefault(x => x.GeneralEnumaratorGroupId == generalEnumaratorGroupModel.GeneralEnumaratorGroupId).GeneralEnumaratorGroupId;
            }
            return generalEnumaratorGroupModel;
        }

        //Update EnumaratorGroup.
        public virtual bool UpdateEnumaratorGroup(GeneralEnumaratorGroupModel generalEnumaratorGroupModel)
        {
            if (IsNull(generalEnumaratorGroupModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (generalEnumaratorGroupModel.GeneralEnumaratorGroupId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "EnumaratorGroupID"));

            if (IsEnumaratorGroupNameAlreadyExist(generalEnumaratorGroupModel.EnumGroup, generalEnumaratorGroupModel.GeneralEnumaratorGroupId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "EnumaratorGroup Name"));

            GeneralEnumaratorGroup GeneralEnumaratorGroup = generalEnumaratorGroupModel.FromModelToEntity<GeneralEnumaratorGroup>();

            //Update EnumaratorGroup
            bool isEnumaratorGroupUpdated = _generalEnumaratorGroupRepository.Update(GeneralEnumaratorGroup);
            if (!isEnumaratorGroupUpdated)
            {
                generalEnumaratorGroupModel.HasError = true;
                generalEnumaratorGroupModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isEnumaratorGroupUpdated;
        }

        //Delete EnumaratorGroup.
        public virtual bool DeleteEnumaratorGroup(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "EnumaratorGroupID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("EnumaratorGroupId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteEnumaratorGroup @EnumaratorGroupId,  @Status OUT", 1, out status);
            return status == 1 ? true : false;
        }

        #region Protected Method
        //Check if EnumaratorGroup code is already present or not.
        protected virtual bool IsEnumaratorGroupNameAlreadyExist(string EnumaratorGroupName, int GeneralEnumaratorGroupId = 0)
         => _generalEnumaratorGroupRepository.Table.Any(x => x.EnumGroup == EnumaratorGroupName && (x.GeneralEnumaratorGroupId != GeneralEnumaratorGroupId || GeneralEnumaratorGroupId == 0));
        #endregion
    }
}
