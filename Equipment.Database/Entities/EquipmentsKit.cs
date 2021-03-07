using Equipment.Database.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Equipment.Database.Entities
{
    public class EquipmentsKit : NamedEntity
    {
        public string InventoryNo  { get; set; }
        public string Owner         { get; set; }
        public DateTime ReceiptDate { get; set; }
        public Location Location    { get; set; }
        public IList<MainEquipment> MainEquipments { get; set; } = new List<MainEquipment>();
    }
}
