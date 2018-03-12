using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MySnapps.MVVM.Model;
using MySnapps.ViewModel;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

namespace MySnapps
{
    public partial class MainWindow
    {
        public MainViewModel MainModel { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            MainModel = new MainViewModel();
        }

        #region UrlTextBox Events

        #region TextChanged
        private void urlTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshListView(UrlTextBox.Text);
        }
        private void RefreshListView(string urlContentUpdated)
        {
            if (UrlListView.SelectedItem != null)
            {
                Url lvc = (Url)UrlListView.SelectedItem;
                if (!MainModel.UrlViewModel.StopRefreshControls)
                {
                    MainModel.UrlViewModel.SetDataChanged(true);
                    MainModel.UrlViewModel.AddRow();
                    MainModel.UrlViewModel.Urls[UrlListView.SelectedIndex].Link = urlContentUpdated;
                    lvc.Link = urlContentUpdated;
                    UrlListView.SelectedValue = lvc;
                    IsDataChangeLabel.Content = "true";
                    UrlListView.Items.Refresh();
                }
            }
        }
        #endregion

        #region KeyDown
        private void urlTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                MainModel.UrlViewModel.RestoreOldValue(sender);
            }
        }
        #endregion

        #region Focus
        private void urlTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            MainModel.UrlViewModel.StoreCurrentValue(sender);
            UrlTextBox.SelectAll();
        }
        #endregion

        #endregion
    }
}
