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
    public class UpdateProjectCommand:Contract<Project>,ICommand
    {
        public long Project_Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public List<long> User_Id { get; set; }

        public UpdateProjectCommand() { }

        public UpdateProjectCommand(long project_id, string title, string description, List<long> user_id) 
        {
            Project_Id = project_id;
            Title = title;
            Description = description;
            User_Id = user_id;
        }

        public void Validate() 
        {
            AddNotifications(new Contract<Project>()
                .Requires()
                .IsNotNullOrWhiteSpace(Title, "Title", "Title is required"));
        }
    }
}
