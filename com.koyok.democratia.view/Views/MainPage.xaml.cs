using com.koyok.democratia.ViewModels.internaute;

namespace com.koyok.democratia.Views
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