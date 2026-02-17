using com.democratia.Utils;
using com.democratia.view.Resources.Localization;
using com.democratia.ViewModels.internaute.gestionCompte;
using com.democratia.Views.Component;
using CommunityToolkit.Mvvm.Messaging;

namespace com.democratia.Views.internaute.gestionCompte;

public partial class ModifierGestionPage : ContentPage
{
	public ModifierGestionPage(ModifierGestionPageViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;

        // redessiner
        WeakReferenceMessenger.Default.Register<ModifierGestionPageViewModel.EventModificationSuccessSender>(this, (r, m) =>
            Content = new VerticalStackLayout
            {
                Children =
                {
                    new Header(),
                    new FinGestionCompte
                    {
                        LabelText = AppResources.bienModifier,
                        ButtonText = AppResources.retourHome,
                        Command = viewModel.NavigateTappedCommand,
                        CommandParameter = "/HomePage"
                    }
                }
            }
        );
    }
}