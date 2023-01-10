using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTicketSystem.Models
{
    //[Index(nameof(UserName), IsUnique = true)]
    //[Index(nameof(GroupID), IsUnique = true)]
    public class GroupUser
    {
        public int GroupUserID { get; set; }

        public string UserName { get; set; }

        public string ComID { get; set; }

        [ForeignKey("Group")]
        public int GroupID { get; set; }

        public virtual Group Group { get; set; }


      

    }
}
