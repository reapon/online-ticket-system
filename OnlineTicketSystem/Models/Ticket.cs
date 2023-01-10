using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTicketSystem.Models
{
    public class Ticket: IEquatable<Ticket>
    {
        public int TicketID { get; set; }
        public DateTime CreatedTime { get; set; }
        public string CreatedBy { get; set; }

        public string AssignedTo { get; set; }

        public string SupportGivenBy { get; set; }

        public string ComID { get; set; }
        public DateTime ClosedTime { get; set; }

        public string SupportType { get; set; }


        //public string TicketType { get; set; }
        public int? StatusID { get; set; }
        public int? PriorityID { get; set; }
        //public int? CategoryID { get; set; }
        public int? GroupID { get; set; }
        public virtual Status Status { get; set; }
        public virtual Priority Priority { get; set; }
        //public virtual Category Category { get; set; }
        public virtual Group Group { get; set; }


        public virtual ICollection<TicketMessage> TicketMessages { get; set; }

        public bool Equals(Ticket other)
        {
            if(object.ReferenceEquals(other, null))
            {
                return false;
            }
            if(object.ReferenceEquals(this, other))
            {
                return true;
            }
            return CreatedBy.Equals(other.CreatedBy);
        }

        public override int GetHashCode()
        {
            int nameHashCode = CreatedBy.GetHashCode();
            return nameHashCode;
        }
    }
}




