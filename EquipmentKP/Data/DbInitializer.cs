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
        private EquipmentsKit       [] equipmentsKits       = new EquipmentsKit[3];
        private MainEquipment       [] mainEquipments       = new MainEquipment[9];
        private Request             [] requests             = new Request[5];
        private RequestState        [] requestStates        = new RequestState[5];
        private RequestMovement     [] requestMovements     = new RequestMovement[8]; 

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
            await Init_EquipmentsKit();
            await Init_MainEquipment();
            await Init_Request();
            await Init_RequestState();
            await Init_RequestMovement();

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
            requests[0] = new Request { Number = 1, MainEquipment = mainEquipments[0] };
            requests[1] = new Request { Number = 2, MainEquipment = mainEquipments[0] };
            requests[2] = new Request { Number = 3, MainEquipment = mainEquipments[0] };
            requests[3] = new Request { Number = 4, MainEquipment = mainEquipments[3] };
            requests[4] = new Request { Number = 5, MainEquipment = mainEquipments[4] };
            await context.AddRangeAsync(requests);
            await context.SaveChangesAsync();
        }
        private async Task Init_MainEquipment()
        {
            mainEquipments[0] = new MainEquipment { SerialNumber = "888 999 777", EquipmentsKit = equipmentsKits[0], EquipmentType = equipmentTypes[0], IpAddress = "192.168.0.1", Name = "Системный блок 1", NetworkName = "sysblock - 1", OperationSystem = "windows 7" };
            mainEquipments[1] = new MainEquipment { SerialNumber = "888 999 888", EquipmentsKit = equipmentsKits[0], EquipmentType = equipmentTypes[1], Name = "Монитор 1"};
            mainEquipments[2] = new MainEquipment { SerialNumber = "888 999 999", EquipmentsKit = equipmentsKits[0], EquipmentType = equipmentTypes[2], Name = "ИБП 1"};

            mainEquipments[3] = new MainEquipment { SerialNumber = "111 222 333", EquipmentsKit = equipmentsKits[1], EquipmentType = equipmentTypes[0], IpAddress = "192.168.0.2", Name = "Системный блок 2", NetworkName = "sysblock - 2", OperationSystem = "windows 10" };
            mainEquipments[4] = new MainEquipment { SerialNumber = "111 222 444", EquipmentsKit = equipmentsKits[1], EquipmentType = equipmentTypes[1], Name = "Монитор 2" };
            mainEquipments[5] = new MainEquipment { SerialNumber = "111 222 555", EquipmentsKit = equipmentsKits[1], EquipmentType = equipmentTypes[2], Name = "ИБП 2" };

            mainEquipments[6] = new MainEquipment { SerialNumber = "222 777 555", EquipmentsKit = equipmentsKits[2], EquipmentType = equipmentTypes[0], IpAddress = "192.168.0.3", Name = "Системный блок 3", NetworkName = "sysblock - 3", OperationSystem = "windows xp" };
            mainEquipments[7] = new MainEquipment { SerialNumber = "222 777 333", EquipmentsKit = equipmentsKits[2], EquipmentType = equipmentTypes[1], Name = "Монитор 3" };
            mainEquipments[8] = new MainEquipment { SerialNumber = "222 777 666", EquipmentsKit = equipmentsKits[2], EquipmentType = equipmentTypes[2], Name = "ИБП 3" };

            await context.MainEquipment.AddRangeAsync(mainEquipments);
            await context.SaveChangesAsync();
        }
        private async Task Init_EquipmentsKit()
        {
            equipmentsKits[0] = new EquipmentsKit { InventoryNum = "456777333", Location = locations[0], Owner = "УСД в Республике Мордовия", ReceiptDate = DateTime.Parse("10.01.2015") };
            equipmentsKits[1] = new EquipmentsKit { InventoryNum = "736123123", Location = locations[1], Owner = "Филиал ФГБУ ИАЦ в РМ", ReceiptDate = DateTime.Parse("20.05.2011") };
            equipmentsKits[2] = new EquipmentsKit { InventoryNum = "021384123", Location = locations[2], Owner = "УСД в Республике Мордовия", ReceiptDate = DateTime.Parse("30.08.2017") };
            
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



        //#region Init_MainEquipmentTypes - инициализация типов основного оборудования
        //private async Task Init_MainEquipmentTypes()
        //{
        //    mainEquipmentTypes = new MainEquipmentType[2];
        //    mainEquipmentTypes[0] = new MainEquipmentType { Name = "Сервер общего назначения" };
        //    mainEquipmentTypes[1] = new MainEquipmentType { Name = "Рабочая станция" };

        //    await context.MainEquipmentTypes.AddRangeAsync(mainEquipmentTypes);
        //    await context.SaveChangesAsync();
        //}
        //#endregion

        //#region Init_SubEquipmentTypes - инициализация типов периферийного оборудования
        //private async Task Init_SubEquipmentTypes()
        //{
        //    subEquipmentTypes = new SubEquipmentType[3];
        //    subEquipmentTypes[0] = new SubEquipmentType { Name = "Монитор" };
        //    subEquipmentTypes[1] = new SubEquipmentType { Name = "Принтер" };
        //    subEquipmentTypes[2] = new SubEquipmentType { Name = "ИБП" };

        //    await context.SubEquipmentTypes.AddRangeAsync(subEquipmentTypes);
        //    await context.SaveChangesAsync();
        //}
        //#endregion

        //#region Init_Locations - инициализация мест установки оборудования
        //private async Task Init_Locations()
        //{
        //    locations = new Location[3];
        //    locations[0] = new Location { CodeName = "13RS0011", Name = "Зубово-Полянский районный суд РМ" };
        //    locations[1] = new Location { CodeName = "13RS0020", Name = "Темниковский районный суд РМ" };
        //    locations[2] = new Location { CodeName = "13RS0021", Name = "Теньгушевский районный суд РМ" };

        //    await context.Locations.AddRangeAsync(locations);
        //    await context.SaveChangesAsync();
        //}
        //#endregion

        //#region Init_MainEquipments - инициализация основного оборудования
        //private async Task Init_MainEquipments()
        //{
        //    mainEquipments = new MainEquipment[3];

        //    mainEquipments[0] = new MainEquipment
        //    {
        //        InvNo = "1234567890",
        //        SerialNo = "18273812738",
        //        Name = "крафтвей экспресс",
        //        Owner = "УСД по РМ",
        //        MainEquipmentType = mainEquipmentTypes[0],
        //        Location = locations[0],
        //        NetworkName = "serverNew",
        //        OperationSystem = "Windows Server 2012R2"
        //    };
        //    mainEquipments[1] = new MainEquipment
        //    {
        //        InvNo = "10954323490",
        //        SerialNo = "18902340123123",
        //        Name = "крафтвей экспресс E155",
        //        Owner = "УСД по РМ",
        //        MainEquipmentType = mainEquipmentTypes[0],
        //        Location = locations[1],
        //        NetworkName = "serverak",
        //        OperationSystem = "Windows Server 2012R2"
        //    };
        //    mainEquipments[2] = new MainEquipment
        //    {
        //        InvNo = "10923849012",
        //        SerialNo = "049258690342",
        //        Name = "Asus prime 123",
        //        Owner = "Зубова поляна",
        //        MainEquipmentType = mainEquipmentTypes[1],
        //        Location = locations[2],
        //        NetworkName = "serverak",
        //        OperationSystem = "Windows 7"
        //    };

        //    await context.MainEquipments.AddRangeAsync(mainEquipments);
        //    await context.SaveChangesAsync();
        //}
        //#endregion

        //#region Init_SubEquipments - инициализация периферийного оборудования
        //private async Task Init_SubEquipments()
        //{
        //    subEquipments = new SubEquipment[9];

        //    subEquipments[0] = new SubEquipment { MainEquipment = mainEquipments[0], SubEquipmentType = subEquipmentTypes[0], Name = "LG 1234", SerialNo = "8912349782" };
        //    subEquipments[1] = new SubEquipment { MainEquipment = mainEquipments[0], SubEquipmentType = subEquipmentTypes[1], Name = "Canon 1234", SerialNo = "123461234" };
        //    subEquipments[2] = new SubEquipment { MainEquipment = mainEquipments[0], SubEquipmentType = subEquipmentTypes[2], Name = "ИБП 1234", SerialNo = "10923481234" };

        //    subEquipments[3] = new SubEquipment { MainEquipment = mainEquipments[1], SubEquipmentType = subEquipmentTypes[0], Name = "LG 345", SerialNo = "2334342545" };
        //    subEquipments[4] = new SubEquipment { MainEquipment = mainEquipments[1], SubEquipmentType = subEquipmentTypes[1], Name = "Canon 345", SerialNo = "1234612312344" };
        //    subEquipments[5] = new SubEquipment { MainEquipment = mainEquipments[1], SubEquipmentType = subEquipmentTypes[2], Name = "ИБП 345", SerialNo = "1091233434" };

        //    subEquipments[6] = new SubEquipment { MainEquipment = mainEquipments[2], SubEquipmentType = subEquipmentTypes[0], Name = "LG 4356", SerialNo = "243334456" };
        //    subEquipments[7] = new SubEquipment { MainEquipment = mainEquipments[2], SubEquipmentType = subEquipmentTypes[1], Name = "Canon 4356", SerialNo = "345444554" };
        //    subEquipments[8] = new SubEquipment { MainEquipment = mainEquipments[2], SubEquipmentType = subEquipmentTypes[2], Name = "ИБП 4356", SerialNo = "109123123465434" };

        //    await context.SubEquipments.AddRangeAsync(subEquipments);
        //    await context.SaveChangesAsync();
        //} 
        //#endregion

    }

}
