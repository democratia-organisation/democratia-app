using com.democratia.Models;
using com.democratia.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace com.democratia.ViewModels.internaute.CreerGroupe
{
    public partial class DeuxiemPageViewModel : ObservableObject, INotifyPropertyChanged, INavigeablleViewModel, IQueryAttributable
    {
        private INavigationService service;
        private Groupe? groupe;
        [ObservableProperty] public Color? color;
        private Internaute? internaute;
        private List<Thematique>? thematiques { get; set; }
        public DeuxiemPageViewModel(INavigationService service)
        {
            this.service = service;
            color = Colors.Transparent;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            groupe = (Groupe)query["groupe"];
            thematiques = (query["thematique"]! as ObservableCollection<Thematique>).ToList();
            internaute = (Internaute)query["internaute"];
        }

        [RelayCommand]
        public async Task NavigateTapped(string commande)
        {
            groupe?.CouleurGroupe = Color?.ToHex();
            await service.GoToAsync(commande, new ()
                {
                    { "groupe", groupe! },
                    { "thematique", thematiques! },
                    {  "internaute", internaute!   }
                });
        }
    }
}
