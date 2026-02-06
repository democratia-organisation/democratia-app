using com.democratia.ViewModels.internaute.CreerGroupe;

namespace com.democratia.Views.internaute.CreerGroupe;

public partial class TroisiemePage : ContentPage
{
	public TroisiemePage(TroisiemePageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;

    }
}