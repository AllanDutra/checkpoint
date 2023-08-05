using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Checkpoint.Shared.Utils
{
    public static partial class Validations
    {
        public static bool IsValidEmail(string email)
        {
            try
            {
                MailAddress mail = new(email);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static bool IsValidUsername(string user)
        {
            Regex usernameRegex = UsernameRegex();

            return usernameRegex.IsMatch(user);
        }

        public static bool IsValidPassword(string password)
        {
            Regex passwordRegex = PasswordRegex();

            return passwordRegex.IsMatch(password);
        }

        [GeneratedRegex("^(?=[a-zA-Z0-9._]{8,20}$)(?!.*[_.]{2})[^_.].*[^_.]$")]
        private static partial Regex UsernameRegex();

        [GeneratedRegex("^.*(?=.{8,})(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$")]
        private static partial Regex PasswordRegex();
    }
}
