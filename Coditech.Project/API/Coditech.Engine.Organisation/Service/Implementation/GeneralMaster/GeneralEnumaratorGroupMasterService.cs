
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
    public class GeneralEnumaratorGroupMasterService : IGeneralEnumaratorGroupMasterService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<GeneralEnumaratorGroupMaster> _generalEnumaratorGroupRepository;
        public GeneralEnumaratorGroupMasterService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _generalEnumaratorGroupRepository = new CoditechRepository<GeneralEnumaratorGroupMaster>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual GeneralEnumaratorGroupListModel GetGeneralEnumaratorGroupList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<GeneralEnumaratorGroupModel> objStoredProc = new CoditechViewRepository<GeneralEnumaratorGroupModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<GeneralEnumaratorGroupModel> GeneralEnumaratorGroupList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetGeneralEnumaratorGroupList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            GeneralEnumaratorGroupListModel listModel = new GeneralEnumaratorGroupListModel();

            listModel.GeneralEnumaratorGroupList = GeneralEnumaratorGroupList?.Count > 0 ? GeneralEnumaratorGroupList : new List<GeneralEnumaratorGroupModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }
        //Create GeneralEnumaratorGroup.
        public virtual GeneralEnumaratorGroupModel CreateGeneralEnumaratorGroup(GeneralEnumaratorGroupModel generalEnumaratorGroupModel)
        {
            if (IsNull(generalEnumaratorGroupModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsGeneralEnumaratorGroupCodeAlreadyExist(generalEnumaratorGroupModel.EnumGroupCode))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "EnumGroup Code"));

            GeneralEnumaratorGroupMaster generalEnumaratorGroupMaster = generalEnumaratorGroupModel.FromModelToEntity<GeneralEnumaratorGroupMaster>();

            //Create new GeneralEnumaratorGroup and return it.
            GeneralEnumaratorGroupMaster enumaratorGroupData = _generalEnumaratorGroupRepository.Insert(generalEnumaratorGroupMaster);
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

        //Get GeneralEnumaratorGroup by GeneralEnumaratorGroup id.
        public virtual GeneralEnumaratorGroupModel GetGeneralEnumaratorGroup(int generalEnumaratorGroupId)
        {
            if (generalEnumaratorGroupId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "GeneralEnumaratorGroupID"));

            //Get the GeneralEnumaratorGroup Details based on id.
            GeneralEnumaratorGroupMaster generalEnumaratorGroupMaster = _generalEnumaratorGroupRepository.Table.FirstOrDefault(x => x.GeneralEnumaratorGroupId == generalEnumaratorGroupId);
            GeneralEnumaratorGroupModel generalEnumaratorGroupModel = generalEnumaratorGroupMaster?.FromEntityToModel<GeneralEnumaratorGroupModel>();
            return generalEnumaratorGroupModel;
        }

        //Update GeneralEnumaratorGroup.
        public virtual bool UpdateGeneralEnumaratorGroup(GeneralEnumaratorGroupModel generalEnumaratorGroupModel)
        {
            if (IsNull(generalEnumaratorGroupModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (generalEnumaratorGroupModel.GeneralEnumaratorGroupId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "GeneralEnumaratorGroupID"));

            if (IsGeneralEnumaratorGroupCodeAlreadyExist(generalEnumaratorGroupModel.EnumGroupCode, generalEnumaratorGroupModel.GeneralEnumaratorGroupId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "GeneralEnumaratorGroup Code"));

            GeneralEnumaratorGroupMaster generalEnumaratorGroupMaster = generalEnumaratorGroupModel.FromModelToEntity<GeneralEnumaratorGroupMaster>();

            //Update GeneralEnumaratorGroup
            bool isGeneralEnumaratorGroupUpdated = _generalEnumaratorGroupRepository.Update(generalEnumaratorGroupMaster);
            if (!isGeneralEnumaratorGroupUpdated)
            {
                generalEnumaratorGroupModel.HasError = true;
                generalEnumaratorGroupModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isGeneralEnumaratorGroupUpdated;
        }

        //Delete GeneralEnumaratorGroup.
        public virtual bool DeleteGeneralEnumaratorGroup(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "GeneralEnumaratorGroupID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("GeneralEnumaratorGroupId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteGeneralEnumaratorGroup @GeneralEnumaratorGroupId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Protected Method
        //Check if GeneralEnumaratorGroup code is already present or not.
        protected virtual bool IsGeneralEnumaratorGroupCodeAlreadyExist(string disaplyText, int generalEnumaratorGroupId = 0)
         => _generalEnumaratorGroupRepository.Table.Any(x => x.DisaplyText == disaplyText && (x.GeneralEnumaratorGroupId != generalEnumaratorGroupId || generalEnumaratorGroupId == 0));
        #endregion
    }
}
