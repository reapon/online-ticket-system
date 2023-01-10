using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTicketSystem.Models
{
    public class UserMail
    {
        public int ID { get; set; }
        public string ClientUser { get; set; }
        public string TicketMailList { get; set; }
        public string SupportMailList { get; set; }

        public string ComID { get; set; }
    }
}
