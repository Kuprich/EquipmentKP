using System;
using System.Collections.Generic;
using System.Text;

namespace Equipment.Database.Entities.Base
{
    public abstract class NamedEntity : Entity
    {
        /// <summary>Имя сущности</summary>
        public string Name { get; set; }
    }
}
