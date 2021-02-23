using Equipment.Database.Entities.Base;
using System.Collections.Generic;

namespace Equipment.Database.Entities
{
    public class MainEquipmentType : NamedEntity
    {
        public IList<MainEquipment> MainEquipments { get; set; } = new List<MainEquipment>();
    }
}
