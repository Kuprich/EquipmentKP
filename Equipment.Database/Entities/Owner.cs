using Equipment.Database.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Equipment.Database.Entities
{
    public class Owner : NamedEntity
    {
        public string Chief     { get; set; }
        public string Address   { get; set; }
        public string Email     { get; set; }
        public string Phone     { get; set; }
        public IList<EquipmentsKit> EquipmentsKits { get; set; } = new List<EquipmentsKit>();

    }
}
