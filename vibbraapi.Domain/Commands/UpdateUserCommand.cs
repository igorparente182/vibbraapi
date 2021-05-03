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
    public class UpdateUserCommand:Contract<User>,ICommand
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public UpdateUserCommand() { }

        public UpdateUserCommand(long id, string name, string email, string login, string password) 
        {
            Id = id;
            Name = name;
            Email = email;
            Login = login;
            Password = password;
        }

        public void Validate()
        {
            AddNotifications(
                new Contract<User>()
                .Requires()
                .IsNotEmpty(Name, "Name", "Name is required")
                .IsNotEmpty(Email, "Email", "Email is required")
                .IsNotEmpty(Login, "Login", "Login is required")
                .IsNotEmpty(Password, "Password", "Password is required"));
        }
    }
}
