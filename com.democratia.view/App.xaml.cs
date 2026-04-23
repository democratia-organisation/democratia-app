namespace com.democratia
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Current?.UserAppTheme = (AppTheme)Preferences.Default.Get("Theme", (int)Current?.UserAppTheme!)!;

#if ANDROID
            MainApplication.SetLocal(Preferences.Default.Get("Language", MainApplication.cultureInfo.Name));
#elif WINDOWS
            WinUI.App.SetLocal(Preferences.Default.Get("Language", WinUI.App.cultureInfo.Name));
#elif IOS || MACCATALYST
            AppDelegate.SetLocal(Preferences.Default.Get("Language", AppDelegate.cultureInfo.Name));
#endif

        }
        protected override Window CreateWindow(IActivationState? activationState) => new(new AppShell());
    }
}
