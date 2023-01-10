using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTicketSystem.ViewModels
{
    public class CreateTicket
    {
        public int TicketID { get; set; }
        public DateTime CreatedTime { get; set; }
        public string CreatedBy { get; set; }
        public string TicketType { get; set; }

        public string MessageBody { get; set; }
        public DateTime MessageTime { get; set; }

        public string UserName { get; set; }
        public string SupporterName { get; set; }

        public IFormFile Photo { get; set; }
    }
}
