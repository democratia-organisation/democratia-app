#if ANDROID
using com.democratia.Platforms.Android;
using AndroidX.Core.View;
using Microsoft.Maui.Handlers;
#endif
using com.democratia.ViewModels.internaute;
using com.democratia.Views;
using com.democratia.Views.internaute;

namespace com.democratia
{

    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Current?.UserAppTheme = (AppTheme)Preferences.Default.Get("Theme", (int)Current?.UserAppTheme!)!;

#if ANDROID
            // Permet de gérer l'AutomationId pour les tests UI sur Android

            ViewHandler.ViewMapper.ModifyMapping("AutomationId", (handler, view, previousAction) =>
            {
                AndroidHandler(handler, view, previousAction);
            });

            LabelHandler.Mapper.ModifyMapping("AutomationId", (handler, view, previousAction) =>
            {
                AndroidHandler(handler, view, previousAction);
            });

            ButtonHandler.Mapper.ModifyMapping("AutomationId", (handler, view, previousAction) =>
            {
                AndroidHandler(handler, view, previousAction);
            });
            MainApplication.SetLocal(Preferences.Default.Get("Language", MainApplication.cultureInfo.Name));
#elif WINDOWS
            WinUI.App.SetLocal(Preferences.Default.Get("Language", WinUI.App.cultureInfo.Name));
#elif IOS || MACCATALYST
            AppDelegate.SetLocal(Preferences.Default.Get("Language", AppDelegate.cultureInfo.Name));
#endif

        }

#if ANDROID

        private static void AndroidHandler(IViewHandler handler, IView view, Action<IElementHandler, IElement>? previousAction)
        {
            if (handler.PlatformView is Android.Views.View androidView)
            {
                if (string.IsNullOrWhiteSpace(view.AutomationId))
                    return;

                if (ViewCompat.GetAccessibilityDelegate(androidView) is not TestDelegate)
                    ViewCompat.SetAccessibilityDelegate(androidView, new TestDelegate());

                if (ViewCompat.GetAccessibilityDelegate(androidView) is TestDelegate td)
                    td.AutomationId = view.AutomationId;

                androidView.ContentDescription = view.AutomationId;
            }
        }
#endif
        protected override Window CreateWindow(IActivationState? activationState) => new(new AppShell());

        protected async override void OnStart()
        {
            base.OnStart();
            var homeViewModel = ServiceHelper.GetService<HomeViewModel>()!;
            var viewModel = ServiceHelper.GetService<MainViewModel>();
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await viewModel!.AutoConnect(homeViewModel);
                if (viewModel.autoConnectSuccess)
                {
                    homeViewModel.internaute = viewModel.modele;
                    Windows[0].Page = new HomePage(homeViewModel);
                }
                else
                    Windows[0].Page = new MainPage(viewModel);
            });
        }
    }
}
