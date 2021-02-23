using Equipment.Database.Context;
using Equipment.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentKP.Data
{
    class DbInitializer
    {
        private readonly EquipmentContext context;
        private readonly ILogger<DbInitializer> logger;
        private Stopwatch timer = new Stopwatch();

        private MainEquipmentType[] mainEquipmentTypes;
        private SubEquipmentType[]  subEquipmentTypes;
        private Location[]          locations;
        private MainEquipment[]     mainEquipments;
        private SubEquipment[]      subEquipments;

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

            await Init_EquipmentSubTypes();
            await Init_MainEquipmentTypes();
        }

        private async Task Init_MainEquipmentTypes()
        {
            mainEquipmentTypes = new MainEquipmentType[2];
            mainEquipmentTypes[0] = new MainEquipmentType { Name = "Сервер общего назначения" };
            mainEquipmentTypes[1] = new MainEquipmentType { Name = "Рабочая станция" };

            await context.MainEquipmentTypes.AddRangeAsync(mainEquipmentTypes);
        }

        private Task Init_EquipmentSubTypes()
        {
            throw new NotImplementedException();
        }
    }
}
