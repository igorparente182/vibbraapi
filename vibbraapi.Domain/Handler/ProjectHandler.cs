using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vibbraapi.Domain.Commands;
using vibbraapi.Domain.Commands.Contracts;
using vibbraapi.Domain.Entities;
using vibbraapi.Domain.Handler.Contratcts;
using vibbraapi.Domain.Repositories;

namespace vibbraapi.Domain.Handler
{
    public class ProjectHandler : 
        Contract<Project>, 
        IHandler<CreateProjectCommand>,
        IHandler<UpdateProjectCommand>
    {
        private readonly IProjectRepository _repository;

        public ProjectHandler(IProjectRepository repository) 
        {
            _repository = repository;
        }
        public ICommandResult Handle(CreateProjectCommand command)
        {
            //validate fields  
            command.Validate();
            
            if (!command.IsValid)
                return new GenericCommandResult(false, "Error", command.Notifications);

            var project = new Project(command.Title, command.Description);

            //access the database to insert 
            _repository.Create(project);

            return new GenericCommandResult(true, "Success", project);

        }

        public ICommandResult Handle(UpdateProjectCommand command)
        {
            //validate fields
            command.Validate();

            if (!command.IsValid)
                return new GenericCommandResult(false, "Error", command.Notifications);

            //access the database to update
            var project = _repository.getById(command.Project_Id);
            project.Title = command.Title;
            project.Description = command.Description;

            _repository.Update(project);

            return new GenericCommandResult(true, "Sucess", project);
        }
    }
}
