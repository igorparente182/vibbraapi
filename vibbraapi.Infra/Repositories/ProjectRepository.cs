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
            return _context.Projects;
        }

        public Project getById(long project_id)
        {
            return _context.Projects.AsNoTracking().Include(p=>p.Times).FirstOrDefault(p=>p.Id==project_id);
        }
        IEnumerable<Project> IProjectRepository.GetProjectsUser(User user)
        {
            return _context.Projects.Where(p => p.Times.Any(u => u.user == user));
        }
        public void Update(Project project)
        {
            _context.Entry(project).State = EntityState.Modified;
            _context.SaveChanges();
        }

    }
}
