using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vibbraapi.Domain.Entities
{
    public class Time:Entity
    {
        public long Projct_Id { get; set; }

        public long User_Id { get; set; }

        public DateTime Started_at { get; set; }

        public DateTime Ended_at { get; set; }

        public User user { get; set; }

        public Project project { get; set; }

    }
}
