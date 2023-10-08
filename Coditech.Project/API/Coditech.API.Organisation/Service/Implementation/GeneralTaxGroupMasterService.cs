using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Resources;

using System.Collections.Specialized;
using System.Data;
using System.Text.RegularExpressions;

using static Coditech.Common.Helper.HelperUtility;

namespace Coditech.API.Service
{
    public class GeneralTaxGroupMasterService : IGeneralTaxGroupMasterService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<GeneralTaxMaster> _generalTaxMasterRepository;
        private readonly ICoditechRepository<GeneralTaxGroupMaster> _generalTaxGroupMasterRepository;
        private readonly ICoditechRepository<GeneralTaxGroupMasterDetails> _generalTaxGroupMasterDetailRepository;
        public GeneralTaxGroupMasterService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _generalTaxGroupMasterRepository = new CoditechRepository<GeneralTaxGroupMaster>(_serviceProvider.GetService<Coditech_Entities>());
            _generalTaxMasterRepository = new CoditechRepository<GeneralTaxMaster>(_serviceProvider.GetService<Coditech_Entities>());
            _generalTaxGroupMasterDetailRepository = new CoditechRepository<GeneralTaxGroupMasterDetails>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual GeneralTaxGroupMasterListModel GetTaxGroupMasterList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<GeneralTaxGroupModel> objStoredProc = new CoditechViewRepository<GeneralTaxGroupModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<GeneralTaxGroupModel> taxGroupMasterList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetTaxGroupList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            GeneralTaxGroupMasterListModel listModel = new GeneralTaxGroupMasterListModel();

