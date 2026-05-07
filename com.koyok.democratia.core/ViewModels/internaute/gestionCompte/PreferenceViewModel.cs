using com.koyok.democratia.Domain.Service;
using com.koyok.democratia.Domain.Enumerations;
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
    public partial class PreferenceViewModel : ObservableObject, INotifyPropertyChanged
    {
        [ObservableProperty] public partial string? language {get; set;}
        [ObservableProperty] public partial string? theme {get; set;}
        [ObservableProperty] public partial ObservableCollection<string> languages {get; set;}
        [ObservableProperty] public partial ObservableCollection<string> themes {get; set;}
        private readonly Dictionary<string,string> languagesDict;
        private readonly Dictionary<string, AppTheme> themesDict;
        
        public PreferenceViewModel(ILocalizationService localizationService) 
        { 
            languages = ["Français","English (American)"];
            themes = [localizationService.GetString("claire"), localizationService.GetString("sombre")];
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
            var langueChoisi = language ?? Preferences.Default.Get(Settings.Language.ToString(), CultureInfo.CurrentCulture.Name);
            var themeChoisi = this.theme ?? themes!.First(value => themesDict[value] == Application.Current!.UserAppTheme);
            var langage = languagesDict[langueChoisi];
            AppTheme theme = themesDict[themeChoisi];
            Preferences.Default.Set(Settings.Language.ToString(), langage);
            // casting car Preferences ne supporte que les types primitif
            Preferences.Default.Set(Settings.Theme.ToString(), (int)theme); 
            WeakReferenceMessenger.Default.Send<EventPreferecesSucess>();
            await Shell.Current.GoToAsync(commande);
        }

        public record EventPreferecesSucess() { }
    }
}
