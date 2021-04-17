using Equipment.Database.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Equipment.Database.Entities
{
    /// <summary>Оборудование</summary>
    public class MainEquipment : NamedEntity
    {
        /// <summary>IpAddress (если есть)</summary>
        public string IpAddress             { get; set; }

        /// <summary>Сетевое имя (если есть)</summary>
        public string NetworkName           { get; set; }

        /// <summary>Операционная система (если есть)</summary>
        public string OperationSystem       { get; set; }

        /// <summary>Серийный номер</summary>
        public string SerialNo              { get; set; }

        /// <summary>Комплект оборудования</summary>
        public EquipmentsKit EquipmentsKit  { get; set; }

        /// <summary>Тип оборудования</summary>
        public EquipmentType EquipmentType  { get; set; }

        /// <summary>Список заявкок по текущему оборудованию</summary>
        public IList<Request> Requests      { get; set; } = new List<Request>();

    }
}
