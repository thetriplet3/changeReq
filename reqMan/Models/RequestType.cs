using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace reqMan.Models
{
    public class RequestType
    {
        public int RequestTypeSeq { get; set; }
        [Key]
        public string RequestTypeId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
