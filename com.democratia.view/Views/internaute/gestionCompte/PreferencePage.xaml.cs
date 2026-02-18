using com.democratia.Utils;
using com.democratia.view.Resources.Localization;
using com.democratia.ViewModels.internaute.gestionCompte;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.Messaging;
using System.Diagnostics;

namespace com.democratia.Views.internaute.gestionCompte
{
    public partial class PreferencePage : ContentPage
    {
        public PreferencePage(PreferenceViewModel viewModel)
        {
            BindingContext = viewModel;
            InitializeComponent();
            WeakReferenceMessenger.Default.Register<PreferenceViewModel.EventPreferecesSucess>
            (this, async (r, m) => {
                string choix = await Application.Current!.Windows[0].Page!.DisplayActionSheetAsync($"{AppResources.ChangementOk}", $"{AppResources.continuer}", $"{AppResources.quitterApp}")!;
                if (choix == $"{AppResources.quitterApp}") Environment.Exit(0);
            });
        }
    }
}