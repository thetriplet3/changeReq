using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reqMan.Models.Enumerations
{
    public class RequestActions
    {
        public static string DB_REQUEST = "REQUEST";
        public static string DB_APPROVE = "APPROVE";
        public static string DB_REJECT = "REJECT";

        public static string CLIENT_REQUEST = "Request";
        public static string CLIENT_APPROV = "Approve";
        public static string CLIENT_REJECT = "Reject";

        public static List<string> ActionCollection = new List<string>()
        {
            DB_REQUEST,
            DB_APPROVE,
            DB_REJECT
        };

        public static bool IsValidAction(string actionDb)
        {
            if (ActionCollection.Contains(actionDb))
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
