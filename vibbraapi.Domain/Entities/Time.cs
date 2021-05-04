﻿using System;
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

        public User user { get; set; }

        public Project project { get; set; }

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
