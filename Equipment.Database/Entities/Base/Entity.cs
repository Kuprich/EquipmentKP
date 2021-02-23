using Equipment.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Equipment.Database.Entities.Base
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}
