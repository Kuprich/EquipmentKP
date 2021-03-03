using Equipment.Database.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Equipment.Database.Entities
{
    public class EquipmentCategory : NamedEntity
    {
        public IList<EquipmentType> EquipmentTypes { get; set; } = new List<EquipmentType>();
    }
}
