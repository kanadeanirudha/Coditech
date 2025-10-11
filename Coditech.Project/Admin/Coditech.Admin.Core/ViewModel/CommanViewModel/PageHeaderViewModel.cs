using Coditech.Common.Helper;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Coditech.Admin.ViewModel
{
    public class PageHeaderViewModel 
    {
        public string Title { get; set; }
        public string SaveButtonText { get; set; }
        public string CancelUrl { get; set; }
        public string FormId { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }

        private string _callback = "";
        private bool _isCancelIsShow = true;
        private bool _isSaveIsShow = true;
        private bool _isSaveCloseIsShow = true;
        public string Controller { get; set; }
        public string CancelButtonText{ get; set; }
        public string SaveId { get; set; }
        public string CancelId { get; set; }
        public bool IsSaveCloseEnable { get; set; }
        public bool IsCancelIsShow
        {
            get { return _isCancelIsShow; }
            set { _isCancelIsShow = value; }
        }
        public string Callback
        {
            get
            {
                return _callback;
            }
            set
            {
                _callback = value;
            }
        }

        public bool IsSaveIsShow
        {
            get
            {
                return _isSaveIsShow;
            }

            set
            {
                _isSaveIsShow = value;
            }
        }

        public bool IsSaveCloseIsShow
        {
            get
            {
                return _isSaveCloseIsShow;
            }

            set
            {
                _isSaveCloseIsShow = value;
            }
        }
    }
}

