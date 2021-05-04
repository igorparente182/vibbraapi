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
     public class CreateTimeCommand:Contract<Time>,ICommand
    {
        public long Project_Id { get; set; }

        public long User_Id { get; set; }

        public DateTime? Started_at { get; set; }

        public DateTime? Ended_at { get; set; }

        public CreateTimeCommand(long project_id, long user_id, DateTime? started_at, DateTime? ended_at)
        {
            Project_Id = project_id;
            User_Id = user_id;
            Started_at = started_at;
            Ended_at = ended_at;
        }

        public void Validate() 
        {
           
        }
    }
}
