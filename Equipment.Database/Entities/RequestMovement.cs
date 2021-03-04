using Equipment.Database.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Equipment.Database.Entities
{
    public class RequestMovement : Entity
    {
        public Request Request { get; set; }
        public DateTime RegistrationDate { get; set; }
        public RequestState RequestState { get; set; }
    }
}
