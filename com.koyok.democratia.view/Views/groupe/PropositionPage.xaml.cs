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
}