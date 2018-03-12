using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using MySnapps.Generator;
using MySnapps.MVVM.Commands;
using MySnapps.MVVM.Model;
using MySnapps.Scheduler;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;

namespace MySnapps.MVVM.ViewModel.Concrete
{
    public class ProcessViewModel : INotifyPropertyChanged
    {
        #region Props

        #region StatusVisibility
        private Visibility _statusVisibility;
        public Visibility StatusVisibility
        {
            get { return _statusVisibility; }
            set
            {
                _statusVisibility = value;
                RaisePropertyChanged("StatusVisibility");
            }
        }
        #endregion

        #region OutputFolder
        private string _outputFolder = "C:/";
        public string OutputFolder
        {
            get { return _outputFolder; }
            set
            {
                _outputFolder = value;
                RaisePropertyChanged("OutputFolder");
            }
        }
        #endregion

        #region PDFChecked
        private bool _pdfChecked = true;
        public bool PDFChecked
        {
            get { return _pdfChecked; }
            set
            {
                _pdfChecked = value;
                RaisePropertyChanged("PDFChecked");
            }
        }
        #endregion

        #region JPEGChecked
        private bool _jpegChecked;
        public bool JPEGChecked
        {
            get { return _jpegChecked; }
            set
            {
                _jpegChecked = value;
                RaisePropertyChanged("JPEGChecked");
            }
        }
        #endregion

        #region DesktopViewChecked
        private bool _desktopViewChecked = true;
        public bool DesktopViewChecked
        {
            get { return _desktopViewChecked; }
            set
            {
                _desktopViewChecked = value;
                RaisePropertyChanged("DesktopViewChecked");
            }
        }
        #endregion

        #region MobileViewChecked
        private bool _mobileViewChecked;
        public bool MobileViewChecked
        {
            get { return _mobileViewChecked; }
            set
            {
                _mobileViewChecked = value;
                RaisePropertyChanged("MobileViewChecked");
            }
        }
        #endregion

        #region StatusProgress
        private int _statusProgress;
        public int StatusProgress
        {
            get { return _statusProgress; }
            set
            {
                _statusProgress = value;
                RaisePropertyChanged("StatusProgress");
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

        #endregion

        #region Fields

        private bool _stopRefreshControls;
        private bool _dataChanged;
        private readonly BackgroundWorker _mOWorker;
        private CancellationTokenSource _tokenSource;
        List<string> _itemList;
        string _rTargetContent;
        string _rViewTargetContent;
        string[] _isMobile = null;
        public ObservableCollection<Url> Urls;

        #endregion

        public ProcessViewModel(ObservableCollection<Url> urls = null)
        {
            if (urls == null)
            {
                Urls = new ObservableCollection<Url>();
            }
            else
            {
                Urls = urls;
            }
            StatusVisibility = Visibility.Hidden;
            _mOWorker = new BackgroundWorker();
            _mOWorker.DoWork += m_oWorker_DoWork;
            _mOWorker.ProgressChanged += m_oWorker_ProgressChanged;
            _mOWorker.RunWorkerCompleted += m_oWorker_RunWorkerCompleted;
            _mOWorker.WorkerReportsProgress = true;
            _mOWorker.WorkerSupportsCancellation = true;
        }

        #region Process Work
        private void m_oWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Work Done");
            StatusVisibility = Visibility.Hidden;
            StatusProgress = 0;
        }

        private void m_oWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            StatusProgress = e.ProgressPercentage;
        }

