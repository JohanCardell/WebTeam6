using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebTeam6.Data
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public string Body { get; set; }
        public User Creator { get; set; }
        public Group Group { get; set; }
    }
}
