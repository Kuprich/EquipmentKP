using Equipment.Database.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Equipment.Database.Entities
{
    /// <summary>движение заявки</summary>
    public class RequestMovement : Entity
    {
        /// <summary>Текущая заявка </summary>
        public Request Request              { get; set; }

        /// <summary>Дата регистрации очередного состояния заявки</summary>
        public DateTime RegistrationDate    { get; set; }

        /// <summary>Состояние заявки</summary>
        public RequestState RequestState    { get; set; }
    }
}
