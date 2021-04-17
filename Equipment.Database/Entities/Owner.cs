using Equipment.Database.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Equipment.Database.Entities
{
    /// <summary>Владелец оборудования</summary>
    public class Owner : NamedEntity
    {
        /// <summary>Ответственное лицо(управляюший данной организацией)</summary>
        public string Chief     { get; set; }

        /// <summary>Адрес организации</summary>
        public string Address   { get; set; }

        /// <summary>Email организации</summary>
        public string Email     { get; set; }

        /// <summary>Телефон организации</summary>
        public string Phone     { get; set; }

        /// <summary>Список комплектов оборудования, принадлежащих данному владельцу</summary>
        public IList<EquipmentsKit> EquipmentsKits { get; set; } = new List<EquipmentsKit>();

    }
}
