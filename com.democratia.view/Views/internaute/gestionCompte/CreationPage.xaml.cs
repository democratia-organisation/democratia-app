using com.democratia.view.Resources.Localization;
using com.democratia.ViewModels.internaute.gestionCompte;
using com.democratia.ViewModels;
using CommunityToolkit.Mvvm.Messaging;
using com.democratia.Views.Component;

namespace com.democratia.Views.internaute.gestionCompte
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