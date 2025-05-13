using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_UserManagement.Application.Common.Utils
{
    public static class ContainsFunctions
    {
        // Also here we can get these settings from the database
        public static bool ContainLetters(string password)
        {
            return password.Count(char.IsLetter) > 1;
        }

        public static bool ContainDigits(string password)
        {
            return password.Count(char.IsDigit) > 1;
        }

        public static bool ContainSpecialChars(string password)
        {
            return password.Count(ch => !char.IsLetterOrDigit(ch)) > 0;
        }
    }
}
