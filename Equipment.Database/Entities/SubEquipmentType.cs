using Equipment.Database.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Equipment.Database.Entities
{
    public class SubEquipmentType : NamedEntity
    {
        public IList<SubEquipment> SubEquipments = new List<SubEquipment>();
    }
}
