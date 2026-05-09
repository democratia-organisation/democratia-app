using com.koyok.democratia.Domain.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System.Collections.ObjectModel;
using System.ComponentModel;
using com.koyok.democratia.Domain.Extension;

namespace com.koyok.democratia.UI.internaute.CreerGroupe
{
    public partial class DeuxiemePageViewModel() 
        : ObservableObject, INotifyPropertyChanged, IQueryAttributable
    {
        private Groupe? groupe;
        [ObservableProperty] public partial Color? couleur { get; set; } = Colors.Transparent;
        private Internaute? internaute;
        private List<Thematique>? thematiques { get; set; }
        

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            groupe = (Groupe)query["groupe"] ?? Shell.Current!.AppContext.Groupe;
            thematiques = [..(query["thematique"] as ObservableCollection<Thematique>)!];
            internaute = (Internaute)query["internaute"] ?? Shell.Current!.AppContext.Internaute;
        }

        [RelayCommand]
        public async Task NavigateTapped(string commande)
        {
            groupe?.couleurGroupe = couleur?.ToHex();
            await Shell.Current.GoToAsync(commande, new ShellNavigationQueryParameters
                {
                    { "groupe", groupe! },
                    { "thematique", thematiques! },
                    {  "internaute", internaute!   }
                });
        }
    }
}
