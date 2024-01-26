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
        private readonly ICoditechRepository<GeneralEnumaratorMaster> _generalEnumaratorMasterRepository;
        public GeneralEnumaratorGroupService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _generalEnumaratorGroupRepository = new CoditechRepository<GeneralEnumaratorGroup>(_serviceProvider.GetService<Coditech_Entities>());
            _generalEnumaratorMasterRepository = new CoditechRepository<GeneralEnumaratorMaster>(_serviceProvider.GetService<Coditech_Entities>());
        }

        #region EnumaratorGroup
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
            if (IsEnumaratorGroupNameAlreadyExist(generalEnumaratorGroupModel.EnumGroupCode))
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
        public virtual GeneralEnumaratorGroupModel GetEnumaratorGroup(int enumaratorGroupId)
        {
            if (enumaratorGroupId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "EnumaratorGroupID"));

            //Get the EnumaratorGroup Details based on id.
            GeneralEnumaratorGroup enumaratorGroupData = _generalEnumaratorGroupRepository.Table.FirstOrDefault(x => x.GeneralEnumaratorGroupId == enumaratorGroupId);
            GeneralEnumaratorGroupModel generalEnumaratorGroupModel = enumaratorGroupData.FromEntityToModel<GeneralEnumaratorGroupModel>();
            if (IsNotNull(generalEnumaratorGroupModel))
            {
                generalEnumaratorGroupModel.GeneralEnumaratorGroupId = _generalEnumaratorGroupRepository.Table.FirstOrDefault(x => x.GeneralEnumaratorGroupId == generalEnumaratorGroupModel.GeneralEnumaratorGroupId).GeneralEnumaratorGroupId;
                if (generalEnumaratorGroupModel.GeneralEnumaratorGroupId > 0)
                {
                    List<GeneralEnumaratorMaster> list = _generalEnumaratorMasterRepository.Table.Where(x => x.GeneralEnumaratorGroupId == generalEnumaratorGroupModel.GeneralEnumaratorGroupId)?.ToList();
                    if (list?.Count > 0)
                    {
                        generalEnumaratorGroupModel.GeneralEnumaratorList = new List<GeneralEnumaratorModel>();
                        GeneralEnumaratorModel generalEnumaratorModel = new GeneralEnumaratorModel();
                        foreach (GeneralEnumaratorMaster item in list?.OrderBy(x => x.SequenceNumber))
                        {
                            generalEnumaratorModel = item.FromEntityToModel<GeneralEnumaratorModel>();
                            generalEnumaratorGroupModel.GeneralEnumaratorList.Add(generalEnumaratorModel);
                        }
                    }
                }
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

            if (IsEnumaratorGroupNameAlreadyExist(generalEnumaratorGroupModel.EnumGroupCode, generalEnumaratorGroupModel.GeneralEnumaratorGroupId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "EnumaratorGroup Name"));

            GeneralEnumaratorGroup generalEnumaratorGroup = generalEnumaratorGroupModel.FromModelToEntity<GeneralEnumaratorGroup>();

            //Update EnumaratorGroup
            bool isEnumaratorGroupUpdated = _generalEnumaratorGroupRepository.Update(generalEnumaratorGroup);
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
            objStoredProc.SetParameter("EnumaratorGroupIds", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteEnumaratorGroup @EnumaratorGroupId,  @Status OUT", 1, out status);
            return status == 1 ? true : false;
        }
        #endregion

        #region Enumarator
        //Create EnumaratorGroup.
        public virtual GeneralEnumaratorModel InsertUpdateEnumarator(GeneralEnumaratorModel generalEnumaratorModel)
        {
            if (IsNull(generalEnumaratorModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsEnumaratorNameAlreadyExist(generalEnumaratorModel.EnumName, generalEnumaratorModel.GeneralEnumaratorGroupId, generalEnumaratorModel.GeneralEnumaratorId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Enumarator Name"));

            if (generalEnumaratorModel.GeneralEnumaratorGroupId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "EnumaratorGroupID"));

            GeneralEnumaratorMaster generalEnumaratorMaster = generalEnumaratorModel.FromModelToEntity<GeneralEnumaratorMaster>();
            if (generalEnumaratorModel.GeneralEnumaratorId > 0)
            {
                if (generalEnumaratorModel.GeneralEnumaratorId < 1)
                    throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "EnumaratorGroupID"));

                //Update Enumarator
                bool isEnumaratorGroupUpdated = _generalEnumaratorMasterRepository.Update(generalEnumaratorMaster);
                if (!isEnumaratorGroupUpdated)
                {
                    generalEnumaratorModel.HasError = true;
                    generalEnumaratorModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
                }
            }
            else
            {
                //Create new Enumarator and return it.
                GeneralEnumaratorMaster enumaratorData = _generalEnumaratorMasterRepository.Insert(generalEnumaratorMaster);
                if (enumaratorData?.GeneralEnumaratorId > 0)
                {
                    generalEnumaratorModel.GeneralEnumaratorId = enumaratorData.GeneralEnumaratorId;
                }
                else
                {
                    generalEnumaratorModel.HasError = true;
                    generalEnumaratorModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
                }
            }
            return generalEnumaratorModel;
        }

        //Get Enumarator by Enumarator id.
        public virtual GeneralEnumaratorModel GetEnumarator(int enumaratorId)
        {
            if (enumaratorId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "EnumaratorID"));

            //Get the Enumarator Details based on id.
            GeneralEnumaratorMaster enumaratorData = _generalEnumaratorMasterRepository.Table.FirstOrDefault(x => x.GeneralEnumaratorId == enumaratorId);
            GeneralEnumaratorModel generalEnumaratorModel = enumaratorData.FromEntityToModel<GeneralEnumaratorModel>();
            return generalEnumaratorModel;
        }

        //Delete Enumarator.
        public virtual bool DeleteEnumarator(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "EnumaratorID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("GeneralEnumaratorId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteEnumarator @GeneralEnumaratorId,  @Status OUT", 1, out status);
            return status == 1 ? true : false;
        }
        #endregion

        #region Protected Method
        //Check if EnumaratorGroup code is already present or not.
        protected virtual bool IsEnumaratorGroupNameAlreadyExist(string enumGroupCode, int generalEnumaratorGroupId = 0)
         => _generalEnumaratorGroupRepository.Table.Any(x => x.EnumGroupCode == enumGroupCode && (x.GeneralEnumaratorGroupId != generalEnumaratorGroupId || generalEnumaratorGroupId == 0));

        //Check if Enumarator code is already present or not.
        protected virtual bool IsEnumaratorNameAlreadyExist(string enumName, int generalEnumaratorGroupId = 0, int generalEnumaratorId = 0)
         => _generalEnumaratorMasterRepository.Table.Any(x => x.EnumName == enumName && x.GeneralEnumaratorGroupId == generalEnumaratorGroupId && (x.GeneralEnumaratorId != generalEnumaratorId || generalEnumaratorId == 0));

        #endregion
    }
}
