using com.koyok.democratia.Models;
using com.koyok.democratia.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace com.koyok.democratia.UI.internaute.CreerGroupe
{
    public partial class DeuxiemePageViewModel(INavigationService service, Services.AppContext context) 
        : ObservableObject, INotifyPropertyChanged, INavigeablleViewModel, IQueryAttributable
    {
        private INavigationService service = service;
        private Groupe? groupe;
        private Services.AppContext context = context;
        [ObservableProperty] public partial Color? couleur { get; set; } = Colors.Transparent;
        private Internaute? internaute;
        private List<Thematique>? thematiques { get; set; }
        

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            groupe = (Groupe)query["groupe"];
            thematiques = [..(query["thematique"] as ObservableCollection<Thematique>)!];
            internaute = (Internaute)query["internaute"] ?? context.Internaute;
        }

        [RelayCommand]
        public async Task NavigateTapped(string commande)
        {
            groupe?.CouleurGroupe = couleur?.ToHex();
            await service.GoToAsync(commande, new ()
                {
                    { "groupe", groupe! },
                    { "thematique", thematiques! },
                    {  "internaute", internaute!   }
                });
        }
    }
}
