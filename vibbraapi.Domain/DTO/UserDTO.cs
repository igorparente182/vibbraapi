using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vibbraapi.Domain.Entities;

namespace vibbraapi.Domain.DTO
{
    public class UserDTO:Entity
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public UserDTO() { }
        public UserDTO(long id,string name, string email, string login, string password)
        {
            Id = id;
            Name = name;
            Email = email;
            Login = login;
            Password = password;
        }
    }
}
