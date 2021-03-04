using Equipment.Database.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Equipment.Database.Entities
{
    public class RequestState : NamedEntity
    {
        public IList<RequestMovement> RequestMovements { get; set; } = new List<RequestMovement>();
    }
}
