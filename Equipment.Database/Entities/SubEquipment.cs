using Equipment.Database.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Equipment.Database.Entities
{
    public class SubEquipment : NamedEntity
    {
        public string SerialNo { get; set; }
        public SubEquipmentType SubEquipmentType { get; set; }
        public MainEquipment MainEquipment { get; set; }
    }
}
