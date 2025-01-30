namespace Coditech.Common.API.Model.Response
{
    public class AccSetupGLBankListResponse : BaseListResponse
    {
        public List<AccSetupGLBankModel> AccSetupGLBankList { get; set; }
        public List<AccSetupBalanceSheetListModel> AccSetupBalanceSheetList { get; set; }

    }
}

