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
            Members = new List<GroupUser>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public User Owner { get; set; }
        public int OwnerId { get; set; }

        public ICollection<GroupUser> Members { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}
