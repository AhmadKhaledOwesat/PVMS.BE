using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PVMS.Application.Helpers
{
    public static class ArabicNumberHelper
    {
        private static readonly char[] ArabicDigits =
            { '٠','١','٢','٣','٤','٥','٦','٧','٨','٩' };

        public static string ToArabicDigits(int number)
        {
            return string.Concat(
                number.ToString().Select(c =>
                    char.IsDigit(c) ? ArabicDigits[c - '0'] : c));
        }
    }
}
