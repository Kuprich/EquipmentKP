using Equipment.Database.Entities.Base;
using System.Collections.Generic;

namespace Equipment.Database.Entities
{
    /// <summary>Место установки</summary>
    public class Location : NamedEntity
    {
        /// <summary>VN Код (Кодовое название объекта автоматизации)</summary>
        public string CodeName  { get; set; }

        /// <summary>Адрес объекта автоматизации</summary>
        public string Address   { get; set; }

        /// <summary>Список комплектов оборудования, принадлежащих данному объекту автоматизации</summary>
        public IList<EquipmentsKit> EquipmentsKits { get; set; } = new List<EquipmentsKit>();
    }
}
  