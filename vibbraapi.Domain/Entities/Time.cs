using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vibbraapi.Domain.Entities
{
    public class Time:Entity
    {
        public long Project_Id { get; private set; }

        public long User_Id { get; private set; }

        public DateTime? Started_at { get; private set; }

        public DateTime? Ended_at { get; private set; }

        public virtual User User { get; private set; }

        public virtual Project Project { get; private set; }

        public Time() { }
        public Time(long project_id, long user_id, DateTime? started_at, DateTime? ended_at) 
        {
           
            Project_Id = project_id;
            User_Id = user_id;
            Started_at = started_at;
            Ended_at = ended_at;
        }

    }
}
