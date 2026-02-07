// conditionner l'import afin d'éviter les erreurs d'imports
#if ANDROID
using com.democratia.Platforms.Android;
using AndroidX.Core.View;
using Android.Views;
#endif
using Microsoft.Maui.Handlers;
using System.Diagnostics;

namespace com.democratia
{

    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

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
        protected override Microsoft.Maui.Controls.Window CreateWindow(IActivationState? activationState) => new(new AppShell());

    }



}
