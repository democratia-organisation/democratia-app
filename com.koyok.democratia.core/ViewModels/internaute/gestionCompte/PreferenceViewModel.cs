
using com.koyok.democratia.core.Domain.Service;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;

namespace com.koyok.democratia.UI.internaute.gestionCompte
{
    public partial class PreferenceViewModel : ObservableObject, INotifyPropertyChanged, INavigeablleViewModel
    {
        [ObservableProperty] public partial string? language {get; set;}
        [ObservableProperty] public partial string? theme {get; set;}
        [ObservableProperty] public partial ObservableCollection<string> languages {get; set;}
        [ObservableProperty] public partial ObservableCollection<string> themes {get; set;}
        private Dictionary<string,string> languagesDict;
        private Dictionary<string, AppTheme> themesDict;
        private INavigationService Shell.Current;
        
        public PreferenceViewModel(INavigationService Shell.Current, ILocalizationService localizationService) 
        { 
            this.Shell.Current = Shell.Current;
            languages = ["Français","English (American)"];
            themes = [localizationService.GetString("claire"), localizationService.GetString("sombre")]; // light, dark
            languagesDict = [];
            themesDict = [];
            languagesDict.Add(languages[0], "fr-FR");
            languagesDict.Add(languages[1], "en-US");
            themesDict.Add(themes[0], AppTheme.Light);
            themesDict.Add(themes[1], AppTheme.Dark);
        }

        [RelayCommand]
        public async Task NavigateTapped(string commande)
        {
            var langueChoisi = language ?? Preferences.Default.Get("Language", CultureInfo.CurrentCulture.Name);
            var themeChoisi = this.theme ?? themes!.First(value => themesDict[value] == Application.Current!.UserAppTheme);
            var langage = languagesDict[langueChoisi];
            AppTheme theme = themesDict[themeChoisi];
            Preferences.Default.Set("Language",langage);
            Preferences.Default.Set("Theme", (int)theme); // casting car Preferences ne supporte que les types primitif
            WeakReferenceMessenger.Default.Send<EventPreferecesSucess>();
            await Shell.Current.GoToAsync(commande, []);
        }

        public record EventPreferecesSucess() { }
    }
}
