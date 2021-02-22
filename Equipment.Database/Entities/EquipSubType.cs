using Equipment.Database.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Equipment.Database.Entities
{
    public class EquipSubType : NamedEntity
    {
        public IList<EquipType> EquipTypes { get; set; }
    }
}
