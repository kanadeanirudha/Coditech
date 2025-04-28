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
        private readonly ICoditechRepository<AccGLIndividualOpeningBalance> _accGLIndividualOpeningBalanceRepository;
        private readonly ICoditechRepository<GeneralFinancialYear> _generalFinancialYearMasterRepository;
        public AccGLOpeningBalanceService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _accGLOpeningBalanceRepository = new CoditechRepository<AccGLOpeningBalance>(_serviceProvider.GetService<Coditech_Entities>());
            _accGLIndividualOpeningBalanceRepository = new CoditechRepository<AccGLIndividualOpeningBalance>(_serviceProvider.GetService<Coditech_Entities>());
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

        public virtual ACCGLOpeningBalanceModel GetControlHeadType(int accSetupBalanceSheetId, short accSetupCategoryId, byte controlNonControl)
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

            ACCGLOpeningBalanceModel listModel = new ACCGLOpeningBalanceModel();
            listModel.ACCGLOpeningBalanceList = ACCGLOpeningBalanceList?.Count > 0 ? ACCGLOpeningBalanceList : new List<ACCGLOpeningBalanceModel>();
            return listModel;
        }

        public virtual AccGLIndividualOpeningBalanceModel GetIndividualOpeningBalance(int accSetupBalanceSheetId, short userTypeId,short generalFinancialYearId, int accSetupGLId)
        {

            CoditechViewRepository<AccGLIndividualOpeningBalanceModel> objStoredProc = new CoditechViewRepository<AccGLIndividualOpeningBalanceModel>(_serviceProvider.GetService<Coditech_Entities>());
            PageListModel pageListModel = new PageListModel(null, null, 0, 0);
            objStoredProc.SetParameter("@AccSetupBalancesheetId", accSetupBalanceSheetId, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@GeneralFinancialYearId", generalFinancialYearId, ParameterDirection.Input, DbType.Int16);
            objStoredProc.SetParameter("@UserTypeId", userTypeId, ParameterDirection.Input, DbType.Int16);
            List<AccGLIndividualOpeningBalanceModel> AccGLIndividualOpeningBalanceList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetIndividualOpeningBalanceByUserType @AccSetupBalancesheetId,@GeneralFinancialYearId,@UserTypeId")?.ToList();

            AccGLIndividualOpeningBalanceModel listModel = new AccGLIndividualOpeningBalanceModel();
            listModel.AccGLIndividualOpeningBalanceList = AccGLIndividualOpeningBalanceList?.Count > 0 ? AccGLIndividualOpeningBalanceList : new List<AccGLIndividualOpeningBalanceModel>();
            return listModel;
        }

        public virtual AccGLIndividualOpeningBalanceModel UpdateIndividualOpeningBalance(AccGLIndividualOpeningBalanceModel accGLIndividualOpeningBalanceModel)
        {
			if (IsNull(accGLIndividualOpeningBalanceModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (accGLIndividualOpeningBalanceModel.AccGLIndividualOpeningBalanceList.Count > 0)
            {
				List<AccGLIndividualOpeningBalance> IndividualOpeningBalanceList = new List<AccGLIndividualOpeningBalance>();

                foreach (AccGLIndividualOpeningBalanceModel item in accGLIndividualOpeningBalanceModel.AccGLIndividualOpeningBalanceList.Where(x => x.OpeningBalance > 0))
                {
					IndividualOpeningBalanceList.Add(new AccGLIndividualOpeningBalance()
                    {
                        AccSetupGLId = item.AccSetupGLId,
                        GeneralFinancialYearId = item.GeneralFinancialYearId,
                        AccSetupBalanceSheetId = item.AccSetupBalanceSheetId,
                        PersonId=item.PersonId,
						UserTypeId =item.UserTypeId,
                        OpeningBalance = 0,
                        IsActive = true,
                        TotalDebitAmount = item.DebitCreditEnum == 0 ? item.TotalDebitAmount : 0,
                        TotalCreditAmount = item.DebitCreditEnum == 1 ? item.TotalCreditAmount : 0,
                        ClosingBalance = item.OpeningBalance,
                        OpeningDatetime = DateTime.Now
                    });
                }
                _accGLIndividualOpeningBalanceRepository.Insert(IndividualOpeningBalanceList);
            }
            return accGLIndividualOpeningBalanceModel;
        }
    }
}
