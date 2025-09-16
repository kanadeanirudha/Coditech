using Coditech.Common.Helper.Utilities;

namespace Coditech.Admin.ViewModel
{
    public class ModelPopUpViewModel
    {
        public string ModelPopUpId { get; set; }
        public string ModalContentId { get; set; }
        public PopUpSizeEnum PopUpSize { get; set; }
        public string PopUpCssClass
        {
            get
            {
                return PopUpSize switch
                {
                    PopUpSizeEnum.Small => "modal-sm",
                    PopUpSizeEnum.Large => "modal-lg",
                    PopUpSizeEnum.ExtraLarge => "modal-xl",
                    PopUpSizeEnum.CenterPopUp => "modal-dialog-centered",
                    PopUpSizeEnum.ScrollablePopUp => "modal-dialog-scrollable",
                    _ => "modal-dialog-centered"
                };
            }
        }


    }
}
