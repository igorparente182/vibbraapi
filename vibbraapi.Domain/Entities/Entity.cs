using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vibbraapi.Domain.Entities
{
    public class Entity : IEquatable<Entity>
    {
        public long Id { get;  set; }

        public Entity() 
        {
           
        }
        public bool Equals(Entity other)
        {
            return Id == other.Id;
        }
    }
}
