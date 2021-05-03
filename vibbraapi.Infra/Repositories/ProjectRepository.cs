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
    public class ProjectRepository : IProjectRepository
    {
        private readonly VibbraContext _context;

        public ProjectRepository(VibbraContext context) 
        {
            _context = context;
        }
        public void Create(Project project)
        {
            _context.Add(project);
            _context.SaveChanges();
        }

        public IEnumerable<Project> getAll()
        {
            return (IEnumerable<Project>)_context.Projects.Select(p=> new {p.Id, p.Title,p.Description}).ToList();
        }

        public Project getById(long project_id)
        {
            return _context.Projects.AsNoTracking().Include(p=>p.Times).FirstOrDefault(p=>p.Id==project_id);
        }

        public IEnumerable<Time> GetProjectsUser(User user)
        {
            return _context.Times.AsNoTracking().Include(p => p.project).Where(p=>p.user==user);
        }

        public void Update(Project project)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Project> IProjectRepository.GetProjectsUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
