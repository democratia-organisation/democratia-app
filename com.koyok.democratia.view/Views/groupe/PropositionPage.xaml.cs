using com.koyok.democratia.view.Resources.Localization;

namespace com.koyok.democratia.UI.groupe;

public partial class PropositionPage : ContentPage
{
    public PropositionPage(PropositionViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is PropositionViewModel viewModel)
        {
            viewModel.LoadCommentairesCommand.Execute(null);
        }
    }

    private void PostMessageButton_Clicked(object? sender, EventArgs? e)
    {

        valideButton.IsVisible = true;
        messagePostButton.IsVisible = false;
        commentaireEntry.IsVisible = true;
    }

    private async void ValideButton_Clicked(object? sender, EventArgs? e)
    {
        valideButton.IsEnabled = false;
        commentaireEntry.IsEnabled = false;
        try {
            if (BindingContext is PropositionViewModel viewModel)
            {
                await viewModel.AjouterCommentaireCommand.ExecuteAsync(null);
            }
        }
        catch {
            await DisplayAlertAsync("Erreur", AppResources.erreurInattendu, AppResources.ChangementOk);
            valideButton.IsEnabled = true;
            commentaireEntry.IsEnabled = true;
        }
        valideButton.IsVisible = false;
        messagePostButton.IsVisible = true;
        commentaireEntry.IsVisible = false;
    }
}