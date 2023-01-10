using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTicketSystem.ViewModels
{
    public class AllTicketViewModel
    {
        public int TicketID { get; set; }
        public DateTime CreatedTime { get; set; }
        public string CreatedBy { get; set; }

        public string AssignedTo { get; set; }

        public string SupportGivenBy { get; set; }

        public int? StatusID { get; set; }
        public string GroupName { get; set; }

        public int GroupID { get; set; }

    }
}
