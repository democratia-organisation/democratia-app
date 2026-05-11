using com.koyok.democratia.view.Resources.Localization;
using CommunityToolkit.Mvvm.Messaging;


namespace com.koyok.democratia.UI.internaute.gestionCompte
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