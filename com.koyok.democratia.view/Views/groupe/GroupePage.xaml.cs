using com.koyok.democratia.ViewModels.groupe;

namespace com.koyok.democratia.UI.groupe;

public partial class GroupePage : ContentPage
{
    public GroupePage(GroupeViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }


    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is GroupeViewModel viewModel)
            await viewModel.ChargerElementsCommand.ExecuteAsync(null);
    }
}