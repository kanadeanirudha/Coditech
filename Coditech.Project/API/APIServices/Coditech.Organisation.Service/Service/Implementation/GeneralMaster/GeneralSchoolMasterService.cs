using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Resources;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Specialized;
using System.Data;

using static Coditech.Common.Helper.HelperUtility;
namespace Coditech.API.Service
{
    public class GeneralSchoolMasterService : IGeneralSchoolMasterService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<GeneralSchoolMaster> _generalSchoolMasterRepository;
        public GeneralSchoolMasterService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _generalSchoolMasterRepository = new CoditechRepository<GeneralSchoolMaster>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual GeneralSchoolListModel GetSchoolList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<GeneralSchoolModel> objStoredProc = new CoditechViewRepository<GeneralSchoolModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<GeneralSchoolModel> SchoolList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetGeneralSchoolList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            GeneralSchoolListModel listModel = new GeneralSchoolListModel();

            listModel.GeneralSchoolList = SchoolList?.Count > 0 ? SchoolList : new List<GeneralSchoolModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }
        //Create School.
        public virtual GeneralSchoolModel CreateSchool(GeneralSchoolModel generalSchoolModel)
        {
            if (IsNull(generalSchoolModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);
            if (IsSchoolCodeAlreadyExist(generalSchoolModel.SchoolName, generalSchoolModel.GeneralSchoolMasterId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "School Name"));
            GeneralSchoolMaster generalSchoolMaster = generalSchoolModel.FromModelToEntity<GeneralSchoolMaster>();
            //Create new School and return it.
            GeneralSchoolMaster SchoolData = _generalSchoolMasterRepository.Insert(generalSchoolMaster);
            if (SchoolData?.GeneralSchoolMasterId > 0)
            {
                generalSchoolModel.GeneralSchoolMasterId = SchoolData.GeneralSchoolMasterId;
            }
            else
            {
                generalSchoolModel.HasError = true;
                generalSchoolModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return generalSchoolModel;
        }

        //Get School by School id.
        public virtual GeneralSchoolModel GetSchool(short SchoolId)
        {
            if (SchoolId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "SchoolID"));

            //Get the School Details based on id.
            GeneralSchoolMaster generalSchoolMaster = _generalSchoolMasterRepository.Table.FirstOrDefault(x => x.GeneralSchoolMasterId == SchoolId);
            GeneralSchoolModel generalSchoolModel = generalSchoolMaster?.FromEntityToModel<GeneralSchoolModel>();
            return generalSchoolModel;
        }

        //Update School.
        public virtual bool UpdateSchool(GeneralSchoolModel generalSchoolModel)
        {
            if (IsNull(generalSchoolModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (generalSchoolModel.GeneralSchoolMasterId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "SchoolID"));

            if (IsSchoolCodeAlreadyExist(generalSchoolModel.SchoolName, generalSchoolModel.GeneralSchoolMasterId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "School Name"));

            GeneralSchoolMaster generalSchoolMaster = generalSchoolModel.FromModelToEntity<GeneralSchoolMaster>();

            //Update School
            bool isSchoolUpdated = _generalSchoolMasterRepository.Update(generalSchoolMaster);
            if (!isSchoolUpdated)
            {
                generalSchoolModel.HasError = true;
                generalSchoolModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isSchoolUpdated;
        }

        //Delete School.
        public virtual bool DeleteSchool(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "SchoolID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("SchoolId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteGeneralSchool @SchoolId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Protected Method
        //Check if School code is already present or not.
        protected virtual bool IsSchoolCodeAlreadyExist(string SchoolName, short generalSchoolMasterId = 0)
         => _generalSchoolMasterRepository.Table.Any(x => x.SchoolName == SchoolName && (x.GeneralSchoolMasterId != generalSchoolMasterId || generalSchoolMasterId == 0));
        #endregion
    }
}
