using Equipment.Database.Entities;
using Equipment.Interfaces;
using EquipmentKP.Infrastructure.Commands;
using EquipmentKP.Services.Interfaces;
using EquipmentKP.ViewModels.Base;
using EquipmentKP.Views.Windows;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;

namespace EquipmentKP.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        #region ПОЛЯ И СВОЙСТВА

        #region IRepository<Entity>                             - Репозитории                               |     

        private readonly IRepository<MainEquipment> _EquipmentsRep;
        private readonly IRepository<EquipmentsKit> _EquipmentsKitsRep;
        private readonly IRepository<Request> _RequestsRep;
        private readonly IRepository<Document> _DocumentsRep;
        private readonly IUserDialog _UserDialog;

        #endregion

        #region string Title                                    - Заголовок окна                            |

        private string _Title = "ИАЦ: Движение оборудования";
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        #endregion
        #region String InventoryNo                              - Поле для фильтра                          |

        private String _InventoryNoFilter;

        public String InventoryNoFilter
        {
            get => _InventoryNoFilter;
            set
            {
                if (!Set(ref _InventoryNoFilter, value)) return;

                _EquipmentsViewSource.View.Refresh();
            }
        }

        #endregion

        #region View & ViewSource equipments                    - Просмотр оборудования (А так же фильтр)   |

        private CollectionViewSource _EquipmentsViewSource;
        public ICollectionView EquipmentsView => _EquipmentsViewSource?.View;
        private void EquipmentsViewSource_Filter(object sender, FilterEventArgs e)
        {
            if ( !(e.Item is MainEquipment equipment) || string.IsNullOrEmpty(InventoryNoFilter) ) return;

            if ( !equipment.EquipmentsKit.InventoryNo.Contains(InventoryNoFilter.Trim(), StringComparison.OrdinalIgnoreCase) )
                e.Accepted = false;
        }

        #endregion
        #region ObservableCollection<MainEquipment> Equipments  - Список оборудования                       |

        private ObservableCollection<MainEquipment> _Equipments;
        public ObservableCollection<MainEquipment> Equipments
        {
            get => _Equipments;
            set
            {
                if (!Set(ref _Equipments, value)) return;

                _EquipmentsViewSource = new CollectionViewSource { Source = value };

                OnPropertyChanged(nameof(EquipmentsView));
                _EquipmentsViewSource.View.Refresh();

            }
        }

        #endregion
        #region MainEquipment SelectedEquipment                 - Выбранное оборудование                    |

        private MainEquipment _SelectedEquipment;
        public MainEquipment SelectedEquipment
        {
            get => _SelectedEquipment;
            set
            {
                if (!Set(ref _SelectedEquipment, value)) return;

                Requests = new ObservableCollection<Request>(SelectedEquipment.Requests);
                OnPropertyChanged(nameof(Requests));
            }
        }

        #endregion

        #region View & ViewSource Requests                      - Просмотр заявок                           | 

        private CollectionViewSource _RequestsViewSource;
        public ICollectionView RequestsView => _RequestsViewSource?.View;

        #endregion
        #region ObservableCollection<Request> Requests          - Список заявок                             | 

        private ObservableCollection<Request> _Requests;

        public ObservableCollection<Request> Requests
        {
            get => _Requests;
            set
            {
                if (!Set(ref _Requests, value)) return;

                _RequestsViewSource = new CollectionViewSource { Source = value };

                OnPropertyChanged(nameof(RequestsView));
                _RequestsViewSource.View.Refresh();
            }
        }

        #endregion
        #region Request SelectedRequest                         - Выбранная заявка                          |

        private Request _SelectedRequest;

        public Request SelectedRequest
        {
            get => _SelectedRequest;
            set
            {
                if (!Set(ref _SelectedRequest, value)) return;

                if (SelectedRequest is null)
                    Documents = new ObservableCollection<Document>();
                else 
                    Documents = new ObservableCollection<Document>(SelectedRequest.Documents);
            }

        }

        #endregion

        #region View & ViewSource Documents                     - Просмотр документов                       | 

        private CollectionViewSource _DocumentsViewSource;
        public ICollectionView DocumentsView => _DocumentsViewSource?.View;

        #endregion
        #region ObservableCollection<Document> Documents        - Список документов                         |

        private ObservableCollection<Document> _Documents;

        public ObservableCollection<Document> Documents
        {
            get => _Documents;
            set
            {
                if (!Set(ref _Documents, value)) return;

                _DocumentsViewSource = new CollectionViewSource { Source = value };

                OnPropertyChanged(nameof(DocumentsView));
                _RequestsViewSource.View.Refresh();
            }

        }

        #endregion
        #region Document SelectedDocument                       - Выбранный документ                        | 

        private Document _SelectedDocument;
        public Document SelectedDocument
        {
            get => _SelectedDocument;
            set => Set(ref _SelectedDocument, value);
        }

        #endregion

        #endregion

        #region КОМАНДЫ

        #region CloseAplicationCommand      - Закрытие окна                                 |

        private ICommand _CloseAplicationCommand = null;
        public ICommand CloseAplicationCommand => _CloseAplicationCommand ??= new LambdaCommand(OnCloseAplicationCommandExecuted);
        //private bool CanCloseAplicationCommandExecute() => true; // если этого параметра нет, то всегда разрешено выполнение данной команды
        private void OnCloseAplicationCommandExecuted()
        {
            Application.Current.Shutdown();
        }

        #endregion
        #region LoadDataCommand             - Загрузка данных из репозитория                | 

        private ICommand _LoadDataCommand = null;
        public ICommand LoadDataCommand => _LoadDataCommand ?? new LambdaCommandAsync(OnLoadDataCommandExecuted);
        private async Task OnLoadDataCommandExecuted()
        {
            Equipments = new ObservableCollection<MainEquipment>(await _EquipmentsRep.Items.ToArrayAsync());


            _EquipmentsViewSource.Filter += EquipmentsViewSource_Filter;
            EquipmentsView.GroupDescriptions.Add(new PropertyGroupDescription($"{nameof(EquipmentsKit)}.{nameof(EquipmentsKit.InventoryNo)}"));
        }

        #endregion

        #region GroupingCammand             - Группировка по инв. номеру (тестовая команда) |

        private ICommand _GroupingCommand = null;
        public ICommand GroupingCammand => _GroupingCommand ?? new LambdaCommand(OnGroupingCammandExecute);
        private void OnGroupingCammandExecute()
        {
            EquipmentsView.GroupDescriptions.Clear();
            EquipmentsView.GroupDescriptions.Add(new PropertyGroupDescription($"{nameof(EquipmentsKit)}.{nameof(EquipmentsKit.InventoryNo)}"));
        }

        #endregion
        #region UnGroupingCammand           - Разгруппировка таблицы "оборудование"         |

        private ICommand _UnGroupingCommand = null;
        public ICommand UnGroupingCammand => _UnGroupingCommand ?? new LambdaCommand(OnUnGroupingCammandExecute);
        private void OnUnGroupingCammandExecute()
        {
            EquipmentsView.GroupDescriptions.Clear();
        }

        #endregion

        #region AddEquipmentsKitCommand     - Добавление комплекта оборудования             |

        private ICommand _AddEquipmentsKitCommand = null;
        public ICommand AddEquipmentsKitCommand => _AddEquipmentsKitCommand ?? new LambdaCommand(OnAddEquipmentsKitCommandExecuted);
        private void OnAddEquipmentsKitCommandExecuted()
        {
            var equipmentsKit = new EquipmentsKit();
            _EquipmentsKitsRep.Add(equipmentsKit);

            if (_UserDialog.Edit(equipmentsKit))
            {
                _EquipmentsKitsRep.Update(equipmentsKit);

                //_ = OnLoadDataCommandExecuted();

                OnPropertyChanged(nameof(SelectedEquipment));

                _EquipmentsViewSource.View.Refresh();
            }
            else
                _EquipmentsKitsRep.Remove(equipmentsKit);
        }

        #endregion
        #region EditEquipmentsKitCommand    - Редактирование комплекта оборудования         | 

        private ICommand _EditEquipmentsKitCommand = null;
        public ICommand EditEquipmentsKitCommand => _EditEquipmentsKitCommand ?? new LambdaCommand(OnEditEquipmentsKitCommandExecuted);
        private bool CanEditEquipmentsKitCommandExecute(object p) => p is EquipmentsKit;
        private void OnEditEquipmentsKitCommandExecuted(object p)
        {
            var equipmentsKit = (EquipmentsKit)p;

            if (_UserDialog.Edit(equipmentsKit))
            {
                _EquipmentsKitsRep.Update(equipmentsKit);

                _ = OnLoadDataCommandExecuted();

                OnPropertyChanged(nameof(SelectedEquipment));

                _EquipmentsViewSource.View.Refresh();
            }
        }

        #endregion

        #region AddEquipmentCommand         - Добавление оборудования                       | 

        private ICommand _AddEquipmentCommand = null;
        public ICommand AddEquipmentCommand => _AddEquipmentCommand ?? new LambdaCommand(OnAddEquipmentCommandExecuted, CanAddEquipmentCommandExecute);
        private bool CanAddEquipmentCommandExecute(object p) => p is MainEquipment;
        private void OnAddEquipmentCommandExecuted(object p)
        {
            var equipment = new MainEquipment
            {
                EquipmentsKit = SelectedEquipment.EquipmentsKit
            };

            _EquipmentsRep.Add(equipment);

            if (_UserDialog.Edit(equipment))
            {
                _EquipmentsRep.Update(equipment);

                _ = OnLoadDataCommandExecuted();

                OnPropertyChanged(nameof(SelectedEquipment));

                _EquipmentsViewSource.View.Refresh();
            }
            else
            {
                _EquipmentsRep.Remove(equipment);
            }
        }

        #endregion
        #region EditEquipmentCommand        - Редактирование оборудования                   |

        private ICommand _EditEquipmentCommand = null;
        public ICommand EditEquipmentCommand => _EditEquipmentCommand ?? new LambdaCommand(OnEditEquipmentCommandExecuted, CanEditEquipmentCommandExecute);
        private bool CanEditEquipmentCommandExecute(object p) => p is MainEquipment;
        private void OnEditEquipmentCommandExecuted(object p)
        {
            var equipment = (MainEquipment)p;

            if (_UserDialog.Edit(equipment))
            {
                _EquipmentsRep.Update(equipment);

                OnPropertyChanged(nameof(SelectedEquipment));

                _EquipmentsViewSource.View.Refresh();
            }
        }

        #endregion

        #region AddRequestCommand           - Добавление заявки к текущему оборудованию     |   

        private ICommand _AddRequestCommand = null;
        public ICommand AddRequestCommand => _AddRequestCommand ?? new LambdaCommand(OnAddRequestCommandExecuted, CanAddRequestCommandExecute);
        private bool CanAddRequestCommandExecute(object p) => p is MainEquipment;
        private void OnAddRequestCommandExecuted(object p)
        {
            var request = new Request { MainEquipment = SelectedEquipment };

            _RequestsRep.Add(request);

            if (_UserDialog.Edit(request))
            {
                _RequestsRep.Update(request);
                Requests.Add(request);
                _RequestsViewSource.View.Refresh();
            }
            else
                _RequestsRep.Remove(request);
        }

        #endregion
        #region EditRequestCommand          - Редактирование заявки по текущему оборудованию|

        private ICommand _EditRequestCommand = null;
        public ICommand EditRequestCommand => _EditRequestCommand ?? new LambdaCommand(OnEditRequestCommandExecuted, CanEditRequestCommandExecute);
        private bool CanEditRequestCommandExecute(object p) => p is Request;
        private void OnEditRequestCommandExecuted(object p)
        {
            var request = (Request)p;

            if (_UserDialog.Edit(request))
            {
                _RequestsRep.Update(request);
                _RequestsViewSource.View.Refresh();
            }
        }

        #endregion

        #region ShowRequestsWindow          - Показать окно "Заявки"                        | 

        private ICommand _ShowRequestsWindow = null;
        public ICommand ShowRequestsWindow => _ShowRequestsWindow ?? new LambdaCommand(OnShowRequestsWindowExecuted);
        private void OnShowRequestsWindowExecuted()
        {
            var viewModel = new RequestsViewModel(_RequestsRep);
            var window = new RequestsWindow
            {
                DataContext = viewModel,
                Owner = App.CurrentWindow,
            };
            window.ShowDialog();
        }

        #endregion
        #region ShowDocumentsWindow         - Показать окно "Документы"                     | 

        private ICommand _ShowDocumentsWindow = null;
        public ICommand ShowDocumentsWindow => _ShowDocumentsWindow ?? new LambdaCommand(OnShowDocumentsWindowExecuted);
        private void OnShowDocumentsWindowExecuted()
        {
            var viewModel = new DocumentsViewModel(_DocumentsRep);
            var window = new DocumentsWindow
            {
                DataContext = viewModel,
                Owner = App.CurrentWindow,
            };
            window.ShowDialog();
        }

        #endregion

        #endregion

        public MainViewModel(
            IRepository<MainEquipment> EquipmentsRep,
            IRepository<EquipmentsKit> EquipmentsKitsRep,
            IRepository<Request> RequestsRep,
            IRepository<Document> DocumentsRep,
            IUserDialog UserDialog
            )
        {
            _EquipmentsRep = EquipmentsRep;
            _EquipmentsKitsRep = EquipmentsKitsRep;
            _RequestsRep = RequestsRep;
            _DocumentsRep = DocumentsRep;
            _UserDialog = UserDialog;
        }

        public MainViewModel()
        {
            if (!App.IsDesignTime)
                throw new InvalidOperationException("Данный конструктор не предназначен для использования вне дизайнера VisualStudio");
        }
    }
}
