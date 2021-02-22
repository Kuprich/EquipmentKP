using Equipment.Database.Entities.Base;
using System.Collections.Generic;

namespace Equipment.Database.Entities
{
    public class Location : NamedEntity
    {
        public string CodeName { get; set; }
        public string Organization { get; set; }
        public IList<Equip> Equips { get; set; }
    }
}
