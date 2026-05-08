using com.koyok.democratia.Domain.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace com.koyok.democratia.UI.internaute.CreerGroupe
{
    public partial class DeuxiemePageViewModel(Domain.Utils.AppContext context) 
        : ObservableObject, INotifyPropertyChanged, IQueryAttributable
    {
        private Groupe? groupe;
        private Domain.Utils.AppContext context = context;
        [ObservableProperty] public partial Color? couleur { get; set; } = Colors.Transparent;
        private Internaute? internaute;
        private List<Thematique>? thematiques { get; set; }
        

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            groupe = (Groupe)query["groupe"] ?? context.Groupe;
            thematiques = [..(query["thematique"] as ObservableCollection<Thematique>)!];
            internaute = (Internaute)query["internaute"] ?? context.Internaute;
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
