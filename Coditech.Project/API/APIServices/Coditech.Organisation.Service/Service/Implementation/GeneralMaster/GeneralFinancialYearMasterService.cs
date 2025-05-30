﻿using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Resources;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Specialized;
using System.Data;
using Z.EntityFramework.Extensions;
using static Coditech.Common.Helper.HelperUtility;
namespace Coditech.API.Service
{
    public class GeneralFinancialYearMasterService : IGeneralFinancialYearMasterService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<GeneralFinancialYear> _generalFinancialYearMasterRepository;
        private readonly ICoditechRepository<AccSetupBalanceSheet> _accSetupBalanceSheetRepository;
        public GeneralFinancialYearMasterService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _generalFinancialYearMasterRepository = new CoditechRepository<GeneralFinancialYear>(_serviceProvider.GetService<Coditech_Entities>());
            _accSetupBalanceSheetRepository = new CoditechRepository<AccSetupBalanceSheet>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual GeneralFinancialYearListModel GetFinancialYearList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {

            string selectedCentreCode = filters?.Find(x => string.Equals(x.FilterName, FilterKeys.SelectedCentreCode, StringComparison.CurrentCultureIgnoreCase))?.FilterValue;
            filters.RemoveAll(x => x.FilterName == FilterKeys.SelectedCentreCode);
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<GeneralFinancialYearModel> objStoredProc = new CoditechViewRepository<GeneralFinancialYearModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@CentreCode", selectedCentreCode, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<GeneralFinancialYearModel> FinancialYearList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetFinancialYearList @CentreCode,@WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 5, out pageListModel.TotalRowCount)?.ToList();
            GeneralFinancialYearListModel listModel = new GeneralFinancialYearListModel();

            listModel.GeneralFinancialYearList = FinancialYearList?.Count > 0 ? FinancialYearList : new List<GeneralFinancialYearModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Create FinancialYear.
        public virtual GeneralFinancialYearModel CreateFinancialYear(GeneralFinancialYearModel generalFinancialYearModel)
        {
            if (IsNull(generalFinancialYearModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsFinancialYearEntryAlreadyExist(generalFinancialYearModel.FromDate, generalFinancialYearModel.ToDate, generalFinancialYearModel.GeneralFinancialYearId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Financial Year"));

            if (generalFinancialYearModel.IsCurrentFinancialYear)
            {
                ResetCurrentFinancialYear();
            }

            GeneralFinancialYear generalFinancialYearMaster = generalFinancialYearModel.FromModelToEntity<GeneralFinancialYear>();

            //Create new FinancialYear and return it.
            GeneralFinancialYear FinancialYearData = _generalFinancialYearMasterRepository.Insert(generalFinancialYearMaster);
            if (FinancialYearData?.GeneralFinancialYearId > 0)
            {
                generalFinancialYearModel.GeneralFinancialYearId = FinancialYearData.GeneralFinancialYearId;
            }
            else
            {
                generalFinancialYearModel.HasError = true;
                generalFinancialYearModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return generalFinancialYearModel;
        }

        //Get FinancialYear by FinancialYear id.
        public virtual GeneralFinancialYearModel GetFinancialYear(short financialYearId)
        {
            if (financialYearId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "financialYearId"));

            //Get the FinancialYear Details based on id.
            GeneralFinancialYear generalFinancialYearMaster = _generalFinancialYearMasterRepository.Table.FirstOrDefault(x => x.GeneralFinancialYearId == financialYearId);
            GeneralFinancialYearModel generalFinancialYearModel = generalFinancialYearMaster?.FromEntityToModel<GeneralFinancialYearModel>();
            return generalFinancialYearModel;
        }

        //Update FinancialYear.
        public virtual bool UpdateFinancialYear(GeneralFinancialYearModel generalFinancialYearModel)
        {
            if (IsNull(generalFinancialYearModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (IsFinancialYearEntryAlreadyExist(generalFinancialYearModel.FromDate, generalFinancialYearModel.ToDate, generalFinancialYearModel.GeneralFinancialYearId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Financial Year"));


            if (generalFinancialYearModel.GeneralFinancialYearId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "FinancialYearID"));

            if (generalFinancialYearModel.IsCurrentFinancialYear)
            {
                ResetCurrentFinancialYear(generalFinancialYearModel.GeneralFinancialYearId); 
            }

            GeneralFinancialYear generalFinancialYearMaster = generalFinancialYearModel.FromModelToEntity<GeneralFinancialYear>();

            //Update FinancialYear
            bool isFinancialYearUpdated = _generalFinancialYearMasterRepository.Update(generalFinancialYearMaster);
            if (!isFinancialYearUpdated)
            {
                generalFinancialYearModel.HasError = true;
                generalFinancialYearModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isFinancialYearUpdated;
        }

        //Delete FinancialYear.
        public virtual bool DeleteFinancialYear(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "FinancialYearID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("FinancialYearId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteFinancialYear @FinancialYearId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        public virtual GeneralFinancialYearModel GetCurrentFinancialYear(int accSetupBalanceSheetId)
        {
            var centreCode = _accSetupBalanceSheetRepository.Table
                                .Where(b => b.AccSetupBalanceSheetId == accSetupBalanceSheetId)
                                .Select(b => b.CentreCode)
                                .FirstOrDefault();

            if (string.IsNullOrEmpty(centreCode))
                return new GeneralFinancialYearModel(); 

            GeneralFinancialYearModel model = (from a in _generalFinancialYearMasterRepository.Table
                                                         where a.CentreCode == centreCode && a.IsCurrentFinancialYear
                                                         select new GeneralFinancialYearModel
                                                         {
                                                             GeneralFinancialYearId = a.GeneralFinancialYearId,
                                                             FromDate = a.FromDate,
                                                             ToDate = a.ToDate,
                                                             CentreCode = a.CentreCode,
                                                             IsCurrentFinancialYear = a.IsCurrentFinancialYear
                                                         })?.FirstOrDefault();

            return model;
        }
        #region Protected Method


        protected virtual bool IsFinancialYearEntryAlreadyExist(DateTime FromDate, DateTime ToDate, short generalFinancialYearId = 0)
        => _generalFinancialYearMasterRepository.Table.Any(x => x.FromDate == FromDate && x.ToDate == ToDate && (x.GeneralFinancialYearId != generalFinancialYearId || generalFinancialYearId == 0));

        public void ResetCurrentFinancialYear(short? financialYearId=null)
        {
            var allYears = _generalFinancialYearMasterRepository.Table.Where(x => x.IsCurrentFinancialYear).ToList();

            foreach (var year in allYears)
            {
                if (!financialYearId.HasValue || year.GeneralFinancialYearId != financialYearId.Value)
                {
                    year.IsCurrentFinancialYear = false;
                    _generalFinancialYearMasterRepository.Update(year); 
                }
            }
        }

        #endregion
    }
}
