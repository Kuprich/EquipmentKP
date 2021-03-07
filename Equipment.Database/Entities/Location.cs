using Equipment.Database.Entities.Base;
using System.Collections.Generic;

namespace Equipment.Database.Entities
{
    public class Location : NamedEntity
    {
        public string CodeName  { get; set; }
        public string Address   { get; set; }
        public IList<EquipmentsKit> EquipmentsKits { get; set; } = new List<EquipmentsKit>();
    }
}
  