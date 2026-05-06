using com.koyok.democratia.view.Resources.Localization;
using com.koyok.democratia.ViewModels.internaute.gestionCompte;
using com.koyok.democratia.Views.Component;
using CommunityToolkit.Mvvm.Messaging;

namespace com.koyok.democratia.Views.internaute.gestionCompte
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