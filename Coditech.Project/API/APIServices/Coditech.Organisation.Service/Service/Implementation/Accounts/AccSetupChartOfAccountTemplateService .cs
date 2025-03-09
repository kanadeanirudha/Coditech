using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Specialized;
using System.Data;
using static Coditech.Common.Helper.HelperUtility;
namespace Coditech.API.Service
{
    public class AccSetupChartOfAccountTemplateService : IAccSetupChartOfAccountTemplateService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<AccSetupChartOfAccountTemplate> _accSetupChartOfAccountTemplateRepository;
        public AccSetupChartOfAccountTemplateService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _accSetupChartOfAccountTemplateRepository = new CoditechRepository<AccSetupChartOfAccountTemplate>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual AccSetupChartOfAccountTemplateListModel GetAccSetupChartOfAccountTemplateList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<AccSetupChartOfAccountTemplateModel> objStoredProc = new CoditechViewRepository<AccSetupChartOfAccountTemplateModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<AccSetupChartOfAccountTemplateModel> AccSetupChartOfAccountTemplateList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetAccSetupChartOfAccountTemplateList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            AccSetupChartOfAccountTemplateListModel listModel = new AccSetupChartOfAccountTemplateListModel();

            listModel.AccSetupChartOfAccountTemplateList = AccSetupChartOfAccountTemplateList?.Count > 0 ? AccSetupChartOfAccountTemplateList : new List<AccSetupChartOfAccountTemplateModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

    }
}
