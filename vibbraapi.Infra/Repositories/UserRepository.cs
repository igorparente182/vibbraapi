using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vibbraapi.Domain.Entities;
using vibbraapi.Domain.Queries;
using vibbraapi.Domain.Repositories;
using vibbraapi.Infra.Contexts;

namespace vibbraapi.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly VibbraContext _vibbraContext;

        public UserRepository(VibbraContext vibbraContext) 
        {
            _vibbraContext = vibbraContext;
        }
        public void Create(User user)
        {
            _vibbraContext.Add(user);
            _vibbraContext.SaveChanges();
        }

        public IEnumerable<User> GetAll()
        {
            return _vibbraContext.Users
                .AsNoTracking()
                .OrderBy(u => u.Name);
        }

        public User GetById(long id)
        {
            return _vibbraContext.Users
                .AsNoTracking()
                .FirstOrDefault(u => u.Id == id);
        }

        public User GetByLogin(string login)
        {
            return _vibbraContext.Users
                .AsNoTracking()
                .FirstOrDefault(UserQueries.getUserByLogin(login));
        }

        public bool isAuthenticate(string login, string password)
        {
            var auth = _vibbraContext.Users.Where(UserQueries.isAuthenticate(login, password)).FirstOrDefault();
            if (auth != null)
                return true;
            return false;
        }

        public void Update(User user)
        {
            _vibbraContext.Entry(user).State = EntityState.Modified;
            _vibbraContext.SaveChanges();
        }
    }
}
