﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EquipmentKP.ViewModels
{
    static class ViewModelsRegistrator
    {
        public static IServiceCollection AddViewModels(this IServiceCollection services) => services
            .AddSingleton<MainViewModel>()
            .AddTransient<EquipmentEditorViewModel>()
            .AddTransient<EquipmentsKitEditorViewModel>()
            .AddTransient<RequestsViewModel>()
            .AddTransient<RequestEditorViewModel>()
            .AddTransient<DocumentsViewModel>()
            ;
    }
}
