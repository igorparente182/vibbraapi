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
    public class CreateProjectCommand:Contract<Project>,ICommand
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public CreateProjectCommand() { }

        public CreateProjectCommand(string title, string description) 
        {
            Title = title;
            Description = description;
        }

        public void Validate() 
        {
            AddNotifications(new Contract<Project>()
                .Requires()
                .IsNotNullOrWhiteSpace(Title, "Title", "Title is required"));
        }
    }
}
