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

namespace vibbraapi.Domain.Handler
{
    public class ProjectHandler : Contract<Project>, IHandler<CreateProjectCommand>
    {
        public ICommandResult Handle(CreateProjectCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
