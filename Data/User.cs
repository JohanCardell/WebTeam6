using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebTeam6.Data

{
    public class User : IdentityUser
    {
        public User()
        {
            GroupsAsMember = new List<UserGroup>();
            GroupsAsOwner = new List<Group>();
            Events = new List<Event>();
        }
        
        public ICollection<UserGroup> GroupsAsMember { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ProfilePicture { get; set; }

        [InverseProperty("Owner")]
        public ICollection<Group> GroupsAsOwner { get; set; }

        public ICollection<Event> Events { get; set; }

    }
}
