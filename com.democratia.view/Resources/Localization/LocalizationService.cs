using com.democratia.Utils;
using com.democratia.view.Resources.Localization;

namespace com.democratia.Resources.Localization
{
    public class LocalizationService : ILocalizationService
    {
        public string GetString(string key) => AppResources.ResourceManager.GetString(key) ?? String.Empty;
    }
}
