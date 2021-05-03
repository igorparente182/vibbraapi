using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vibbraapi.Domain.Entities;

namespace vibbraapi.Domain.Repositories
{
    public interface IUserRepository
    {
        void Create(User user);

        void Update(User user);

        User GetById(long id);

        User GetByLogin(string login);

        IEnumerable<User> GetAll();

        bool isAuthenticate(string login, string password);



    }
}
