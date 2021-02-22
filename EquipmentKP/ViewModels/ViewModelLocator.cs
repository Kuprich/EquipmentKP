using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EquipmentKP.ViewModels
{
    class ViewModelLocator
    {
        public MainWindowViewModel MainVindovVM => App.Services.GetRequiredService<MainWindowViewModel>();
    }
}
