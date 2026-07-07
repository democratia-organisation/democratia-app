namespace com.koyok.democratia.UI.groupe;

public partial class PropositionPage : ContentPage
{
	public PropositionPage(PropositionViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}
}