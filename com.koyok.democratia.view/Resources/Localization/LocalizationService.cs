using com.koyok.democratia.Domain.Service;
using com.koyok.democratia.view.Resources.Localization;

namespace com.koyok.democratia.UI
{

    public class LocalizationService : ILocalizationService
    {
        public string GetString(string key) => AppResources.ResourceManager.GetString(key) ?? string.Empty;

        public string GetString(string key, params object[] args)
        {
            var chaineBrut = GetString(key);
            if (args.Length > 0) return string.Format(chaineBrut, args);
            else return chaineBrut;
        }
    }
}
