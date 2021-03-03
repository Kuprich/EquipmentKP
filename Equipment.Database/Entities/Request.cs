using Equipment.Database.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Equipment.Database.Entities
{
    public class Request : Entity
    {
        public int Number { get; set; }
        public Equipment Equipment { get; set; }
        public IList<RequestStatus> RequestStatuses { get; set; } = new List<RequestStatus>();
    }
}
