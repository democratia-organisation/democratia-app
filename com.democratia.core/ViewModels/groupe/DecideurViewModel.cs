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
        [ObservableProperty] private ObservableCollection<Thematique>? thematiques;
        [ObservableProperty] private Groupe? groupe;
        [ObservableProperty] private float? ration;
        [ObservableProperty] private int? cursor = 0;

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Thematiques = (ObservableCollection<Thematique>)query["thematiques"];
            Groupe = (Groupe)query["groupe"];
        }

        [RelayCommand]
        private void ChargerThematiques()
        {
            throw new NotImplementedException();
        }

        [RelayCommand]
        private void RechargeThematiques()
        {
            Cursor += 1;
            throw new NotImplementedException();
        }
    }
}
