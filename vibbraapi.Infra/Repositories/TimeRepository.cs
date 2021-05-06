using Microsoft.EntityFrameworkCore;
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
            _context.Times.Add(time);
            _context.SaveChanges();
        }

        public Time getById(long time_id)
        {
            return _context.Times.AsNoTracking().FirstOrDefault(t => t.Id == time_id);
        }

        public IEnumerable<Time> getByTime(long time_id)
        {
            return _context.Times.AsNoTracking().Include(p => p.Project).Include(p => p.User).Where(t => t.Id == time_id);
        }

        public IEnumerable<Time> getTimeByProject(long project_id)
        {
            return _context.Times.AsNoTracking().Include(p=>p.Project).Include(p=>p.User).Where(t=>t.Project_Id==project_id);
        }

        public Time getTimeByProjectByUser(long project_id, long _user_id)
        {
            return _context.Times.AsNoTracking().FirstOrDefault(t => t.Project_Id == project_id && t.User_Id==_user_id);
        }

        public IEnumerable<Time> getTimeByUser(long user_id)
        {
            return _context.Times.AsNoTracking().Include(p => p.Project).Include(p => p.User).Where(t => t.User_Id == user_id);
        }

        public void Update(Time time)
        {
            _context.Entry(time).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