            listModel.GeneralTaxGroupMasterList = taxGroupMasterList?.Count > 0 ? taxGroupMasterList : new List<GeneralTaxGroupModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Create Tax Group Master.
        public GeneralTaxGroupModel CreateTaxGroupMaster(GeneralTaxGroupModel generalTaxGroupModel)
        {
            if (IsNull(generalTaxGroupModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);
            if (IsTaxGroupNameAlreadyExist(generalTaxGroupModel.TaxGroupName))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Tax Group Name"));
            
            decimal? taxGroupRate = _generalTaxMasterRepository.Table.Where(x => generalTaxGroupModel.GeneralTaxMasterIds.Contains(x.GeneralTaxMasterId.ToString())).Sum(y => y.TaxRate);
            GeneralTaxGroupMaster generalTaxGroupMaster = new GeneralTaxGroupMaster()
            {
                TaxGroupName = generalTaxGroupModel.TaxGroupName,
                TaxGroupRate = taxGroupRate
            };

            //Create new Tax Group Master and return it.
            GeneralTaxGroupMaster taxGroupMasterData = _generalTaxGroupMasterRepository.Insert(generalTaxGroupMaster);
            if (taxGroupMasterData?.GeneralTaxGroupMasterId > 0)
            {
                generalTaxGroupModel.GeneralTaxGroupMasterId = taxGroupMasterData.GeneralTaxGroupMasterId;
                foreach (string genTaxMasterId in generalTaxGroupModel.GeneralTaxMasterIds)
                {
                    GeneralTaxGroupMasterDetails generalTaxGroupMasterDetail = new GeneralTaxGroupMasterDetails()
                    {
                        GeneralTaxGroupMasterId = generalTaxGroupModel.GeneralTaxGroupMasterId,
                        GeneralTaxMasterId = Convert.ToInt16(genTaxMasterId)
                    };
                    _generalTaxGroupMasterDetailRepository.Insert(generalTaxGroupMasterDetail);
                }
            }
            else
            {
                generalTaxGroupModel.HasError = true;
                generalTaxGroupModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return generalTaxGroupModel;
        }

        //Get Tax Group Master by GeneralTaxGroupMasterId.
        public GeneralTaxGroupModel GetTaxGroupMaster(short taxGroupMasterId)
        {
            if (taxGroupMasterId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "TaxGroupMasterId"));

            //Get the Tax Group Master Details based on id.
            GeneralTaxGroupMaster taxGroupMasterData = _generalTaxGroupMasterRepository.Table.FirstOrDefault(x => x.GeneralTaxGroupMasterId == taxGroupMasterId);
            GeneralTaxGroupModel generalTaxGroupModel = taxGroupMasterData.FromEntityToModel<GeneralTaxGroupModel>();
            generalTaxGroupModel.GeneralTaxMasterIds = _generalTaxGroupMasterDetailRepository.Table.Where(x => x.GeneralTaxGroupMasterId == taxGroupMasterId)?.Select(y => y.GeneralTaxMasterId.ToString())?.ToList();
            return generalTaxGroupModel;
        }

        //Update TaxGroupMaster.
        public virtual bool UpdateTaxGroupMaster(GeneralTaxGroupModel generalTaxGroupModel)
        {
            if (IsNull(generalTaxGroupModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (generalTaxGroupModel.GeneralTaxGroupMasterId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "TaxGroupMasterId"));
            
            if (IsTaxGroupNameAlreadyExist(generalTaxGroupModel.TaxGroupName, generalTaxGroupModel.GeneralTaxGroupMasterId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Tax Group Name"));

            decimal? taxGroupRate = _generalTaxMasterRepository.Table.Where(x => generalTaxGroupModel.GeneralTaxMasterIds.Contains(x.GeneralTaxMasterId.ToString())).Sum(y => y.TaxRate);
            GeneralTaxGroupMaster generalTaxGroupMaster = new GeneralTaxGroupMaster()
            {
                GeneralTaxGroupMasterId = generalTaxGroupModel.GeneralTaxGroupMasterId,
                TaxGroupName = generalTaxGroupModel.TaxGroupName,
                TaxGroupRate = taxGroupRate
            };

            bool isTaxGroupMasterUpdated = _generalTaxGroupMasterRepository.Update(generalTaxGroupMaster);

            if (isTaxGroupMasterUpdated)
            {
                List<GeneralTaxGroupMasterDetails> list = _generalTaxGroupMasterDetailRepository.Table.Where(x => x.GeneralTaxGroupMasterId == generalTaxGroupModel.GeneralTaxGroupMasterId)?.ToList();
                if (list?.Count > 0)
                {
                    _generalTaxGroupMasterDetailRepository.Delete(list);
                    List<GeneralTaxGroupMasterDetails> generalTaxGroupMasterDetailList = new List<GeneralTaxGroupMasterDetails>();
                    foreach (string genTaxMasterId in generalTaxGroupModel.GeneralTaxMasterIds)
                    {
                        GeneralTaxGroupMasterDetails generalTaxGroupMasterDetail = new GeneralTaxGroupMasterDetails()
                        {
                            GeneralTaxGroupMasterId = generalTaxGroupModel.GeneralTaxGroupMasterId,
                            GeneralTaxMasterId = Convert.ToInt16(genTaxMasterId)
                        };
                        generalTaxGroupMasterDetailList.Add(generalTaxGroupMasterDetail);
                    }
                    _generalTaxGroupMasterDetailRepository.Insert(generalTaxGroupMasterDetailList);
                }
            }
            else
            {
                generalTaxGroupModel.HasError = true;
                generalTaxGroupModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isTaxGroupMasterUpdated;
        }

        //Delete TaxGroupMaster.
        public virtual bool DeleteTaxGroupMaster(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "TaxGroupMasterId"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("TaxGroupMasterId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteTaxGroup @TaxGroupMasterId,  @Status OUT", 1, out status);
            return status == 1 ? true : false;
        }

        #region Protected Method
        //Check if Tax Group Name is already present or not.
        protected virtual bool IsTaxGroupNameAlreadyExist(string taxGroupName, byte generalTaxGroupMasterId = 0)
         => _generalTaxGroupMasterRepository.Table.Any(x => x.TaxGroupName == taxGroupName && (x.GeneralTaxGroupMasterId != generalTaxGroupMasterId || generalTaxGroupMasterId == 0));
        #endregion
    }
}
