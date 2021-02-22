using Equipment.Database.Entities.Base;
using System.Collections.Generic;

namespace Equipment.Database.Entities
{
    public class EquipType : NamedEntity
    {
        public IList<Equip> Equips { get; set; }
    }
}
