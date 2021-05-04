using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vibbraapi.Domain.Entities
{
    public class Project:Entity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Time> Times { get; set; }

        public Project() { }

        public Project(string title, string description) 
        {
            Title = title;
            Description = description; 
        }
    }
}
