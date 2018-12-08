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

        //Pension
        public float PensionPerecentage { get; set;}
        public bool OptOut { get; set; }

        //Cycle to Work
        public string CycleSchemeRequest { get; set; }

        //Gym and Season Ticket Loan
        public DateTime StartDate { get; set; }

        //Season Ticket Loan
        public string ZoneFrom { get; set; } 
        public string ZoneTo { get; set; }

        //Taste Card
        public string TasteCardLink { get; set; }

        public virtual User User { get; set; }
        public virtual  RequestType RequestType { get; set; }
        public virtual ICollection<RequestAction> RequestActions { get; set; }
    }
}
