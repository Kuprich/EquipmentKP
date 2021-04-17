using Equipment.Database.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Equipment.Database.Entities
{
    /// <summary>Категория оборудования</summary>
    public class EquipmentCategory : NamedEntity
    {
        /// <summary>Список оборудования с текущим типом</summary>
        public IList<EquipmentType> EquipmentTypes { get; set; } = new List<EquipmentType>();
    }
}
