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
        private IDictionary<string, string> validatorMessages;

        public RequestValidator()
        {
            validatorMessages = new Dictionary<string, string>();
        }

        public IDictionary<string, string> ValidateRequest(ApplicationDbContext _context, Request request)
        {
            validatorMessages.Clear();

            CommonValidator(_context, request);

            if (request.RequestTypeId == "REQ-T-003")
            {
                PensionValidator(request);
            }
            return validatorMessages;
        }

        private void PensionValidator(Request request)
        {
            string message = string.Empty;
            if(!(request.CurrentPensionPerecentage.In(1, 99) || request.RevisedPensionPerecentage.In(1, 99)))
            {
                message = "Current and Revised values should be in between 1 and 99.";
                validatorMessages.Add("PensionValError", message);
            }
        }

        private void CommonValidator(ApplicationDbContext _context, Request request)
        {
            string message = string.Empty;
            if (!_context.Users.Any(e => e.Username == request.Username))
            {
                message = "User does not exist.";
                validatorMessages.Add("InvalidUserError", message);
            }
            if(!_context.RequestType.Any(e => e.RequestTypeId == request.RequestTypeId))
            {
                message = "Request Type does not exist.";
                validatorMessages.Add("InvalidRequestTypeError", message);
            }
        }

       
    }
}
