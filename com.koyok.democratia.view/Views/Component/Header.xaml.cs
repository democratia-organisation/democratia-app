using com.koyok.democratia.view.Resources.Localization;
using com.koyok.democratia.UI.internaute;
using com.koyok.democratia.Domain.Enumerations;

namespace com.koyok.democratia.UI.Component
{
    public partial class Header : ContentView
    {
        public Header()
        {
            InitializeComponent();
            SetTheme();
        }


        private async void OnClicked(object? sender, EventArgs e)
        {
            var button = (ImageButton)sender!;
            if (button == switchImageButton)
            {
                if (Application.Current?.RequestedTheme == AppTheme.Dark)
                    Application.Current.UserAppTheme = AppTheme.Light;
                else if (Application.Current?.RequestedTheme == AppTheme.Light)
                    Application.Current.UserAppTheme = AppTheme.Dark;
                SetTheme();
            }
            else if (button == backButton)
            {
                Shell shell = AppShell.Current!;
                IReadOnlyList<Page> pile = shell.Navigation?.NavigationStack!;
                if (pile.Count == 2 && pile[0] is null)
                {
                    bool estDanLaPageDeConnexionOuPageHome = shell.CurrentPage is MainPage || shell.CurrentPage is HomePage;
                    if (estDanLaPageDeConnexionOuPageHome)
                    {
                        bool souhaiteQuitter = await App.Current!.Windows[0].Page!.DisplayAlertAsync(AppResources.quitterApp, AppResources.confirmQuitt, AppResources.oui, AppResources.non);
                        if (souhaiteQuitter)
                            Environment.Exit(0);
                    }
                }

                if (pile.Count > 1) await shell.GoToAsync("..");

            }
            else
            {
                bool isConnected = bool.Parse((await SecureStorage.Default.GetAsync(SecureStorageKeys.isConnected.ToString()))!);
                if(isConnected) await AppShell.Current.GoToAsync($"{nameof(HomePage)}");
            }
        }

        private void SetTheme() =>
            switchImageButton.Source = Application.Current?.RequestedTheme == AppTheme.Dark ? "dark.png" : "light.png";
    }
}