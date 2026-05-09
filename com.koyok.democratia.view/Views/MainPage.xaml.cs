using com.koyok.democratia.UI.internaute;

namespace com.koyok.democratia.UI
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }


        protected async override void OnNavigatedTo(NavigatedToEventArgs args)
        {
            base.OnNavigatedTo(args);
            var viewModel = BindingContext as MainViewModel;
            await Task.Delay(3000);
            viewModel!.ConnectCommand.Execute(null);
            var route = viewModel.isConnected ? $"/{nameof(HomePage)}" : $"/{nameof(LoginPage)}";
            await Shell.Current.GoToAsync(route);
        }
        
    }
}