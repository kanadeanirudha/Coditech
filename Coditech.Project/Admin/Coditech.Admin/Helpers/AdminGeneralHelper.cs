using Coditech.Admin.Utilities;
using Coditech.Common.API.Model;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;

using System.Globalization;

namespace Coditech.Admin.Helpers
{
    public class AdminGeneralHelper
    {
        public static string GetSystemGlobleSettingFeatureValue(string featureName)
        {
            List<GeneralSystemGlobleSettingModel> settingMasterList = SessionHelper.GetDataFromSession<UserModel>(AdminConstants.UserDataSession)?.GeneralSystemGlobleSettingList;
            string featureValue = settingMasterList?.FirstOrDefault(x => x.FeatureName.Equals(featureName, StringComparison.InvariantCultureIgnoreCase))?.FeatureValue;
            return featureValue;
        }

        public static string FormatPriceWithCurrency(decimal? price, string UOM = "")
        {
            if (HelperUtility.IsNotNull(price))
            {
                string priceRoundOff = GetSystemGlobleSettingFeatureValue(GeneralSystemGlobleSettingEnum.PriceRoundOff.ToString());
                string cultureName = GetSystemGlobleSettingFeatureValue(GeneralSystemGlobleSettingEnum.DefaultCultureName.ToString()) ?? "en-IN";
                string cultureValue;
                if (!string.IsNullOrEmpty(cultureName))
                {
                    CultureInfo info = new CultureInfo(cultureName);
                    info.NumberFormat.CurrencyDecimalDigits = Convert.ToInt32(priceRoundOff);
                    cultureValue = $"{price.GetValueOrDefault().ToString("c", info.NumberFormat)}";
                }
                else
                    cultureValue = Convert.ToString(price);

                return !string.IsNullOrEmpty(UOM) ? $"{cultureValue} / {UOM}" : cultureValue;
            }
            return null;
        }
    }
}
