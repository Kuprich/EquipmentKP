using Equipment.Database.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Equipment.Database.Entities
{
    public class MainEquipment : NamedEntity
    {
        public string IpAddress             { get; set; }
        public string NetworkName           { get; set; }
        public string OperationSystem       { get; set; }
        public string SerialNo              { get; set; }
        public EquipmentsKit EquipmentsKit  { get; set; }
        public EquipmentType EquipmentType  { get; set; }
        public IList<Request> Requests      { get; set; } = new List<Request>();

    }
}
