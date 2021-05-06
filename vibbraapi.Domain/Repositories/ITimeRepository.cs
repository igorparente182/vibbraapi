using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vibbraapi.Domain.Entities;

namespace vibbraapi.Domain.Repositories
{
    public interface ITimeRepository
    {
        void Create(Time time);

        void Update(Time time);

        Time getById(long time_id);

        Time getTimeByProjectByUser(long project_id, long _user_id);

        IEnumerable<Time> getTimeByUser(long user_id);

        IEnumerable<Time> getTimeByProject(long project_id);

        IEnumerable<Time> getByTime(long time_id);

    }
}
