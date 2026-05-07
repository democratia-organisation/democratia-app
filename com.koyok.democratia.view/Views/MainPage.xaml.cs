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

        protected async override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            var viewModel = BindingContext as MainViewModel;
            AnimationChargement();
            viewModel!.ConnectCommand.Execute(null);
            var route = viewModel.isConnected ? $"/{nameof(HomePage)}" : $"/{nameof(LoginPage)}";
            await Shell.Current.GoToAsync(route);

        }

        private void AnimationChargement()
        {
            throw new NotImplementedException();
            // TODO : faire une animation de chargement pendant 3 secondes
        }
    }
}