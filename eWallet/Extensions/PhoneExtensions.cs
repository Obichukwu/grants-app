using System.Text.RegularExpressions;

namespace eWallet.Helpers {
    public static class PhoneExtensions
    {
        public static bool IsValidNumber(string phoneNum)
        {
            return Regex.IsMatch(phoneNum, "^(?:\\+?[2][3][4]|[0])\\d{10}$");

            //^(?:\+?[2][3][4]|[0])\d{10}$
            /*/Matches
        08036517944
        2348036517944
        +2348036517944

        but not
        +08036517944
        */
        }

        public static string MakeIntlPhoneNumber(string phoneNum)
        {
            if (phoneNum.StartsWith("0"))
            {
                phoneNum = "234" + phoneNum.TrimStart('0');
            }
            if (phoneNum.StartsWith("+"))
            {
                phoneNum = phoneNum.TrimStart('+');
            }
            return phoneNum;
        }
    }
}