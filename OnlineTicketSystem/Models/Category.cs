using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTicketSystem.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        [Required(ErrorMessage ="Required!!!")]
        public string CategoryName { get; set; }

        public string ComID { get; set; }

        //public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
