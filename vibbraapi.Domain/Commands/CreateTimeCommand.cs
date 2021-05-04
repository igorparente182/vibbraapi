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
        public Project Project { get; set; }

        public User User { get; set; }

        public DateTime? Started_at { get; set; }

        public DateTime? Ended_at { get; set; }

        public CreateTimeCommand(Project project, User user, DateTime? started_at, DateTime? ended_at)
        {
            Project = project;
            User = user;
            Started_at = started_at;
            Ended_at = ended_at;
        }

        public void Validate() 
        {
           
        }
    }
}
