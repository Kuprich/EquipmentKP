using Equipment.Database.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Equipment.Database.Entities
{
    public class Document : NamedEntity
    {
        public DateTime CreationDate { get; set; }
        public string Number { get; set; }
        public byte[] Content { get; set; }
    }
}
