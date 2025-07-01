using Coditech.Admin.Utilities;
using Coditech.Common.API.Model;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;

using System.Data;
using System.Globalization;
using System.Reflection;

namespace Coditech.Admin.Helpers
{
    public static class AdminGeneralHelper
    {
        public static string GetSystemGlobleSettingFeatureValue(string featureName)
        {
            List<GeneralSystemGlobleSettingModel> settingMasterList = SessionHelper.GetDataFromSession<UserModel>(AdminConstants.UserDataSession)?.GeneralSystemGlobleSettingList;
            string featureValue = settingMasterList?.Where(x => x.FeatureName.Equals(featureName, StringComparison.InvariantCultureIgnoreCase))?.FirstOrDefault()?.FeatureValue;
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

        //Convert List To DataTable
        public static DataTable ConvertListToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }

        public static string DateFormatForTextBox()
        {
            string dateTimeformat = "{0:" + GetSystemGlobleSettingFeatureValue(GeneralSystemGlobleSettingEnum.DateFormat.ToString()) + "}";
            return dateTimeformat;
        }

        public static string DateFormatForCalendar()
        {
            string dateTimeformat = GetSystemGlobleSettingFeatureValue(GeneralSystemGlobleSettingEnum.DateFormatForCalendar.ToString());
            return dateTimeformat;
        }
        public static string ToCoditechDateFormat(this DateTime? dateTime)
        {
            string dateTimeformat = dateTime == null ? "" : Convert.ToDateTime(dateTime).ToString(GetSystemGlobleSettingFeatureValue(GeneralSystemGlobleSettingEnum.DateFormat.ToString()));
            return dateTimeformat;
        }

        public static string ToCoditechDateFormat(this DateTime dateTime)
        {
            string dateTimeformat = dateTime.ToString(GetSystemGlobleSettingFeatureValue(GeneralSystemGlobleSettingEnum.DateFormat.ToString()));
            return dateTimeformat;
        }

        public static string ToCoditechDateWithTimeFormat(this DateTime dateTime)
        {
            string dateTimeformat = dateTime.ToString(GetSystemGlobleSettingFeatureValue(GeneralSystemGlobleSettingEnum.DateFormat.ToString()) + " " + GetSystemGlobleSettingFeatureValue(GeneralSystemGlobleSettingEnum.TimeFormat.ToString()));
            return dateTimeformat;
        }
        public static string ToCoditechTimeFormat(this TimeOnly time)
        {
            string timeformat = time.ToString(GetSystemGlobleSettingFeatureValue(GeneralSystemGlobleSettingEnum.TimeFormat.ToString()));
            return timeformat;
        }
        public static string ToCoditechTimeFormat(this TimeSpan time)
        {
            string timeformat = time.ToString(GetSystemGlobleSettingFeatureValue(GeneralSystemGlobleSettingEnum.TimeFormat.ToString()));
            return timeformat;
        }
        public static string ToCoditechTimeFormat(this TimeSpan? time)
        {
            string timeformat = time == null ? "" : new TimeOnly(time.Value.Hours, time.Value.Minutes, time.Value.Seconds).ToString(GetSystemGlobleSettingFeatureValue(GeneralSystemGlobleSettingEnum.TimeFormat.ToString()));
            return timeformat;
        }
        public static int GetSelectedBalanceSheetId()
        {
            return Convert.ToInt32(SessionHelper.GetDataFromSession<UserModel>(AdminConstants.UserDataSession)?.SelectedBalanceSheetId);
        }

        public static bool IsBalanceSheetAssociated()
        {
            return GetSelectedBalanceSheetId() > 0;
        }
    }
}
