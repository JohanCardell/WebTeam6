using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebTeam6.Data
{
    public class Group
    {
        public Group()
        {
            Members = new List<User>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [InverseProperty("OwnedGroups")]
        public User Owner { get; set; }
        [InverseProperty("Groups")]
        public ICollection<User> Members { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}
