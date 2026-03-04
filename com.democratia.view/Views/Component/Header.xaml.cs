using com.democratia.view.Resources.Localization;
using com.democratia.Views.internaute;

namespace com.democratia.Views.Component
{
    public partial class Header : ContentView
    {
        public Header()
        {
            InitializeComponent();
            SetTheme();
        }


        private async void OnClicked(object sender, EventArgs e)
        {
            var button = (ImageButton)sender;
            if (button == switchImageButton)
            {
                if (Application.Current?.RequestedTheme == AppTheme.Dark)
                {
                    Application.Current.UserAppTheme = AppTheme.Light;
                    button.Source = "light.png";
                }
                else if (Application.Current?.RequestedTheme == AppTheme.Light)
                {
                    Application.Current.UserAppTheme = AppTheme.Dark;
                    button.Source = "dark.png";
                }
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
                        {
                            string[] files = Directory.GetDirectories(Path.Combine(FileSystem.Current.CacheDirectory, "cache"));
                            foreach (string file in files)
                            {
                                try
                                {
                                    File.Delete(file);
                                }
                                catch (Exception)
                                {
                                    throw;
                                }
                            }
                            Environment.Exit(0);

                        }
                    }
                }

                if (pile.Count > 1) await shell.GoToAsync("..");

            }

            else
            {
                string[] files = Directory.GetDirectories(Path.Combine(FileSystem.Current.CacheDirectory));
                if (files.Length > 0) await AppShell.Current.GoToAsync($"{nameof(HomePage)}"); // naviguer que si des données ont été mis en cache
            }

        }

        private void SetTheme() =>
            switchImageButton.Source = Application.Current?.RequestedTheme == AppTheme.Dark ? "dark.png" : "light.png";
    }
}