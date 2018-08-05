using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace reqMan.Models
{
    public class Request
    {
        [Key]
        public string RequestId { get; set; }
        public string RequestTypeId { get; set; }
        public string Username { get; set; }
        public string Description { get; set; }
        public string State { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime DateRequested { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DateModified { get; set; }

        public User User { get; set; }
        public RequestType RequestType { get; set; }
        public ICollection<RequestAction> RequestActions { get; set; }
    }
}
