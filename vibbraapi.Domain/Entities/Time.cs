using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vibbraapi.Domain.Entities
{
    public class Time:Entity
    {
        public long Project_Id { get; set; }

        public long User_Id { get; set; }

        public DateTime? Started_at { get; set; }

        public DateTime? Ended_at { get; set; }

        public User User { get; set; }

        public Project Project { get; set; }

        public Time() { }
        public Time(Project project, User user, DateTime? started_at, DateTime? ended_at) 
        {
            Project = project;
            User = user;
            Started_at = started_at;
            Ended_at = ended_at;
        }

    }
}
