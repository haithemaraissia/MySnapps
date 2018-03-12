using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using MySnapps.Data;
using MySnapps.MVVM.Model;
using RelayCommand = MySnapps.MVVM.Commands.RelayCommand;

namespace MySnapps.MVVM.ViewModel.Concrete
{
    public class UrlViewModel : INotifyPropertyChanged
    {
        #region Props

        #region UrlSelectedIndex

        private int _urlSelectedIndex;
        public int UrlSelectedIndex
        {
            get { return _urlSelectedIndex; }
            set
            {
                _urlSelectedIndex = value;
                RaisePropertyChanged("UrlSelectedIndex");
            }
        }

        #endregion

        #region UrlSelectedItem

        private Url _urlSelectedItem;
        public Url UrlSelectedItem
        {
            get { return _urlSelectedItem; }
            set
            {
                _urlSelectedItem = value;
                RaisePropertyChanged("UrlSelectedItem");
            }
        }

        #endregion

        #region UrlContent

        private string _urlContent;
        public string UrlContent
        {
            get { return _urlContent; }
            set
            {
                _urlContent = value;
                RaisePropertyChanged("UrlContent");
            }
        }

        #endregion

        #region SaveButtonEnabled

        private bool _saveButtonEnabled;
        public bool SaveButtonEnabled
        {
            get { return _saveButtonEnabled; }
            set
            {
                _saveButtonEnabled = value;
                RaisePropertyChanged("SaveButtonEnabled");
            }
        }

        #endregion

        #region OkButtonEnabled

        private bool _okButtonEnabled;
        public bool OkButtonEnabled
        {
            get { return _okButtonEnabled; }
            set
            {
                _okButtonEnabled = value;
                RaisePropertyChanged("OkButtonEnabled");
            }
        }

        #endregion

        #region UrlTextBoxFocus
        private bool _isUrlTextFocused = false;
        public bool IsUrlTextFocused
        {
            get
            {
                return _isUrlTextFocused;
            }
            set
            {
                if (_isUrlTextFocused == value)
                {
                    _isUrlTextFocused = false;
                    RaisePropertyChanged("IsUrlTextFocused");
                }
                _isUrlTextFocused = value;
                RaisePropertyChanged("IsUrlTextFocused");
            }
        }
        #endregion

        #region StopRefreshControls

        private bool _stopRefreshControls;
        public bool StopRefreshControls
        {
            get
            {
                return _stopRefreshControls;
            }
            set
            {
                _stopRefreshControls = value;
            }
        }
        #endregion

        #region PropertyChange
        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region DataChanged

        private bool _dataChanged;
        public bool DataChanged
        {
            get { return _dataChanged; }
            set
            {
                _dataChanged = value;
            }
        }

        #endregion

        #region Urls

        public ObservableCollection<Url> _urls;
        public ObservableCollection<Url> Urls
        {
            get
            {
                if (_urls == null)
                    _urls = new ObservableCollection<Url>();
                return _urls;
            }
            set
            {
                RaisePropertyChanged("Urls");
                Urls = value;
            }
        }
        
        #endregion

        #endregion

        public UrlViewModel()
        {
            _urls = new ObservableCollection<Url>();
            WindowLoaded();
        }

        #region Data ORM
        public void AddRow()
        {
            _urls.Add(new Url { Link = "" });
            UrlSelectedIndex = _urls.Count - 1;
            UrlContent = "";
            _isUrlTextFocused = true;
        }

        private void ShowData()
        {
            MyData md = new MyData();
            _urls.Clear();
            foreach (var row in md.GetRows())
            {
                if (row != null)
                {
                    string r = ((ListViewData)(row)).Col1;
                    _urls.Add(new Url { Link = r });
                }
            }
        }

        public void SetDataChanged(bool value)
        {
            _dataChanged = value;
            _saveButtonEnabled = value;
        }

        public void RestoreOldValue(object sender)
        {
            TextBox myText = (TextBox)sender;
            if (myText.Text != myText.Tag.ToString())
            {
                myText.Text = myText.Tag.ToString();
                myText.SelectAll();
            }
        }

        public void StoreCurrentValue(object sender)
        {
            TextBox myText = (TextBox)sender;
            myText.Tag = myText.Text;
        }

        public void SaveData()
        {
            MyData md = new MyData();
            md.Save(new CollectionView(_urls.ToList()));
            SetDataChanged(false);
        }
        #endregion

        #region Action Buttons

        #region AddButton
        private ICommand _addUrlCommand;
        public ICommand AddUrlCommand
        {
            get
            {
                if (_addUrlCommand == null)
                {
                    _addUrlCommand = new RelayCommand(
                        param => AddUrl(),
                        null
                    );
                }
                return _addUrlCommand;
            }
        }
        private void AddUrl()
        {
            SetDataChanged(true);
            AddRow();
        }
        #endregion

