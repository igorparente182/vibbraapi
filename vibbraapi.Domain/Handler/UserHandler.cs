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
    public class UserHandle :
        Contract<User>,
        IHandler<AuthCommand>,
        IHandler<CreateUserCommand>,
        IHandler<UpdateUserCommand>
    {
        private readonly IUserRepository _repository;

        public UserHandle(IUserRepository repository) 
        {
            _repository = repository;
        }
        public ICommandResult Handle(CreateUserCommand command)
        {
            command.Validate();
            if (!command.IsValid)
                return new GenericCommandResult(false, "Error: ", command.Notifications);

            var user = new User(command.Name,command.Email,command.Login,command.Password);

            _repository.Create(user);

            return new GenericCommandResult(true, "Create user with success!",user);
        }

        public ICommandResult Handle(UpdateUserCommand command)
        {
            command.Validate();
            if (!command.IsValid)
                return new GenericCommandResult(false, "Error: ", command.Notifications);

            var user = _repository.GetById(command.Id);
            if (user == null)
                return new GenericCommandResult(false, "User not found!", null);
            user.Email = command.Email;
            user.Login = command.Login;
            user.Name = command.Name;
            user.Password = command.Password;
            
            _repository.Update(user);

            return new GenericCommandResult(true, "Update user with success!", user);
        }

        public ICommandResult Handle(AuthCommand command)
        {
            command.Validate();

            if (!command.IsValid)
                return new GenericCommandResult(false, "Error", command.Notifications);

            var user = _repository.isAuthenticate(command.Login, command.Password);
            if (user)
                return new GenericCommandResult(true, "Success", _repository.GetByLogin(command.Login));
            return new GenericCommandResult(false, "Usuario not authenticate", null);
        }
    }
}
