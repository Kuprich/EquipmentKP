using Equipment.Database.Context;
using Equipment.Database.Entities;
using Equipment.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentKP.Data
{
    class DbInitializer
    {
        private readonly EquipmentContext context;
        private readonly ILogger<DbInitializer> logger;
        private Stopwatch timer = new Stopwatch();

        private EquipmentCategory   [] equipmentCategories  = new EquipmentCategory[2];
        private EquipmentType       [] equipmentTypes       = new EquipmentType[4];
        private Location            [] locations            = new Location[3];
        private Owner               [] owners               = new Owner[2];
        private EquipmentsKit       [] equipmentsKits       = new EquipmentsKit[4];
        private MainEquipment       [] mainEquipments       = new MainEquipment[10];
        private Request             [] requests             = new Request[5];
        private RequestState        [] requestStates        = new RequestState[5];
        private RequestMovement     [] requestMovements     = new RequestMovement[8];

        private Document            [] documents            = new Document[5];

        public DbInitializer(EquipmentContext context, ILogger<DbInitializer> logger)
        {
            this.context = context;
            this.logger = logger;
        }
        public async Task InitializeAsync()
        {
            timer = Stopwatch.StartNew();
            logger.LogInformation("инициализация БД");

            logger.LogInformation("Начало удаления БД");
            // на этаме отладки понадобится удалять БД
            await context.Database.EnsureDeletedAsync().ConfigureAwait(false);
            logger.LogInformation($"Удаление БД произведено за {timer.ElapsedMilliseconds}");

            logger.LogInformation("Вызов миграций БД");
            await context.Database.MigrateAsync();
            logger.LogInformation($"Миграция БД произведена за {timer.ElapsedMilliseconds}");

            logger.LogInformation("Вызов миграций БД");
            await context.Database.MigrateAsync();
            logger.LogInformation($"Миграция БД произведена за {timer.ElapsedMilliseconds}");

            await Init_EquipmentCategory();
            await Init_EquipmentType();
            await Init_Location();
            await Init_Owner();
            await Init_EquipmentsKit();
            await Init_MainEquipment();
            await Init_Request();
            await Init_RequestState();
            await Init_RequestMovement();

            await Init_Document();

        }

        private async Task Init_Document()
        {
            documents[0] = new Document { Name = "Приказ № 1-02-227 Об утв. Метод. рукомендаций по проведению проверок", Number = "1-02-227", CreationDate = DateTime.Parse("12.05.2016") };
            documents[1] = new Document { Name = "Приказ  №113 от 13.07.2018 Об утверждении устава ФГБУ ИАЦ", Number = "113", CreationDate = DateTime.Parse("13.07.2018") };
            documents[2] = new Document { Name = "Приказ № 1-02-43 Об утв. Поручения филиалам на 2018 год", Number = "1-02-43", CreationDate = DateTime.Parse("03.02.2002") };
            documents[3] = new Document { Name = "Приказ № 1-02-269 Об утверждении Регламента взаимодействия ФГБУ ИАЦ с филиалами", Number = "№ 1-02-269", CreationDate = DateTime.Parse("22.08.2019") };
            documents[4] = new Document { Name = "положение об антикорруп политике", CreationDate = DateTime.Parse("01.03.2019") };

            await context.AddRangeAsync(documents);
            await context.SaveChangesAsync();
        }

        private async Task Init_RequestMovement()
        {
            requestMovements[0] = new RequestMovement { Request = requests[1], RegistrationDate = DateTime.Now, RequestState = requestStates[0] };
            requestMovements[1] = new RequestMovement { Request = requests[1], RegistrationDate = DateTime.Now, RequestState = requestStates[2] };
            requestMovements[2] = new RequestMovement { Request = requests[1], RegistrationDate = DateTime.Now, RequestState = requestStates[4] };

            requestMovements[3] = new RequestMovement { Request = requests[2], RegistrationDate = DateTime.Now, RequestState = requestStates[0] };
            requestMovements[4] = new RequestMovement { Request = requests[2], RegistrationDate = DateTime.Now, RequestState = requestStates[1] };
            requestMovements[5] = new RequestMovement { Request = requests[2], RegistrationDate = DateTime.Now, RequestState = requestStates[2] };

            requestMovements[6] = new RequestMovement { Request = requests[3], RegistrationDate = DateTime.Now, RequestState = requestStates[0] };
            requestMovements[7] = new RequestMovement { Request = requests[3], RegistrationDate = DateTime.Now, RequestState = requestStates[4] };

            await context.AddRangeAsync(requestMovements);
            await context.SaveChangesAsync();
        }
        private async Task Init_RequestState()
        {
            requestStates[0] = new RequestState { Name = "Получена" };
            requestStates[1] = new RequestState { Name = "Состояние - 1" };
            requestStates[2] = new RequestState { Name = "Состояние - 2" };
            requestStates[3] = new RequestState { Name = "Состояние - 3" };
            requestStates[4] = new RequestState { Name = "Исполнена" };
            await context.AddRangeAsync(requestStates);
            await context.SaveChangesAsync();
        }
        private async Task Init_Request()
        {
            requests[0] = new Request { Number = 1, ReceiptDate = DateTime.Parse("12.12.2015"), MainEquipment = mainEquipments[0] };
            requests[1] = new Request { Number = 2, ReceiptDate = DateTime.Parse("11.01.2012"), MainEquipment = mainEquipments[0] };
            requests[2] = new Request { Number = 3, ReceiptDate = DateTime.Parse("15.02.2013"), MainEquipment = mainEquipments[0] };
            requests[3] = new Request { Number = 4, ReceiptDate = DateTime.Parse("16.03.2014"), MainEquipment = mainEquipments[3] };
            requests[4] = new Request { Number = 5, ReceiptDate = DateTime.Parse("17.04.2015"), MainEquipment = mainEquipments[4] };
            await context.AddRangeAsync(requests);
            await context.SaveChangesAsync();
        }
        private async Task Init_MainEquipment()
        {
            mainEquipments[0] = new MainEquipment { SerialNo = "110", EquipmentsKit = equipmentsKits[0], EquipmentType = equipmentTypes[0], IpAddress = "192.168.0.1", Name = "Системный блок 1", NetworkName = "sysblock - 1", OperationSystem = "windows 7" };
            mainEquipments[1] = new MainEquipment { SerialNo = "111", EquipmentsKit = equipmentsKits[0], EquipmentType = equipmentTypes[1], Name = "Монитор 1"};
            mainEquipments[2] = new MainEquipment { SerialNo = "112", EquipmentsKit = equipmentsKits[0], EquipmentType = equipmentTypes[2], Name = "ИБП 1"};

            mainEquipments[3] = new MainEquipment { SerialNo = "440", EquipmentsKit = equipmentsKits[1], EquipmentType = equipmentTypes[0], IpAddress = "192.168.0.2", Name = "Системный блок 2", NetworkName = "sysblock - 2", OperationSystem = "windows 10" };
            mainEquipments[4] = new MainEquipment { SerialNo = "441", EquipmentsKit = equipmentsKits[1], EquipmentType = equipmentTypes[1], Name = "Монитор 2" };
            mainEquipments[5] = new MainEquipment { SerialNo = "442", EquipmentsKit = equipmentsKits[1], EquipmentType = equipmentTypes[2], Name = "ИБП 2" };

            mainEquipments[6] = new MainEquipment { SerialNo = "220", EquipmentsKit = equipmentsKits[2], EquipmentType = equipmentTypes[0], IpAddress = "192.168.0.3", Name = "Системный блок 3", NetworkName = "sysblock - 3", OperationSystem = "windows xp" };
            mainEquipments[7] = new MainEquipment { SerialNo = "221", EquipmentsKit = equipmentsKits[2], EquipmentType = equipmentTypes[1], Name = "Монитор 3" };
            mainEquipments[8] = new MainEquipment { SerialNo = "222", EquipmentsKit = equipmentsKits[2], EquipmentType = equipmentTypes[2], Name = "ИБП 3" };

            mainEquipments[9] = new MainEquipment{ EquipmentsKit = equipmentsKits[3] };

            await context.MainEquipment.AddRangeAsync(mainEquipments);
            await context.SaveChangesAsync();
        }
        private async Task Init_EquipmentsKit()
        {
            equipmentsKits[0] = new EquipmentsKit { InventoryNo = "456777333", Location = locations[0], Owner = owners[0], ReceiptDate = DateTime.Parse("10.01.2015") };
            equipmentsKits[1] = new EquipmentsKit { InventoryNo = "736123123", Location = locations[1], Owner = owners[1], ReceiptDate = DateTime.Parse("20.05.2011") };
            equipmentsKits[2] = new EquipmentsKit { InventoryNo = "021384123", Location = locations[2], Owner = owners[0], ReceiptDate = DateTime.Parse("30.08.2017") };
            equipmentsKits[3] = new EquipmentsKit { InventoryNo = "102310239012", Location = locations[2], Owner = owners[1], ReceiptDate = DateTime.Parse("30.08.2020") };
            
            await context.AddRangeAsync(equipmentsKits);
            await context.SaveChangesAsync();
        }
        private async Task Init_Location()
        {
            locations[0] = new Location { Name = "Зубово-Поляснкий районный суд", CodeName = "13RS0011", Address = "431110, п. Зубова-Поляна, ул. Советская 30" };
            locations[1] = new Location { Name = "Темниковский районный суд", CodeName = "13RS0020", Address = "430021, г. Темников, ул. Бараева 72А" };
            locations[2] = new Location { Name = "ПСП в с. Теньгушево", CodeName = "13RS0021", Address = "412302, п. Теньгушево, ул. Шельтяева 49" };

            await context.Locations.AddRangeAsync(locations);
            await context.SaveChangesAsync();
        }
        private async Task Init_Owner()
        {
            owners[0] = new Owner { Name = "УСД Судебного департамента в Республике Мордовия", Address = "430034, г. Саранск, ул. Гоголя, д.34", Chief = "Иванова Наталья Петровна" };
            owners[1] = new Owner { Name = "Верховный Суд республики Мордовия", Address = "430034, г. Саранcк, ул. Полежаева, д.23", Chief = "Петрова Валентина Эдуардовна" };


            await context.Owners.AddRangeAsync(owners);
            await context.SaveChangesAsync();
        }
        private async Task Init_EquipmentCategory()
        {
            equipmentCategories[0] = new EquipmentCategory { Name = "Основное оборудование"};
            equipmentCategories[1] = new EquipmentCategory { Name = "Периферийное оборудование"};

            await context.EquipmentCategories.AddRangeAsync(equipmentCategories);
            await context.SaveChangesAsync();
        }
        private async Task Init_EquipmentType()
        {
            equipmentTypes[0] = new EquipmentType { Name = "Системный блок", EquipmentCategory = equipmentCategories[0] };
            equipmentTypes[1] = new EquipmentType { Name = "Монитор", EquipmentCategory = equipmentCategories[1] };
            equipmentTypes[2] = new EquipmentType { Name = "ИБП", EquipmentCategory = equipmentCategories[1] };
            equipmentTypes[3] = new EquipmentType { Name = "Сервер", EquipmentCategory = equipmentCategories[0] };

            await context.EquipmentTypes.AddRangeAsync(equipmentTypes);
            await context.SaveChangesAsync();
        }

    }

}
