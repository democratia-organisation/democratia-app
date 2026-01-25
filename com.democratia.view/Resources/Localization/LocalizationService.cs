using com.democratia.core.Utils;

namespace com.democratia.view.Resources.Localization
{
    public class LocalizationService : ILocalizationService
    {
        public string GetString(string key) => AppResources.ResourceManager.GetString(key) ?? String.Empty;
    }
}
