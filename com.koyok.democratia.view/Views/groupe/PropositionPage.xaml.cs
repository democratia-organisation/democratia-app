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
        TurnComponent(true);
    }

    private void TurnComponent(bool isPosting)
    {
        valideButton.IsVisible = isPosting;
        valideButton.IsEnabled = isPosting;
        messagePostButton.IsVisible = !isPosting;
        messagePostButton.IsEnabled = !isPosting;
        commentaireEntry.IsVisible = isPosting;
        commentaireEntry.IsEnabled = isPosting;
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
            await DisplayAlertAsync("Erreur", AppResources.erreurInattendu, AppResources.erreurInattendu);
        }
        TurnComponent(false);
    }
}