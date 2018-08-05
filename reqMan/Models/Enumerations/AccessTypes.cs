using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reqMan.Models.Enumerations
{
    public class AccessTypes
    {
        public static string DB_FULL = "FULL";
        public static string DB_READ_ONLY = "READ_ONLY";

        public static string CLIENT_FULL = "Full Access";
        public static string CLIENT_READ_ONLY = "Read-only Access";

        public static List<string> AccessTypeCollection = new List<string>()
        {
            DB_FULL,
            DB_READ_ONLY
        };

        public static bool IsValidState(string accessTypeDb)
        {
            if (AccessTypeCollection.Contains(accessTypeDb))
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
