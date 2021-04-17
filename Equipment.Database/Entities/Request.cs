using Equipment.Database.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Equipment.Database.Entities
{
    /// <summary>Заявка по ремонту или обслуживанию оборудоавния</summary>
    public class Request : Entity
    {
        /// <summary>Номер заявки</summary>
        public int Number { get; set; }

        /// <summary>Дата поступления заявки</summary>
        public DateTime ReceiptDate { get; set; }

        /// <summary>Оборудование, к которому принадлежит эта заявка</summary>
        public MainEquipment MainEquipment { get; set; }

        /// <summary>Признак закрытия заявки(если true, то закрыта)</summary>
        public bool Closed { get; set; }

        /// <summary>Движение заявки</summary>
        public IList<RequestMovement> RequestMovements { get; set; } = new List<RequestMovement>();

        /// <summary>Документ, прикрепленный к данной заявке</summary>
        public IList<Document> Documents { get; set; } = new List<Document>();
    }
}
