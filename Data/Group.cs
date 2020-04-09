using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebTeam6.Data
{
    public class Group
    {
        public Group()
        {
            this.Id = new Guid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<MemberGroup> MemberGroups { get; set; }

        public virtual ICollection<OwnerGroup> OwnerGroups { get; set; }
    }
}
