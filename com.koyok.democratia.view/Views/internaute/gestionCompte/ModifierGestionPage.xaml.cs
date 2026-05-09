using com.koyok.democratia.view.Resources.Localization;
using CommunityToolkit.Mvvm.Messaging;
using com.koyok.democratia.UI.Component.internaute;

namespace com.koyok.democratia.UI.internaute.gestionCompte
{
    public partial class ModifierGestionPage : ContentPage
    {
        public ModifierGestionPage(ModifierGestionViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;

            // redessiner
            WeakReferenceMessenger.Default.Register<ModifierGestionViewModel.EventModificationSuccessSender>(this, (r, m) =>
                Content = new VerticalStackLayout
                {
                    Children =
                    {
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
}