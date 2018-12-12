using reqMan.Data;
using reqMan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reqMan.Validators
{
    public class RequestValidator
    {

        public static string ValidateRequest(ApplicationDbContext _context, Request request)
        {
            if(request.RequestTypeId == "REQ-T-003")
            {
                return PensionValidator(request);
            }
            return string.Empty;
        }
        private static string PensionValidator(Request request)
        {
            string message = string.Empty;
            if(!(request.CurrentPensionPerecentage.In(1, 99) || request.RevisedPensionPerecentage.In(1, 99)))
            {
                message = "Current and Revised values should be in between 1 and 99.";
            }
            return message;
        }

        private static string CommonValidator(ApplicationDbContext _context, Request request)
        {
            string message = string.Empty;
            if (!_context.Users.Any(e => e.Username == request.Username))
            {
                message = "";
            }
            if(!_context.RequestType.Any(e => e.RequestTypeId == request.Username))
            {

            }
            return message;
        }

       
    }
}
