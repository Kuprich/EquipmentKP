using Equipment.Database.Entities;
using Equipment.Database.Repositories.Base;
using Equipment.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Equipment.Database.Repositories
{
    public static class RepositoriesRegistrator
    {
        public static IServiceCollection AddDbRepositories(this IServiceCollection services) => services
            .AddSingleton<IRepository<SubEquipmentType>, DbRepositoryBase<SubEquipmentType>>()
            .AddSingleton<IRepository<MainEquipmentType>, DbRepositoryBase<MainEquipmentType>>()
            .AddSingleton<IRepository<Location>, DbRepositoryBase<Location>>()
            .AddSingleton<IRepository<MainEquipment>, DbRepositoryBase<MainEquipment>>()
            .AddSingleton<IRepository<SubEquipment>, DbRepositoryBase<SubEquipment>>()
            ;

    }
}
