using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vibbraapi.Domain.Entities;

namespace vibbraapi.Domain.Repositories
{
    public interface IProjectRepository
    {
        void Create(Project project);

        void Update(Project project);

        Project getById(long project_id);

        IEnumerable<Project> getAll();

        IEnumerable<Project> GetProjectsUser(User user);


    }
}
