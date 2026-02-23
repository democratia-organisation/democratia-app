using com.democratia.ViewModels.internaute;
using com.democratia.ViewModels;

namespace com.democratia.Views
{
    public partial class MainPage : ContentPage
    {

        public MainPage(IEnumerable<INavigeablleViewModel?>? navigeablleViewModels)
        {
            InitializeComponent();
            MainViewModel? viewModel = navigeablleViewModels!.OfType<MainViewModel>().FirstOrDefault();
            BindingContext = viewModel!;
        }
    }

}