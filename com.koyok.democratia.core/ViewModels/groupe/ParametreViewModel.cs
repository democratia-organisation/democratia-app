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
            groupe = query.TryGetValue("groupe", out var group) ? (Groupe)group: Shell.Current.AppContext.Groupe;
            internaute = query.TryGetValue("internaute",out var user) ? (Internaute)user : Shell.Current.AppContext.Internaute;
            retourErreur = query.TryGetValue("retourMessage", out var message) ? (string)message : string.Empty;
        }

        [RelayCommand]
        private static async Task NavigatePage(string name) 
        {
            await Shell.Current.GoToAsync(name);
        }
    }
}
