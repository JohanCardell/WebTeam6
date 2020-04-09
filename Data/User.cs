using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebTeam6.Data
{
    public class User
    {
        public User()
        {
            this.Id = new Guid();
            this.MemberGroups = new List<MemberGroup>();
            this.OwnerGroups = new List<OwnerGroup>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public virtual ICollection<MemberGroup> MemberGroups { get; set; }

        public virtual ICollection<OwnerGroup> OwnerGroups { get; set; }
    }
}
