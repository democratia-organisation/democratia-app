using com.democratia.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace com.democratia.ViewModels.groupe
{
    public partial class DecideurViewModel : ObservableObject, INotifyPropertyChanged, IQueryAttributable
    {
        [ObservableProperty] public partial bool isRefreshing { get; set; } = false;

        [ObservableProperty] public partial ObservableCollection<Thematique>? thematiques { get; set; }
        [ObservableProperty] public partial Groupe? groupe { get; set; }
        [ObservableProperty] public partial float? ration { get; set; }
        [ObservableProperty] public partial int? cursor { get; set; } = 0;

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            thematiques = (ObservableCollection<Thematique>)query["thematiques"];
            groupe = (Groupe)query["groupe"];
        }

        [RelayCommand]
        private async Task InitialisationListe()
        {
            if (isRefreshing)
            {
                await ChargerThematiquesAsync();
                isRefreshing = false;
            }
            else return;
        }

        [RelayCommand]
        private async Task ChargerThematiquesAsync()
        {
            
        }

        [RelayCommand]
        private void RechargeThematiques()
        {
            cursor += 1;
            
        }
    }
}
