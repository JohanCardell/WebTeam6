using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebTeam6.Data
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }


        public Group Group { get; set; }

        public User Creator { get; set; }
    }
}
