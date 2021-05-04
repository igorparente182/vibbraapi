﻿using Flunt.Validations;
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
    public class TimeHandler : Contract<Time>, IHandler<CreateTimeCommand>
    {
        private readonly ITimeRepository _repository;

        public TimeHandler(ITimeRepository repository)
        {
            _repository = repository;
        }
        public ICommandResult Handle(CreateTimeCommand command)
        {
            command.Validate();
            if(!command.IsValid) return new GenericCommandResult(false, "Error: ", command.Notifications);

            var time = new Time(command.Project, command.User, command.Started_at, command.Ended_at);
            _repository.Create(time);

            return new GenericCommandResult(true, "Create time with success!", time);
        }
    }
}
