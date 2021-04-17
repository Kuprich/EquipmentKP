using Equipment.Database.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Equipment.Database.Entities
{
    /// <summary>Тип оборудования</summary>
    public class EquipmentType : NamedEntity
    {
        /// <summary>Категория оборудования, которая относится к данному типу</summary>
        public EquipmentCategory EquipmentCategory { get; set; }
        
        /// <summary>Список оборудования, относящихся к данному типу</summary>
        public IList<MainEquipment> MainEquipments { get; set; } = new List<MainEquipment>();
    }
}
