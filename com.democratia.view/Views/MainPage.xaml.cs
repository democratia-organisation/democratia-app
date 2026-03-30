using com.democratia.ViewModels.internaute;
using com.democratia.ViewModels;

namespace com.democratia.Views
{
    public partial class MainPage : ContentPage
    {

        public MainPage(MainViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel!;
        }
    }

}