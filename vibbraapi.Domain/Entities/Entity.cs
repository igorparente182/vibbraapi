using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vibbraapi.Domain.Entities
{
    public class Entity : IEquatable<Entity>
    {
        public long Id { get; private set; }

        public Entity() 
        {
           
        }
        public bool Equals(Entity other)
        {
            throw new NotImplementedException();
        }
    }
}
