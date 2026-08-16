using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Extension;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using System.ComponentModel;

namespace com.koyok.democratia.UI.groupe
{
    public partial class ParametreViewModel : ObservableObject, INotifyPropertyChanged, IQueryAttributable
    {
        [ObservableProperty]
        public partial Groupe? groupe { get;set; }
        [ObservableProperty]
        public partial string? retourErreur { get; set; }
        private Internaute? internaute;
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            groupe = (Groupe?)query["groupe"] ?? Shell.Current.AppContext.Groupe;
            internaute = (Internaute?)query["internaute"] ?? Shell.Current.AppContext.Internaute;
            retourErreur = (string?)query["retourMessage"] ?? string.Empty;
        }

        [RelayCommand]
        private static async Task NavigatePage(string name) 
        {
            await Shell.Current.GoToAsync(name);
        }
    }
}
