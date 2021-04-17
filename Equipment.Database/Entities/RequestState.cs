using Equipment.Database.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Equipment.Database.Entities
{
    /// <summary>Состояние заявки</summary>
    public class RequestState : NamedEntity
    {
        /// <summary>Список движений заявки, которому соответствует текущее сосотояние</summary>
        public IList<RequestMovement> RequestMovements { get; set; } = new List<RequestMovement>();
    }
}
