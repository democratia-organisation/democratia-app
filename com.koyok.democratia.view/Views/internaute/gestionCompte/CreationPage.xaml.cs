using com.koyok.democratia.view.Resources.Localization;
using com.koyok.democratia.ViewModels.internaute.gestionCompte;
using com.koyok.democratia.ViewModels;
using CommunityToolkit.Mvvm.Messaging;
using com.koyok.democratia.UI.Component;

namespace com.koyok.democratia.UI.internaute.gestionCompte
{
    public partial class CreationPage : ContentPage
    {
        public CreationPage(IEnumerable<INavigeablleViewModel?>? navigeablleViewModels)
        {
            InitializeComponent();
            var viewModel = navigeablleViewModels!.OfType<CreationViewModel>().FirstOrDefault();
            BindingContext = viewModel!;

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