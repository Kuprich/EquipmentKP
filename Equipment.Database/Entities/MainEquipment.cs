using Equipment.Database.Entities.Base;
using System.Collections.Generic;

namespace Equipment.Database.Entities
{
    public class MainEquipment : NamedEntity
    {
        public string InvNo { get; set; }
        public string SerialNo { get; set; }
        public string Owner { get; set; }
        public string NetworkName { get; set; }
        public string OperationSystem { get; set; }
        public MainEquipmentType MainEquipmentType { get; set; }
        public Location Location { get; set; }
        public IList<SubEquipment> SubEquipments { get; set; } = new List<SubEquipment>();

    }
}
