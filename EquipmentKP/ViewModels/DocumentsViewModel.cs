using Equipment.Database.Entities;
using Equipment.Interfaces;
using EquipmentKP.Infrastructure.Commands;
using EquipmentKP.Services.Interfaces;
using EquipmentKP.ViewModels.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace EquipmentKP.ViewModels
{
    class DocumentsViewModel : ViewModelBase
    {
        private readonly IRepository<Document> _DocumentsRep;

        #region ObservableCollection<Document> Documents - Документы
        private ObservableCollection<Document> _Documents;
        public ObservableCollection<Document> Documents
        {
            get => _Documents;
            set
            {
                if (!Set(ref _Documents, value)) return;

                _DocumentsViewSource = new CollectionViewSource { Source = value };

                OnPropertyChanged(nameof(DocumentsView));
                _DocumentsViewSource.View.Refresh();

            }
        }
        #endregion
        #region View & ViewSource Documents - отображение документов

        private CollectionViewSource _DocumentsViewSource;
        public ICollectionView DocumentsView => _DocumentsViewSource?.View;

        #endregion
        #region Document SelectedDocument - выбранный документ
        private Document _SelectedDocument;

        public Document SelectedDocument
        {
            get => _SelectedDocument;
            set => Set(ref _SelectedDocument, value);
        }
        #endregion

        #region LoadDataCommand - Команда загрузки данных из репозитория
        private ICommand _LoadDataCommand = null;
        public ICommand LoadDataCommand => _LoadDataCommand ?? new LambdaCommandAsync(OnLoadDataCommandExecuted);
        private async Task OnLoadDataCommandExecuted()
        {
            Documents = new ObservableCollection<Document>(await _DocumentsRep.Items.ToArrayAsync());
        }
        #endregion

    
        //private ICommand _ShowFileCommand = null;
        //public ICommand ShowFileCommand => _ShowFileCommand ?? new LambdaCommand(OnShowFileCommandExecuted);

        //private bool CanShowFileCommandExecute(object p) => p is Document; 
        //private void OnShowFileCommandExecuted(object p)
        //{
        //    if (p is Document document)
        //    {
        //        string fileName = "tmp.pdf";
        //        string dirPath = Environment.CurrentDirectory;
        //        DirectoryInfo dirInfo = new DirectoryInfo(dirPath);
        //        if (!dirInfo.Exists)
        //            dirInfo.Create();
        //        File.WriteAllBytes(dirPath+fileName, document.Content);

        //        new Process { StartInfo = new ProcessStartInfo(dirPath + fileName) { UseShellExecute = true } }.Start();
        //    }
        //}


        public DocumentsViewModel()
        {
            if (!App.IsDesignTime)
                throw new InvalidOperationException("Данный конструктор не предназначен для использования вне дизайнера VisualStudio");
        }
        public DocumentsViewModel(IRepository<Document> DocumentsRep )
        {
            _DocumentsRep = DocumentsRep;
        }
    }
}
