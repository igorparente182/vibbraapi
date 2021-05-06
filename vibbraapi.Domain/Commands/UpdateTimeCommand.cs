using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vibbraapi.Domain.Commands.Contracts;
using vibbraapi.Domain.Entities;

namespace vibbraapi.Domain.Commands
{
    public class UpdateTimeCommand:Contract<Time>,ICommand
    {
        public long Time_Id { get; set; }
        public long Project_Id { get; set; }

        public long User_Id { get; set; }

        public DateTime? Started_at { get; set; }

        public DateTime? Ended_at { get; set; }

        public UpdateTimeCommand(long time_id,long project_id, long user_id, DateTime? started_at, DateTime? ended_at)
        {
            Time_Id = time_id;
            Project_Id = project_id;
            User_Id = user_id;
            Started_at = started_at;
            Ended_at = ended_at;
        }

        public void Validate()
        {
            AddNotifications(
                new Contract<Time>()
                .Requires()
                .IsNotNull(Time_Id, "Time_Id", "Time_Id is required")
                .IsNotNull(Project_Id, "Project_Id", "Project_Id is required")
                .IsNotNull(User_Id, "User_Id", "User_Id is required"));
        }
    }
}
