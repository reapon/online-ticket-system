using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTicketSystem.Models
{
    public class CompanyInformation
    {
        [Key]
        public Guid ComID { get; set; }
        [Required(ErrorMessage ="Company Name Required!!!")]
        public string CompanyName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
