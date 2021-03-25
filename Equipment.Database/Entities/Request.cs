using Equipment.Database.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Equipment.Database.Entities
{
    public class Request : Entity
    {
        public int Number { get; set; }
        public DateTime ReceiptDate { get; set; }
        public MainEquipment MainEquipment { get; set; }
        public IList<RequestMovement> RequestMovements { get; set; } = new List<RequestMovement>();
    }
}
