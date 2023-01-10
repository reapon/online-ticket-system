using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTicketSystem.Models
{
    public class Group
    {
        public int GroupID { get; set; }
        [Required(ErrorMessage ="Group Name Required!!")]
        public string GroupName { get; set; }

        public string ComID { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
        //public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<GroupUser> GroupUsers { get; set; }
    }
}
