using com.democratia.ViewModels;

namespace com.democratia.Views;

public partial class MainPage : ContentPage
{
    

    public MainPage(IEnumerable<INavigeablleViewModel?>? navigeablleViewModels)
    {
        InitializeComponent();
        MainPageViewModel? viewModel = navigeablleViewModels!.OfType<MainPageViewModel>().FirstOrDefault();
        BindingContext = viewModel ?? throw new ArgumentNullException(nameof(viewModel), "ViewModel cannot be null.");
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
