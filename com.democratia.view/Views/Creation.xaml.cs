using com.democratia.ViewModels;

namespace com.democratia.Views;


public partial class Creation : ContentPage
{
    public Creation(IEnumerable<INavigeablleViewModel?>? navigeablleViewModels)
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