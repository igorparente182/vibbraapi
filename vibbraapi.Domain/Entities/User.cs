using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vibbraapi.Domain.Entities
{
    public class User:Entity
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public virtual ICollection<Time> ProjectTimes { get; set; }

      
        public User(string name, string email, string login, string password) 
        {
            Name = name;
            Email = email;
            Login = login;
            Password = password;
        }
    }
}
