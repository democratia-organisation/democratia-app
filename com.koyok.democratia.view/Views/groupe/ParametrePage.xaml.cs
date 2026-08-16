namespace com.koyok.democratia.UI.groupe;

public partial class ParametrePage : ContentPage
{
	public ParametrePage(ParametreViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}