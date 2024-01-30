using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using System.Collections.Specialized;
using System.Data;

namespace Coditech.API.Service
{
    public class GeneralRunningNumbersService : IGeneralRunningNumbersService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<GeneralRunningNumbers> _generalRunningNumbersRepository;

        public GeneralRunningNumbersService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _generalRunningNumbersRepository = new CoditechRepository<GeneralRunningNumbers>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual GeneralRunningNumbersListModel GetRunningNumbersList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            string selectedCentreCode = filters?.Find(x => string.Equals(x.FilterName, FilterKeys.SelectedCentreCode, StringComparison.CurrentCultureIgnoreCase))?.FilterValue;
            filters.RemoveAll(x => x.FilterName == FilterKeys.SelectedCentreCode);

            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<GeneralRunningNumbersModel> objStoredProc = new CoditechViewRepository<GeneralRunningNumbersModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@CentreCode", selectedCentreCode, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<GeneralRunningNumbersModel> generalRunningNumbersList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetRunningNumbersList @CentreCode,@WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 6, out pageListModel.TotalRowCount)?.ToList();
            GeneralRunningNumbersListModel listModel = new GeneralRunningNumbersListModel();

            listModel.GeneralRunningNumbersList = generalRunningNumbersList?.Count > 0 ? generalRunningNumbersList : new List<GeneralRunningNumbersModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }
    }
}

