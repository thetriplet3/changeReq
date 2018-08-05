using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reqMan.Models.Enumerations
{
    public class ApplicationRoles
    {
        public static string DB_ADMIN = "ADMIN";
        public static string DB_NORMAL = "NORMAL";

        public static string CLIENT_ADMIN = "Administrator";
        public static string CLIENT_NORMAL = "Normal";

        public static List<string> ApplicationRoleCollection = new List<string>()
        {
            DB_ADMIN,
            DB_NORMAL
        };

        public static bool IsValidState(string applicationRoleDb)
        {
            if (ApplicationRoleCollection.Contains(applicationRoleDb))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
