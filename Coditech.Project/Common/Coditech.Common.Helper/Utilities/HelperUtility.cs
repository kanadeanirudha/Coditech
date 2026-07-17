using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.Helper.Utilities;

using System.Security.Cryptography;
using System.Text;

namespace Coditech.Common.Helper
{
    public static class HelperUtility
    {
        #region Private Variables
        private delegate bool ParseDelegate<T>(string s, out T result);
        #endregion
        //Returns true if the passed value is not null, else return false.
        public static bool IsNotNull(object value)
            => !Equals(value, null);

        //Returns true if the passed value is null else false.
        public static bool IsNull(object value)
            => Equals(value, null);

        //Try parse boolean value 
        public static bool TryParseBoolean(this string value)
        {
            return TryParse<bool>(value, bool.TryParse);
        }

        //Method to perform tryparse
        private static T TryParse<T>(this string value, ParseDelegate<T> parse) where T : struct
        {
            T result;
            parse(value, out result);
            return result;
        }
        public static void BindPageListModel(this BaseListModel baseListModel, PageListModel pageListModel)
        {
            if (IsNotNull(pageListModel))
            {
                baseListModel.TotalResults = pageListModel.TotalRowCount;
                baseListModel.PageIndex = pageListModel.PagingStart;
                baseListModel.PageSize = pageListModel.PagingLength;
            }
        }

        public static void BindPageListResponseModel(this BaseListResponse baseListResponseModel, PageListModel pageListModel)
        {
            if (IsNotNull(pageListModel))
            {
                baseListResponseModel.TotalResults = pageListModel.TotalRowCount;
                baseListResponseModel.PageIndex = pageListModel.PagingStart;
                baseListResponseModel.PageSize = pageListModel.PagingLength;
            }
        }
        public static bool IsAdminUser(string userType)
        {
            return userType.Equals(UserTypeEnum.Admin.ToString(), StringComparison.InvariantCultureIgnoreCase);
        }

        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        ///// <summary>
        ///// Replaces the "client_ip" header in current HttpRequest object with the "x-forwarded-for" header value
        ///// </summary>
        public static void ReplaceProxyToClientIp()
        {
            try
            {
                // If a request is coming from a proxy server, "client_ip" header will have "proxy server's ip", in such case, replace it with actual IP received in "x-forwarded-for" header.
                var fwdIp = HttpContextHelper.Current.Request.Headers["x-forwarded-for"];
                //to do scrpions
                //    if (fwdIp != null)
                //    {
                //        HttpContextHelper.Current.Features.Get<IServerVariablesFeature>() = fwdIp;
                //    }
                //}
            }
            catch
            {
                // Do not Throw any exception or  add a logs here 
            }

        }
        public static string EncodeBase64(string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            return Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
        }

        public static string DecodeBase64(string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            try
            {
                return Encoding.UTF8.GetString(Convert.FromBase64String(value));
            }
            catch
            {
                return value;
            }
        }
        public static string GenerateNumericCode(byte length = 6)
        {
            Random generator = new Random();
            String r = generator.Next(0, 1000000).ToString($"D{length}");
            return r;
        }
		public static string ToFirstLetterCapital(this string data)
		{
			if (string.IsNullOrEmpty(data))
			{
				return data;
			}

			char[] chars = data.ToCharArray();
			chars[0] = char.ToUpper(chars[0]);
			return new string(chars);
		}
        public static string GenerateAlphaNumericCode(short size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);
            char letterOffset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length = 26
            const int digitsOffset = 10; // 0...9: length = 10
            Random _random = new Random();

            for (var i = 0; i < size; i++)
            {
                // Randomly decide whether to add a letter or a digit
                if (_random.Next(2) == 0)
                {
                    // Add a letter
                    var letter = (char)_random.Next(letterOffset, letterOffset + lettersOffset);
                    builder.Append(letter);
                }
                else
                {
                    // Add a digit
                    var digit = (char)_random.Next('0', '0' + digitsOffset);
                    builder.Append(digit);
                }
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }
        public static string MaskData(string value, MaskType maskType)
        {
            if (string.IsNullOrWhiteSpace(value))
                return value;

            switch (maskType)
            {
                case MaskType.Email:

                    int atIndex = value.IndexOf('@');

                    if (atIndex <= 0)
                        return value;

                    string localPart = value.Substring(0, atIndex);
                    string domain = value.Substring(atIndex);

                    if (localPart.Length <= 2)
                    {
                        return new string('*', localPart.Length) + domain;
                    }

                    char firstChar = localPart[0];
                    char lastChar = localPart[localPart.Length - 1];
                    string middleMask = new string('*', localPart.Length - 2);

                    return $"{firstChar}{middleMask}{lastChar}{domain}";

                case MaskType.Mobile:

                    if (value.Length <= 4)
                        return value;

                    return new string('*', value.Length - 4) + value.Substring(value.Length - 4);

                default:
                    return value;
            }
        }
        public static bool IsMinor(DateTime? dateOfBirth)
        {
            if (!dateOfBirth.HasValue)
                return false;

            DateTime today = DateTime.Today;
            int age = today.Year - dateOfBirth.Value.Year;

            if (dateOfBirth.Value.Date > today.AddYears(-age))
                age--;

            return age < 18;
        }
    }
}

