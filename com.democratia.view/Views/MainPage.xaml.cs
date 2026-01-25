using com.democratia.ViewModels;

namespace com.democratia.Views;

public partial class MainPage : ContentPage
{

    public MainPage(IEnumerable<INavigeablleViewModel?>? navigeablleViewModels)
    {
        InitializeComponent();
        MainPageViewModel? viewModel = navigeablleViewModels!.OfType<MainPageViewModel>().FirstOrDefault();
        BindingContext = viewModel !;
    }
}
