using com.democratia.Models;
using com.democratia.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace com.democratia.ViewModels.groupe.decideur
{
    public partial class DecideurViewModel : ObservableObject, INotifyPropertyChanged, IQueryAttributable
    {
        [ObservableProperty] private ObservableCollection<Thematique>? thematiques;
        [ObservableProperty] private Groupe? groupe;
        [ObservableProperty] private float? ration;

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Thematiques = (ObservableCollection<Thematique>)query["thematiques"];
            Groupe = (Groupe)query["groupe"];

        }
    }
}
