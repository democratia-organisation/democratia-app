using com.democratia.ViewModels.groupe;

namespace com.democratia.Views.groupe;

public partial class GroupePage : ContentPage
{
    public GroupePage(GroupeViewModel viewModel)
    {
        InitializeComponent();
    }


    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is GroupeViewModel viewModel)
            await viewModel.ChargerElementsCommand.ExecuteAsync(null);
    }
}