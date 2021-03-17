using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EquipmentKP.ViewModels
{
    class ViewModelLocator
    {
        public MainViewModel                MainVM                  => App.Services.GetRequiredService<MainViewModel>();
        public EquipmentEditorViewModel     EquipmentEditorVM       => App.Services.GetRequiredService<EquipmentEditorViewModel>();
        public EquipmentsKitEditorViewModel EquipmentsKitEditorVM   => App.Services.GetRequiredService<EquipmentsKitEditorViewModel>();
        public RequestsViewModel            RequestsVM              => App.Services.GetRequiredService<RequestsViewModel>();
    }
}
