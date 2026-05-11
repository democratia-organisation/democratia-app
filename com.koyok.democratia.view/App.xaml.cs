using com.koyok.democratia.Domain.Enumerations;

namespace com.koyok.democratia
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Current?.UserAppTheme = (AppTheme)Preferences.Default.Get(Settings.Theme.ToString(), (int)Current?.UserAppTheme!)!; 

#if ANDROID
            MainApplication.SetLocal(Preferences.Default.Get(Settings.Language.ToString(), MainApplication.cultureInfo.Name));
#elif WINDOWS
            WinUI.App.SetLocal(Preferences.Default.Get(Settings.Language.ToString(), WinUI.App.cultureInfo.Name));
#elif IOS || MACCATALYST
            AppDelegate.SetLocal(Preferences.Default.Get(Settings.Language.ToString(), AppDelegate.cultureInfo.Name));
#endif

        }
        protected override Window CreateWindow(IActivationState? activationState) => new(new AppShell());
    }
}
