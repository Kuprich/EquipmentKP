using Equipment.Database.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Equipment.Database.Entities
{
    /// <summary> Документ, который прикрепляется к заявке (обычно по ремнту и обслуживанию) </summary>
    public class Document : NamedEntity
    {
        /// <summary> Дата создания документа </summary>
        public DateTime CreationDate { get; set; }

        /// <summary> Номер документа </summary>
        public string Number { get; set; }

        /// <summary> Прикрепляемый документ </summary>
        public byte[] Content { get; set; }

        /// <summary> тип файла (рисширение) </summary>
        public string FileType { get; set; }

        /// <summary>Заявка по текущему документу</summary>
        public Request Request { get; set; }

    }
}
