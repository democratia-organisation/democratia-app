namespace com.koyok.democratia.UI.internaute
{
    public partial class LoginPage : ContentPage
    {

        public LoginPage(LoginViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel!;
        }
    }
}