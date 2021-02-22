using System;
using System.Collections.Generic;
using System.Text;

namespace Equipment.Database.Entities.Base
{
    public abstract class NamedEntity : Entity
    {
        public string Name { get; set; }
    }
}
