
using com.democratia.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;

namespace com.democratia.ViewModels.internaute.gestionCompte
{
    public partial class PreferenceViewModel : ObservableObject, INotifyPropertyChanged, INavigeablleViewModel
    {
        [ObservableProperty] private string? language;
        [ObservableProperty] private string? theme;
        [ObservableProperty] private ObservableCollection<string> languages;
        [ObservableProperty] private ObservableCollection<string> themes;
        private Dictionary<string,string> languagesDict;
        private Dictionary<string, AppTheme> themesDict;
        private INavigationService navigationService;
        
        public PreferenceViewModel(INavigationService navigationService, ILocalizationService localizationService) 
        { 
            this.navigationService = navigationService;
            languages = ["Français","English (American)"];
            themes = [localizationService.GetString("claire"), localizationService.GetString("sombre")]; // light, dark
            languagesDict = new ();
            themesDict = new();
            languagesDict.Add(languages[0], "fr-FR");
            languagesDict.Add(languages[1], "en-US");
            themesDict.Add(themes[0], AppTheme.Light);
            themesDict.Add(themes[1], AppTheme.Dark);
        }

        [RelayCommand]
        public async Task NavigateTapped(string commande)
        {
            var langueChoisi = Language ?? Preferences.Default.Get("Language", CultureInfo.CurrentCulture.Name);
            var themeChoisi = Theme ?? Themes!.First(value => themesDict[value] == Application.Current!.UserAppTheme);
            var langage = languagesDict[langueChoisi];
            AppTheme theme = themesDict[themeChoisi];
            Preferences.Default.Set("Language",langage);
            Preferences.Default.Set("Theme", (int)theme); // casting car Preferences ne supporte que les types primitif
            WeakReferenceMessenger.Default.Send<EventPreferecesSucess>();
            await navigationService.GoToAsync(commande);
        }

        public record EventPreferecesSucess() { }
    }
}
