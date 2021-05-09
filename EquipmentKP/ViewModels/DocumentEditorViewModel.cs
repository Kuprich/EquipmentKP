using Equipment.Database.Entities;
using EquipmentKP.Infrastructure.Commands;
using EquipmentKP.ViewModels.Base;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace EquipmentKP.ViewModels
{
    class DocumentEditorViewModel : ViewModelBase
    {
        private readonly Document _Document;
        public Document Document
        {
            get => _Document;
        }
        public RequestMovement LastRequestMovement
        {
            get => _Document?.Request?.RequestMovements?.Last();
        }

        #region string Name - наименование документа

        private string _Name;
        public string Name
        {
            get => _Name;
            set => Set(ref _Name, value);
        }

        #endregion

        #region string Number - номер документа

        private string _Number;
        public string Number
        {
            get => _Number;
            set => Set(ref _Number, value);
        }

        #endregion

        #region DateTime CreationDate - дата документа

        private DateTime _CreationDate;
        public DateTime CreationDate
        {
            get => _CreationDate;
            set => Set(ref _CreationDate, value);
        }

        #endregion

        #region byte[] Content  - контент документа

        private byte[] _Content;

        public byte[] Content
        {
            get => _Content;
            set => Set(ref _Content, value);
        }

        #endregion

        #region string FileType - тип документа

        private string _FileType;
        public string FileType
        {
            get => _FileType;
            set => Set(ref _FileType, value);
        }

        #endregion

        #region bool IsAttached  | true - если документ прикреплен

        //private bool _IsAttached;
        public bool IsAttached
        {
            get => Content != null && Content?.Length > 0;
        }

        #endregion

        #region string Title Заголовок окна

        private string _Title = "Документы";

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        #endregion

        #region UploadFileCommand - Команда загрузки документа из файла

        private ICommand _UploadFileCommand = null;
        public ICommand UploadFileCommand => _UploadFileCommand ?? new LambdaCommand(OnUploadFileCommandExecuted);
        private void OnUploadFileCommandExecuted()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                FileType = filePath[filePath.LastIndexOf('.')..];
                Content = File.ReadAllBytes(filePath);
                OnPropertyChanged(nameof(IsAttached));
            }
            
        }
        #endregion

        #region ShowUploadedFileCommand - Команда просмотра прикрепленного документа

        private ICommand _ShowUploadedFileCommand = null;
        public ICommand ShowUploadedFileCommand => _ShowUploadedFileCommand ?? new LambdaCommand(OnShowUploadedFileCommandExecuted, CanShowUploadedFileCommandExecute);
        private bool CanShowUploadedFileCommandExecute() => Content != null && Content?.Length > 0;
        private void OnShowUploadedFileCommandExecuted()
        {
            string fileName = $"tmp{FileType}";
            string dirPath = Environment.CurrentDirectory;
            DirectoryInfo dirInfo = new DirectoryInfo(dirPath);
            if (!dirInfo.Exists)
                dirInfo.Create();
            File.WriteAllBytes(dirPath + fileName, Content);

            new Process { StartInfo = new ProcessStartInfo(dirPath + fileName) { UseShellExecute = true } }.Start();
        }
        #endregion


        public DocumentEditorViewModel(Document document)
        {
            _Document = document;
        }
        public DocumentEditorViewModel()
        {
            if (!App.IsDesignTime)
                throw new InvalidOperationException("Данный конструктор не предназначен для использования вне дизайнера VisualStudio");
        }
    }
}
