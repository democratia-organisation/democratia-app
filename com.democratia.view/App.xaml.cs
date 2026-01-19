// conditionner l'import afin d'éviter les erreurs d'imports
#if ANDROID
using com.democratia.Platforms.Android;
#endif
using Microsoft.Maui.Handlers;

namespace com.democratia
{

    public partial class App : Application
    {
        public App()
        {

            InitializeComponent();

            // Permet de gérer les exceptions non gérées dans l'application
            //AppDomain.CurrentDomain.UnhandledException += UnhadledException;
            //TaskScheduler.UnobservedTaskException += UnhadledException;
            //Dispatcher.Dispatch(() => UnhadledException());
            //Current?.Dispatcher.Dispatch(() => UnhadledException());
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
#endif

        }


#if ANDROID

        private static void AndroidHandler(IViewHandler handler, IView view, Action<IElementHandler, IElement>? previousAction)
        {
            if (handler.PlatformView is Android.Views.View androidView)
            {
                if (String.IsNullOrWhiteSpace(view.AutomationId))
                    return;

                if (AndroidX.Core.View.ViewCompat.GetAccessibilityDelegate(androidView) is not TestDelegate)
                    AndroidX.Core.View.ViewCompat.SetAccessibilityDelegate(androidView, new TestDelegate());

                if (AndroidX.Core.View.ViewCompat.GetAccessibilityDelegate(androidView) is TestDelegate td)
                    td.AutomationId = view.AutomationId;

                androidView.ContentDescription = view.AutomationId;
            }
        }
#endif
        //private void UnhadledException(object? sender = null, EventArgs? e = null)
        //        {
        //            if (e is not null && e is UnhandledExceptionEventArgs unhandledExceptionEventArgs)
        //                Current?.Windows[0]?.Page?.DisplayAlert("Erreur", "Une erreur inattendu est survenue. Voici le message : \n" + unhandledExceptionEventArgs.ExceptionObject.ToString(), "OK");

        //            else 
        //                Current?.Windows[0]?.Page?.DisplayAlert("Erreur", "Une erreur inattendu est survenue", "OK");

        //            Current?.Quit();
        //        }

        protected override Window CreateWindow(IActivationState? activationState) => new(new AppShell());

    }



}
