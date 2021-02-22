using Equipment.Database.Entities.Base;

namespace Equipment.Database.Entities
{
    public class Equip : NamedEntity
    {
        public string InvNo { get; set; }
        public string SerialNo { get; set; }
        public string Owner { get; set; }
        public EquipType EquipType { get; set; }
        public Location Location { get; set; }
        public string NetworkName { get; set; }
        public string OperationSystem { get; set; }

    }
}
