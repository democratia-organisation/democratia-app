using com.koyok.democratia.view.Resources.Localization;
using CommunityToolkit.Mvvm.Messaging;
using com.koyok.democratia.UI.Component.internaute;

namespace com.koyok.democratia.UI.internaute.gestionCompte
{
    public partial class CreationPage : ContentPage
    {
        public CreationPage(CreationViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;

            WeakReferenceMessenger.Default.Register<CreationViewModel.EventCreationSucess>(this, (r, s) =>
            {
                Content = new StackLayout
                {
                    Children =
                    {
                        new FinGestionCompte
                        {
                            LabelText = AppResources.BonneNouvelle,
                            ButtonText = AppResources.connecter,
                            Command = viewModel!.NavigateTappedCommand,
                            CommandParameter = $"///{nameof(MainPage)}"
                        }
                    }
                };
            });
        }
    }
}