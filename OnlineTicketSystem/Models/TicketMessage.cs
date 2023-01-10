using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTicketSystem.Models
{
    public class TicketMessage
    {
        public int TicketMessageID { get; set; }

        [Required(ErrorMessage ="Message Required!!")]
        public string MessageBody { get; set; }
        public DateTime MessageTime { get; set; }

        public string UserName { get; set; }
        //public string SupporterName { get; set; }

        public string ComID { get; set; }

        public string PhotoPath { get; set; }

        public int TicketID { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}
