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
    public class AuthCommand:Contract<User>,ICommand
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public AuthCommand() { }

        public AuthCommand(string login, string password) 
        {
            Login = login;
            Password = password;
        }

        public void Validate() 
        {
            AddNotifications(
                new Contract<User>()
                .Requires()
                .IsNotNullOrWhiteSpace(Login, "Login", "Login is required")
                .IsNotNullOrWhiteSpace(Password, "Password", "Password is required"));
        }
    }
}
