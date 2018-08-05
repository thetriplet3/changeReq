using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace reqMan.Models
{
    public class RequestAction
    {
        [Key]
        public int RequestActionSeq { get; set; }
        public string RequestId { get; set; }
        public string Username { get; set; }
        public string Action { get; set; }
        public string Comment { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Date { get; set; }

        [JsonIgnore]
        public Request Request { get; set; }
        public User User { get; set; }
    }
}
