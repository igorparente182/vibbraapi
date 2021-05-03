using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using vibbraapi.Domain.Entities;

namespace vibbraapi.Domain.Queries
{
    public static class UserQueries
    {
        public static Expression<Func<User, bool>> isAuthenticate(string login, string password) 
        {
            return a => a.Login == login && a.Password == password;
        }

        public static Expression<Func<User, bool>> getUserByLogin(string login) 
        {
            return u => u.Login == login;
        }
    }
}