        #region RemoveButton
        private ICommand _removeUrlCommand;
        public ICommand RemoveUrlCommand
        {
            get
            {
                if (_removeUrlCommand == null)
                {
                    _removeUrlCommand = new RelayCommand(
                        param => RemoveUrl(),
                        null
                    );
                }
                return _removeUrlCommand;
            }
        }
        public void RemoveUrl()
        {
            SetDataChanged(true);
            int selectedIndex = _urlSelectedIndex;
            _urls.Remove(_urls.FirstOrDefault(x => x.Link == _urlSelectedItem.Link));
            if (_urls.Count == 0)
            {
                AddRow();
            }
            else if (selectedIndex <= _urls.Count - 1)
            {
                _urlSelectedIndex = selectedIndex;
            }
            else
            {
                _urlSelectedIndex = _urls.Count - 1;
            }
        }

        #endregion

        #region SaveButton
        private ICommand _saveUrlListCommand;

        public ICommand SaveUrlListCommand
        {
            get
            {
                if (_saveUrlListCommand == null)
                {
                    _saveUrlListCommand = new RelayCommand(
                        param => SaveUrls(),
                        null
                    );
                }
                return _saveUrlListCommand;
            }
        }
        private void SaveUrls()
        {
            SaveButtonEnabled = false;

            MyData md = new MyData();
            md.Save(new CollectionView(_urls.ToList()));
            SetDataChanged(false);

            SaveButtonEnabled = true;
        }

        #endregion

        #region OkButton
        private ICommand _oKCommand;

        public ICommand OkButton
        {
            get
            {
                if (_oKCommand == null)
                {
                    _oKCommand = new RelayCommand(
                        param => SaveAndCloseButton(),
                        null
                    );
                }
                return _oKCommand;
            }
        }
        private void SaveAndCloseButton()
        {
            OkButtonEnabled = false;
            if (_dataChanged)
            {
                MyData md = new MyData();
                md.Save(new CollectionView(_urls.ToList()));
                SetDataChanged(false);
            }
            if (Application.Current.MainWindow != null)
                Application.Current.MainWindow.Close();
        }
        #endregion

        #region ListBox
        #region SelectionChanged

        private ICommand _selectionChanged;

        public ICommand SelectionChanged
        {
            get
            {
                if (_selectionChanged == null)
                {
                    _selectionChanged = new RelayCommand(
                        param => UrlSelectionChanged(),
                        null
                    );
                }
                return _selectionChanged;
            }
        }
        private void UrlSelectionChanged()
        {
            if (UrlSelectedItem != null)
            //if (UrlSelectedIndex != -1)

            {
                _stopRefreshControls = true;
                var selectedUrl = _urls.FirstOrDefault(x => x.Link == UrlSelectedItem.Link)?.Link;
                if (selectedUrl == null) return;
                UrlContent = selectedUrl;
                _stopRefreshControls = false;
            }
        }
        #endregion
        #endregion

        #region CloseButton
        private ICommand _closeButton;

        public ICommand CloseButton
        {
            get
            {
                if (_closeButton == null)
                {
                    _closeButton = new RelayCommand(
                        param => CloseWindow(),
                        null
                    );
                }
                return _closeButton;
            }
        }

        private void CloseWindow()
        {
            if (Application.Current.MainWindow != null)
                Application.Current.MainWindow.Close();
        }
        #endregion

        #endregion

        #region Window Event

        #region WindowLoaded
        public void WindowLoaded()
        {
            ShowData();

            if (_urls.Count == 0)
            {
                AddRow();
            }
            else
            {
                UrlSelectedIndex = 0;
            }
            SetDataChanged(false);
            _isUrlTextFocused = true;
        }
        
        #endregion
        
        #region WindowClosing
        private ICommand _windowClosing;

        public ICommand WindowClosing
        {
            get
            {
                if (_windowClosing == null)
                {
                    _windowClosing = new RelayCommand<CancelEventArgs>(e =>
                        ClosingWindow(e),
                        null
                    );
                }
                return _windowClosing;
            }
        }

        private void ClosingWindow(CancelEventArgs e)
        {
            if (DataChanged)
            {
                string message = "Your changes are not saved. Do you want to save it now?";
                MessageBoxResult result = MessageBox.Show(message, "", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    SaveData();
                }
                else if (result == MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                }
                else if (result == MessageBoxResult.No)
                {
                    // do nothing
                }
            }
        }
        #endregion


        #endregion
    }
}
