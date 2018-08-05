using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reqMan.Models.Enumerations
{
    public class RequestStates
    {
        public static string DB_REQUESTED = "REQUESTED";
        public static string DB_APPROVED = "APPROVED";
        public static string DB_REJECED = "REJECTED";

        public static string CLIENT_REQUESTED = "Requested";
        public static string CLIENT_APPROVED = "Approved";
        public static string CLIENT_REJECED = "Rejected";

        public static List<string> StateCollection = new List<string>()
        {
            DB_REQUESTED,
            DB_APPROVED,
            DB_REJECED
        };

        public static bool IsValidState(string stateDb)
        {
            if(StateCollection.Contains(stateDb))
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
