using Equipment.Database.Context;
using Equipment.Database.Entities;
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

            //await Init_SubEquipmentTypes();
            //await Init_MainEquipmentTypes();
            //await Init_Locations();
            //await Init_MainEquipments();
            //await Init_SubEquipments();
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
