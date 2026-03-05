using com.democratia.view.Resources.Localization;
using com.democratia.ViewModels.internaute.gestionCompte;
using com.democratia.Views.Component;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows.Input;

namespace com.democratia.Views.internaute.gestionCompte
{
    public partial class HomeGestionPage : ContentPage
    {

        public HomeGestionPage(HomeGestionViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            var referenceMessenger = WeakReferenceMessenger.Default;

            referenceMessenger.Register<HomeGestionViewModel.EventSuppressionSend>(this, (r, m) =>
                AjoutGrille(AppResources.mauvaiseNouvelle, AppResources.confirmeSupp, viewModel.SupprimerCompteCommand)
            );

            referenceMessenger.Register<HomeGestionViewModel.EventSuppressionSuccess>(this, (r, m) =>
                AjoutGrille(AppResources.bienSupp, AppResources.retourConnexion, viewModel.NavigateTappedCommand, "///MainPage")
            );
        }

        private void AjoutGrille(string labelText, string buttonText, ICommand command, object? parameter = null)
        {
            grille.Remove(stackLayout);
            stackLayout = new StackLayout
            {
                Children =
                {
                    new FinGestionCompte
                    {
                        LabelText = labelText,
                        ButtonText = buttonText,
                        Command = command,
                        CommandParameter = parameter
                    }
                }
            };
            grille.Add(stackLayout);
            grille.SetRow(stackLayout, 2);
        }
    }
}