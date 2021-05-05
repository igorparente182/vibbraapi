using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vibbraapi.Domain.Entities
{
    public class Project:Entity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        [NotMapped]
        public virtual ICollection<Time> UserTimes { get; set; }

        public Project() { }

        public Project(string title, string description) 
        {
            Title = title;
            Description = description; 
        }
    }
}
