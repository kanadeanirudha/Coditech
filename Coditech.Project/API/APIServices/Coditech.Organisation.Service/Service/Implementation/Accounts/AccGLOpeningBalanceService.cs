using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Logger;
using Coditech.Common.Service;
using Coditech.Resources;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using static Coditech.Common.Helper.HelperUtility;

namespace Coditech.API.Service
{
    public class AccGLOpeningBalanceService : BaseService, IAccGLOpeningBalanceService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<AccGLOpeningBalance> _accGLOpeningBalanceRepository;
        private readonly ICoditechRepository<GeneralFinancialYear> _generalFinancialYearMasterRepository;
        public AccGLOpeningBalanceService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _accGLOpeningBalanceRepository = new CoditechRepository<AccGLOpeningBalance>(_serviceProvider.GetService<Coditech_Entities>());
            _generalFinancialYearMasterRepository = new CoditechRepository<GeneralFinancialYear>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual ACCGLOpeningBalanceListModel GetNonControlHeadType(int accSetupBalanceSheetId, short accSetupCategoryId, byte controlNonControl)
        {
            CoditechViewRepository<ACCGLOpeningBalanceModel> objStoredProc = new CoditechViewRepository<ACCGLOpeningBalanceModel>(_serviceProvider.GetService<Coditech_Entities>());
            PageListModel pageListModel = new PageListModel(null, null, 0, 0);
            objStoredProc.SetParameter("@AccSetupBalancesheetId", accSetupBalanceSheetId, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@GLCategory", accSetupCategoryId, ParameterDirection.Input, DbType.Int16);
            objStoredProc.SetParameter("@ControlNonControl", controlNonControl, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<ACCGLOpeningBalanceModel> ACCGLOpeningBalanceList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetGLAccountBalanceByCntNonCntList @AccSetupBalancesheetId,@GLCategory,@ControlNonControl,@WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 6, out pageListModel.TotalRowCount)?.ToList();

            ACCGLOpeningBalanceListModel listModel = new ACCGLOpeningBalanceListModel();
            listModel.ACCGLOpeningBalanceList = ACCGLOpeningBalanceList?.Count > 0 ? ACCGLOpeningBalanceList : new List<ACCGLOpeningBalanceModel>();
            return listModel;
        }
        
        //Update ACCGLOpeningBalance.
        public virtual ACCGLOpeningBalanceModel UpdateNonControlHeadType(ACCGLOpeningBalanceModel accGLOpeningBalanceModel)
        {
            if (IsNull(accGLOpeningBalanceModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            short generalFinancialYearId = _generalFinancialYearMasterRepository.Table.Where(x => x.IsCurrentFinancialYear).Select(x => x.GeneralFinancialYearId).FirstOrDefault();
            
            if (accGLOpeningBalanceModel.ACCGLBalanceList.Count > 0)
            {
                List<AccGLOpeningBalance> ACCGLOpeningBalanceList = new List<AccGLOpeningBalance>();
                
                foreach (ACCGLOpeningBalanceModel item in accGLOpeningBalanceModel.ACCGLBalanceList.Where(x => x.OpeningBalance > 0))
                {
                    item.GeneralFinancialYearId = generalFinancialYearId;
                    ACCGLOpeningBalanceList.Add(new AccGLOpeningBalance()
                    {
                        AccSetupGLId = item.AccSetupGLId,
                        GeneralFinancialYearId = generalFinancialYearId,
                        AccSetupBalanceSheetId = item.AccSetupBalanceSheetId,
                        OpeningBalance = 0,
                        IsActive = true,
                        TotalDebitAmount = item.DebitCreditEnum == 0 ? item.TotalDebitAmount : 0,
                        TotalCreditAmount = item.DebitCreditEnum == 1 ? item.TotalCreditAmount : 0,
                        ClosingBalance = item.OpeningBalance,
                        OpeningDatetime = DateTime.Now
                    });
                }
                _accGLOpeningBalanceRepository.Insert(ACCGLOpeningBalanceList);
            }
            return accGLOpeningBalanceModel;
        }
    }
}

