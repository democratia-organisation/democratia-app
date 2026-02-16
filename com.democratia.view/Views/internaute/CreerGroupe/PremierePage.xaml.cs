using com.democratia.ViewModels.internaute.CreerGroupe;

namespace com.democratia.Views.internaute.CreerGroupe;

public partial class PremierePage : ContentPage
{
	public PremierePage(PremierePageViewModel viewModel)
	{
		BindingContext = viewModel;
        InitializeComponent();
        behavior.BindingContext = BindingContext;
    }
}