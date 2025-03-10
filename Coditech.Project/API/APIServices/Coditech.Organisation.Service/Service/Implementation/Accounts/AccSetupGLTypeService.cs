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
    class AccSetupGLTypeService : IAccSetupGLTypeService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<AccSetupGLType> _accSetupGLTypeRepository;
        public AccSetupGLTypeService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _accSetupGLTypeRepository = new CoditechRepository<AccSetupGLType>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual AccSetupGLTypeListModel GetAccSetupGLTypeList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<AccSetupGLTypeModel> objStoredProc = new CoditechViewRepository<AccSetupGLTypeModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<AccSetupGLTypeModel> AccSetupGLTypeList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetAccSetupGLTypeList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            AccSetupGLTypeListModel listModel = new AccSetupGLTypeListModel();

            listModel.AccSetupGLTypeList = AccSetupGLTypeList?.Count > 0 ? AccSetupGLTypeList : new List<AccSetupGLTypeModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }
    }
}
