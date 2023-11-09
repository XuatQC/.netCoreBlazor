namespace CandidateSystem
{
    public static class Util
    {
        /// <summary>
        /// Datetime type -> dd/MM/yyyy string
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static string DatetimeFormatter(DateTime a)
        {
            return a.ToString("dd/MM/yyyy");
        }

        /// <summary>
        /// Number Phone Formatter
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static string PhoneNumberFormatter(this string phoneNumber)
        {
            return phoneNumber.Replace("+84", "0");
        }

        /// <summary>
        /// Bit Integer16 -> Boolean String
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static string BitIntToBooleanString(int i)
        {
            return i == 1 ? "Yes" : (i == 0 ? "No" : "Unknown");
        }

        public static string InsertSpace(this string str)
        {
            string newString = "";
            for (int i = 0; i < str.Length; i++)
            {

                if (Char.IsUpper(str[i]) && i != 0)
                {
                    newString += $" {str[i]}";
                    continue;
                }
                newString += str[i];
            }
            return newString;
        }
    }
}
