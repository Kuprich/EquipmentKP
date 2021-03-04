using Equipment.Database.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Equipment.Database.Entities
{
    public class EquipmentType : NamedEntity
    {
        public EquipmentCategory EquipmentCategory { get; set; }
        public IList<MainEquipment> MainEquipments { get; set; } = new List<MainEquipment>();
    }
}
