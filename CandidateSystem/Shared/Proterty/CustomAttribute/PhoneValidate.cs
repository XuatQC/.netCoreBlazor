using System.ComponentModel.DataAnnotations;

namespace CandidateSystem.Shared.Proterty.CustomAttribute
{
    /// <summary>
    /// phone valid
    /// </summary>
    [AttributeUsage(AttributeTargets.Property |
  AttributeTargets.Field, AllowMultiple = false)]
    sealed public class PhoneValidate :ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value != null)
            {
                string valueToString = value.ToString().Replace("+", "");
                //at least 8 character and empty
                if (value == null || valueToString.Length < 7 || string.IsNullOrEmpty(valueToString))
                {
                    return false;
                }
                foreach (char c in valueToString)
                {
                    if (c < '0' || c > '9')
                        return false;
                }
                return true;
            }
            return false;

        }
    }
}
