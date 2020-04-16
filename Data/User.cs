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
            Groups = new List<Group>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [InverseProperty("Members")]
        public virtual ICollection<Group> Groups { get; set; }
    }
}
