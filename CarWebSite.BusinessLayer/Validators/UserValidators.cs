using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;


namespace CarWebSite.BusinessLayer.Validators
{
    public static class UserValidators
    {
        private static readonly Regex PhoneRegex =
            new Regex(@"^(\+373\s?|0\s?)[67]\d\s?\d{3}\s?\d{3}$", RegexOptions.Compiled);
        private static readonly Regex CityRegex =
            new Regex(@"^\p{L}[\p{L}\s.\-']*\p{L}$", RegexOptions.Compiled);
        private static readonly Regex EmailRegex =
            new Regex(@"^[^\s@]+@[^\s@]+\.[^\s@]+$", RegexOptions.Compiled);


    public static bool IsValidPhoneNumber(string? phone)
        {
            if(string.IsNullOrWhiteSpace(phone)) return true;
            return PhoneRegex.IsMatch(phone);
        }

        public static bool IsValidCity(string? city)
        {
            if (string.IsNullOrWhiteSpace(city)) return true;
            var trimmed = city.Trim();
            if (trimmed.Length > 50) return false;
            if (!CityRegex.IsMatch(trimmed)) return false;
            return trimmed.Count(char.IsLetter) >= 3;
        }

        public static bool IsValidEmail(string? email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            return EmailRegex.IsMatch(email);
        }

        public static bool IsValidFullName(string? fullName)
        {
            if(string.IsNullOrEmpty(fullName)) return false;
            var trimmed = fullName.Trim();
            return trimmed.Length >= 2 && trimmed.Length <= 70;
        }
    }
}
