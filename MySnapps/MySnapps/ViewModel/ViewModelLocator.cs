/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:MySnapps"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using MySnapps.MVVM.ViewModel.Concrete;

namespace MySnapps.ViewModel
{

    public class ViewModelLocator
    {
        private static UrlViewModel UrlViewModel = new UrlViewModel();
        private static ProcessViewModel ProcessViewModel = new ProcessViewModel(UrlViewModel.Urls);

        public static MainViewModel MainViewModel
        {
            get
            {
                return new MainViewModel();
            }
        }

     }
}