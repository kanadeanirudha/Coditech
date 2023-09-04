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
    public class GeneralNationalityMasterService : IGeneralNationalityMasterService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<GeneralNationalityMaster> _generalNationalityMasterRepository;
        public GeneralNationalityMasterService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _generalNationalityMasterRepository = new CoditechRepository<GeneralNationalityMaster>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual GeneralNationalityListModel GetNationalityList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<GeneralNationalityModel> objStoredProc = new CoditechViewRepository<GeneralNationalityModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<GeneralNationalityModel> nationalityList = objStoredProc.ExecuteStoredProcedureList("RARIndia_GetNationalityList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            GeneralNationalityListModel listModel = new GeneralNationalityListModel();

            listModel.GeneralNationalityList = nationalityList?.Count > 0 ? nationalityList : new List<GeneralNationalityModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Create Nationality.
        public GeneralNationalityModel CreateNationality(GeneralNationalityModel generalNationalityModel)
        {
            if (IsNull(generalNationalityModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);
            if (IsNationalityNameAlreadyExist(generalNationalityModel.Description))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Nationality Name"));

            GeneralNationalityMaster generalNationalityMaster = generalNationalityModel.FromModelToEntity<GeneralNationalityMaster>();

            //Create new Nationality and return it.
            GeneralNationalityMaster nationalityData = _generalNationalityMasterRepository.Insert(generalNationalityMaster);
            if (nationalityData?.GeneralNationalityMasterId > 0)
            {
                generalNationalityModel.GeneralNationalityMasterId = nationalityData.GeneralNationalityMasterId;
            }
            else
            {
                generalNationalityModel.HasError = true;
                generalNationalityModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return generalNationalityModel;
        }

        //Get Nationality by GeneralNationalityMasterId.
        public GeneralNationalityModel GetNationality(short nationalityId)
        {
            if (nationalityId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "Nationality Name"));

            //Get the Nationality Details based on id.
            GeneralNationalityMaster nationalityData = _generalNationalityMasterRepository.Table.FirstOrDefault(x => x.GeneralNationalityMasterId == nationalityId);
            GeneralNationalityModel generalNationalityModel = nationalityData.FromEntityToModel<GeneralNationalityModel>();
            return generalNationalityModel;
        }

        //Update Nationality.
        public virtual bool UpdateNationality(GeneralNationalityModel generalNationalityModel)
        {
            if (IsNull(generalNationalityModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (generalNationalityModel.GeneralNationalityMasterId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "NationalityId"));

            if (IsNationalityNameAlreadyExist(generalNationalityModel.Description, generalNationalityModel.GeneralNationalityMasterId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Nationality Name"));

            GeneralNationalityMaster generalNationalityMaster = generalNationalityModel.FromModelToEntity<GeneralNationalityMaster>();

            //Update Nationality
            bool isNationalityUpdated = _generalNationalityMasterRepository.Update(generalNationalityMaster);
            if (!isNationalityUpdated)
            {
                generalNationalityModel.HasError = true;
                generalNationalityModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isNationalityUpdated;
        }

        //Delete Nationality.
        public virtual bool DeleteNationality(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "NationalityId"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("NationalityId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("RARIndia_DeleteNationality @NationalityId,  @Status OUT", 1, out status);
            return status == 1 ? true : false;
        }

        #region Protected Method
        //Check if Nationality Name is already present or not.
        protected virtual bool IsNationalityNameAlreadyExist(string nationalityName, short generalNationalityMasterId = 0)
         => _generalNationalityMasterRepository.Table.Any(x => x.Description == nationalityName && (x.GeneralNationalityMasterId != generalNationalityMasterId || generalNationalityMasterId == 0));
        #endregion
    }
}
