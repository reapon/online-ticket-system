using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTicketSystem.Models
{
    public class Priority
    {
        public int PriorityID { get; set; }
        public string PriorityName { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }

    }
}
