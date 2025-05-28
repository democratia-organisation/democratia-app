namespace com.democratia.Views;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        viewModel.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == nameof(viewModel.ErrorMessage) && !string.IsNullOrEmpty(viewModel.ErrorMessage))
            {
                DisplayAlert("Erreur", viewModel.ErrorMessage, "OK");
                viewModel.ErrorMessage = null;
            }
        };
    }
}
