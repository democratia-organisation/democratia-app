using AndroidX.Core.View.Accessibility;
using Microsoft.Maui.Platform;
using Vue = Android.Views.View;

namespace com.democratia.Platforms.Android
{
    public class TestDelegate : MauiAccessibilityDelegateCompat
    {
        public string AutomationId { get; internal set; }

        public override void OnInitializeAccessibilityNodeInfo(Vue host, AccessibilityNodeInfoCompat info)
        {
            base.OnInitializeAccessibilityNodeInfo(host, info);

            if (!string.IsNullOrWhiteSpace(AutomationId))
            {
                info.ViewIdResourceName = $"{host?.Context?.PackageName}:id/{AutomationId}";
            }
        }
    }
}
