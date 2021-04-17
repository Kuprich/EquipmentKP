using Equipment.Database.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Equipment.Database.Entities
{
    /// <summary>Комплект оборудования</summary>
    public class EquipmentsKit : NamedEntity
    {
        /// <summary>Инвентарный номер оборудования</summary>
        public string InventoryNo   { get; set; }
        
        /// <summary>Владелец оборудования</summary>
        public Owner Owner          { get; set; }

        /// <summary>Дата поступления</summary>
        public DateTime ReceiptDate { get; set; }

        /// <summary>Место установки</summary>
        public Location Location    { get; set; }

        /// <summary>Список оборудования, входящего в данный комплект</summary>
        public IList<MainEquipment> MainEquipments { get; set; } = new List<MainEquipment>();
    }
}
