using GalaSoft.MvvmLight;
using MySnapps.MVVM.Model;
using MySnapps.MVVM.ViewModel.Concrete;
using System.Collections.ObjectModel;

namespace MySnapps.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        public ProcessViewModel ProcessViewModel { get; set; }
        public UrlViewModel UrlViewModel { get; set; }
        public ObservableCollection<Url> Items { get; set; }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            UrlViewModel = new UrlViewModel();
            Items = UrlViewModel.Urls;
            ProcessViewModel = new ProcessViewModel(Items);
        }
    }
}