        private void m_oWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            StatusVisibility = Visibility.Visible;
            if (_mOWorker.CancellationPending)
            {
                e.Cancel = true;
                _mOWorker.ReportProgress(0);
                Application.Current.Dispatcher.Invoke(() =>
                {
                    StatusProgress = 0;
                    StatusVisibility = Visibility.Visible;
                });
                return;
            }
            Application.Current.Dispatcher.Invoke(() =>
            {
                _statusVisibility = Visibility.Visible;
                _rTargetContent = _pdfChecked ? "PDF" : "JPEG";
                _rViewTargetContent = _desktopViewChecked ? "DesktopView" : "MobileView";
                if (_rTargetContent == "PDF" && _rViewTargetContent != "MobileView")
                {
                    _isMobile = new[] { "--page-size A4 --viewport-size 1280x1024 --disable-smart-shrinking" };
                }
                _itemList = (from Url lvc in Urls where lvc.Link != "" select lvc.Link).ToList();
            });
            var indexer = 1;
            foreach (var item in _itemList)
            {
                int percentage;
                var generatorFactory = new GeneratorFactory();
                var generatorType = (GeneratorType)Enum.Parse(typeof(GeneratorType), _rTargetContent);
                var generator = generatorFactory.GetGenerator(generatorType);
                _tokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(10));
                var token = _tokenSource.Token;
                var QueryTimeOut = 5000;
                var task = Task.Factory.StartNew(() =>
                {
                    generator.GenerateDocument(_outputFolder, "Tx", new[] { item }, _isMobile);
                    percentage = indexer * 100 / _itemList.Count;
                    _mOWorker.ReportProgress(percentage);
                    indexer += 1;

                }, token);
                if (!task.Wait(QueryTimeOut, token))
                {
                    MessageBox.Show($"Url is taking more than 5 seconds to Load -- Please Check the url: {item}");
                    _tokenSource.Cancel();
                }
                if (task.IsCanceled)
                {
                    MessageBox.Show($"Url not Valid: {item}");
                }
            }
            _mOWorker.ReportProgress(100);
        }
        #endregion

        #region Scheduled Task

        #region Props

        #region TimePicker
        private DateTime? _timePicker;
        public DateTime? TimePicker
        {
            get { return _timePicker; }
            set
            {
                _timePicker = value;
                RaisePropertyChanged("TimePicker");
            }
        }
        #endregion

        #region RunEveryDayCheckBox
        private bool _runEveryDayCheckBox;
        public bool RunEveryDayCheckBox
        {
            get { return _runEveryDayCheckBox; }
            set
            {
                _runEveryDayCheckBox = value;
                RaisePropertyChanged("RunEveryDayCheckBox");
            }
        }
        #endregion

        #endregion

        #region Commands

        #region CheckScheduleButton
        private ICommand _checkScheduleCommand;
        public ICommand CheckSchedule
        {
            get
            {
                if (_checkScheduleCommand == null)
                {
                    _checkScheduleCommand = new RelayCommand(
                        param => CheckScheduleTask(),
                        null
                    );
                }
                return _checkScheduleCommand;
            }
        }
        private void CheckScheduleTask()
        {
            if (_runEveryDayCheckBox != true) return;
            if (_timePicker == null) return;
            var trigger = new DailyTrigger(_timePicker.Value.Hour, _timePicker.Value.Minute);
            trigger.OnTimeTriggered += () =>
            {
                MessageBox.Show("Task Scheduled to Run EveryDay at : " + _timePicker.Value.Hour + ":" + _timePicker.Value.Minute);
                _statusProgress = 10;
                try
                {
                    _mOWorker.RunWorkerAsync();
                }
                catch (Exception)
                {
                    MessageBox.Show("Problem with Url Data");
                }
            };
        }
        #endregion

        #endregion

        #endregion

        #region Action Buttons

        #region StartButton
        private ICommand _startButton;
        public ICommand StartButton
        {
            get
            {
                if (_startButton == null)
                {
                    _startButton = new RelayCommand(
                        param => StartButtonClick(),
                        null
                    );
                }
                return _startButton;
            }
        }
        private void StartButtonClick()
        {
            _statusProgress = 10;
            try
            {
                _mOWorker.RunWorkerAsync();
            }
            catch (Exception)
            {
                MessageBox.Show("Problem with Url Data");
            }

            CheckScheduleTask();
        }
        #endregion

        #region StopButton
        private ICommand _stopButton;
        public ICommand StopButton
        {
            get
            {
                if (_stopButton == null)
                {
                    _stopButton = new RelayCommand(
                        param => StopButtonClick(),
                        null
                    );
                }
                return _stopButton;
            }
        }
        private void StopButtonClick()
        {
            _tokenSource?.Cancel();

            if (_mOWorker.IsBusy)
            {
                _mOWorker.CancelAsync();
            }
        }
        #endregion

        #endregion

        #region Destination 

        #region BrowseButton
        private ICommand _browseButton;
        public ICommand BrowseButton
        {
            get
            {
                if (_browseButton == null)
                {
                    _browseButton = new RelayCommand(
                        param => BrowseButtonClick(),
                        null
                    );
                }
                return _browseButton;
            }
        }
        private void BrowseButtonClick()
        {
            var folderDialog = new FolderBrowserDialog
            {
                ShowNewFolderButton = false,
                SelectedPath = AppDomain.CurrentDomain.BaseDirectory
            };
            var result = folderDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                var sPath = folderDialog.SelectedPath;
                OutputFolder = sPath;
                var folder = new DirectoryInfo(sPath);
                if (!folder.Exists) return;
                foreach (FileInfo fileInfo in folder.GetFiles())
                {
                    var sDate = fileInfo.CreationTime.ToString("yyyy-MM-dd");
                    Debug.WriteLine("#Debug: File: " + fileInfo.Name + " Date:" + sDate);
                }
            }
        }
        #endregion

        #endregion

    }
}
