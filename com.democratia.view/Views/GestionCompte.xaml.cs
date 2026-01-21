using com.democratia.ViewModels;

namespace com.democratia.Views;

public partial class GestionCompte : ContentPage
{
	public GestionCompte(GestionCompteViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;     
        viewModel.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == nameof(viewModel.RetourMessage) && !string.IsNullOrEmpty(viewModel.RetourMessage))
            {
                if (viewModel.RetourMessage == "Modif")
                {
                    
                }
                if (viewModel.RetourMessage == "Supp")
                {
                    
                }
            }

        };
    }
}