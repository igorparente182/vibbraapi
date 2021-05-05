using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vibbraapi.Domain.Entities;

namespace vibbraapi.Domain.DTO
{
    public class TimeDTO:Entity
    {
        public DateTime? Started_at { get; private set; }

        public DateTime? Ended_at { get; private set; }

        public virtual User User { get; private set; }

        public virtual Project Project { get; private set; }

        public TimeDTO() { }
        public TimeDTO(long id, Project project, User user, DateTime? started_at, DateTime? ended_at)
        {
            Id = id;
            Project = project;
            User = user;
            Started_at = started_at;
            Ended_at = ended_at;
        }
    }
}
