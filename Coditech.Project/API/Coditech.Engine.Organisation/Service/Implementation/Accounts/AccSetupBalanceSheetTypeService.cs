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
    public class AccSetupBalanceSheetTypeService : IAccSetupBalanceSheetTypeService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<AccSetupBalanceSheetType> _accSetupBalanceSheetTypeRepository;
        public AccSetupBalanceSheetTypeService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _accSetupBalanceSheetTypeRepository = new CoditechRepository<AccSetupBalanceSheetType>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual AccSetupBalanceSheetTypeListModel GetAccSetupBalanceSheetTypeList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<AccSetupBalanceSheetTypeModel> objStoredProc = new CoditechViewRepository<AccSetupBalanceSheetTypeModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<AccSetupBalanceSheetTypeModel> AccSetupBalanceSheetTypeList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetAccSetupBalanceSheetTypeList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            AccSetupBalanceSheetTypeListModel listModel = new AccSetupBalanceSheetTypeListModel();

            listModel.AccSetupBalanceSheetTypeList = AccSetupBalanceSheetTypeList?.Count > 0 ? AccSetupBalanceSheetTypeList : new List<AccSetupBalanceSheetTypeModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

    }
}
