﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vibbraapi.Domain.Entities;
using vibbraapi.Domain.Repositories;
using vibbraapi.Infra.Contexts;

namespace vibbraapi.Infra.Repositories
{
    public class TimeRepository : ITimeRepository
    {
        private readonly VibbraContext _context;

        public TimeRepository(VibbraContext context) 
        {
            _context = context;
        }
        public void Create(Time time)
        {
            var project = _context.Projects.AsNoTracking().FirstOrDefault(p => p.Id == time.Project_Id);
            var user = _context.Users.AsNoTracking().FirstOrDefault(u => u.Id == time.User_Id);
            time.project = project;
            time.user = user;
            _context.Add(time);
            _context.SaveChanges();
        }

        public IEnumerable<Time> getByTime(long time_id)
        {
            return null;
        }

        public IEnumerable<Time> getTimeByProject(long project_id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Time> getTimeByUser(long user_id)
        {
            throw new NotImplementedException();
        }

        public void Update(Time time)
        {
            throw new NotImplementedException();
        }
    }
}
