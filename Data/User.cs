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
            Groups = new List<Group>();
        }

        public ICollection<Group> Groups { get; set; }
    }
}
