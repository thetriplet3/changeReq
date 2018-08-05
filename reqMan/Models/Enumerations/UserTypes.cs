using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reqMan.Models.Enumerations
{
    public class UserTypes
    {
        public static string DB_ADMIN = "ADMIN";
        public static string DB_MANAGER = "MANAGER";
        public static string DB_USER = "USER";
        
        public static string CLIENT_ADMIN = "Administrator";
        public static string CLIENT_MANAGER = "Manager";
        public static string CLIENT_USER = "User";

        public static List<string> UserTypeDbCollection = new List<string>()
        {
            DB_ADMIN,
            DB_MANAGER,
            DB_USER
        };

        public static IDictionary<string, string> UserTypeDecode = new Dictionary<string, string>()
        {
            { DB_ADMIN, CLIENT_ADMIN },
            { DB_MANAGER, CLIENT_MANAGER },
            { DB_USER, CLIENT_USER }

        };

        public static IDictionary<string, string> UserTypEncode = new Dictionary<string, string>()
        {
            { CLIENT_ADMIN, DB_ADMIN },
            { CLIENT_MANAGER, DB_MANAGER },
            { CLIENT_USER, DB_USER }

        };

        public static bool IsValidUserType(string userTypeDb)
        {
            if (UserTypeDbCollection.Contains(userTypeDb))
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
