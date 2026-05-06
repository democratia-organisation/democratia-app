namespace com.koyok.democratia.UI.internaute;

public partial class HomePage : ContentPage
{
    public HomePage(HomeViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is HomeViewModel vm)
            await vm.InitializeCommand.ExecuteAsync(null);   
    }
}