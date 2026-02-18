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
                
                bool veutChangerPreferencesMaintenant = await App.Current!.Windows[0].Page!.DisplayAlertAsync(AppResources.quitterApp, AppResources.ChangementOk, AppResources.quitterApp, AppResources.continuer)!;
                if (veutChangerPreferencesMaintenant) Environment.Exit(0);
            });
        }
    }
}