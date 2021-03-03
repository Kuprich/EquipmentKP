using Equipment.Database.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Equipment.Database.Entities
{
    public class RequestStatus : NamedEntity
    {
        public Request Request { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
