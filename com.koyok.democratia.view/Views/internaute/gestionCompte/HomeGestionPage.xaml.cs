using com.koyok.democratia.view.Resources.Localization;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows.Input;
using com.koyok.democratia.UI.Component.internaute;
using com.koyok.democratia.Domain.Enumerations;

namespace com.koyok.democratia.UI.internaute.gestionCompte
{
    public partial class HomeGestionPage : ContentPage
    {

        public HomeGestionPage(HomeGestionViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            var referenceMessenger = WeakReferenceMessenger.Default;

            referenceMessenger.Register<HomeGestionPage, HomeGestionViewModel.EventSuppression, string>
                (this, HomeGestionViewModel.SuceffulyEnum.Send.ToString() ,(r, m) =>
                    AjoutGrille(AppResources.mauvaiseNouvelle, AppResources.confirmeSupp, viewModel.SupprimerCompteCommand)
                );

            referenceMessenger.Register<HomeGestionPage,HomeGestionViewModel.EventSuppression, string>
                (this,HomeGestionViewModel.SuceffulyEnum.Sucess.ToString(),(r, m) =>
                    AjoutGrille(AppResources.bienSupp, AppResources.retourConnexion, viewModel.NavigateTappedCommand, "///MainPage")
                );
            referenceMessenger.Register<HomeGestionPage, HomeGestionViewModel.EventDeconnexion, string>
                (this, HomeGestionViewModel.SuceffulyEnum.Send.ToString(),async (r, m) =>
                {
                    bool souhaiteQuitter = await App.Current!.Windows[0].Page!.DisplayAlertAsync(AppResources.deconnexion, AppResources.confirmDeconnexion, AppResources.oui, AppResources.non);
                    if (souhaiteQuitter)
                    {
                        SecureStorage.Default.RemoveAll();
                        Environment.Exit(0);
                    }
                });
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
                        CommandParameter = parameter!
                    }
                }
            };
            grille.Add(stackLayout);
            grille.SetRow(stackLayout, 2);
        }
    }
}