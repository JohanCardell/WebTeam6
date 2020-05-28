using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebTeam6.Data
{
    public class UserGroup
    {
        public int GroupId { get; set; }
        [InverseProperty("Members")]
        public Group Group { get; set; }
        public string UserId { get; set; }
        [InverseProperty("GroupsAsMember")]
        public User User { get; set; }
    }
}
