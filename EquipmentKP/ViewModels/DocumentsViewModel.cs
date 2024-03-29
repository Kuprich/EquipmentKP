﻿using Equipment.Database.Entities;
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
        // ПОЛЯ И СВОЙСТВА
        private readonly IRepository<Document> _DocumentsRep;
        private IUserDialog _UserDialog;

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

        // КОМАНДЫ
        #region LoadDataCommand - Команда загрузки данных из репозитория

        private ICommand _LoadDataCommand = null;
        public ICommand LoadDataCommand => _LoadDataCommand ?? new LambdaCommandAsync(OnLoadDataCommandExecuted);
        private async Task OnLoadDataCommandExecuted()
        {
            Documents = new ObservableCollection<Document>(await _DocumentsRep.Items.ToArrayAsync());
        }

        #endregion

        #region AddDocumentCommand - Добавить документ

        private ICommand _AddDocumentCommand = null;
       
        public ICommand AddDocumentCommand => _AddDocumentCommand ?? new LambdaCommand(OnAddDocumentCommandExecuted);
        private void OnAddDocumentCommandExecuted()
        {

            var document = new Document();

            Documents.Add(document);

            using var scope = App.Host.Services.CreateScope();
            var _UserDialog = scope.ServiceProvider.GetRequiredService<IUserDialog>();

            if (_UserDialog.Edit(document))
            {
                Documents[Documents.IndexOf(document)] = document;
                _DocumentsViewSource?.View.Refresh();
            }

            else Documents.Remove(document);
        }

        #endregion
        #region EditDocumentCommand - Добавить документ

        private ICommand _EditDocumentCommand = null;
        public ICommand EditDocumentCommand => _EditDocumentCommand ?? new LambdaCommand(OnEditDocumentCommandExecuted, CanEditDocumentCommandExecute);
        private bool CanEditDocumentCommandExecute(object p) => p is Document && p != null;
        private void OnEditDocumentCommandExecuted(object p)
        {
            using var scope = App.Host.Services.CreateScope();
            var _UserDialog = scope.ServiceProvider.GetRequiredService<IUserDialog>();

            var document = (Document)p;

            if (_UserDialog.Edit(document))
            {
                Documents[Documents.IndexOf(document)] = document;
                _DocumentsViewSource?.View.Refresh();
            }
        }

        #endregion


        public DocumentsViewModel()
        {
            if (!App.IsDesignTime)
                throw new InvalidOperationException("Данный конструктор не предназначен для использования вне дизайнера VisualStudio");
        }
        public DocumentsViewModel(IRepository<Document> DocumentsRep )
        {
            _DocumentsRep = DocumentsRep;

            using var scope = App.Host.Services.CreateScope();
            _UserDialog = scope.ServiceProvider.GetRequiredService<IUserDialog>();
        }
    }
}
