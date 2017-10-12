using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Dflow_Inventory.Helpers
{
    public class CommanMethods
    {
        internal static bool Validate_Email(string emailId)
        {
            Regex rEmail = new Regex(@"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");

            if (emailId.Length > 0 && emailId.Trim().Length != 0)
                if (!rEmail.IsMatch(emailId.Trim()))
                    return false;

            return true;
        }

        internal static DateTime? ConvertDate(string date)
        {
            if (!string.IsNullOrWhiteSpace(date))
            {
                return (DateTime?)DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }

            return null;
        }

        internal static bool Allow_CRUD()
        {
            if (SessionHelper.UserGroupId == 2)
                return false;

            return true;
        }
    }
}
